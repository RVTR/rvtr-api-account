using System.ComponentModel.DataAnnotations;

using System.Security.Cryptography;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models 
{
  /// <summary>
  /// References Profile and Account details to display all data related to one Account.
  /// </summary>
  
  public class AccountModel 
  {
    [Key]
    public string AccountID { get => AccountID ; set{
      AccountID = Hash.hash(value);
    } } 
    public Profile[] Profiles { get; set; } // Multiple profiles can be associated with one account, such as wife and kids all on one bill
    public AccountDetails AccountDetails { get; set; }


  }
}
