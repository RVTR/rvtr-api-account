using System;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Domain.Models;

namespace RVTR.Account.Context
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
      modelBuilder.Entity<AccountModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<AddressModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<PaymentModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<ProfileModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<AccountModel>().HasData
      (
        new AccountModel()
        {
          EntityId = 1,
          Email = "jsmith@gmail.com"
        }
      );
      modelBuilder.Entity<PaymentModel>().HasData
      (
        new PaymentModel()
        {
          EntityId = 1,
          CardExpirationDate = new DateTime(),
          CardNumber = "1234123412341234",
          CardName = "Visa",
          SecurityCode = "123",
          AccountModelId = 1
        }
      );
      modelBuilder.Entity<AddressModel>().HasData
      (
        new AddressModel()
        {
          EntityId = 1,
          AccountId = 1,
          City = "Austin",
          Country = "USA",
          PostalCode = "73301",
          StateProvince = "TX",
          Street = "101 Blume Avenue"
        }
      );
      modelBuilder.Entity<ProfileModel>().HasData
      (
        new ProfileModel()
        {
          EntityId = 1,
          AccountModelId = 1,
          GivenName = "John",
          FamilyName = "Smith",
          Email = "jsmith@gmail.com",
          Phone = "123445679",
          Type = "Adult"
        }
      );
    }
  }
}