using LMS_CMS_BL.UOW;
using LMS_CMS_PL.Attribute;

namespace LMS_CMS_PL.Middleware
{
    public class Endpoint_Authorization_Middleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Endpoint_Authorization_Middleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<Authorize_Endpoint_Attribute>();

            if (attribute == null)
            {
                // No attribute, allow access
                await _next(context);
                return;
            }

            var userClaims = context.User.Claims;

            var userType = userClaims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userType == null)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("You Need To Login.");
                return;
            }

            var roleId = userClaims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            // Allow `pyramakerz` full access
            if (userType == "pyramakerz")
            {
                await _next(context);
                return;
            }

            if (!attribute.AllowedTypes.Contains(userType))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied.");
                return;
            }

            if (userType == "employee" && int.TryParse(roleId, out var parsedRoleId))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<UOW>();

                    // Retrieve Role_Detailes with related Page details using the repository
                    var roleDetails = _unitOfWork.role_Detailes_Repository
                        .Select_All_With_Includes(rd => rd.Page)
                        .Where(rd => rd.Role_ID == parsedRoleId)
                        .ToList();

                    // Collect accessible pages
                    var accessiblePages = roleDetails.Select(rd => rd.Page.en_name).ToHashSet();

                    if (attribute.Pages?.Length > 0)
                    {
                        // Check if the user has access to at least one page in the attribute
                        if (!attribute.Pages.Any(page => accessiblePages.Contains(page)))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync("Access denied.");
                            return;
                        }

                        // Check edit permissions if required
                        if (attribute.AllowEdit == 1)
                        {
                            var editablePages = roleDetails
                                .Where(rd => rd.Allow_Edit)
                                .Select(rd => rd.Page.en_name)
                                .ToHashSet();

                            if (!attribute.Pages.Any(page => editablePages.Contains(page)))
                            {
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                await context.Response.WriteAsync("Edit permission denied.");
                                return;
                            }
                        }

                        // Check delete permissions if required
                        if (attribute.AllowDelete == 1)
                        {
                            var deletablePages = roleDetails
                                .Where(rd => rd.Allow_Delete)
                                .Select(rd => rd.Page.en_name)
                                .ToHashSet();

                            if (!attribute.Pages.Any(page => deletablePages.Contains(page)))
                            {
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                await context.Response.WriteAsync("Delete permission denied.");
                                return;
                            }
                        }
                    }
                }
            }
            await _next(context);
        }
    }
}
