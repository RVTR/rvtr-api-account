using System.Net.Http;
using System.Threading.Tasks;

namespace RVTR.Account.Domain.Interfaces
{
  public interface IValidator
  {
    internal static async Task<bool> getValidation(string input)
    {
      await Task.Run(() => false);
      return false;
    }
  }
}