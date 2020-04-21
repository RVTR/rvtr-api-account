namespace RVTR.Account.ObjectModel.Models
{
  public class Payment
  {
    public string CardholderName { get; set; }
    public string PaymentType { get; set; }
    public string ExpirationDate { get; set; } // TODO: Datetime object instead of string
    public string CardType { get; set; }
    public string CardNumber { get; set; } // TODO: hash instead of string
    public string SecurityNumber { get; set; }// TODO: hash instead of string
    public string BillingAddress { get; set; }
  }
}
