using System;
using System.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RVTR.Account.Domain.Interfaces;

namespace RVTR.Account.Domain.Validators
{
  public class AddressValidator : IValidator
  {


    internal static async Task<bool> getValidation(string address, HttpClient client)
    {
      try
      {
        var geoKey = ConfigurationManager.AppSettings["GOOGLE_GEOCODE_API_KEY"];
        Console.WriteLine($"{geoKey}: geokey");
        HttpResponseMessage response = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={geoKey}");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        JObject result = JObject.Parse(responseBody);
        List<string> acceptedTypes = new List<string>(){
          "subpremise", "street_address", "premise"
        };

        JToken resultStatus = result.GetValue("status");
        if (resultStatus.ToString().Contains("OK"))
        {
          
          JToken resultType = result.GetValue("results");
          foreach (var acceptedType in acceptedTypes)
          {
            if (resultType.ToString().Contains(acceptedType))
            {
              return true;
            }
          }
        }

      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
        return false;
      }
      return false;
    }
  }
}