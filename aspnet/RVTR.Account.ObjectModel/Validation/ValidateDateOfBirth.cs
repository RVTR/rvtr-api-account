using System.ComponentModel.DataAnnotations;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.ObjectModel.Validation
{
  public class ValidateDateOfBirth : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var dob = (System.DateTime) value;
      //var vc = validationContext.Items["checkout"];
      if (dob > System.DateTime.Now)
      {
        return new ValidationResult("You cannot be born in the future.");
      }
      return null;
    }
  }
}