using System.Collections.Generic;
using RVTR.Account.Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.Domain.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : AEntity, IValidatableObject
  {

    [Required(ErrorMessage = "City is required")]
    [MaxLength(50, ErrorMessage = "Name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [RegularExpression(@"(^USA$)|(^US$)", ErrorMessage = "Country must be either US or USA")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Postal code is required")]
    [StringLength(5, ErrorMessage = "Postal code must be 5 numbers long")]
    [RegularExpression(@"\d{5}", ErrorMessage = "Postal code must be a number")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "State is required")]
    [StringLength(2, ErrorMessage = "State must be 2 characters long")]
    [RegularExpression(@"[A-Z]{2}", ErrorMessage = "State must be abbreviated properly.")]
    public string StateProvince { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [MaxLength(100, ErrorMessage = "Street name is too long.")]
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

      if(!ValidatorSwitch.validate(AddressBuilder(), 0).Result)
      {
        yield return new ValidationResult("Address is not valid");
      }

    }

    private string AddressBuilder()
    {
      return $"{Street}%20{City},%20{StateProvince}%20{Country}%20{PostalCode}";
    }
  }
}
