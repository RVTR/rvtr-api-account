using System.ComponentModel.DataAnnotations;

// using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models 
{
  public class Profile 
  {
    public string ProfileID { get => ProfileID ; set{
      ProfileID = Hash.hash(value);
    } } 
    public string AccountRole { get; set; }
    public string ProfilePicture { get; set; } // URI to profile picture
    public ContactInformation ContactInformation { get; set; }
    public Address Address { get; set; }
    public Payment Payment { get; set; }
    public Name Name { get; set; }
    public EmergencyInformation EmergencyContact { get; set; }
  }
}