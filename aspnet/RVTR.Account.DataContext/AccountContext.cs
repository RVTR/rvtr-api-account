using System;
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
        new AccountModel
        {
          Id = -1,
          Name = "David Dowd",
          Email = "ddowd97@gmail.com"
        },
        new AccountModel()
        {
          Id = 1,
          Name = "JonnyCode",
          Email = "jonsledge39@gmail.com"
        },
        new AccountModel()
        {
          Id = 2,
          Name = "Richard Noel",
          Email = "richard.noel@revature.net"
        },
        new AccountModel()
        {
          Id = 3,
          Name = "Mr. Sun",
          Email = "sunzh95@gmail.com"
        }
      );
      modelBuilder.Entity<PaymentModel>().HasData
      (
        new PaymentModel()
        {
          Id = -1,
          CardExpirationDate = new DateTime(),
          CardNumber = "1234123412341234",
          CardName = "Visa",
          SecurityCode = "123",
          AccountId = -1
        },
        new PaymentModel()
        {
          Id = 1,
          AccountId = 1,
          CardExpirationDate = new System.DateTime(2020, 08, 31),
          CardNumber = "4111111111111111",
          SecurityCode = "123",
          CardName = "User's credit card"
        },
        new PaymentModel()
        {
          Id = 2,
          AccountId = 2,
          CardExpirationDate = new System.DateTime(9999, 01, 01),
          CardNumber = "9999999999999999",
          SecurityCode = "999",
          CardName = "Richard's Trusty Card"
        },
        new PaymentModel()
        {
          Id = 3,
          AccountId = 3,
          CardExpirationDate = new System.DateTime(2020, 12, 01),
          CardNumber = "1234567887654321",
          SecurityCode = "010",
          CardName = "Sun's Credit Card"
        }
      );
      modelBuilder.Entity<AddressModel>().HasData
      (
        new AddressModel()
        {
          Id = -1,
          City = "City",
          Country = "USA",
          PostalCode = "21345",
          StateProvince = "NC",
          Street = "123 elm street",
          AccountId = -1,
        },
        new AddressModel()
        {
          Id = 1,
          AccountId = 1,
          City = "Austin",
          Country = "USA",
          PostalCode = "73301",
          StateProvince = "TX",
          Street = "Test St"
        },
        new AddressModel()
        {
          Id = 2,
          AccountId = 2,
          City = "Seattle",
          Country = "USA",
          PostalCode = "65780",
          StateProvince = "WA",
          Street = "See Sharp St"
        },
        new AddressModel()
        {
          Id = 3,
          AccountId = 3,
          City = "West Lafayette",
          Country = "USA",
          PostalCode = "47906",
          StateProvince = "IN",
          Street = "272 Littleton St"
        }
      );
      modelBuilder.Entity<ProfileModel>().HasData
      (
        new ProfileModel()
        {
          Id = -1,
          Email = "Test@test.com",
          FamilyName = "Jones",
          GivenName = "Tom",
          Phone = "1234567891",
          Type = "Adult",
          AccountId = -1
        },
        new ProfileModel()
        {
          Id = 1,
          AccountId = 1,
          Email = "demo.camper@revature.com",
          FamilyName = "FamilyName",
          GivenName = "GivenName",
          Phone = "123-456-7891",
          Type = "Child"
        },
        new ProfileModel()
        {
          Id = 2,
          AccountId = 2,
          Email = "random@email.com",
          FamilyName = "FamilyName",
          GivenName = "GivenName",
          Phone = "123-456-7891",
          Type = "Adult"
        },
        new ProfileModel()
        {
          Id = 3,
          AccountId = 3,
          Email = "anotherone@email.com",
          FamilyName = "FamilyName",
          GivenName = "GivenName",
          Phone = "123-456-7891",
          Type = "Adult"
        }
      );
    }
  }
}
