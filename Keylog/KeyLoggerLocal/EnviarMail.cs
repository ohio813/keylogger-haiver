using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System;

namespace Mailer
{

    public class EnviarMail
    {

        public bool enviar(string file, string destinatario, string asunto)
        {
            bool enviado = true;
            //Se crea un Objeto llamado "msg" de la clase "MailMessage"

            System.Net.Mail.MailMessage msg = new
            System.Net.Mail.MailMessage();


            //Se establece el destinatario

            msg.To.Add(destinatario);


            /*Se establece el remitente, asi como el nombre que aparecerá en la

            bandeja de entrada, así como el formato de codificación
            */

            msg.From = new MailAddress("haiver77@gmail.com", System.Environment.MachineName + " / " + System.Environment.UserName, System.Text.Encoding.UTF8);


            //Se establece el asunto del mail

            msg.Subject = asunto;


            //Formato de codificación del Asunto

            msg.SubjectEncoding = System.Text.Encoding.UTF8;


            //Se establece el cuerpo del mail 
            msg.Body = "";


            //Se establece la codificación del Cuerpo

            msg.BodyEncoding = System.Text.Encoding.Unicode;


            //Se indica si al cuerpo del mail, se interpretara como código HTMl

            msg.IsBodyHtml = false;

            //Se adjunta el archivo

            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            msg.Attachments.Add(data);

           

            //Se prepara el envio del mail creando un objeto de tipo SmtpClient

            SmtpClient client = new SmtpClient();


            //Se establecen las credenciales para enviar el mail, muy importante autentificarse con la cuenta de correo y la contraseña

            client.Credentials = new System.Net.NetworkCredential("haiver77@gmail.com", "agosto14");


            //Se establece el puerto de envio

            client.Port = 587;


            //Se establece el servidor SMTP, en este caso GMAIL

            client.Host = "smtp.gmail.com";

            //Seguridad SSL si o no

            client.EnableSsl = false;

            //Se envia el Mail controlando la  ecepción

            try
            {

                client.Send(msg);

            }
            catch (Exception ex)
            {
                enviado = false;
            }
            data.Dispose();
            return enviado;
            
        }
        
    }

}
