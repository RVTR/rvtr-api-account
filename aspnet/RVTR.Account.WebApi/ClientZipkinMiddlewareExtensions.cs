using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace RVTR.Account.WebApi
{
  [ExcludeFromCodeCoverage]
  internal static class ZipkinClientMiddlewareExtensions
  {
    public static IApplicationBuilder UseZipkin(this IApplicationBuilder applicationBuilder)
    {
      return applicationBuilder.UseMiddleware<ClientZipkinMiddleware>();
    }
  }
}
