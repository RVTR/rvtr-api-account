using System.Net.Http;
using System.Threading.Tasks;

namespace RVTR.Account.Domain.Validators
{
  public class ValidatorSwitch
  {
    private static HttpClient client = new HttpClient();

    public static async Task<bool> validate(string address, int option)
    {
      switch(option){
      case 0:
        return await AddressValidator.getValidation(address, client);
      default:
        break;
      }
       return false;
    }
  } 
}