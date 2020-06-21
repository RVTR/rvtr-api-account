using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Name_ model
  /// </summary>
  [Table("Names")]
  public class NameModel : IValidatableObject
  {
    public int Id { get; set; }

    public string Family { get; set; }

    public string Given { get; set; }

    public int? ProfileId { get; set; }

    public virtual ProfileModel Profile { get; set; }

    /// <summary>
    /// Represents the _Name_ `Validate` method
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
