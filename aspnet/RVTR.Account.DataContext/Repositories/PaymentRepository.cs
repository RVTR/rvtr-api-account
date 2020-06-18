using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVTR.Account.DataContext.Repositories
{
  public class PaymentRepository : GenericRepository<PaymentModel>
  {
    public PaymentRepository(AccountContext context) : base(context)
    {
    }
  }
}
