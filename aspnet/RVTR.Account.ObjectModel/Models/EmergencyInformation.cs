using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  /// <summary>
  /// In case of user Incident, provides information on someone to be contacted.
  /// </summary>
  public class EmergencyInformation : ContactInformation
  {
    [Key]
    public string EmergencyInformationID { get => EmergencyInformationID ; set{
      EmergencyInformationID = Hash.hash(value);
    } } 
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Name is required.")]
    public string EmergencyContactName { get; set; }
    [Display(Name = "Relationship")]
    [Required(ErrorMessage = "Relationship is required.")]
    public string Relationship { get; set; }
  }
}
