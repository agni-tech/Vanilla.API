using AutoMapper.Internal;
using Vanilla.Domain.Users.Interfaces;
using Vanilla.Shared.Dtos;
using Vanilla.Shared.Helpers;
using Vanilla.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MySaviors.Helpers.Extensions;
using System.Dynamic;
using System.Globalization;
using System.Net;

namespace Vanilla.API.Helpers;

public class ControllerHelper : IControllerHelper
{
    private const string ERROR_SERVER_MESSAGE = "ERROR_SERVER_MESSAGE";
    private const string BAD_REQUEST_MESSAGE = "BAD_REQUEST_MESSAGE";
    private const string OK_MESSAGE = "OK_MESSAGE";

    readonly INotifications _notifications;
    readonly ILocalizationHelper _localizationHelper;
    readonly CultureInfo _culture;

    public ControllerHelper(INotifications notifications, ILocalizationHelper localizationHelper
         , IAuthService authService)
    {
        _notifications = notifications;
        _localizationHelper = localizationHelper;
        _culture = authService.GetLocale();
    }

    public ObjectResult PackResult(Func<dynamic> procedure)
    {
        try
        {
            var response = procedure();

            Type responseType = response.GetType();

            if (DoesPropertyExist(response, "Errors"))
            {
                var errors = response?.Errors as List<FluentValidation.Results.ValidationFailure>;
                errors.SForEach(q => NotificationsWrapper.AddMessage(q.ErrorMessage));
            }

            if (!responseType.IsListType() && !_notifications.IsValid)
                return MyObjectResult(null, HttpStatusCode.BadRequest, string.Join(", ", _notifications.Messages));

            return MyObjectResult(response, HttpStatusCode.OK, null);
        }
        catch (Exception ex)
        {
            if (_notifications.Messages.HaveAny())
                return MyObjectResult(null, HttpStatusCode.BadRequest, string.Join(", ", _notifications.Messages));
            else
                return MyObjectResult(null, HttpStatusCode.InternalServerError, $"[error] => {ex.Message} :: {ex.InnerException}");
        }
    }

    public async Task<ObjectResult> PackResultAsync(Func<dynamic> procedure)
    {
        try
        {
            var response = await procedure();

            if (response is object)
            {
                Type responseType = response.GetType();

                if (DoesPropertyExist(response, "Errors"))
                {
                    var errors = response?.Errors as List<FluentValidation.Results.ValidationFailure>;
                    errors.SForEach(q => NotificationsWrapper.AddMessage(q.ErrorMessage));
                }

                if (!responseType.IsListType() && !_notifications.IsValid)
                    return MyObjectResult(null, HttpStatusCode.BadRequest, string.Join(", ", Translate(_notifications.Messages)));
            }
            return MyObjectResult(response, HttpStatusCode.OK, null);
        }
        catch (Exception ex)
        {
            if (_notifications.Messages.HaveAny())
                return MyObjectResult(null, HttpStatusCode.BadRequest, string.Join(", ", Translate(_notifications.Messages)));
            else
                return MyObjectResult(null, HttpStatusCode.InternalServerError, $"[error] => {ex.Message} :: {ex.InnerException}");
        }
    }

    public static bool DoesPropertyExist(dynamic settings, string name)
    {
        if (settings is ExpandoObject)
            return ((IDictionary<string, object>)settings).ContainsKey(name);

        return settings.GetType().GetProperty(name) != null;
    }

    private ObjectResult MyObjectResult(object message = null, HttpStatusCode statusCode = HttpStatusCode.OK, string log = null)
    {
        var result = new ObjectResult(null);

        var processed = (((int)statusCode).ToString().Substring(0, 1) == "2");
        var PrefixMessage = $"[PackResult] :: {statusCode} ::";
        var resultMessage = processed ? Translate(OK_MESSAGE) :
                           (statusCode == HttpStatusCode.InternalServerError ? Translate(ERROR_SERVER_MESSAGE) :
                                                                               Translate(BAD_REQUEST_MESSAGE));

        result.Value = new ResultDto(resultMessage, message ?? log, (int)statusCode);
        result.StatusCode = (int)statusCode;

        if (processed)
            LogHelper.InfoLog($"{PrefixMessage} => {log ?? resultMessage}");
        else
            LogHelper.ExceptionLog($"{PrefixMessage} {log}", $"{PrefixMessage} {log}");


        return result;
    }

    private List<string> Translate(List<string> keys)
    {
        var result = new List<string>();
        keys.SForEach(msg => result.Add(_localizationHelper.GetString(msg, _culture)));
        return result;
    }

    private string Translate(string key)
    {
        return _localizationHelper.GetString(key, _culture);
    }
}