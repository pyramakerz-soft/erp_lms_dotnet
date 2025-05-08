using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using MimeKit;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DomainController : ControllerBase
    {
        private readonly UOW _Unit_Of_Work;
        private readonly DynamicDatabaseService _dynamicDatabaseService;
        private readonly DbContextFactoryService _dbContextFactory;
        HashSet<long> addedPageIds = new HashSet<long>();
        IMapper mapper;
        private readonly GetConnectionStringService _getConnectionStringService;

        public DomainController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, DbContextFactoryService dbContextFactory, IMapper mapper, GetConnectionStringService getConnectionStringService)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _getConnectionStringService = getConnectionStringService;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public IActionResult Get()
        {
            List<Domain> Domains = _Unit_Of_Work.domain_Octa_Repository.FindBy_Octa(t => t.IsDeleted != true);
            if (Domains == null || Domains.Count == 0)
            {
                return NotFound();
            }

            List<DomainGetDTO> domainGetDTOs = mapper.Map<List<DomainGetDTO>>(Domains);

            return Ok(domainGetDTOs);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{Id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public IActionResult GetById(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("Enter Bus ID");
            }

            Domain Domain = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(t => t.IsDeleted != true && t.ID == Id);

            if (Domain == null)
            {
                return NotFound();
            }

            //HttpContext.Items["ConnectionString"] = Domain.ConnectionString;
            HttpContext.Items["ConnectionString"] = _getConnectionStringService.BuildConnectionString(Domain.Name);

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<LMS_CMS_DAL.Models.Domains.Page> pages = Unit_Of_Work.page_Repository.FindBy(p => p.Page_ID == null);

            List<long> pagesId = new List<long>();
            for (int i = 0; i < pages.Count; i++)
            {
                pagesId.Add(pages[i].ID);
            }

            DomainGetDTO domianDTO = new DomainGetDTO{ ID = Domain.ID, Name = Domain.Name, Pages = pagesId.ToArray() };

            return Ok(domianDTO);
        }
        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private IEnumerable<LMS_CMS_DAL.Models.Octa.Page> GetPagesByParentId(long parentId)
        {
            return _Unit_Of_Work.domain_Octa_Repository.OctaDatabase().Page.Where(p => p.Page_ID == parentId).ToList();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void AddPageWithChildren(LMS_CMS_DAL.Models.Octa.Page page, UOW Unit_Of_Work)
        {
            if (page == null) return;

            if (addedPageIds.Contains(page.ID))
                return;

            addedPageIds.Add(page.ID);

            //LMS_CMS_DAL.Models.Domains.Page pageNew = new LMS_CMS_DAL.Models.Domains.Page
            //{
            //    ID = page.ID,
            //    en_name = page.en_name,
            //    ar_name = page.ar_name,
            //    IsDisplay = page.IsDisplay,
            //    Page_ID = page.Page_ID
            //};

            //Unit_Of_Work.page_Repository.Add(pageNew);

            var alreadyExists = Unit_Of_Work.page_Repository.Select_By_Id(page.ID);
            if (alreadyExists == null)
            {
                LMS_CMS_DAL.Models.Domains.Page pageNew = new LMS_CMS_DAL.Models.Domains.Page
                {
                    ID = page.ID,
                    en_name = page.en_name,
                    ar_name = page.ar_name,
                    IsDisplay = page.IsDisplay,
                    Page_ID = page.Page_ID
                };

                Unit_Of_Work.page_Repository.Add(pageNew);
            }

            var childPages = GetPagesByParentId(page.ID);
            foreach (var childPage in childPages)
            {
                AddPageWithChildren(childPage, Unit_Of_Work);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void DeletePageWithChildren(LMS_CMS_DAL.Models.Octa.Page page, UOW Unit_Of_Work)
        {
            if (page == null) return;
             
            var childPages = GetPagesByParentId(page.ID);
             
            foreach (var childPage in childPages)
            {
                DeletePageWithChildren(childPage, Unit_Of_Work);  
            }

            // After deleting all children, delete the current page
            Unit_Of_Work.page_Repository.Delete(page.ID);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string GenerateSecurePassword(int length)
        {
            const string allCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*-_=+?";
            var password = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[length];
                rng.GetBytes(buffer);
                foreach (var b in buffer)
                {
                    password.Append(allCharacters[b % allCharacters.Length]);
                }
            }
            return password.ToString();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public async Task<IActionResult> AddDomain([FromBody] DomainAdd_DTO domain)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if (userIdClaim == null)
            {
                return Unauthorized("User Id claim not found.");
            }

            if (string.IsNullOrWhiteSpace(domain.Name))
            {
                return BadRequest("Invalid domain name.");
            }

            var domainNameRegex = @"^[a-zA-Z_]+$";
            if (!Regex.IsMatch(domain.Name, domainNameRegex))
            {
                return BadRequest("Domain name can only contain letters and underscores, and no spaces or numbers.");
            }

            var existingDomain = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(d => d.Name == domain.Name);
            if (existingDomain != null)
            {
                return Conflict("Domain already exists.");
            }

            await _dynamicDatabaseService.AddDomainAndSetupDatabase(domain.Name, userId);

            // Make the DB Connection
            var domainEx = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(d => d.Name == domain.Name);

            //HttpContext.Items["ConnectionString"] = domainEx.ConnectionString;
            HttpContext.Items["ConnectionString"] = _getConnectionStringService.BuildConnectionString(domainEx.Name);

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Create Admin Role
            Role role = new Role { Name = "Admin" };
            role.InsertedByOctaId = userId;
            role.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.role_Repository.Add(role);
            Unit_Of_Work.SaveChanges();

            // Create Employee
            // Create Random Password
            string Pass = GenerateSecurePassword(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Pass);

            Employee emp = new Employee { User_Name = domain.Name, en_name = domain.Name, Password = hashedPassword, Role_ID = 1, EmployeeTypeID = 1 };
            emp.InsertedByOctaId= userId;
            emp.InsertedAt= TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.employee_Repository.Add(emp);
            Unit_Of_Work.SaveChanges();

            // Create Pages
            if (domain.Pages == null || domain.Pages.Length == 0)
            {
                return BadRequest("Pages array cannot be null or empty.");
            }

            var notFoundPages = new List<long>();
            var notModulePages = new List<long>();

            for (long i = 0; i < domain.Pages.Length; i++)
            {
                var page = _Unit_Of_Work.page_Octa_Repository.Select_By_Id_Octa(domain.Pages[i]);
                if (page == null)
                {
                    notFoundPages.Add(domain.Pages[i]);
                } 
                else if (page.Page_ID != null)
                {
                    notModulePages.Add(domain.Pages[i]);
                }
                else
                {
                    AddPageWithChildren(page, Unit_Of_Work);
                }
            }


            //// Add all pages to Admin role
            foreach (var pageId in addedPageIds)
            {
                Role_Detailes roleDetail = new Role_Detailes
                {
                    Role_ID = 1, // Admin Role
                    Page_ID = pageId,
                    Allow_Edit = true,
                    Allow_Delete = true,
                    Allow_Edit_For_Others = true,
                    Allow_Delete_For_Others = true,
                };
                roleDetail.InsertedByOctaId = userId;
                roleDetail.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                Unit_Of_Work.role_Detailes_Repository.Add(roleDetail);
            }
            Unit_Of_Work.SaveChanges();


            //// add schoolType
            List<LMS_CMS_DAL.Models.Octa.SchoolType> SchoolTypes = _Unit_Of_Work.schoolType_Octa_Repository.Select_All_Octa();
            foreach (var item in SchoolTypes)
            {
                LMS_CMS_DAL.Models.Domains.LMS.SchoolType schoolType = new LMS_CMS_DAL.Models.Domains.LMS.SchoolType();
                schoolType.Name = item.Name;
                schoolType.ID = item.ID;
                Unit_Of_Work.schoolType_Repository.Add(schoolType);
            }

            Unit_Of_Work.SaveChanges();

            string domainLink = null;

            // to make a new route in sever 
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    subdomain = domain.Name
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://8b1r2kegpb.execute-api.us-east-1.amazonaws.com/CreateSubDomain", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                // Parse JSON and extract domain link
                var outerJson = JsonDocument.Parse(responseContent);
                if (outerJson.RootElement.TryGetProperty("body", out JsonElement bodyElement))
                {
                    var innerJson = JsonDocument.Parse(bodyElement.GetString());
                    if (innerJson.RootElement.TryGetProperty("message", out JsonElement messageElement))
                    {
                        string message = messageElement.GetString();
                        var match = Regex.Match(message, @"Subdomain (\S+) created successfully\.");
                        if (match.Success)
                        {
                            domainLink = match.Groups[1].Value; 
                        }
                    }
                }
            }

            return Ok(new
            {
                message = "Domain and database setup successfully.",
                userName = domain.Name,
                password = Pass,
                link = domainLink,
                notFoundPages = notFoundPages.Any() ? notFoundPages : null,
                notModulePages = notModulePages.Any() ? notModulePages : null
            });
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("ReRunMigrations")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public async Task<IActionResult> ReRunMigrations(string domainName)
        {
            if (string.IsNullOrWhiteSpace(domainName))
            {
                return BadRequest("Invalid domain name.");
            }

            var existingDomain = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(d => d.Name == domainName);
            if (existingDomain == null)
            {
                return Conflict("Domain doesn't exist.");
            }

            await _dynamicDatabaseService.ApplyMigrations(domainName);

            return Ok(new { message = "Migrations are Updated successfully." });
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public async Task<IActionResult> EditDomain(DomainPut_DTO domain)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            if (userIdClaim == null)
            {
                return Unauthorized("User Id claim not found.");
            }

            if (string.IsNullOrWhiteSpace(domain.Name))
            {
                return BadRequest("Invalid domain name.");
            }

            var existingDomain = _Unit_Of_Work.domain_Octa_Repository.Select_By_Id_Octa(domain.ID);
            if (existingDomain == null || existingDomain.IsDeleted == true)
            {
                return Conflict("Domain doesn't exist.");
            }

            //HttpContext.Items["ConnectionString"] = existingDomain.ConnectionString;
            HttpContext.Items["ConnectionString"] = _getConnectionStringService.BuildConnectionString(existingDomain.Name);

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (domain.Pages == null || domain.Pages.Length == 0)
            {
                return BadRequest("Pages array cannot be null or empty.");
            }

            // Delete What is not there
            //var FoundPages = Unit_Of_Work.page_Repository.Select_All();
            //if (FoundPages != null || FoundPages.Count != 0)
            //{
            //    for (int i = 0; i < FoundPages.Count; i++)
            //    {
            //        long id = FoundPages[i].ID;
            //        var existingPage = Unit_Of_Work.page_Repository.Select_By_Id(id);

            //        if (existingPage != null)
            //        {
            //            Unit_Of_Work.page_Repository.Delete(existingPage.ID);
            //        }
            //    }
            //    Unit_Of_Work.SaveChanges();
            //}

            // 1) Get All the Existing
            var existingPages = Unit_Of_Work.page_Repository.Select_All();

            // 2) Get The New Ones (Make Sure This step is before deleting the deleted ones)
            var notFoundPages = new List<long>();
            var notModulePages = new List<long>();

            if (domain.Pages != null || domain.Pages.Length != 0)
            {
                for (int i = 0; i < domain.Pages.Length; i++)
                {
                    var page = _Unit_Of_Work.page_Octa_Repository.Select_By_Id_Octa(domain.Pages[i]);
                    if (page != null)
                    {
                        var existingPage = Unit_Of_Work.page_Repository.Select_By_Id(domain.Pages[i]);

                        if (page == null)
                        {
                            notFoundPages.Add(domain.Pages[i]);
                        }
                        else if (page.Page_ID != null)
                        {
                            notModulePages.Add(domain.Pages[i]);
                        }
                        else
                        {
                            AddPageWithChildren(page, Unit_Of_Work);
                        }
                    }
                }
                Unit_Of_Work.SaveChanges();
            }

            // 3) Get The Deleted Ones (By Default it will delete the role details as on delete cascade
            if (existingPages != null || existingPages.Count != 0)
            {
                for (int i = 0; i < existingPages.Count; i++)
                {
                    if (!domain.Pages.Contains(existingPages[i].ID) && existingPages[i].Page_ID == null)
                    { 
                        var page = _Unit_Of_Work.page_Octa_Repository.Select_By_Id_Octa(existingPages[i].ID);
                        DeletePageWithChildren(page, Unit_Of_Work);
                    } 
                }
                Unit_Of_Work.SaveChanges();
            }

            //var notFoundPages = new List<long>();
            //var notModulePages = new List<long>();

            //for (long i = 0; i < domain.Pages.Length; i++)
            //{
            //    var page = _Unit_Of_Work.page_Octa_Repository.Select_By_Id_Octa(domain.Pages[i]);

            //    if (page == null)
            //    {
            //        notFoundPages.Add(domain.Pages[i]);
            //    }
            //    else if (page.Page_ID != null)
            //    {
            //        notModulePages.Add(domain.Pages[i]);
            //    }
            //    else
            //    {
            //        AddPageWithChildren(page, Unit_Of_Work);
            //    }
            //}

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //// Add all pages to Admin role
            foreach (var pageId in addedPageIds)
            {
                Role_Detailes roleDetail = new Role_Detailes
                {
                    Role_ID = 1, // Admin Role
                    Page_ID = pageId,
                    Allow_Edit = true,
                    Allow_Delete = true,
                    Allow_Edit_For_Others = true,
                    Allow_Delete_For_Others = true,
                };
                roleDetail.InsertedByOctaId = userId;
                roleDetail.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

                Role_Detailes existingRoleDetail =  Unit_Of_Work.role_Detailes_Repository.First_Or_Default(d => d.IsDeleted != true && d.Role_ID == roleDetail.Role_ID && d.Page_ID == roleDetail.Page_ID);
                if (existingRoleDetail == null)
                {
                    Unit_Of_Work.role_Detailes_Repository.Add(roleDetail);
                }
            } 

            Unit_Of_Work.SaveChanges();

            existingDomain.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            existingDomain.UpdatedByUserId = userId;

            _Unit_Of_Work.SaveOctaChanges();

            return Ok(new
            {
                message = "Domain and database Updated successfully.",
                notFoundPages = notFoundPages.Any() ? notFoundPages : null,
                notModulePages = notModulePages.Any() ? notModulePages : null
            });
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpDelete("{Id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa" }
        )]
        public IActionResult Delete(long Id)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }

            if (Id == 0)
            {
                return BadRequest("Domain ID cannot be null.");
            }

            Domain domain = _Unit_Of_Work.domain_Octa_Repository.Select_By_Id_Octa(Id);
            if (domain == null || domain.IsDeleted == true)
            {
                return NotFound("No Domain with this ID");
            }
            else
            {
                domain.IsDeleted = true;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                domain.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                domain.DeletedByUserId = userId;

                _Unit_Of_Work.domain_Octa_Repository.Update_Octa(domain);
                _Unit_Of_Work.SaveOctaChanges();
                return Ok(new { message = "Domain has Successfully been deleted" });
            }
        }
    }

}