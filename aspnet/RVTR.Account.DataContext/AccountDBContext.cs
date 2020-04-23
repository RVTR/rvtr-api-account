using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext
{
  public class AccountDbContext: DbContext
  {
    public AccountDbContext(DbContextOptions options) : base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }
    }
    public DbSet<AccountDetails> AccountDetails { get; set; }
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<AccountRewards> AccountRewards { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ContactInformation> ContactInformation { get; set; }
    public DbSet<EmergencyInformation> EmergencyContactInformation { get; set; }
    public DbSet<Name> Names { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder) 
    {
      // Set keys
      builder.Entity<AccountDetails>().HasKey(x => x.AccountDetailsID);
      builder.Entity<AccountModel>().HasKey(x => x.AccountID);
      builder.Entity<AccountRewards>().HasKey(x => x.AccountRewardsID);
      builder.Entity<Address>().HasKey(x => x.AddressID);
      builder.Entity<ContactInformation>().HasKey(x => x.ContactInformationID);
      builder.Entity<EmergencyInformation>().HasKey(x => x.EmergencyInformationID);
      builder.Entity<Name>().HasKey(x => x.NameID);
      builder.Entity<Payment>().HasKey(x => x.PaymentID);
      builder.Entity<Profile>().HasKey(x => x.ProfileID);

      // Define entity relationships
      builder.Entity<AccountModel>().HasMany(x => x.Profiles).WithOne(x => x.AccountModel);
      builder.Entity<AccountModel>().HasOne(x => x.AccountDetails).WithOne(x => x.AccountModel);
      builder.Entity<AccountDetails>().HasOne(x => x.AccountRewards).WithOne(x => x.AccountDetails);
      builder.Entity<Profile>().HasOne(x => x.Name).WithOne(x => x.Profile);
      builder.Entity<Profile>().HasOne(x => x.Address).WithOne(x => x.Profile);
      builder.Entity<Profile>().HasOne(x => x.Payment).WithOne(x => x.Profile);
      builder.Entity<Profile>().HasOne(x => x.ContactInformation).WithOne(x => x.Profile);
      builder.Entity<Profile>().HasOne(x => x.EmergencyInformation).WithOne(x => x.Profile);
    }
    
    // Input dummy data
    // builder.Entity<UserModel>().HasData(new UserModel[]
    // {
    //     new AccountModel() { UserName = "Alex1234" },
    // });
  }
}