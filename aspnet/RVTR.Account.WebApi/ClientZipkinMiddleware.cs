using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipkin4net;
using zipkin4net.Tracers.Zipkin;

namespace RVTR.Account.WebApi
{
  [ExcludeFromCodeCoverage]
  internal class ClientZipkinMiddleware : IMiddleware
  {
    public ClientZipkinMiddleware(){ }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      var lifetime = context.RequestServices.GetService<IHostApplicationLifetime>();
      var statistics = new Statistics();

      lifetime.ApplicationStopped.Register(() =>
      {
        TraceManager.Stop();
      });

      await next(context);
    }
  }
}
