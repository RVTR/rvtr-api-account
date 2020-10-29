using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Payment_ model
  /// </summary>
  public class PaymentModel : IValidatableObject
  {
    public int Id { get; set; }

    [Required]
    public DateTime CardExpirationDate { get; set; }

    [Required]
    [StringLength(19)]
    [RegularExpression(@"[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}", ErrorMessage = "Credit card must be properly formatted")]
    public string CardNumber { get; set; }

    [Required]
    [StringLength(3)]
    [RegularExpression(@"[0-9]{3}", ErrorMessage = "Security code must be properly formatted")]
    public string SecurityCode { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string CardName { get; set; }

    public int AccountId { get; set; }

    [Required]
    public AccountModel Account { get; set; }

    /// <summary>
    /// Empty constructor
    /// </summary>
    public PaymentModel() { }

    /// <summary>
    /// Constructor instantiating all the required properties
    /// </summary>
    /// <param name="expirationDate"></param>
    /// <param name="cardNumber"></param>
    /// <param name="securityCode"></param>
    /// <param name="cardName"></param>
    /// <param name="account"></param>
    public PaymentModel(DateTime expirationDate, string cardNumber, string securityCode, string cardName, AccountModel account)
    {
      CardExpirationDate = expirationDate;
      CardNumber = cardNumber;
      SecurityCode = securityCode;
      CardName = cardName;
      Account = account;
    }


    /// <summary>
    /// Represents the _Payment_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(CardName))
      {
        yield return new ValidationResult("cardName cannot be null.");
      }
      if (string.IsNullOrEmpty(CardNumber))
      {
        yield return new ValidationResult("cardNumber cannot be null.");
      }
      if (string.IsNullOrEmpty(SecurityCode))
      {
        yield return new ValidationResult("Security code cannot be null.");
      }
      if (CardExpirationDate == null)
      {
        yield return new ValidationResult("Expiration date cannot be null.");
      }
    }
  }
}
