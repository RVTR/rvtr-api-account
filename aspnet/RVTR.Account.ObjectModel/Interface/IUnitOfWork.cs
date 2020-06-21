using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Account.ObjectModel.Interface
{
  /// <summary>
  /// Methods and properties that needs to be defined in the UnitOfWork class
  /// </summary>
  public interface IUnitOfWork
  {
    IRepository<AccountModel> AccountRepository { get; }
    IRepository<PaymentModel> PaymentRepository { get; }
    IRepository<ProfileModel> ProfileRepository { get; }

    Task Complete();
  }
}
