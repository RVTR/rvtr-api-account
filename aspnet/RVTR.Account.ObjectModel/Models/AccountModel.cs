<<<<<<< HEAD
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Account.ObjectModel.Models
{
  public class AccountModel : IValidatableObject
  {
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => throw new System.NotImplementedException();
=======
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> Added validation annotations and hash fields.
using System.Security.Cryptography;

// using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Util;

namespace RVTR.Account.ObjectModel.Models 
{
  public class AccountModel 
  {
    public string AccountID { get => AccountID ; set{
      AccountID = Hash.hash(value);
    } } 
    public Profile[] Profile { get; set; } // Multiple profiles can be associated with one account, such as wife and kids all on one bill
    public AccountDetails AccountDetails { get; set; }
>>>>>>> 172281055 Added object models and Hash Util. Need to fix build errors and change some instance types to hash
  }
}
