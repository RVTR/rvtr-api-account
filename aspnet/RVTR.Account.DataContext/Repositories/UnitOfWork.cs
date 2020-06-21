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
    /// <summary>
    /// Adds AccountRepository to the Unit of Work
    /// </summary>
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
    /// <summary>
    /// Adds PaymentRepository to the Unit of Work
    /// </summary>
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
    /// <summary>
    /// Adds ProfileRepository to the Unit of Work
    /// </summary>
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
    /// <summary>
    /// Complete() method to save any changes made to the database
    /// </summary>
    public async Task Complete() => await _context.SaveChangesAsync();
    }
  }
