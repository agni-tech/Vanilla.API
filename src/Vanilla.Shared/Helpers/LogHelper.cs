
using Serilog;
using Microsoft.AspNetCore.Builder;

namespace Vanilla.Shared.Helpers;

public static class LogHelper
{
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());
        return builder;
    }

    public static void InfoLog(string message)
        => Log.Information(message);

    public static void ExceptionLog(string error, string message)
        => Log.Fatal(error, message);

    public static void ExceptionLog(Exception ex, string message)
        => Log.Fatal(ex, message);
}
