using System.ComponentModel.DataAnnotations;

// using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  public class ContactInformation 
  {
    public string ContactInformationID { get => ContactInformationID ; set{
      ContactInformationID = Hash.hash(value);
    } } 
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required.")]
    public string Email { get; set; }
    [Display(Name = "Phone number")]
    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; }
  }
}
