using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet <User> Users {  get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<User>(entity =>
    //    {
    //        entity.HasKey(u => u.Id);
    //        entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
    //        entity.Property(u => u.Age).HasMaxLength(100).IsRequired();
    //        entity.Property(u => u.State).HasMaxLength(256).IsRequired();
    //        entity.Property(u => u.Pincode).HasMaxLength(100).IsRequired();
    //    });
    //}
}
