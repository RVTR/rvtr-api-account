<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  public class AccountModel : IValidatableObject
  {
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => throw new System.NotImplementedException();
=======
using System.Security.Cryptography;
using RVTR.Account.ObjectModel.Interfaces;

namespace RVTR.Account.ObjectModel.Models 
{
  public class AccountModel : IHash
  {
    public string AccountID { get; set; } // TODO: hash instead of string
    public Profile[] Profile { get; set; } // Multiple profiles can be associated with one account, such as wife and kids all on one bill
    public AccountDetails AccountDetails { get; set; }
>>>>>>> 172281055 Added object models and Hash Util. Need to fix build errors and change some instance types to hash
  }
}
