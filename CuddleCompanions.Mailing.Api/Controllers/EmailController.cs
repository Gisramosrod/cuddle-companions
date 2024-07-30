using CuddleCompanions.Mailing.Api.Contracts;
using CuddleCompanions.Mailing.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CuddleCompanions.Mailing.Api.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) => _emailService = emailService;

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            var result = await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
