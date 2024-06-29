using System.Net.Mail;
using System.Net;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {

        private static string from = string.Empty;
        private static string to = string.Empty;
        public LocalMailService(IConfiguration configuration)
        {
            var to = configuration["mailSettings:mailaAddressTo"];
            var from = configuration["mailSettings:mailaAddressFrom"];
        }
        string _mailTo = "mehdi.jamali@gmail.com";
        string _mailFrom = "mehdi.jamali@gmail.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail  From {_mailFrom}  To {_mailTo}  , "
                + $"with {nameof(LocalMailService)}  ,  ");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");
        }



        public static void Email(string subject, string htmlString
            , string to)
        {
            try
            {
                string _mailFrom = "mehdi.jamali@gmail.com";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailFrom);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                //  smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(_mailFrom, "plkb aopu olvj vxws");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
