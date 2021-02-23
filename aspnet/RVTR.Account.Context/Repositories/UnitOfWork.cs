using System.Threading.Tasks;
using RVTR.Account.Domain.Interfaces;
using RVTR.Account.Domain.Models;

namespace RVTR.Account.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AccountContext _context;

    public IRepository<AccountModel> Account { get; }
    public IRepository<ProfileModel> Profile { get; }
    public IRepository<AddressModel> Address { get; }
    public IRepository<PaymentModel> Payment { get; }

    public UnitOfWork(AccountContext context)
    {
      _context = context;

      Account = new Repository<AccountModel>(context);
      Profile = new Repository<ProfileModel>(context);
      Address = new Repository<AddressModel>(context);
      Payment = new Repository<PaymentModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
