using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Octa;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Middleware
{
    public class GetConnectionStringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public GetConnectionStringMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var domainName = context.Request.Headers["DomainName"].FirstOrDefault();

            if (string.IsNullOrEmpty(domainName))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Domain name header is missing or invalid.");
                return;
            }
             
            using (var scope = _serviceProvider.CreateScope())
            { 
                var dbContext = scope.ServiceProvider.GetRequiredService<Octa_DbContext>();
                 
                var domain = await dbContext.Domains.FirstOrDefaultAsync(d => d.Name == domainName);

                if (domain != null)
                {
                    context.Items["ConnectionString"] = domain.ConnectionString;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Domain not found in the database.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
 