using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Payment_ model
  /// </summary>
  [Table("Payments")]
  public class PaymentModel : IValidatableObject
  {
    [ForeignKey("NameModel")]
    public int Id { get; set; }

    public DateTime CardExpirationDate { get; set; }

    public string CardNumber { get; set; }

    public string CardName { get; set; }

    public int? AccountId { get; set; }
    public virtual AccountModel Account { get; set; }

    /// <summary>
    /// Represents the _Payment_ `Validate` method
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
