namespace Vanilla.Domain.Common.Interfaces;

public interface IEmailService
{
    Task Send(string to, string subject, string message, string locale = "pt-BR");
    Task<bool> SendTemplate(string to, string subject, string template, dynamic data, string locale = "pt-BR");
}
