using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext
{
  /// <summary>
  /// Represents the _Account_ context
  /// </summary>
  public class AccountContext : DbContext
  {
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }
    public DbSet<PaymentModel> Payments { get; set; }
    public DbSet<AddressModel> Addresses { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AccountModel>().HasKey(e => e.Id);
      modelBuilder.Entity<AddressModel>().HasKey(e => e.Id);
      modelBuilder.Entity<PaymentModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ProfileModel>().HasKey(e => e.Id);
      modelBuilder.Entity<AccountModel>().HasData
      (
        new AccountModel(){
          Id = 1,
          Email = "demo.camper@revature.com",
          Name = "camper"
        }
      );
      modelBuilder.Entity<PaymentModel>().HasData
      (
        new PaymentModel(){
          Id = 1,
          AccountId = 1,
          cardExpirationDate = new System.DateTime(2020,08,31),
          cardNumber = "4111111111111111",
          securityCode = "123",
          cardName = "user's credit card"
        }
      );
      modelBuilder.Entity<AddressModel>().HasData
      (
        new AddressModel(){
          Id = 1,
          AccountId = 1,
          City = "Austin",
          Country = "USA",
          PostalCode = "73301",
          StateProvince = "Texas",
          Street = "Test St"
        }
      );
      modelBuilder.Entity<ProfileModel>().HasData
      (
        new ProfileModel(){
          Id = 1,
          AccountId = 1,
          Email = "demo.camper@revature.com",
          familyName = "familyName",
          givenName = "givenName",
          Phone = "123-456-7891",
          Type = ""
        }
      );

      
    }
  }
}
