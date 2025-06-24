using System;
using Microsoft.EntityFrameworkCore;

namespace Domus.Models;

public class NpgSqlContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserCredentials> Users { get; set; }
}
