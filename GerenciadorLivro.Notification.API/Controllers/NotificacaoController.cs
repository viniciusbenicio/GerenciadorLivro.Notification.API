using GerenciadorLivro.Notification.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivro.Notification.API.Controllers
{
    [Route("api/notificacoes")]
    public class NotificacaoController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public NotificacaoController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await _emailService.Send("ToName", "vinicius.benicio97@gmail.com","ToSubject", "ToBody", "FromName", "vinicius.benicio97@gmail.com");

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
