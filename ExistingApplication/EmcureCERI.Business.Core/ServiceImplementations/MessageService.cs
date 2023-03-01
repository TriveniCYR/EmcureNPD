using EmcureCERI.Business.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Text;


namespace EmcureCERI.Business.Core
{

    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;
        #region Default Construtor

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        public string GetApprovalMessage(string afn, string userName, string password, string url, string admin)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                    sb.Append("<p>Welkom bij de postautorisatiestudie voor het Cidofovir Blootstelling Register. Uw registratie is succesvol verlopen.</p>");
                    sb.Append("<p>Uw inloggegevens voor de portal zijn als volgt: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Gebruikersnaam :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Wachtwoord :-  " + password + "</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                    sb.Append("<p>Willkommen bei der Post-Authorization-Studie für das Cidofovir-Expositionsregister. Ihre Registrierung war erfolgreich.</p>");
                    sb.Append("<p>Ihre Anmeldeinformationen für das Portal lauten wie folgt: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Nutzername :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Passwort :-  " + password + "</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>Welcome to the Post Authorization Study for the Cidofovir Exposure Registry. Your registration has been successful.</p>");
                    sb.Append("<p>Your credentials for the portal are as follows: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>User Name :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Password :-  " + password + "</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                    sb.Append("<p>Bienvenido al Estudio de Autorización Posterior para el Registro de Exposición al Cidofovir. Su registro ha sido exitoso.</p>");
                    sb.Append("<p>Sus credenciales para el portal son las siguientes: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Nombre de usuario :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Contraseña :-  " + password + "</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                    sb.Append("<p>Bienvenue à l’étude post-autorisation pour le registre d’exposition au cidofovir. Votre inscription a réussi.</p>");
                    sb.Append("<p>Vos informations d'identification pour le portail sont les suivantes: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Nom d'utilisateur :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Mot de passe :-  " + password + "</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
          
            
            return sb.ToString();
        }

        public string GetRejectedMessage(string afn, string userName, string url, string message, string admin)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                    sb.Append("<p>Uw account wordt door Admin afgewezen, de reden van afwijzing is " + message + ".</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                    sb.Append("<p>Ihr Account wird von Admin abgelehnt, der Grund der Ablehnung ist " + message + ".</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>Your account is rejected by Admin, the reason of rejection is " + message + ".</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                    sb.Append("<p>Su cuenta es rechazada por el administrador, la razón del rechazo es " + message + ".</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                    sb.Append("<p>Votre compte est rejeté par l'administrateur, le motif du refus est " + message + ".</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            
            return sb.ToString();

        }

        public string GetActivatedMessage(string afn, string userName, string url, string admin)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
        
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                    sb.Append("<p>Uw account wordt geactiveerd door Admin.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                    sb.Append("<p>Ihr Konto wird von Admin aktiviert.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>Your account is activated by Admin.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                    sb.Append("<p>Tu cuenta está activada por Admin.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                    sb.Append("<p>Votre compte est activé par Admin.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
          
            return sb.ToString();
        }

        public string GetDeactivatedMessage(string afn, string userName, string url, string message, string admin)
        {
           
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
         
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                    sb.Append("<p>Uw account is gedeactiveerd door Admin, de reden voor deactivering is " + message + ".</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                    sb.Append("<p>Ihr Account ist von Admin deaktiviert, der Grund für die Deaktivierung ist " + message + ".</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>Your account is de-activated by Admin, the reason of deactivation is " + message + ".</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                    sb.Append("<p>Su cuenta está desactivada por el administrador, la razón de la desactivación es " + message + ".</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                    sb.Append("<p>Votre compte est désactivé par l'administrateur, le motif de la désactivation est " + message + ".</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + admin + "' target='_top'>" + admin + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();
        }

        public string GetUserPassMessage(string afn, string userName, string password, string url)
        {
         
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                    sb.Append("<p>Wij danken u voor het kiezen van Tillomed Laboratories. We zijn verheugd om u te helpen bij uw mobiliteit Journey.</p>");
                    sb.Append("<p>De portal is een informatiebron voor uw bestemmingsstad en fungeert tevens als een eenvoudige verwijzing naar alle communicatie en activiteiten die betrekking hebben op uw verhuizing.</p>");
                    sb.Append("<p>We kijken ernaar uit om u te verwelkomen, en mocht u op enig moment vragen hebben, neem dan contact op met <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p>Uw inloggegevens voor de portal zijn als volgt: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Gebruikersnaam :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Wachtwoord :-  " + password + "</p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                    sb.Append("<p>Wir danken Ihnen, dass Sie sich für Tillomed Laboratories entschieden haben. Wir freuen uns, Sie bei Ihrer Mobilitätsreise zu unterstützen.</p>");
                    sb.Append("<p>Das Portal ist eine Informationsquelle für Ihre Zielstadt und dient als einfache Referenz für alle Kommunikations- und Aktivitäten, die Ihren Umzug betreffen. </p>");
                    sb.Append("<p>Wir freuen uns, Sie bei uns begrüßen zu dürfen. Sollten Sie zu einem bestimmten Zeitpunkt Fragen haben, wenden Sie sich bitte an <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p>Ihre Anmeldeinformationen für das Portal lauten wie folgt: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Nutzername :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Passwort :-  " + password + "</p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>We thank you for choosing Tillomed Laboratories. We are delighted to be assisting you in your mobility Journey.</p>");
                    sb.Append("<p>The portal, will be a source of information for your destination city and also, act as a easy reference of all communication and activities pertaining to your move. </p>");
                    sb.Append("<p>We look forward to hosting you, and should you at any given point in time, have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p>Your credentials for the portal are as follows: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>User Name :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Password :-  " + password + "</p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                    sb.Append("<p>Le agradecemos la elección de los laboratorios Tillomed. Estamos encantados de ayudarlo en su viaje de movilidad.</p>");
                    sb.Append("<p>El portal, será una fuente de información para su ciudad de destino y también, actuará como una referencia fácil de todas las comunicaciones y actividades relacionadas con su mudanza. </p>");
                    sb.Append("<p>Esperamos recibirlo y, en caso de que tenga alguna pregunta, en cualquier momento, comuníquese con <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p>Sus credenciales para el portal son las siguientes: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Nombre de usuario :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Contraseña :-  " + password + "</p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                    sb.Append("<p>Nous vous remercions d'avoir choisi les laboratoires Tillomed. Nous sommes ravis de vous aider dans votre parcours de mobilité.</p>");
                    sb.Append("<p>Le portail constituera une source d’informations pour votre ville de destination et servira également de référence facile pour toutes les communications et activités relatives à votre déménagement.</p>");
                    sb.Append("<p>Nous sommes impatients de vous accueillir. Si vous avez des questions à un moment donné, veuillez contacter <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p>Vos informations d'identification pour le portail sont les suivantes: </p>");
                    sb.Append("<p style='margin-bottom:2px;'>Nom d'utilisateur :-  " + userName + "</p>");
                    sb.Append("<p style='margin-top:5px;'>Mot de passe :-  " + password + "</p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();
        }

        public string GetForgotPasswordMessage(string afn, string callbackUrl)
        {
           
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
            
            switch (cultureName)
            {
                //case "nl-BE":
                //    sb.Append($"<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                //    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                //    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                //    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                //    sb.Append("<p>Bedankt voor het gebruik van de portal van Tillomed Laboratories.</p>");
                //    sb.Append("<p>Op verzoek van u, gelieve uw wachtwoord opnieuw in te stellen door op de onderstaande knop te klikken</p>");
                //    sb.Append("<br><a href='" + callbackUrl + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Reset wachtwoord</a><br><br>");
                //    sb.Append("<p>Als u het door het systeem gegenereerde wachtwoord wilt wijzigen, kunt u nadat u bent ingelogd hetzelfde wijzigen door naar uw profielsectie te gaan. </p>");
                //    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                //    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                //    sb.Append("</div>");
                //    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                //    sb.Append("</div></div></div></body>");
                //    break;
                //case "de-DE":
                //    sb.Append($"<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                //    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                //    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                //    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                //    sb.Append("<p>Vielen Dank, dass Sie das Tillomed Laboratories-Portal nutzen.</p>");
                //    sb.Append("<p>Bitte setzen Sie Ihr Passwort zurück, indem Sie auf die Schaltfläche unten klicken</p>");
                //    sb.Append("<br><a href='" + callbackUrl + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Passwort zurücksetzen</a><br><br>");
                //    sb.Append("<p>Wenn Sie das vom System generierte Kennwort ändern möchten, können Sie es nach dem Anmelden in Ihrem Profil ändern. </p>");
                //    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                //    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                //    sb.Append("</div>");
                //    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                //    sb.Append("</div></div></div></body>");
                //    break;
                case "en-GB":
                    sb.Append($"<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color:#007BFF;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Dossier Management</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>Thank you for using the Dossier Management portal.</p>");
                    sb.Append("<p>As requested by you, Please reset your password by clicking below button</p>");
                    sb.Append("<br><a href='" + callbackUrl + "' style='text-decoration:none;line-height:100%;background:#007bff;color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#007bff;' target='_blank'>Reset Password</a><br><br>");
                    sb.Append("<p>Should you wish to change the system generated password, once you login, you can change the same, by visiting your Profile section. </p>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Dossier Management.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                //case "es-ES":
                //    sb.Append($"<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                //    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                //    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                //    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                //    sb.Append("<p>Gracias por usar el portal de Tillomed Laboratories.</p>");
                //    sb.Append("<p>Cuando lo solicites, restablece tu contraseña haciendo clic en el botón de abajo</p>");
                //    sb.Append("<br><a href='" + callbackUrl + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Restablecer la contraseña</a><br><br>");
                //    sb.Append("<p>Si desea cambiar la contraseña generada por el sistema, una vez que inicie sesión, puede cambiar la misma, visitando la sección de su perfil. </p>");
                //    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                //    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                //    sb.Append("</div>");
                //    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                //    sb.Append("</div></div></div></body>");
                //    break;
                //case "fr-FR":
                //    sb.Append($"<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                //    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                //    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                //    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                //    sb.Append("<p>Merci d’utiliser le portail Tillomed Laboratories.</p>");
                //    sb.Append("<p>Selon vos souhaits, veuillez réinitialiser votre mot de passe en cliquant sur le bouton ci-dessous.</p>");
                //    sb.Append("<br><a href='" + callbackUrl + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Réinitialiser le mot de passe</a><br><br>");
                //    sb.Append("<p>Si vous souhaitez modifier le mot de passe généré par le système, une fois que vous êtes connecté, vous pouvez le changer en visitant votre section de profil. </p>");
                //    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                //    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                //    sb.Append("</div>");
                //    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                //    sb.Append("</div></div></div></body>");
                //    break;
            }
       
            return sb.ToString();
        }

        public string GetAcknowledgement(string afn, string userName, string url)
        {
            
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + afn + ",</h2>");
                    sb.Append("<p>Bedankt voor uw registratie in de postautorisatiestudie voor het Cidofovir-blootstellingsregister uitgevoerd door Tillomed Laboratories Ltd. De status van uw registratie is nu in behandeling bij de houder van de handelsvergunning en u wordt binnenkort via e-mail op de hoogte gebracht als uw registratie succesvol is verlopen. Hartelijk dank voor uw belangstelling, vriendelijke groeten, Tillomed Laboratories.</p>");
                    sb.Append("<p>Heeft u nog vragen, neem dan contact op met <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + afn + ",</h2>");
                    sb.Append("<p>Vielen Dank für Ihre Registrierung in der von Tillomed Laboratories Ltd. durchgeführten Post-Authorization-Studie für das Cidofovir-Expositionsregister. Der Status Ihrer Registrierung steht jetzt beim Inhaber der Genehmigung für das Inverkehrbringen aus. Nach erfolgreicher Registrierung werden Sie in Kürze per E-Mail benachrichtigt. Vielen Dank für Ihr Interesse, Mit freundlichen Grüßen, Tillomed Laboratories.</p>");
                    sb.Append("<p>Bei Fragen wenden Sie sich bitte an <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + afn + ",</h2>");
                    sb.Append("<p>Thank you for your registration into the Post Authorisation Study for the Cidofovir Exposure Registry conducted by Tillomed Laboratories Ltd.The status of your registration is now pending with the marketing authorisation holder, and you will be informed shortly via email if your registration has been successful. Many Thanks for your interest, Kind Regards, Tillomed Laboratories.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + afn + ",</h2>");
                    sb.Append("<p>Gracias por su registro en el Estudio de Autorización para el Registro de Exposición al Cidofovir realizado por Tillomed Laboratories Ltd. El estado de su registro ahora está pendiente con el titular de la autorización de comercialización, y se le informará en breve por correo electrónico si su registro ha sido exitoso. Muchas gracias por su interés, Saludos cordiales, Tillomed Laboratories.</p>");
                    sb.Append("<p>Tienes alguna duda, por favor contacta <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + afn + ",</h2>");
                    sb.Append("<p>Nous vous remercions de votre inscription à l’étude de post-autorisation pour le registre des expositions au cidofovir réalisée par Tillomed Laboratories Ltd. Merci beaucoup pour votre intérêt, Cordialement, Laboratoires Tillomed.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contacter <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();
        }

        public string GetAdminAcknowledgement(string adminName, string prescriberName, string url)
        {
         
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
          
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + adminName + ",</h2>");
                    sb.Append("<p>Er is een nieuw registratieverzoek ingediend door Dr. "+ prescriberName +", voor de postautorisatiestudie voor het Cidofovir - blootstellingsregister.Download de registratiedetails vriendelijk naar het PV UK - team voor goedkeuring / afwijzing.Zodra PV UK Team het verzoek goedkeurt / afwijst, update dan de status op de Site.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + adminName + ",</h2>");
                    sb.Append("<p>Ein neuer Registrierungsantrag wurde von Dr. " + prescriberName + ", für die Post Authorization Study für das Cidofovir Exposure Registry gestellt. Bitte laden Sie die Registrierungsdaten zur Genehmigung / Ablehnung an das PV UK Team herunter. Sobald das PV UK Team die Anfrage genehmigt / ablehnt, aktualisieren Sie bitte den Status auf der Site.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + adminName + ",</h2>");
                    sb.Append("<p>A new registration request has been raised by Dr. " + prescriberName + ", for the Post Authorization Study for the Cidofovir Exposure Registry. Kindly download the registration details to the PV UK Team for approval / rejection. Once PV UK Team approves / rejects the request, please update the status on the Site.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + adminName + ",</h2>");
                    sb.Append("<p>El Dr. " + prescriberName + ", presentó una nueva solicitud de registro para el Estudio de Autorización Posterior para el Registro de Exposición de Cidofovir Descargue los detalles de registro al equipo de PV UK para su aprobación / rechazo. Una vez que el equipo de PV UK apruebe / rechace la solicitud, actualice el estado en el sitio.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + adminName + ",</h2>");
                    sb.Append("<p>Dr. " + prescriberName + ", a lancé une nouvelle demande d'enregistrement pour l'étude de post-autorisation pour le registre d'exposition au cidofovir. Veuillez télécharger les détails de l’enregistrement auprès de l’équipe PV UK pour approbation / rejet. Une fois que PV UK Team a approuvé / rejeté la demande, veuillez mettre à jour le statut sur le site.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }

           
            return sb.ToString();
        }

        public string GetAdminAcknowledgementBDFModification(string adminName, string prescriberName, string patientName)
        {
          
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + adminName + ",</h2>");
                    sb.Append("<p>Basislijnformulier is gewijzigd door Dr. " + prescriberName + " voor " + patientName + ". Beoordeel en keur het goed.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");

                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + adminName + ",</h2>");
                    sb.Append("<p>Das Basisformular wurde von Dr. " + prescriberName + " für " + patientName + ".  geändert. Bitte überprüfen und genehmigen.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");

                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + adminName + ",</h2>");
                    sb.Append("<p>Baseline Form has been modified by Dr. " + prescriberName + " for " + patientName + ".  Please review and approve.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");

                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + adminName + ",</h2>");
                    sb.Append("<p>El formulario de línea base fue modificado por el Dr. " + prescriberName + " para " + patientName + ".  Por favor revise y apruebe.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");

                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + adminName + ",</h2>");
                    sb.Append("<p>Le formulaire de ligne de base a été modifié par le Dr. " + prescriberName + " pour " + patientName + ".  S'il vous plaît examiner et approuver.</p>");
                    sb.Append("<p>Thanks and Regards, </p>");
                    sb.Append("<p>Tillomed Laboratories.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");

                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();

        }

        public string GetPatientReject(string prescriber, string patient, string message, string form)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
          
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + prescriber + ",</h2>");
                    sb.Append("<p>Het " + form + " for " + patient + " wordt afgewezen omdat de reden is " + message + " </p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + prescriber + ",</h2>");
                    sb.Append("<p>Das " + form + " für " + patient + " wird abgelehnt, da dies der Grund ist " + message + " </p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriber + ",</h2>");
                    sb.Append("<p>The " + form + " for " + patient + " is rejected because the reason is " + message + " </p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + prescriber + ",</h2>");
                    sb.Append("<p>El " + form + " para " + patient + " se rechaza porque la razón es " + message + " </p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + prescriber + ",</h2>");
                    sb.Append("<p>Le " + form + " pour " + patient + " est rejeté car la raison est " + message + " </p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
          
            return sb.ToString();
        }

        public string GetPatientApprovedICF(string prescriber, string patient)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
          
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + prescriber + ",</h2>");
                    sb.Append("<p>Het geïnformeerde toestemmingsformulier voor " + patient + " is goedgekeurd. Na goedkeuring van het Basisgegevensformat, krijgt u toegang tot het vervolgformulier, voor voltooiing tijdens het volgende bezoek van de patiënt.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + prescriber + ",</h2>");
                    sb.Append("<p>Das Einverständniserklärung für " + patient + " wird genehmigt. Nach Genehmigung des Basisdatenformulars erhalten Sie Zugriff auf das Folgeformular, das Sie beim nächsten Besuch des Patienten ausfüllen können.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriber + ",</h2>");
                    sb.Append("<p>The Informed Consent Form for " + patient + " is approved. Upon approval of Baseline Data Form, you will be granted to have access to the follow up form, for completion during the patients next visit.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + prescriber + ",</h2>");
                    sb.Append("<p>Se aprueba el formulario de consentimiento informado para " + patient + " . Tras la aprobación del Formulario de datos de referencia, se le otorgará acceso al formulario de seguimiento, para completar durante la próxima visita del paciente.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + prescriber + ",</h2>");
                    sb.Append("<p>Le formulaire de consentement éclairé pour " + patient + " est approuvé. Après approbation du formulaire de données de base, vous serez autorisé à avoir accès au formulaire de suivi, à remplir lors de la prochaine visite du patient.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
          
            return sb.ToString();

        }

        public string GetPatientApprovedBDF(string prescriber, string patient)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + prescriber + ",</h2>");
                    sb.Append("<p>Het basisgegevensformulier voor " + patient + " is goedgekeurd. Nu hebt u toegang tot het follow-upformulier, voor voltooiing tijdens het volgende bezoek van de patiënt.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + prescriber + ",</h2>");
                    sb.Append("<p>Das Basisdatenformular für " + patient + " wird genehmigt. Jetzt haben Sie Zugriff auf das Follow-up-Formular, das Sie beim nächsten Besuch des Patienten ausfüllen können.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriber + ",</h2>");
                    sb.Append("<p>The Baseline Data Form for " + patient + " is approved. Now you have access to the follow up form, for completion during the patients next visit.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + prescriber + ",</h2>");
                    sb.Append("<p>Se aprueba el formulario de datos de referencia para " + patient + ". Ahora tiene acceso al formulario de seguimiento, para completar durante la próxima visita de los pacientes.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + prescriber + ",</h2>");
                    sb.Append("<p>Le formulaire de données de base pour " + patient + " est approuvé. Vous avez maintenant accès au formulaire de suivi, à remplir lors de la prochaine visite du patient.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
            
            return sb.ToString();
        }

        public string SendMessageToAdmin(string admin, string prescriber, string patient, string form)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
            
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + admin + ",</h2>");
                    sb.Append("<p>Een nieuw " + form + " wordt door de " + prescriber + " voor " + patient + " voor uw beoordeling ingediend.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + admin + ",</h2>");
                    sb.Append("<p>Ein neues " + form + " wird vom " + prescriber + " für " + patient + " zur Überprüfung eingereicht.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + admin + ",</h2>");
                    sb.Append("<p>A new " + form + " is submitted by the " + prescriber + " for " + patient + " for your review.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + admin + ",</h2>");
                    sb.Append("<p>Un " + form + " nuevo se envía con " + prescriber + " para " + patient + " para su revisión.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + admin + ",</h2>");
                    sb.Append("<p>Un nouveau " + form + " est soumis par le " + prescriber + " pour " + patient + " pour examen.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + _configuration["ContactTo"].ToString() + "' target='_top'>" + _configuration["ContactTo"].ToString() + "</a></p>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();
        }

        public string GetPrescriberAcknowledgementICF(string prescriber, string url, string adminEmail)
        {
        
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
          
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + prescriber + ",</h2>");
                    sb.Append("<p>Bedankt voor het insturen van het informed consent-formulier, we erkennen uw inzending. Na goedkeuring van het geïnformeerde toestemmingsformulier samen met het basisgegevensformat, krijgt u toegang tot het follow-upformulier voor voltooiing tijdens het volgende bezoek van de patiënt.</p>");
                    sb.Append("<p>Hartelijk dank voor uw belangstelling, vriendelijke groeten, Tillomed Laboratories.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + prescriber + ",</h2>");
                    sb.Append("<p>Vielen Dank für die Übermittlung des Formulars für die Einwilligung nach Inkenntnissetzung. Wir bestätigen Ihre Übermittlung. Wenn Sie das Einverständniserklärung-Formular zusammen mit dem Grunddatenformular genehmigen, erhalten Sie Zugriff auf das Folgeformular, das Sie beim nächsten Besuch des Patienten ausfüllen können.</p>");
                    sb.Append("<p>Vielen Dank für Ihr Interesse, Mit freundlichen Grüßen, Tillomed Laboratories.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriber + ",</h2>");
                    sb.Append("<p>Thank you for submitting the Informed Consent Form, we acknowledge your submission. Upon approval of Informed Consent Form along with Baseline Data Form, you will be granted to have access to the follow up form, for completion during the patients next visit.</p>");
                    sb.Append("<p>Many Thanks for your interest, Kind Regards, Tillomed Laboratories.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + prescriber + ",</h2>");
                    sb.Append("<p>Gracias por enviar el Formulario de consentimiento informado, reconocemos su envío.Tras la aprobación del Formulario de consentimiento informado junto con el Formulario de datos de referencia, se le otorgará el acceso al formulario de seguimiento para completar durante la próxima visita de los pacientes.</p>");
                    sb.Append("<p>Muchas gracias por su interés, Saludos cordiales, Tillomed Laboratories.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + prescriber + ",</h2>");
                    sb.Append("<p>Merci d'avoir soumis le formulaire de consentement éclairé, nous accusons réception de votre soumission. Après approbation du formulaire de consentement éclairé et du formulaire de données de base, vous serez autorisé à accéder au formulaire de suivi, à remplir lors de la prochaine visite du patient.</p>");
                    sb.Append("<p>Merci beaucoup pour votre intérêt, Cordialement, Laboratoires Tillomed.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
          
            return sb.ToString();
        }

        public string GetPrescriberAcknowledgementBDF(string prescriber, string url, string adminEmail)
        {
        
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
         
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + prescriber + ",</h2>");
                    sb.Append("<p>Bedankt voor het indienen van het basisgegevensformulier, we erkennen uw inzending. Na goedkeuring van het Basisgegevensformat, krijgt u toegang tot het vervolgformulier, voor voltooiing tijdens het volgende bezoek van de patiënt.</p>");
                    sb.Append("<p>Hartelijk dank voor uw belangstelling, vriendelijke groeten, Tillomed Laboratories.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + prescriber + ",</h2>");
                    sb.Append("<p>Vielen Dank für die Übermittlung des Basisdatenformulars. Wir bestätigen Ihre Übermittlung. Nach Genehmigung des Basisdatenformulars erhalten Sie Zugriff auf das Folgeformular, das Sie beim nächsten Besuch des Patienten ausfüllen können.</p>");
                    sb.Append("<p>Vielen Dank für Ihr Interesse, Mit freundlichen Grüßen, Tillomed Laboratories.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriber + ",</h2>");
                    sb.Append("<p>Thank you for submitting the Baseline Data Form, we acknowledge your submission. Upon approval of Baseline Data Form, you will be granted to have access to the follow up form, for completion during the patients next visit.</p>");
                    sb.Append("<p>Many Thanks for your interest, Kind Regards, Tillomed Laboratories.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + prescriber + ",</h2>");
                    sb.Append("<p>Gracias por enviar el formulario de datos de referencia, reconocemos su envío. Tras la aprobación del Formulario de datos de referencia, se le otorgará acceso al formulario de seguimiento, para completar durante la próxima visita del paciente.</p>");
                    sb.Append("<p>Muchas gracias por su interés, Saludos cordiales, Tillomed Laboratories.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + prescriber + ",</h2>");
                    sb.Append("<p>Merci d'avoir envoyé le formulaire de données de base, nous accusons réception de votre soumission. Après approbation du formulaire de données de base, vous serez autorisé à avoir accès au formulaire de suivi, à remplir lors de la prochaine visite du patient.</p>");
                    sb.Append("<p>Merci beaucoup pour votre intérêt, Cordialement, Laboratoires Tillomed.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();
        }

        public string GetPrescriberAcknowledgementFUF(string prescriber, string url, string adminEmail)
        {
        
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
           
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte " + prescriber + ",</h2>");
                    sb.Append("<p>Bedankt aan het toevoegen van een nieuw follow-upformulier</p>");
                    sb.Append("<p>Hartelijk dank voor uw belangstelling, vriendelijke groeten, Tillomed Laboratories.</p>");
                    sb.Append("<p>Heb je nog vragen, Neem contact op <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ga naar portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Vriendelijke groeten, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter " + prescriber + ",</h2>");
                    sb.Append("<p>Vielen Dank, dass Sie ein neues Formular hinzugefügt haben</p>");
                    sb.Append("<p>Vielen Dank für Ihr Interesse, Mit freundlichen Grüßen, Tillomed Laboratories.</p>");
                    sb.Append("<p>Hast du irgendwelche Fragen, kontaktieren Sie bitte <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Gehe zum Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Freundliche Grüße, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Mannschaft @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriber + ",</h2>");
                    sb.Append("<p>Thanks you from adding a new follow up form</p>");
                    sb.Append("<p>Many Thanks for your interest, Kind Regards, Tillomed Laboratories.</p>");
                    sb.Append("<p>You have any questions, please contact <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Go To Portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Best Regards, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido " + prescriber + ",</h2>");
                    sb.Append("<p>Gracias por agregar un nuevo formulario de seguimiento</p>");
                    sb.Append("<p>Muchas gracias por su interés, Saludos cordiales, Tillomed Laboratories.</p>");
                    sb.Append("<p>Tienes alguna pregunta, por favor contactar <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Ir al portal</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Atentamente, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Equipo @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher " + prescriber + ",</h2>");
                    sb.Append("<p>Merci d'avoir ajouté un nouveau formulaire de suivi</p>");
                    sb.Append("<p>Merci beaucoup pour votre intérêt, Cordialement, Laboratoires Tillomed.</p>");
                    sb.Append("<p>Vous avez des questions, s'il vous plaît contactez <a href='mailto:" + adminEmail + "' target='_top'>" + adminEmail + "</a></p>");
                    sb.Append("<br><a href='" + url + "' style='text-decoration:none;line-height:100%;background:rgba(44,80,102,1);color:white;font-family:sans-serif;font-size:15px;font-weight:bold;text-transform:none;margin:0px; border:none;border-radius:3px;color:white; padding:15px 19px; background-color:#059a9d;' target='_blank'>Aller au portail</a><br><br>");
                    sb.Append("<p style='margin-bottom:5px;'>Meilleures salutations, </p>");
                    sb.Append("<p style='margin-top:5px; margin-bottom:0;'>Équipe @ Tillomed Laboratoriess.</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
          
            return sb.ToString();
        }

        public string ContactUs(string name, string email, string query)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            StringBuilder sb = new StringBuilder();
          
            switch (cultureName)
            {
                case "nl-BE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welkom bij <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Geachte Admin,</h2>");
                    sb.Append("<p>U hebt ane enquiery/Vraag van volgende persoon</p>");
                    sb.Append("<p><b>Naam : </b> " + name + "</p>");
                    sb.Append("<p><b>E-mail : </b> " + email + "</p>");
                    sb.Append("<p><b>Query/aanvraag : </b> " + query + "</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "de-DE":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Willkommen zu <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Sehr geehrter Admin,</h2>");
                    sb.Append("<p>Sie haben eine Anfrage / Anfrage von folgender Person</p>");
                    sb.Append("<p><b>Name : </b> " + name + "</p>");
                    sb.Append("<p><b>Email : </b> " + email + "</p>");
                    sb.Append("<p><b>Anfrage : </b> " + query + "</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "en-GB":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear Admin,</h2>");
                    sb.Append("<p>You have any enquiry/Query from following person</p>");
                    sb.Append("<p><b>Name : </b> " + name + "</p>");
                    sb.Append("<p><b>Email : </b> " + email + "</p>");
                    sb.Append("<p><b>Query/enquiry : </b> " + query + "</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "es-ES":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenido a <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Querido Admin,</h2>");
                    sb.Append("<p>Tiene alguna consulta / consulta de la siguiente persona</p>");
                    sb.Append("<p><b>Nombre : </b> " + name + "</p>");
                    sb.Append("<p><b>Email : </b> " + email + "</p>");
                    sb.Append("<p><b>Consulta/consulta : </b> " + query + "</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
                case "fr-FR":
                    sb.Append("<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'> <div style='margin:0px auto 0;max-width:640px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'> <div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Bienvenue à <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody> </table> </div><div style='margin:0px auto;max-width:640px;background:#ffffff;'>");
                    sb.Append("<table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'> <div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'>");
                    sb.Append("<div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'>");
                    sb.Append("<h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Cher Admin,</h2>");
                    sb.Append("<p>Vous avez une requête / requête de la part de la personne suivante</p>");
                    sb.Append("<p><b>prénom : </b> " + name + "</p>");
                    sb.Append("<p><b>Email : </b> " + email + "</p>");
                    sb.Append("<p><b>Requête / enquête : </b> " + query + "</p>");
                    sb.Append("</div>");
                    sb.Append("</td></tr></tbody> </table> </div></td></tr></tbody> </table>");
                    sb.Append("</div></div></div></body>");
                    break;
            }
           
            return sb.ToString();
        }

    }
}
