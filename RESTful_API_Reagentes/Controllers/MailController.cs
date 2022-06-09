using System.Net;
using System.Net.Mail;

namespace Reagentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "管理,用户,技术员")]
    public class MailController : Controller
    {
        public class 邮件
        {
            public string ToAddress { get; set; }
            public string FromAddress { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string DisplayName { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool IsBodyHtml { get; set; }
            public int Port { get; set; }
            public string SmtpHost { get; set; }
            public bool EnableSsl { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendMail([FromBody] 邮件 邮)
        {
            MailMessage mail = new();
            SmtpClient smtpClient = new();

            mail.To.Add(new MailAddress(邮.ToAddress));
            mail.From = new MailAddress(邮.FromAddress, 邮.DisplayName, System.Text.Encoding.UTF8);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = 邮.Subject;
            mail.Body = 邮.Body;
            mail.IsBodyHtml = 邮.IsBodyHtml;
            mail.Priority = MailPriority.High;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(邮.UserName, 邮.Password);
            smtpClient.Port = 邮.Port;
            smtpClient.Host = 邮.SmtpHost;
            smtpClient.EnableSsl = 邮.EnableSsl;

            try
            {
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Empty;
                while(ex != null)
                {
                    errorMessage += ex.ToString();
                    ex = ex.InnerException;
                }
                mail.Dispose();
                smtpClient.Dispose();
                return BadRequest(errorMessage);
            }

            mail.Dispose();
            smtpClient.Dispose();
            return Ok();
        }
    }
}
