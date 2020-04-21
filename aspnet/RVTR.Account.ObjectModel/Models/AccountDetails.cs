namespace RVTR.Account.ObjectModel.Models
{
  public class AccountDetails
  {
    public string AccountDetailsID { get; set; } // TODO: hash instead of string
    public string AccountType { get; set; }
    public AccountRewards AccountRewards { get; set; }
  }
}
