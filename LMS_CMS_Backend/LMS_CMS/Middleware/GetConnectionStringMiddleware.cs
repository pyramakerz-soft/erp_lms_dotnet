using Azure.Core;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS_CMS_PL.Middleware
{
    public class GetConnectionStringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly GetConnectionStringService _getConnectionStringService;

        public GetConnectionStringMiddleware(RequestDelegate next, IServiceProvider serviceProvider, GetConnectionStringService getConnectionStringService)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _getConnectionStringService = getConnectionStringService;
        }


        //public async Task InvokeAsync(HttpContext context)
        //{
        //    var domainName = context.Request.Headers["Domain-Name"].FirstOrDefault();


        //    if (string.IsNullOrEmpty(domainName))
        //    {
        //        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        //        await context.Response.WriteAsync("Domain name header is missing or invalid.");
        //        return;
        //    }

        //    using (var scope = _serviceProvider.CreateScope())
        //    { 
        //        var dbContext = scope.ServiceProvider.GetRequiredService<Octa_DbContext>();

        //        var domain = await dbContext.Domains.FirstOrDefaultAsync(d => d.Name == domainName); 

        //        if (domain != null)
        //        {
        //            context.Items["ConnectionString"] = domain.ConnectionString; 
        //        }
        //        else
        //        {
        //            context.Response.StatusCode = StatusCodes.Status404NotFound;
        //            await context.Response.WriteAsync("Domain not found in the database.");
        //            return;
        //        }
        //    }

        //    await _next(context);
        //}

        public async Task InvokeAsync(HttpContext context)
        {
            var domainName = context.Request.Headers["Domain-Name"].FirstOrDefault();


            if (string.IsNullOrEmpty(domainName))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Domain name header is missing or invalid.");
                return;
            }
             
            var connectionString = _getConnectionStringService.BuildConnectionString(domainName);
             
            if (string.IsNullOrEmpty(connectionString))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Domain not found or invalid.");
                return;
            }
             
            context.Items["ConnectionString"] = connectionString;
             
            await _next(context);
        } 
    }
} 