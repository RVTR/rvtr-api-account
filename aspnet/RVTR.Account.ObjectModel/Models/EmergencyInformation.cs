using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  public class EmergencyInformation: ContactInformation
  {
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Name is required.")]
    public string EmergencyContactName { get; set; }
    [Display(Name = "Relationship")]
    [Required(ErrorMessage = "Relationship is required.")]
    public string Relationship { get; set; }
  }
}
