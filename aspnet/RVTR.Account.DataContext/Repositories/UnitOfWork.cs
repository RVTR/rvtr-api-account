using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Interface;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AccountContext _context;

    public UnitOfWork(AccountContext context)
    {
      _context = context;
    }

    private IRepository<AccountModel> accountRepository;
    public virtual IRepository<AccountModel> AccountRepository
    {
      get
      {
        if (accountRepository == null)
          accountRepository = new AccountRepository(_context);

        return accountRepository;
      }
    }
    private IRepository<PaymentModel> paymentRepository;
    public virtual IRepository<PaymentModel> PaymentRepository
    {
      get
      {
        if (paymentRepository == null)
          paymentRepository = new PaymentRepository(_context);

        return paymentRepository;
      }
    }

    private IRepository<ProfileModel> profileRepository;
    public virtual IRepository<ProfileModel> ProfileRepository
    {
      get
      {
        if (profileRepository == null)
          profileRepository = new ProfileRepository(_context);

        return profileRepository;
      }
    }

    public async Task Complete() => await _context.SaveChangesAsync();
    }
  }
