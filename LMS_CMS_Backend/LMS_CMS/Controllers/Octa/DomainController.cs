using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
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

        [HttpGet]
        public IActionResult Get()
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

            List<Domain> Domains = _Unit_Of_Work.domain_Octa_Repository.Select_All_Octa();

            return Ok(Domains);
        }
        private IEnumerable<LMS_CMS_DAL.Models.Octa.Page> GetPagesByParentId(long parentId)
        {
            return _Unit_Of_Work.domain_Octa_Repository.OctaDatabase().Page.Where(p => p.Page_ID == parentId).ToList();
        }

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

        [HttpPost]
        public async Task<IActionResult> AddDomain([FromBody] DomainAdd_DTO domain)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
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

            await _dynamicDatabaseService.AddDomainAndSetupDatabase(domain.DomainName);

            // Make the DB Connection
            var domainEx = await _Unit_Of_Work.domain_Octa_Repository.OctaDatabase().Domains.FirstOrDefaultAsync(d => d.Name == domain.DomainName);

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

            for (long i = 0; i < domain.Pages.Length; i++)
            {
                var page = _Unit_Of_Work.page_Octa_Repository.Select_By_Id_Octa(domain.Pages[i]);
                if (page == null)
                {
                    notFoundPages.Add(domain.Pages[i]);
                }
                AddPageWithChildren(page, Unit_Of_Work);
            }

            Unit_Of_Work.SaveChanges();

            return Ok(new
            {
                message = "Domain and database setup successfully.",
                userName = domain.DomainName,
                password = Pass,
                notFoundPages = notFoundPages.Any() ? notFoundPages : null
            });
        }

        [HttpPut]
        public async Task<IActionResult> EditDomain(string domainName)
        {
            var userClaims = HttpContext.User.Claims;
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userTypeClaim == null)
            {
                return Unauthorized("User Type claim not found.");
            }

            if (userTypeClaim != "octa")
            {
                return Unauthorized("Access Denied");
            }

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

            return Ok(new { message = "Domain and database Updated successfully." });
        }
    }

}