using RVTR.Account.ObjectModel.Models;
using System.Threading.Tasks;

namespace RVTR.Account.DataContext.Repositories
{
  public class PaymentRepository : GenericRepository<PaymentModel>
  {
    public PaymentRepository(AccountContext context) : base(context)
    {
    }
    /// <summary>
    /// Payment Delete() method to remove a payment from the database
    /// </summary>
    /// <param name="id">PaymentId</param>
    public async override Task Delete(int id)
    {
      PaymentModel ent = await Get(id);
      _context.Payments.Remove(ent);
    }
  }
}
