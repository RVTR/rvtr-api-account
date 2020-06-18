using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;
using System.Linq;

namespace RVTR.Account.DataContext
{
  /// <summary>
  /// Represents the _Account_ context
  /// </summary>
  public class AccountContext : DbContext
  {
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }
    //public DbSet<PaymentModel> Payments { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

    public AccountContext() { }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      if (!options.IsConfigured)
      {
        options.UseNpgsql("Host=localhost;Database=AccountDb;Username=postgres;Password=abc123");
      }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
      {
        foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
      }
/*      modelBuilder.Entity<AccountModel>().HasKey(e => e.Id);
      modelBuilder.Entity<AddressModel>().HasKey(e => e.Id);
      modelBuilder.Entity<BankCardModel>().HasKey(e => e.Id);
      modelBuilder.Entity<NameModel>().HasKey(e => e.Id);
      modelBuilder.Entity<PaymentModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ProfileModel>().HasMany(e => e.Name)
    .WithOne(e => e.)
    .OnDelete(DeleteBehavior.ClientCascade); ;
*/    }
  }
}
