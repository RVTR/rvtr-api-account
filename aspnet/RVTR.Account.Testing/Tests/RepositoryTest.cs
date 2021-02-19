using Microsoft.EntityFrameworkCore;
using RVTR.Account.Context;
using RVTR.Account.Context.Repositories;
using RVTR.Account.Domain.Models;
using Xunit;

namespace RVTR.Account.Testing.Tests
{
  public class RepositoryTest : DataTest
  {
    private readonly AccountModel _account = new AccountModel() { EntityId = 1, Email = "ddowd97@gmail.com" };
    private readonly ProfileModel _profile = new ProfileModel() { FamilyName = "Dowd", GivenName = "David", EntityId = 1, Email = "ddowd97@gmail.com", Phone = "123456789", Type = "Adult" };
    private readonly AddressModel _address = new AddressModel() { EntityId = 1, AccountId = 1 };

    [Fact]
    public async void Test_Repository_Delete()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var profile = await ctx.Profiles.FirstAsync();
        await _unit.Delete<ProfileModel>(profile);
        Assert.Equal(EntityState.Deleted, ctx.Entry(profile).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var profile = await ctx.Accounts.FirstAsync();
        await _unit.Delete<AccountModel>(profile);
        Assert.Equal(EntityState.Deleted, ctx.Entry(profile).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var profile = await ctx.Addresses.FirstAsync();
        await _unit.Delete<AddressModel>(profile);
        Assert.Equal(EntityState.Deleted, ctx.Entry(profile).State);
      }
    }

    [Fact]
    public async void Test_Repository_Insert()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        await _unit.Insert(_account);
        Assert.Equal(EntityState.Added, ctx.Entry(_account).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        await _unit.Insert(_profile);
        Assert.Equal(EntityState.Added, ctx.Entry(_profile).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        await _unit.Insert(_address);
        Assert.Equal(EntityState.Added, ctx.Entry(_address).State);
      }
    }

    [Fact]
    public async void Test_Repository_GetAll()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.GetAll<AccountModel>();
        Assert.NotEmpty(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.GetAll<ProfileModel>();
        Assert.NotEmpty(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.GetAll<AddressModel>();
        Assert.NotEmpty(actual);
      }
    }

    [Fact]
    public async void Test_Repository_Get()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.Get<AddressModel>(1);
        Assert.NotNull(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.Get<ProfileModel>(1);
        Assert.NotNull(actual);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.Get<AccountModel>(1);
        Assert.NotNull(actual);
      }
    }

    [Fact]
    public async void Test_Repository_Update()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var account = await ctx.Accounts.FirstAsync();

        account.Name = "name";
        await _unit.Update(account);

        var result = ctx.Accounts.Find(account.EntityId);
        Assert.Equal(account.Name, result.Name);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var profile = await ctx.Profiles.FirstAsync();

        profile.Email = "email";
        await _unit.Update(profile);

        var result = ctx.Profiles.Find(profile.EntityId);
        Assert.Equal(profile.Email, result.Email);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var address = await ctx.Addresses.FirstAsync();

        address.City = "Denver";
        await _unit.Update(address);

        var result = ctx.Addresses.Find(address.EntityId);
        Assert.Equal(address.City, result.City);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }
    }

    [Fact]
    public async void Test_CommitAsync()
    {
      using (var ctx = new AccountContext(Options))
      {
        UnitOfWork _unit = new UnitOfWork(ctx);
        var actual = await _unit.CommitAsync();
        Assert.Equal(0, actual);
      }
    }
  }
}
