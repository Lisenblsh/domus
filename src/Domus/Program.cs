using Domus.Models;
using Domus.Models.Dish;
using Domus.Models.Menu;
using Domus.Models.Product;
using Domus.Models.Recipe;
using Domus.Models.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<NpgSlqContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))    
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<MenuService>();
builder.Services.AddTransient<RecipeService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<DishService>();

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
