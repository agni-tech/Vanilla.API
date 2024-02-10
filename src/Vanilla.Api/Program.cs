using AutoMapper;
using Vanilla.API.Helpers;
using Vanilla.Domain.Activities.MappingProfiles;
using Vanilla.Domain.Options.MappingProfiles;
using Vanilla.Domain.UserGroupFeatures.MappingProfiles;
using Vanilla.Domain.UserGroups.MappingProfiles;
using Vanilla.Domain.Users.MappingProfiles;
using Vanilla.Domain.Users.Services.Middlewares;
using Vanilla.Infra.Data.UnityOfWork;
using Vanilla.Infra.IoC.Configurations;
using Vanilla.Shared.Helpers;
using Vanilla.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddContextDependecies(builder.Configuration);
builder.Services.AddApplicationDependecies(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(UnitOfWork)));

builder.Services.AddMvcCore()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        });

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .SetIsOriginAllowed(x => true)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("APP_TOKEN_SECURITYKEY") ??
                                    builder.Configuration.GetSection("Secret").Value ?? string.Empty);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vanilla API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
    mc.AddProfile(new UserGroupMappingProfile());
    mc.AddProfile(new AddressMappingProfile());
    mc.AddProfile(new UserGroupFeatureMappingProfile());
    mc.AddProfile(new OptionMappingProfile());


});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IControllerHelper, ControllerHelper>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<INotifications, Notifications>();
builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<ILocalizationHelper, LocalizationHelper>();
builder.Services.AddSingleton<IContainer, ServiceProviderProxy>();
builder.Services.AddDIHandler();

var app = builder.Build();

ServiceLocator.Initialize(app.Services.GetService<IContainer>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api") && app.Environment.IsDevelopment(), appBuilder =>
{
    appBuilder.UseMiddleware<LocalAndDevAuthMock>();
});

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
