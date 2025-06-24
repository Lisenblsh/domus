using System.Text;
using Domus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//IConfigurationSection configurationSection = builder.Configuration.GetSection("AppSettings");
//AppSettings settings = configurationSection.Get<AppSettings>();// ?? throw new Exception("Fatal no appsettings.json");
//byte[] signingKey = Encoding.UTF8.GetBytes(settings.EncryptionKey);
var redisHost = builder.Configuration["Redis:Host"] ?? "localhost";
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = $"{redisHost}:6379";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".App.Session";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<NpgSqlContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<AppSettings>(configurationSection);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.MapControllers();

app.Run();
