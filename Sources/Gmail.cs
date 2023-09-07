using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DragonBoyZ.Application.IO;
using DragonBoyZ.Application.Threading;

namespace DragonBoyZ
{
    public class Gmail
    {
        public static void SendEmail(string gmailRecieve, string gmailSend, string passwordGmaillSend)
        {
            var passwordXacThuc = ServerUtils.RandomNumber(1000000, 9999999);
            MailMessage MailMessage = new MailMessage();
            MailMessage.To.Add(gmailRecieve);
            MailMessage.From = new MailAddress(gmailSend);
            MailMessage.Subject = "Thư xác thực tài khoản";
            //MailMessage.Body = $"{gmailRecieve}"
            //                  + "Bạn vừa dùng email này để đăng ký tài khoản game Ngọc Rồng Kame"
            //                  + $"-Tài khoản của bạn là: {gmailRecieve}"
            //                  + $"- Mật khẩu đăng nhập là: {passwordXacThuc}"
            //                  + "Đây là email tự động, vui lòng không reply email này. Cám ơn."
            //                  + $"Trang chủ Ngọc Rồng Kame Online -http://ngocrongonline.com"
            //                  + $"Diễn đàn Ngọc Rồng Kame Online -http://forum.ngocrongonline.com"
            //                  + "Fanpage - Ngọc Rồng Online";
            MailMessage.Body = "zz";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(gmailSend, passwordGmaillSend);
            try
            {
                smtp.Send(MailMessage);
                Server.Gi().Logger.Print("red", "Send " + gmailRecieve + " | " + passwordXacThuc);
            }catch(Exception )
            {
                Server.Gi().Logger.Print("red", "Cannot Send " + gmailRecieve + " | " + passwordXacThuc);

            }
        }
    }
}
