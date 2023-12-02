using SH.Infrastructure;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Settings;

using VG.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.UseSentryLogging();

// Add services to the container.

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString(configuration["CurrentConnectionString"]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<SiteSettings>(siteSetting => configuration.GetSection(nameof(SiteSettings)).Bind(siteSetting));

builder.Services.AddSharedKernel(connectionString);

builder.Services.AddVarietyGroupModule(connectionString);

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

await app.RunAsync();
