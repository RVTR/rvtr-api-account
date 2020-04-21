namespace RVTR.Account.ObjectModel.Models 
{
  public class Profile
  {
    public string ProfileID { get; set; } // TODO: hash instead of string
    public string AccountRole { get; set; }
    public string ProfilePicture { get; set; } // URI to profile picture
    public ContactInformation ContactInformation { get; set; }
    public Address Address { get; set; }
    public Payment Payment { get; set; }
    public Name Name { get; set; }
    public EmergencyInformation EmergencyContact { get; set; }
  }
}