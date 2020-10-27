using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : IValidatableObject
  {
    public int Id { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public string StateProvince { get; set; }

    public string Street { get; set; }

    public int AccountId { get; set; }

    public AccountModel Account { get; set; }

    /// <summary>
    /// Represents the _Address_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(City))
      {
        yield return new ValidationResult("City cannot be null.");
      }
      if (string.IsNullOrEmpty(Country))
      {
        yield return new ValidationResult("Country cannot be null.");
      }
      else if (Country != "USA" && Country != "US")
      {
        yield return new ValidationResult("Address must be in the United States");
      }
      if (string.IsNullOrEmpty(PostalCode))
      {
        yield return new ValidationResult("PostalCode cannot be null.");
      }
      if (string.IsNullOrEmpty(StateProvince))
      {
        yield return new ValidationResult("StateProvince cannot be null.");
      }
      if (string.IsNullOrEmpty(Street))
      {
        yield return new ValidationResult("Street cannot be null.");
      }
    }
  }
}
