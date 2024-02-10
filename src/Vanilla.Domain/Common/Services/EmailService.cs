using Vanilla.Domain.Common.Interfaces;
using Vanilla.Shared.Dtos;
using Vanilla.Shared.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Vanilla.Domain.Common.Services;

public class EmailService : IEmailService
{
    readonly AppSettingsDto _appSettings;
    readonly MailMessage _mail;
    readonly ILocalizationHelper _localizationHelper;

    public EmailService(AppSettingsDto appSettings, ILocalizationHelper localizationHelper)
    {
        _localizationHelper = localizationHelper;
        _appSettings = appSettings;
        _mail = new MailMessage
        {
            From = new MailAddress(_appSettings.EmailSettings.Credencial, _appSettings.EmailSettings.From),
            IsBodyHtml = true
        };
    }

    public async Task Send(string to, string subject, string message, string locale = "pt-BR")
    {
        using var smtp = new SmtpClient(_appSettings.EmailSettings.Smtp, _appSettings.EmailSettings.Port);

        smtp.Credentials = new NetworkCredential(_appSettings.EmailSettings.Credencial, _appSettings.EmailSettings.Pwd);
        smtp.EnableSsl = _appSettings.EmailSettings.Ssl;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;

        _mail.To.Add(to);
        _mail.Subject = _localizationHelper.GetString(subject, new System.Globalization.CultureInfo(locale));
        _mail.Body = message;
        _mail.Priority = MailPriority.Normal;

        await smtp.SendMailAsync(_mail);
    }

    public async Task<bool> SendTemplate(string to, string subject, string template, dynamic data, string locale = "pt-BR")
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Htmls", $"{template}.html");

        if (!File.Exists(path))
            return false;

        string html = File.ReadAllText(path);
        html = html.Replace("{{url_site}}", _appSettings.Links.Site.ToLower());

        PropertyInfo[] myPropertyInfo = data.GetType().GetProperties();
        foreach (PropertyInfo prop in myPropertyInfo)
        {
            try
            {
                html = html.Replace("{{" + prop.Name.ToLower() + "}}", $"{prop.GetValue(data, null)}");
            }
            catch (Exception)
            {

            }
        }

        await Send(to, subject, html, locale);

        return true;
    }
}