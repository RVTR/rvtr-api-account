using System.ComponentModel.DataAnnotations;

// using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  public class AccountDetails 
  {
    public string AccountDetailsID { get => AccountDetailsID ; set{
      AccountDetailsID = Hash.hash(value);
    } } 
    public string AccountType { get; set; }
    public AccountRewards AccountRewards { get; set; }
  }
}
