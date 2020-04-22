using System;
using System.ComponentModel.DataAnnotations;

// using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  public class Name 
  {
    public string NameID { get => NameID ; set{
      NameID = Hash.hash(value);
    } } 
    [Required(ErrorMessage = "Common name is required.")]
    public string CommonName { get; set; }
    [Required(ErrorMessage = "Family name is required.")]
    public string FamilyName { get; set; }
    [Required(ErrorMessage = "Full name is required.")]
    public string FullName { get; set; }
    [Required(ErrorMessage = "Date of birth is required.")]
    public DateTime DateOfBirth { get; set; }
    public string Title { get; set; }
    public string Suffix { get; set; }
    [Required(ErrorMessage = "Culture is required.")]
    public string Culture { get; set; }
    public string Gender { get; set; }
    [Required(ErrorMessage = "Language is required.")]
    public string Language { get; set; }
  }
}
