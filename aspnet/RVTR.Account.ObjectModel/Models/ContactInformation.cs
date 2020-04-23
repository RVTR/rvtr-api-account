using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
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
  }
}
