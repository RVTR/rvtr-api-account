using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models 
{
  public class Profile 
  {
    [Key]
    public string ProfileID { get => ProfileID ; set{
      ProfileID = Hash.hash(value);
    } } 
    
    [Required(ErrorMessage = "Account role is required.")]
    public string AccountRole { get; set; }
    public string ProfilePicture { get; set; } // URI to profile picture
    public ContactInformation ContactInformation { get; set; }
    public Address Address { get; set; }
    public Payment Payment { get; set; }
    public Name Name { get; set; }
    public EmergencyInformation EmergencyContact { get; set; }
  }
}