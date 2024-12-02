using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Octa;

namespace LMS_CMS_PL.Middleware
{
    public class DbConnection_Check_Middleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public DbConnection_Check_Middleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Octa_DbContext>();

                try
                {
                    bool canConnect = await dbContext.Database.CanConnectAsync();

                    if (!canConnect)
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unable to connect to the database.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync($"Database connection failed: {ex.Message}");
                    return;
                }
            }

            // If everything is okay, pass control to the next middleware
            await _next(context);
        }
    }
}
