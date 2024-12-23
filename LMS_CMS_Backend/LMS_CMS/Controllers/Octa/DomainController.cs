using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.DTO.Octa;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations.Domains;
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

        public DomainController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work, DbContextFactoryService dbContextFactory)
        {
            _Unit_Of_Work = Unit_Of_Work;
            _dynamicDatabaseService = dynamicDatabaseService;
            _dbContextFactory = dbContextFactory;
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

            return Ok(Domains);
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

            HttpContext.Items["ConnectionString"] = Domain.ConnectionString;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<LMS_CMS_DAL.Models.Domains.Page> pages = Unit_Of_Work.page_Repository.FindBy(p => p.Page_ID == null);

            DomainGetDTO domianDTO = new DomainGetDTO{ ID = Domain.ID, Name = Domain.Name, Pages = pages.ToArray() };

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

            LMS_CMS_DAL.Models.Domains.Page pageNew = new LMS_CMS_DAL.Models.Domains.Page
            {
                ID = page.ID,
                en_name = page.en_name,
                ar_name = page.ar_name,
                IsDisplay = page.IsDisplay,
                Page_ID = page.Page_ID
            };

            Unit_Of_Work.page_Repository.Add(pageNew);

            var childPages = GetPagesByParentId(page.ID);
            foreach (var childPage in childPages)
            {
                AddPageWithChildren(childPage, Unit_Of_Work);
            }
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
            if (userIdClaim == null)
            {
                return Unauthorized("User Id claim not found.");
            }

            if (string.IsNullOrWhiteSpace(domain.DomainName))
            {
                return BadRequest("Invalid domain name.");
            }

            var domainNameRegex = @"^[a-zA-Z_]+$";
            if (!Regex.IsMatch(domain.DomainName, domainNameRegex))
            {
                return BadRequest("Domain name can only contain letters and underscores, and no spaces or numbers.");
            }

            var existingDomain = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(d => d.Name == domain.DomainName);
            if (existingDomain != null)
            {
                return Conflict("Domain already exists.");
            }

            await _dynamicDatabaseService.AddDomainAndSetupDatabase(domain.DomainName, userId);

            // Make the DB Connection
            var domainEx = _Unit_Of_Work.domain_Octa_Repository.First_Or_Default_Octa(d => d.Name == domain.DomainName);

            HttpContext.Items["ConnectionString"] = domainEx.ConnectionString;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Create Admin Role
            Role role = new Role { Name = "Admin" };
            Unit_Of_Work.role_Repository.Add(role);
            Unit_Of_Work.SaveChanges();

            // Create Employee
            // Create Random Password
            string Pass = GenerateSecurePassword(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Pass);

            Employee emp = new Employee { User_Name = domain.DomainName, en_name = domain.DomainName, Password = hashedPassword, Role_ID = 1, EmployeeTypeID = 1 };
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

            Unit_Of_Work.SaveChanges();

            return Ok(new
            {
                message = "Domain and database setup successfully.",
                userName = domain.DomainName,
                password = Pass,
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

            if (string.IsNullOrWhiteSpace(domain.DomainName))
            {
                return BadRequest("Invalid domain name.");
            }

            var existingDomain = _Unit_Of_Work.domain_Octa_Repository.Select_By_Id_Octa(domain.ID);
            if (existingDomain == null)
            {
                return Conflict("Domain doesn't exist.");
            }

            HttpContext.Items["ConnectionString"] = existingDomain.ConnectionString;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (domain.Pages == null || domain.Pages.Length == 0)
            {
                return BadRequest("Pages array cannot be null or empty.");
            }

            // Delete What is not there
            var FoundPages = Unit_Of_Work.page_Repository.Select_All();
            if (FoundPages != null || FoundPages.Count != 0)
            {
                for (int i = 0; i < FoundPages.Count; i++)
                {
                    long id = FoundPages[i].ID;
                    var existingPage = Unit_Of_Work.page_Repository.Select_By_Id(id);

                    if (existingPage != null)
                    {
                        Unit_Of_Work.page_Repository.Delete(existingPage.ID);
                    }
                }
                Unit_Of_Work.SaveChanges();
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

            Unit_Of_Work.SaveChanges();

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
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