using FluentValidation.AspNetCore;

using Identity.Extensions;
using Identity.Infrastructure;

using SH.Infrastructure;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.UseSentryLogging();

string connectionString = configuration.GetConnectionString(configuration["CurrentConnectionString"]);

var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(_ =>
{
    _.DisableDataAnnotationsValidation = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<SiteSettings>(siteSetting => configuration.GetSection(nameof(SiteSettings)).Bind(siteSetting));

builder.Services.AddSharedKernel(connectionString);

//Add Identity services to container.
builder.Services.AddIdentity(siteSettings, connectionString, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsDevelopmentOnServer())
{
    //initialize user(identity) module seed data
    await app.InitializeSeedData();
}

app.UseHttpsRedirection();

app.UseHsts();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Auth}/{action=Register}/{id?}");

app.Run();
