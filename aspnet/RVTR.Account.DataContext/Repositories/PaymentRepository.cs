using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Account.DataContext.Repositories
{
  public class PaymentRepository : GenericRepository<PaymentModel>
  {
    public PaymentRepository(AccountContext context) : base(context)
    {
    }

    public async override Task Delete(int id)
    {
      PaymentModel ent = await Get(id);
      _context.Payments.Remove(ent);
    }
  }
}
