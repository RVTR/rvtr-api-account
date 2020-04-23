using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Contains Reward information for related Account.
  /// </summary>
  public class AccountRewards 
  {
    [Key]
    public string AccountRewardsID { get => AccountRewardsID ; set{
      AccountRewardsID = Hash.hash(value);
    } } 
    public string RewardsStatus { get; set; }
    public int RewardsPoints { get; set; }

    #region NAVIGATIONAL PROPERTIES
    public AccountDetails AccountDetails { get; set; }

    #endregion
  }
}
