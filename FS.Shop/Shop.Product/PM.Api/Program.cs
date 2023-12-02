using _0_Framework.Application;
using _0_Framework.Application.Email;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;

using Inventory.Infrastructure.EFCore;

using PM.Api;
using PM.Infrastructure.EFCore;

using SH.Infrastructure;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Settings;

using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.UseSentryLogging();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddHttpClient();
services.AddHttpContextAccessor();

services.Configure<SiteSettings>(siteSetting => configuration.GetSection(nameof(SiteSettings)).Bind(siteSetting));

#region Modules

var connectionString = configuration.GetConnectionString(configuration["CurrentConnectionString"]);

services.AddSharedKernel(connectionString);

services.AddProductModule(connectionString);

services.AddInventoryModule(connectionString);

#endregion

services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddTransient<IFileUploader, FileUploader>();
services.AddTransient<IAuthHelper, AuthHelper>();
services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
services.AddTransient<ISmsService, SmsService>();
services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsDevelopmentOnServer())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
