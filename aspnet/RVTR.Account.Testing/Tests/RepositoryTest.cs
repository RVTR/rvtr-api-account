using Microsoft.EntityFrameworkCore;
using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class RepositoryTest : DataTest
  {
    private readonly AccountModel _account = new AccountModel() { EntityId = 1, Email = "jsmith@gmail.com" };
    private readonly ProfileModel _profile = new ProfileModel() { FamilyName = "Smith", GivenName = "John", EntityId = 1, Email = "jsmith@gmail.com", Phone = "123456789", Type = "Adult" };
    private readonly AddressModel _address = new AddressModel() { EntityId = 1, AccountId = 1 };

    [Fact]
    public async void Test_Repository_DeleteAsync()
    {
      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        var profile = await ctx.Profiles.FirstAsync();
        await profiles.DeleteAsync(profile.EntityId);
        Assert.Equal(EntityState.Deleted, ctx.Entry(profile).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        var address = await ctx.Addresses.FirstAsync();
        await addresses.DeleteAsync(address.EntityId);
        Assert.Equal(EntityState.Deleted, ctx.Entry(address).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var accounts = new Repository<AccountModel>(ctx);
        var account = await ctx.Accounts.FirstAsync();
        await accounts.DeleteAsync(account.EntityId);
        Assert.Equal(EntityState.Deleted, ctx.Entry(account).State);
      }
    }

    [Fact]
    public async void Test_Repository_InsertAsync()
    {
      using (var ctx = new AccountContext(Options))
      {
        var accounts = new Repository<AccountModel>(ctx);
        await accounts.InsertAsync(_account);
        Assert.Equal(EntityState.Added, ctx.Entry(_account).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        await profiles.InsertAsync(_profile);
        Assert.Equal(EntityState.Added, ctx.Entry(_profile).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        await addresses.InsertAsync(_address);
        Assert.Equal(EntityState.Added, ctx.Entry(_address).State);
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using (var ctx = new AccountContext(Options))
      {
        var accounts = new Repository<AccountModel>(ctx);

        var actual = await accounts.SelectAsync();

        Assert.NotEmpty(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);

        var actual = await profiles.SelectAsync();

        Assert.NotEmpty(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);

        var actual = await addresses.SelectAsync();

        Assert.NotEmpty(actual);
      }
    }

    [Theory]
    [InlineData(1)]
    public async void Test_Repository_SelectAsync_ById(int id)
    {
      using (var ctx = new AccountContext(Options))
      {
        var accounts = new Repository<AccountModel>(ctx);

        var actual = await accounts.SelectAsync(e => e.EntityId == id);

        Assert.NotNull(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);

        var actual = await profiles.SelectAsync(e => e.EntityId == id);

        Assert.NotNull(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);

        var actual = await addresses.SelectAsync(e => e.EntityId == id);

        Assert.NotNull(actual);
      }
    }

    [Theory]
    [InlineData("email", "Denver")]
    public async void Test_Repository_Update(string email, string city)
    {

      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        var profile = await ctx.Profiles.FirstAsync();

        profile.Email = email;
        profiles.Update(profile);

        var result = ctx.Profiles.Find(profile.EntityId);
        Assert.Equal(profile.Email, result.Email);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        var address = await ctx.Addresses.FirstAsync();

        address.City = city;
        addresses.Update(address);

        var result = ctx.Addresses.Find(address.EntityId);
        Assert.Equal(address.City, result.City);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }
    }
  }
}
