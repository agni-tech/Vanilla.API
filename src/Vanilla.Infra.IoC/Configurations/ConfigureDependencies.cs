using Vanilla.Domain.Options.Interfaces;
using Vanilla.Domain.Options.Services;
using Vanilla.Domain.Common.Interfaces;
using Vanilla.Domain.Common.Services;
using Vanilla.Domain.User.Interfaces;
using Vanilla.Domain.UserGroupFeatures.Interfaces;
using Vanilla.Domain.UserGroupFeatures.Services;
using Vanilla.Domain.UserGroups.Interfaces;
using Vanilla.Domain.UserGroups.Services;
using Vanilla.Domain.Users.Interfaces;
using Vanilla.Domain.Users.Services;
using Vanilla.Infra.Data.Configurations;
using Vanilla.Infra.Data.Contexts;
using Vanilla.Infra.Data.Repositories;
using Vanilla.Infra.Data.UnityOfWork;
using Vanilla.Shared.Dtos;
using Vanilla.Shared.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vanilla.Infra.IoC.Configurations;

public static class ConfigureDependencies
{
    public static void AddContextDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = EValues("APP_CONNECTIONSTRING", "DefaultConnection", configuration, true);

        services.AddDbContext<VanillaContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(connection, x => x.MigrationsAssembly("Vanilla.Infra.Data"));
        });

        services.AddSingleton<Func<DbContext>>(provider => () =>
        {
            var scope = provider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<VanillaContext>();
        });

        services.AddSingleton<Func<DisposableScopedContextWrapper>>(provider => () =>
        {
            var scope = provider.CreateScope();
            return new DisposableScopedContextWrapper(scope);
        });

    }

    public static void AddApplicationDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        services.EnvironmentValues(configuration);

        services.AddScoped<IUnityOfWork, UnitOfWork>();

        services.AddScoped<IRedemetService, RedemetService>();

        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IEmailService, EmailService>();

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IZipCodeService, ZipCodeService>();
        services.AddScoped<IAddressService, AddressService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IOptionService, OptionService>();
        services.AddScoped<IOptionRepository, BuildingRepository>();

        services.AddScoped<IUserGroupService, UserGroupService>();
        services.AddScoped<IUserGroupRepository, UserGroupRepository>();

        services.AddScoped<IUserGroupFeatureService, UserGroupFeatureService>();
        services.AddScoped<IUserGroupFeatureRepository, UserGroupFeatureRepository>();
    }
    private static void EnvironmentValues(this IServiceCollection services, IConfiguration configuration)
    {
        var cnf = new AppSettingsDto();

        cnf.ConnectionStrings.DefaultConnection = EValues("APP_CONNECTIONSTRING", "DefaultConnection", configuration, true);

        cnf.Secrets.TokenSecurityKey = EValues("APP_TOKEN_SECURITYKEY", "Secrets:TokenSecurityKey", configuration);
        cnf.Secrets.RedemetApiKey = EValues("APP_TOKEN_SECURITYKEY", "Secrets:RedemetApiKey", configuration);
        cnf.Locale = EValues("APP_LOCALE", "Locale", configuration);

        cnf.Links.Site = EValues("LINKS_SITE", "Links:Site", configuration);
        cnf.Links.ZipCodeSearch = EValues("LINKS_ZIPCODE", "Links:ZipCodeSearch", configuration);
        cnf.Links.Redemet = EValues("LINKS_REDEMET", "Links:Redemet", configuration);

        cnf.EmailSettings.Smtp = EValues("EMAIL_SMTP", "Email:Smtp", configuration);
        cnf.EmailSettings.From = EValues("EMAIL_FROM", "Email:From", configuration);
        cnf.EmailSettings.Credencial = EValues("EMAIL_CREDENCIAL", "Email:Credencial", configuration);
        cnf.EmailSettings.Pwd = EValues("EMAIL_PWD", "Email:Pwd", configuration);
        cnf.EmailSettings.Ssl = bool.Parse(EValues("EMAIL_SSL", "Email:Ssl", configuration));
        cnf.EmailSettings.Port = int.Parse(EValues("EMAIL_PORT", "Email:Port", configuration));

        services.AddSingleton(cnf);
    }



    private static string EValues(string envName, string appName, IConfiguration configuration, bool isCnStr = false)
    {
        if (isCnStr)
            return (Environment.GetEnvironmentVariable(envName) ?? configuration.GetConnectionString(appName)) ?? string.Empty;

        return (Environment.GetEnvironmentVariable(envName) ?? configuration.GetSection(appName)?.Value) ?? string.Empty;
    }
}
