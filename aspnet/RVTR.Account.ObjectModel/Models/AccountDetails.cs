using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Account Details [Considering Merge with AccountModel.cs...]
  /// </summary>
  public class AccountDetails 
  {
    [Key]
    public string AccountDetailsID { get => AccountDetailsID ; set{
      AccountDetailsID = Hash.hash(value);
    } } 
    [Display(Name = "Account type")]
    [Required(ErrorMessage = "Account type is required.")]
    public string AccountType { get; set; }
    public AccountRewards AccountRewards { get; set; }

    #region NAVIGATIONAL PROPERTIES
    public AccountModel AccountModel { get; set; }

    #endregion

  }
}
