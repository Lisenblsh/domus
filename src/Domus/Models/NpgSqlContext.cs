using System;
using Domus.Models.Dish;
using Domus.Models.Menu;
using Domus.Models.Product;
using Domus.Models.Recipe;
using Domus.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models;

public class NpgSqlContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDto>()
            .HasOne(x => x.Credentials)
            .WithOne(x => x.User)
            .HasForeignKey<UserCredentialsDto>(x => x.User.Id)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<MenuDto> Menus { get; set; }
    public DbSet<ProductDto> Products { get; set; }
    public DbSet<DishDto> Dishes { get; set; }
    public DbSet<RecipeDto> Recipes { get; set; }
    public DbSet<UserDto> Users { get; set; }
    public DbSet<UserCredentialsDto> UserCredentials { get; set; }
    public DbSet<Session> Sessions{ get; set; }
}
