using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly UOW _Unit_Of_Work_Octa;


        public SupplierController(DbContextFactoryService dbContextFactory, IMapper mapper, UOW Unit_Of_Work)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _Unit_Of_Work_Octa= Unit_Of_Work;
        }


        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Supplier", "Accounting" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Supplier> suppliers = await Unit_Of_Work.supplier_Repository.Select_All_With_IncludesById<Supplier>(
                    f => f.IsDeleted != true,
                    query => query.Include(emp => emp.AccountNumber));

            if (suppliers == null || suppliers.Count == 0)
            {
                return NotFound();
            }

            List<SupplierGetDTO> DTOS = mapper.Map<List<SupplierGetDTO>>(suppliers);

            foreach (SupplierGetDTO obj in DTOS) 
            {
                Country country = _Unit_Of_Work_Octa.country_Repository.Select_By_Id_Octa(obj.CountryID);
                obj.CountryName= country.Name;
            }

            return Ok(DTOS);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Supplier", "Accounting" }
     )]
        public IActionResult Add(SupplierAddDTO NewSupplier)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewSupplier == null)
            {
                return BadRequest("Save cannot be null");
            }

            if (NewSupplier.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            if (NewSupplier.TaxCard == null)
            {
                return BadRequest("the TaxCard cannot be null");
            }

            if (NewSupplier.CommercialRegister == null)
            {
                return BadRequest("the name CommercialRegister be null");
            }

            if (NewSupplier.Email == null)
            {
                return BadRequest("the name Email be null");
            }

            if (NewSupplier.Address == null)
            {
                return BadRequest("the name Address be null");
            }

            if (NewSupplier.Website == null)
            {
                return BadRequest("the name Website be null");
            }

            if (NewSupplier.Phone1 == null)
            {
                return BadRequest("the name Phone be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewSupplier.AccountNumberID);

            if (account == null)
            {
                return NotFound("No Account chart with this Id");
            }
            else
            {
                if (account.SubTypeID == 1)
                {
                    return BadRequest("You can't use main account, only sub account");
                }

                if (account.LinkFileID != 2)
                {
                    return BadRequest("Wrong Link File, it should be Save file link ");
                }
            }

            Country country = _Unit_Of_Work_Octa.country_Repository.Select_By_Id_Octa(NewSupplier.CountryID);
            if (country==null)
            {
                return BadRequest("There is no country with this id");
            }

            Supplier supplier = mapper.Map<Supplier>(NewSupplier);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            supplier.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                supplier.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                supplier.InsertedByUserId = userId;
            }

            Unit_Of_Work.supplier_Repository.Add(supplier);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSupplier);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowEdit: 1,
        pages: new[] { "Supplier", "Accounting" }
     )]
        public IActionResult Edit(SupplierGetDTO NewSupplier)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewSupplier == null)
            {
                return BadRequest("Save cannot be null");
            }

            if (NewSupplier.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            if (NewSupplier.TaxCard == null)
            {
                return BadRequest("the TaxCard cannot be null");
            }

            if (NewSupplier.CommercialRegister == null)
            {
                return BadRequest("the name CommercialRegister be null");
            }

            if (NewSupplier.Email == null)
            {
                return BadRequest("the name Email be null");
            }

            if (NewSupplier.Address == null)
            {
                return BadRequest("the name Address be null");
            }

            if (NewSupplier.Website == null)
            {
                return BadRequest("the name Website be null");
            }

            if (NewSupplier.Phone1 == null)
            {
                return BadRequest("the name Phone be null");
            }

            Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(s => s.ID == NewSupplier.ID&&s.IsDeleted!=true);
            if (supplier == null)
            {
                return BadRequest("there is no supplier with This id");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewSupplier.AccountNumberID);

            if (account == null)
            {
                return NotFound("No Account chart with this Id");
            }
            else
            {
                if (account.SubTypeID == 1)
                {
                    return BadRequest("You can't use main account, only sub account");
                }

                if (account.LinkFileID != 2)
                {
                    return BadRequest("Wrong Link File, it should be Save file link ");
                }
            }

            Country country = _Unit_Of_Work_Octa.country_Repository.Select_By_Id_Octa(NewSupplier.CountryID);
            if (country == null)
            {
                return BadRequest("There is no country with this id");
            }

            mapper.Map(NewSupplier,supplier);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            supplier.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                supplier.UpdatedByOctaId = userId;
                if (supplier.UpdatedByUserId != null)
                {
                    supplier.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                supplier.UpdatedByUserId = userId;
                if (supplier.UpdatedByOctaId != null)
                {
                    supplier.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.supplier_Repository.Update(supplier);
            Unit_Of_Work.SaveChanges();
            return Ok(NewSupplier);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
          pages: new[] { "Save", "Accounting" }
      )]
        public IActionResult Delete(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (id == 0)
            {
                return BadRequest("Enter Supplier ID");
            }

            Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(s => s.ID == id && s.IsDeleted != true);
            if (supplier == null)
            {
                return BadRequest("there is no supplier with This id");
            }

            if (userTypeClaim == "employee")
            {
                LMS_CMS_DAL.Models.Domains.Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Supplier");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (supplier.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("supplier page doesn't exist");
                }
            }

            supplier.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            supplier.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                supplier.DeletedByOctaId = userId;
                if (supplier.DeletedByUserId != null)
                {
                    supplier.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                supplier.DeletedByUserId = userId;
                if (supplier.DeletedByOctaId != null)
                {
                    supplier.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.supplier_Repository.Update(supplier);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

    }
}
