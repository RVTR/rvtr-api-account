using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// Data for Communication with user including Email, and Phone Number.
  /// </summary>
  public class ContactInformation 
  {
    [Key]
    public string ContactInformationID { get => ContactInformationID ; set{
      ContactInformationID = Hash.hash(value);
    } } 
    [Display(Name = "Email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email address is required.")]
    public string Email { get; set; }
    [Display(Name = "Phone number")]
    [MaxLength(20)]
    [MinLength(10)]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric.")]
    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; }

    #region NAVIGATIONAL PROPERTIES
    
    public Profile Profile { get; set; }

    public EmergencyInformation EmergencyInformation { get; set; }

    #endregion
  }
}
