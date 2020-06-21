using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using RVTR.Account.DataContext.Repositories;

namespace RVTR.Account.WebRpc
{
  public class AccountService : Account.AccountBase
  {
    //private readonly ILogger<AccountService> _logger;
    //private readonly UnitOfWork _unitOfWork;

    public AccountService(/*ILogger<AccountService> logger, UnitOfWork unitOfWork*/)
    {
     // _logger = logger;
     // _unitOfWork = unitOfWork;
    }

    public override Task<AccountResponse> Book(AccountRequest request, ServerCallContext context)
    {
      return Task.FromResult(new AccountResponse()
      {
        Message = "Hello " + request.Name
      });
    }
  }
}
