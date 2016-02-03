using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ZwhAid
{
    /// <summary>
    /// SMTP邮件操作
    /// </summary>
    public class SMTPAid : ZwhBase
    {
        private string host;
        public string Host
        {
            set { host = value; }
            get { return host; }
        }

        private int port;
        public int Port
        {
            set
            {
                if (value == null)
                    port = 25;
                else
                    port = value;
            }
            get { return port; }
        }

        private string user;
        public string User
        {
            set { user = value; }
            get { return user; }
        }

        private string pwd;
        public string Pwd
        {
            set { pwd = value; }
            get { return pwd; }
        }

        private string from;
        public string From
        {
            set { from = value; }
            get { return from; }
        }

        private string[,] froms;
        /// <summary>
        /// 发件人邮箱,前一地址，后一显示名称
        /// </summary>
        public string[,] Froms
        {
            set { froms = value; }
            get { return froms; }
        }

        private string to;
        private string To
        {
            set { to = value; }
            get { return to; }
        }

        private string[,] tos;
        /// <summary>
        /// 收件人邮箱,前一地址，后一显示名称
        /// </summary>
        public string[,] Tos
        {
            set { tos = value; }
            get { return tos; }
        }

        private List<string> tol;
        public List<string> Tol
        {
            set { tol = value; }
            get { return tol; }
        }

        private string cc;
        public string Cc
        {
            set { cc = value; }
            get { return cc; }
        }

        private string[,] ccs;
        public string[,] Ccs
        {
            set { ccs = value; }
            get { return ccs; }
        }

        private List<string> ccl;
        public List<string> Ccl
        {
            set { ccl = value; }
            get { return ccl; }
        }

        private string replyTo;
        public string ReplyTo
        {
            set { replyTo = value; }
            get { return replyTo; }
        }

        private string[,] replyTos;
        public string[,] ReplyTos
        {
            set { replyTos = value; }
            get { return replyTos; }
        }

        private List<string> replyTol;
        public List<string> ReplyTol
        {
            set { replyTol = value; }
            get { return replyTol; }
        }

        private MailAddress fromMA;
        private MailAddress toMA;

        private string subject;
        public string Subject
        {
            set { subject = value; }
            get { return subject; }
        }

        private string body;
        public string Body
        {
            set { body = value; }
            get { return body; }
        }

        private Attachment attach;
        public Attachment Attach
        {
            set { attach = value; }
            get { return attach; }
        }

        /// <summary>
        /// 默认构造方法，注明SMTP默认端口25
        /// </summary>
        public SMTPAid() { }

        public SMTPAid(string host, int port, string user, string pwd, string from, string to, string subject, string body, string attachPath)
        {
            Host = host;
            Port = port;
            User = user;
            Pwd = pwd;
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            Attach = new Attachment(System.Web.HttpContext.Current.Server.MapPath(attachPath));
        }

        public SMTPAid(string host, int port, string user, string pwd, string[,] froms, string[,] tos)
        {
            Host = host;
            Port = port;
            User = user;
            Pwd = pwd;
            Froms = froms;
            Tos = tos;
        }

        public void SMTP()
        {
            //SMTP传输协议
            SmtpClient client = new SmtpClient();
            client.Host = host;//SMTP邮件服务器
            client.Port = port;//SMTP主机上的端口号,默认是25.
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//邮件发送方式
            client.Credentials = new System.Net.NetworkCredential(user, pwd);//发件人登录邮箱的用户名和密码

            //SMTP信息
            if (!string.IsNullOrEmpty(Froms[1, 1]))
            {
                fromMA = new MailAddress(Froms[1, 1]);//发件人
            }
            else
            {
                fromMA = new MailAddress(From);//发件人
            }
            if (!string.IsNullOrEmpty(Tos[1, 1]))
            {
                toMA = new MailAddress(Tos[1, 1]);//收件人
            }
            else
            {
                toMA = new MailAddress(To);//收件人
            }
            MailMessage mm = new MailMessage(fromMA, toMA);//创建一个电子邮件类
            //mm.To.Add(toMA);
            mm.Subject = subject;
            mm.Body = body;//可为html格式文本
            mm.Attachments.Add(attach); //附件
            mm.SubjectEncoding = System.Text.Encoding.UTF8;//邮件主题编码
            mm.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");//邮件内容编码
            mm.IsBodyHtml = true;//邮件内容是否为html格式
            mm.Priority = MailPriority.High;//邮件的优先级,有三个值:高(在邮件主题前有一个红色感叹号,表示紧急),低(在邮件主题前有一个蓝色向下箭头,表示缓慢),正常(无显示).
            try
            {
                client.Send(mm);//发送邮件
                //client.SendAsync(mailMessage, "ojb");异步方法发送邮件,不会阻塞线程.
            }
            catch (Exception)
            {
            }
        }
        public void SendEmail(string emails,string num)
        {
            try
            {
                string[] sArray = emails.Split(new char[2] { '@', '.' });
                string bb = sArray[1];
                MailMessage mm = new MailMessage();
                MailAddress Fromma = new MailAddress("zknu110@163.com", "SMEChina");
                // MailAddress Toma = new MailAddress("MMMMMMM@qq.com", null);
                mm.From = Fromma;
                //收件人
                mm.To.Add(emails);
                //邮箱标题
                mm.Subject = "SMEChina 账号邮箱验证:";
                mm.IsBodyHtml = true;
                //邮件内容

                mm.Body = "您的验证码为：" + num + "<br />";
                //内容的编码格式
                //mm.BodyEncoding = System.Text.Encoding.UTF8;
                mm.SubjectEncoding = System.Text.Encoding.UTF8;//邮件主题编码
                mm.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");//邮件内容编码
                mm.IsBodyHtml = true;//邮件内容是否为html格式
                //mm.ReplyTo = Toma;
                //mm.Sender =Fromma;
                //mm.IsBodyHtml = false;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                //mm.CC.Add(Toma);
                SmtpClient sc = new SmtpClient();
                NetworkCredential nc = new NetworkCredential();
                nc.UserName = "zknu110@163.com";//你的邮箱地址
                nc.Password = "092110";//你的邮箱密码,这里的密码是xxxxx@qq.com邮箱的密码，特别说明下~
                sc.UseDefaultCredentials = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.Credentials = nc;
                //如果这里报mail from address must be same as authorization user这个错误，是你的QQ邮箱没有开启SMTP，
                //到你自己的邮箱设置一下就可以啦！在帐户下面,如果是163邮箱的话，下面该成smtp.163.com
              /*  switch (bb)
                {
                    case "qq": sc.Host = "smtp.qq.com";
                        break;
                    case "gmail": sc.Host = "smtp.gmail.com";
                        break;
                    case "sina": sc.Host = "smtp.sina.com.cn";
                        break;
                    case "yahoo": sc.Host = "smtp.mail.yahoo.com";
                        break;
                    case "sohu": sc.Host = "smtp.sohu.com";
                        break;
                    case "china": sc.Host = "smtp.china.com";
                        break;
                    case "21cn": sc.Host = "smtp.21cn.com sina.com";
                        break;
                    case "163": sc.Host = "smtp.163.com";
                        break;
                    case "126": sc.Host = "smtp.126.com";
                        break;
                    default:
                        break;
                }*/
               sc.Host = "smtp.163.com";
                sc.Send(mm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
