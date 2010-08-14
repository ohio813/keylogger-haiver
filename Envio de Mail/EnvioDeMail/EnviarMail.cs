using System.Net;
using System.Net.Mail;
using System.Net.Mime; 

namespace Mailer {

    public class EnviarMail{

        public bool enviar()
        {
            bool enviado = true;
            //Se crea un Objeto llamado "msg" de la clase "MailMessage"

            System.Net.Mail.MailMessage msg = new
            System.Net.Mail.MailMessage();


            //Se establece el destinatario

            msg.To.Add("haiver77@hotmail.com");


            /*Se establece el remitente, asi como el nombre que aparecerá en la

            bandeja de entrada, así como el formato de codificación
            */

            msg.From = new MailAddress("haiver77@gmail.com",  "Soy Haiver", System.Text.Encoding.UTF8);


            //Se establece el asunto del mail

            msg.Subject = "Prueba de envío de correo desde Cuenta Gmail en c# by Haiver";


            //Formato de codificación del Asunto

            msg.SubjectEncoding = System.Text.Encoding.UTF8;


            //Se establece el cuerpo del mail 
            msg.Body = "Si veo este correo en una cuenta mía esque si se pudo jjejejejej !!!! by adryan";


            //Se establece la codificación del Cuerpo

            msg.BodyEncoding = System.Text.Encoding.Unicode;


            //Se indica si al cuerpo del mail, se interpretara como código HTMl

            msg.IsBodyHtml = false;


            //Se establece la cadena de texto con la ubicación del archivo a adjuntar
            //string file = "d:/foto.jpg";

            //Se adjunta el archivo

           // Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

            //ContentDisposition disposition = data.ContentDisposition;

            //disposition.CreationDate = System.IO.File.GetCreationTime(file);

           // disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);

            //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

           // msg.Attachments.Add(data);


            //Se prepara el envio del mail creando un objeto de tipo SmtpClient

            SmtpClient client = new SmtpClient();


            //Se establecen las credenciales para enviar el mail, muy importante autentificarse con la cuenta de correo y la contraseña

            client.Credentials = new System.Net.NetworkCredential("haiver77@gmail.com", "sandrayjohn");


            //Se establece el puerto de envio

            client.Port = 587;


            //Se establece el servidor SMTP, en este caso GMAIL

            client.Host = "smtp.gmail.com";

            //Seguridad SSL si o no

            client.EnableSsl = true;


            //Se envia el Mail controlando la  ecepción

            try
            {

                client.Send(msg);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
               enviado = false;
            }
            return enviado;
        }
    }

}
