using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EmailHelper
    {

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="MailAddress">邮箱平台</param>
        /// <param name="FromName">登录用户</param>
        /// <param name="FromPass">登录密码</param>
        /// <param name="FromAddress">发送人地址</param>
        /// <param name="ToAddress">接收人地址</param>
        /// <param name="ToAddress">抄送人</param>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <param name="AttachmentName">附件名称</param>
        /// <param name="outstring"></param>
        /// <returns></returns>
        public static bool SendMail(Mail_Object _mail_object, out string outstring)
        {
            outstring = "";
            bool bo = false;
            try
            {
                SmtpClient smtpclient = new SmtpClient(_mail_object.MailAddress);                                       //smtp.163.com
                smtpclient.UseDefaultCredentials = true;
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpclient.Credentials = new System.Net.NetworkCredential(_mail_object.FromName, _mail_object.FromPass);//凭证 用户名18954251539@163.com, 密码78ds99qq78dd
                MailMessage Message = new MailMessage();
                Message.From = new System.Net.Mail.MailAddress(_mail_object.FromAddress);                               //发送人18954251539@163.com
                Message.To.Add(_mail_object.ToAddress);                                                                 //接受地址
                for (int i = 0; i < _mail_object.CcAddress.Length; i++)
                {
                    Message.CC.Add(_mail_object.CcAddress[i]);
                }
                Message.Subject = _mail_object.Title;
                Message.Body = _mail_object.Content;
                Message.SubjectEncoding = Encoding.UTF8;
                Message.BodyEncoding = Encoding.UTF8;
                Message.Priority = MailPriority.High;
                Message.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(_mail_object.AttachmentName))
                {
                    Attachment att = Attachment.CreateAttachmentFromString("str.zip", "rar");
                    att.Name = _mail_object.AttachmentName;
                    Message.Attachments.Add(att);
                }
                smtpclient.Send(Message);
                bo = true;
            }
            catch (Exception e)
            {
                outstring = e.Message;
                bo = false;
            }
            return bo;
        }
    }
    /// <summary>
    /// Mailclass
    /// </summary>
    public class Mail_Object
    {
        public string MailAddress { get; set; }
        public string FromName { get; set; }
        public string FromPass { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AttachmentName { get; set; }
        public string[] CcAddress { get; set; }
    }
}
