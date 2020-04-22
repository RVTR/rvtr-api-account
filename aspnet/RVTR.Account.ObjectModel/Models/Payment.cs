using System;
using System.ComponentModel.DataAnnotations;

// using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  public class Payment 
  {
    public string PaymentID { get => PaymentID ; set{
      PaymentID = Hash.hash(value);
    } } 
    [Required(ErrorMessage = "Carholder's name is required.")]
    public string CardholderName { get; set; }
    [Required(ErrorMessage = "Payment Type is required.")]
    public string PaymentType { get; set; }
    [Required(ErrorMessage = "Expiration date is required.")]
    public DateTime ExpirationDate { get; set; }
    [Required(ErrorMessage = "Card type is required.")]
    public string CardType { get; set; }
    [Required(ErrorMessage = "Card number is required.")]
    public string CardNumber { get => CardNumber ; set{
      CardNumber = Hash.hash(value);
    } } 
    [Required(ErrorMessage = "Security number is required.")]
    public string SecurityNumber { get => SecurityNumber ; set{
      SecurityNumber = Hash.hash(value);
    } } 
    [Required(ErrorMessage = "Billing address is required.")]
    public Address BillingAddress { get; set; }
  }
}
