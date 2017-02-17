using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Data;

namespace BusinessObjects.Common
{
    public enum MailType
    {
        Activation = 1,
        PasswordReminder = 2,
        Question = 3
    }

    public sealed class MailManager
    {
        /// <summary>
        /// Satýþ sonrasýnda satýcýya, "XYZ123 nolu kodu kullanarak telefonunuzu kargoya verebilirsiniz" konulu mail atar.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="cargoKey"></param>
        /// <returns></returns>
        /// 

        public static int mailType;

        /// <summary>
        /// Yeni üyelik maili, facebook ve normal üyeler için 2 farklý template kullanýr.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool SendNewMemberMail(Member m)
        {
            string body = GetContentByTitle("new.member.mail", m);
            return SendCustomEmail(body, m.Email, ResourceManager.GetResource("new.member.subject"), MailManager.mailType, "kayit@cepstop.com");
        }

        /// <summary>
        /// Aktivasyon maili atar.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool SendActivationMail(Member m)
        {
            string body = GetContentByTitle("member.activation.mail", m);
            MailManager.mailType = (int)MailType.Activation;

            body = body.Replace("[ACTIVATION_CODE]", string.Concat(m.MemberID, ":", DateTime.Now.Millisecond).Encrypt());
            return SendCustomEmail(body, m.Email, ResourceManager.GetResource("member.welcome"), (int)MailType.Activation, "kayit@cepstop.com");
        }

        /// <summary>
        /// Þifre hatýrlatma maili atar
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool SendPasswordReminder(Member m)
        {
            string subject = ResourceManager.GetResource("password.reminder.subject");
            string body = GetContentByTitle("password.reminder.mail", m);

            MailManager.mailType = (int)MailType.PasswordReminder;
            return SendCustomEmail(body, m.Email, subject, MailManager.mailType, "destek@cepstop.com");
        }

        /// <summary>
        /// Genel mail gönderme fonksiyonudur.
        /// </summary>
        /// <param name="From"></param>
        /// <param name="Message"></param>
        /// <param name="Email"></param>
        /// <param name="Subject"></param>
        /// <returns></returns>
        public static bool SendCustomEmail(string Message, string Email, string Subject, int mailType, string From = null)
        {
            string Body = File.ReadAllText(ConfigManager.Current.pathForEmailTemplate, Encoding.Default);
            Body = Body.Replace("[message]", Message);
            Body = Body.Replace("[date]", DateTime.Now.ToString());

            return SendGenericMail(Email, Subject, Body, mailType, From);
        }

        public static string GetContentByTitle(string title, Member m = null)
        {
            BusinessObjects.Content c = ContentManager.GetContentByTitle(title, Util.CurrentUserLang);

            string body = c != null ? c.Body : "";

            if (!body.NullOrEmpty())
            {
                body = body.Replace("[SITE_URL]", ConfigManager.Current.VirtualPath);
                body = body.Replace("[IP]", Util.CurrentUserIP);
                body = body.Replace("[DATE]", DateTime.Now.ToString());

                if (m != null)
                {
                    body = body.Replace("[MEMBER_ID]", m.MemberID.ToString());
                    body = body.Replace("[EMAIL]", m.Email);
                    body = body.Replace("[MEMBER_NAME]", m.FullName);
                    body = body.Replace("[PASSWORD]", m.PasswordHashed.Decrypt());
                }
            }
            return body.Replace(Environment.NewLine, "<br />");
        }

        public static bool SendGenericMail(string Email, string Subject, string Body, int mailType, string From = null)
        {
            Body = Body.Replace("[date]", System.DateTime.Today.ToShortDateString());
            Body = Body.Replace("[IP]", Util.CurrentUserIP);

            return Util.SendMail(From ?? "support@meramobile.com.pk", Email, Subject, Body, mailType);
        }
    }
}
