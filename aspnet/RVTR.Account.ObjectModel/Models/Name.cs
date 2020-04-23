using System;
using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models
{
  public class Name 
  {
    [Key]
    public string NameID { get => NameID ; set{
      NameID = Hash.hash(value);
    } } 
    [Display(Name = "Common name")]
    [Required(ErrorMessage = "Common name is required.")]
    public string CommonName { get; set; }
    [Display(Name = "Family name")]
    [Required(ErrorMessage = "Family name is required.")]
    public string FamilyName { get; set; }
    [Display(Name = "Full name")]
    [Required(ErrorMessage = "Full name is required.")]
    public string FullName { get; set; }
    [Display(Name = "Date of birth")]
    [Required(ErrorMessage = "Date of birth is required.")]
    [DataType(DataType.DateTime)]
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Title")]
    public string Title { get; set; }
    [Display(Name = "Suffix")]
    public string Suffix { get; set; }
    [Display(Name = "Culture")]
    [Required(ErrorMessage = "Culture is required.")]
    public string Culture { get; set; }
    [Display(Name = "Gender")]
    public string Gender { get; set; }
    [Display(Name = "Language")]
    [Required(ErrorMessage = "Language is required.")]
    public string Language { get; set; }
  }
}
