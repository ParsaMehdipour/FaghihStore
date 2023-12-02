using Category.InfrastructureEfCore;

using SH.Infrastructure;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.UseSentryLogging();

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHttpContextAccessor();

services.Configure<SiteSettings>(siteSetting => configuration.GetSection(nameof(SiteSettings)).Bind(siteSetting));

#region Modules

var connectionString = configuration.GetConnectionString(configuration["CurrentConnectionString"]);

services.AddSharedKernel(connectionString);

services.AddCategoryModule(connectionString);

#endregion

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
