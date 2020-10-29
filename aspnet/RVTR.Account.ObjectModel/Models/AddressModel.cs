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

    [Required]
    [MinLength(1, ErrorMessage = "Name must be at least one character.")]
    [MaxLength(50, ErrorMessage = "Name must be fewer than 50 characters.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Name must start with a capital letter and only use letters.")]
    public string City { get; set; }

    [Required]
    [RegularExpression(@"(USA)|(US)")]
    public string Country { get; set; }

    [Required]
    [StringLength(5, ErrorMessage = "Postal code must be 5 numbers long")]
    [RegularExpression(@"[0-9]{5}", ErrorMessage ="Postal code must be a number")]
    public string PostalCode { get; set; }

    [Required]
    [StringLength(2, ErrorMessage = "State must be 2 characters long")]
    [RegularExpression(@"[A-Z]{2}", ErrorMessage = "State must be abbreviated properly.")]
    public string StateProvince { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(50, ErrorMessage="Street name is too long.")]
    public string Street { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Required]
    public AccountModel Account { get; set; }

    /// <summary>
    /// Empty constructor
    /// </summary>
    public AddressModel() { }

    /// <summary>
    /// Constructor for all the required properties
    /// </summary>
    /// <param name="city"></param>
    /// <param name="country"></param>
    /// <param name="postalCode"></param>
    /// <param name="state"></param>
    /// <param name="street"></param>
    /// <param name="account"></param>
    public AddressModel(string city, string country, string postalCode, string state, string street, AccountModel account)
    {
      City = city;
      Country = country;
      PostalCode = postalCode;
      StateProvince = state;
      Street = street;
      Account = account;
    }


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
