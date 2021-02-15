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
        new AccountModel
        {
          EntityId = -1,
          Name = "David Dowd",
          Email = "ddowd97@gmail.com"
        },
        new AccountModel()
        {
          EntityId = 1,
          Name = "JonnyCode",
          Email = "jonsledge39@gmail.com"
        },
        new AccountModel()
        {
          EntityId = 2,
          Name = "Richard Noel",
          Email = "richard.noel@revature.net"
        },
        new AccountModel()
        {
          EntityId = 3,
          Name = "Mr. Sun",
          Email = "sunzh95@gmail.com"
        }
      );
      modelBuilder.Entity<PaymentModel>().HasData
      (
        new PaymentModel()
        {
          EntityId = -1,
          CardExpirationDate = new DateTime(),
          CardNumber = "1234123412341234",
          CardName = "Visa",
          SecurityCode = "123",
          AccountModelId = -1
        },
        new PaymentModel()
        {
          EntityId = 1,
          AccountModelId = 1,
          CardExpirationDate = new System.DateTime(2020, 08, 31),
          CardNumber = "4111111111111111",
          SecurityCode = "123",
          CardName = "User's credit card"
        },
        new PaymentModel()
        {
          EntityId = 2,
          AccountModelId = 2,
          CardExpirationDate = new System.DateTime(9999, 01, 01),
          CardNumber = "9999999999999999",
          SecurityCode = "999",
          CardName = "Richard's Trusty Card"
        },
        new PaymentModel()
        {
          EntityId = 3,
          AccountModelId = 3,
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
          EntityId = -1,
          City = "City",
          Country = "USA",
          PostalCode = "21345",
          StateProvince = "NC",
          Street = "123 elm street",
          AccountId = -1,
        },
        new AddressModel()
        {
          EntityId = 1,
          AccountId = 1,
          City = "Austin",
          Country = "USA",
          PostalCode = "73301",
          StateProvince = "TX",
          Street = "Test St"
        },
        new AddressModel()
        {
          EntityId = 2,
          AccountId = 2,
          City = "Seattle",
          Country = "USA",
          PostalCode = "65780",
          StateProvince = "WA",
          Street = "See Sharp St"
        },
        new AddressModel()
        {
          EntityId = 3,
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
          EntityId = -1,
          Email = "Test@test.com",
          FamilyName = "Dowd",
          GivenName = "David",
          Phone = "1234567891",
          Type = "Adult",
          AccountModelId = -1,
          ImageUri = "https://avataaars.io/?avatarStyle=Circle&topType=LongHairStraight&accessoriesType=Blank&hairColor=Platinum&facialHairType=BeardLight&facialHairColor=Platinum&clotheType=CollarSweater&clotheColor=PastelYellow&eyeType=Default&eyebrowType=Angry&mouthType=Twinkle&skinColor=Brown"
        },
        new ProfileModel()
        {
          EntityId = 1,
          AccountModelId = 1,
          Email = "demo.camper@revature.com",
          FamilyName = "Sledge",
          GivenName = "Jon",
          Phone = "123-456-7891",
          Type = "Child",
          ImageUri = "https://avataaars.io/?avatarStyle=Circle&topType=LongHairFrida&accessoriesType=Round&hatColor=Blue01&facialHairType=MoustacheMagnum&facialHairColor=Black&clotheType=BlazerSweater&clotheColor=Heather&eyeType=Side&eyebrowType=SadConcerned&mouthType=Serious&skinColor=Brown"
        },
        new ProfileModel()
        {
          EntityId = 2,
          AccountModelId = 2,
          Email = "random@email.com",
          FamilyName = "Noel",
          GivenName = "Richard",
          Phone = "123-456-7891",
          Type = "Adult",
          ImageUri = "https://avataaars.io/?avatarStyle=Circle&topType=LongHairCurly&accessoriesType=Prescription01&hairColor=Black&facialHairType=BeardMedium&facialHairColor=Auburn&clotheType=CollarSweater&clotheColor=White&eyeType=Close&eyebrowType=DefaultNatural&mouthType=Twinkle&skinColor=DarkBrown"
        },
        new ProfileModel()
        {
          EntityId = 3,
          AccountModelId = 3,
          Email = "anotherone@email.com",
          FamilyName = "Sun",
          GivenName = "Mr.",
          Phone = "123-456-7891",
          Type = "Adult",
          ImageUri = "https://avataaars.io/?avatarStyle=Circle&topType=ShortHairDreads01&accessoriesType=Prescription01&hairColor=BlondeGolden&facialHairType=MoustacheFancy&facialHairColor=Auburn&clotheType=BlazerSweater&clotheColor=Gray02&eyeType=Cry&eyebrowType=UnibrowNatural&mouthType=Smile&skinColor=Brown"
        }
      );
    }
  }
}