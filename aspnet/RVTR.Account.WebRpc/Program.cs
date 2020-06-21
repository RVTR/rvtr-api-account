using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RVTR.Account.WebRpc
{
  public class Program
  {
    public static void Main()
    {
      CreateHostBuilder().Build().Run();
    }

    public static IHostBuilder CreateHostBuilder() =>
      Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });
  }
}
