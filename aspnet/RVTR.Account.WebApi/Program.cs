using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RVTR.Account.DataContext;

namespace RVTR.Account.WebApi
{
  /// <summary>
  ///
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class Program
  {

    /// <summary>
    /// Main Method for RVTR.Account.WebApi
    /// </summary>
    /// <returns></returns>
    public static async Task Main()
    {
      var host = CreateHostBuilder().Build();

      await CreateDbContextAsync(host);
      await host.RunAsync();
    }

    /// <summary>
    /// IHostBuilder Method
    /// </summary>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder() =>
      Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });

    /// <summary>
    /// CreateDbContext Method
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static async Task CreateDbContextAsync(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var provider = scope.ServiceProvider;
        var context = provider.GetRequiredService<AccountContext>();

        await context.Database.EnsureCreatedAsync();
      }
    }

    /// <summary>
    /// Protected Constructor
    /// </summary>
    protected Program() { }
  }
}
