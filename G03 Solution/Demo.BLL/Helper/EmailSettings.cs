using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Helper
{
    public class EmailSettings
    {
        //public static class EmailSettings
        //{
        //    public static void SendEmail(Email email)
        //    {
        //        var Client = new SmtpClient("smtp.gmail.com", 587);
        //        Client.EnableSsl = true;//Encrypted
        //        Client.Credentials = new NetworkCredential("yassersayed4422@gmail.com", "Sayed@123");
        //        Client.Send("yassersayed4422@gmail.com", "yassersaid476@gmail.com", email.Title, email.Body);
        //    }
        //}

        // Sending email using [SendGrid Configurations]
        public static void SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.sendgrid.net", 587);
                client.EnableSsl = true;//Encrypted
                client.Credentials = new NetworkCredential("apikey", "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");
                client.Send("yassersayed4422@gmail.com", email.To, email.Title, email.Body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}