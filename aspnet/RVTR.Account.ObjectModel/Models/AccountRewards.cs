namespace RVTR.Account.ObjectModel.Models
{
  public class AccountRewards
  {
    public string accountRewardsID { get; set; } //should be hash
    public string rewardsStatus { get; set; }
    public int rewardsPoints { get; set; }
  }
}
