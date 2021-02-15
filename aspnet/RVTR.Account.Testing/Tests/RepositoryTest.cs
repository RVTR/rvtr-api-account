using Microsoft.EntityFrameworkCore;
using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class RepositoryTest : DataTest
  {
    private readonly AccountModel _account = new AccountModel() { EntityId = 3 };
    private readonly ProfileModel _profile = new ProfileModel() { FamilyName = "FN", GivenName = "GN", EntityId = 3, Email = "anemail@random.com", Phone = "123456789", Type = "" };
    private readonly AddressModel _address = new AddressModel() { EntityId = 3, AccountId = 3 };

    [Theory]
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

    [Theory]
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

    [Theory]
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
    public async void Test_Repository_SelectAsync_ById()
    {
      using (var ctx = new AccountContext(Options))
      {
        var accounts = new Repository<AccountModel>(ctx);

        var actual = await accounts.SelectAsync(1);

        Assert.NotNull(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);

        var actual = await profiles.SelectAsync(1);

        Assert.NotNull(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);

        var actual = await addresses.SelectAsync(1);

        Assert.NotNull(actual);
      }
    }

    [Theory]
    public async void Test_Repository_Update()
    {
      using (var ctx = new AccountContext(Options))
      {
        var accounts = new Repository<AccountModel>(ctx);
        var account = await ctx.Accounts.FirstAsync();

        account.Name = "name";
        accounts.Update(account);

        var result = ctx.Accounts.Find(account.EntityId);
        Assert.Equal(account.Name, result.Name);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        var profile = await ctx.Profiles.FirstAsync();

        profile.Email = "email";
        profiles.Update(profile);

        var result = ctx.Profiles.Find(profile.EntityId);
        Assert.Equal(profile.Email, result.Email);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        var address = await ctx.Addresses.FirstAsync();

        address.City = "Denver";
        addresses.Update(address);

        var result = ctx.Addresses.Find(address.EntityId);
        Assert.Equal(address.City, result.City);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }
    }
  }
}
