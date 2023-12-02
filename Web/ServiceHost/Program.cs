using Category.InfrastructureEfCore;

using CD.Infrastructure.EfCore;

using CommentManagement.Infrastructure.Configuration;

using DiscountManagement.Configuration;

using FaghihstoreQuery;

using Identity.Infrastructure;

using Inventory.Infrastructure.EFCore;

using PB.Infrastructure.EfCore;

using PM.Infrastructure.EFCore;

using Serilog;

using ServiceHost.Extensions;

using SH.Infrastructure;
using SH.Infrastructure.Settings;

using SS.Infrastructure.EfCore;

using TG.Infrastructure.EfCore;

using VG.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.UseSentryLogging();

services.AddHttpContextAccessor();

services.Configure<SiteSettings>(siteSetting => configuration.GetSection(nameof(SiteSettings)).Bind(siteSetting));

services.AddIdentityAuthorizationPolicy();

#region Modules

var connectionString = configuration.GetConnectionString(configuration["CurrentConnectionString"]);
var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

services.AddSharedKernel(connectionString);

services.AddSiteSettingModule(connectionString);

services.AddIdentity(siteSettings, connectionString, builder.Environment);

services.AddCountryDivisionModule(connectionString);
services.AddCategoryModule(connectionString);
services.AddProductModule(connectionString);

services.AddBrandModule(connectionString);

services.AddTraitGroupModule(connectionString);

services.AddVarietyGroupModule(connectionString);

services.AddInventoryModule(connectionString);

services.AddQuery();

DiscountManagementBootstrapper.Configure(services, connectionString);

CommentManagementBootstrapper.Configure(services, connectionString);

#endregion

services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddTransient<IFileUploader, FileUploader>();
services.AddTransient<IAuthHelper, AuthHelper>();
services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
services.AddTransient<ISmsService, SmsService>();
services.AddTransient<IEmailService, EmailService>();

services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
    builder
        .WithOrigins("https://localhost:5002")
        .AllowAnyHeader()
        .AllowAnyMethod()));

services.AddControllersWithViews();

services.AddHealthChecksProject(connectionString);

var app = builder.Build();

if (builder.Environment.IsDevelopment() || app.Environment.IsDevelopmentOnServer())
{
    app.UseDeveloperExceptionPage();
    await app.InitializeSeedData();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapHealthChecksProject();

app.UseCors("MyPolicy");

app.MapControllers();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.Run();
