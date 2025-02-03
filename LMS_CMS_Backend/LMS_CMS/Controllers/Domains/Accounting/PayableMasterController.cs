using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains;
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
    public class PayableMasterController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public PayableMasterController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Payable", "Accounting" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<PayableMaster> Payables = await Unit_Of_Work.payableMaster_Repository.Select_All_With_IncludesById<PayableMaster>(
                    t => t.IsDeleted != true,
                    query => query.Include(Master => Master.PayableDocType),
                    query => query.Include(Master => Master.LinkFile)
                    );

            if (Payables == null || Payables.Count == 0)
            {
                return NotFound();
            }

            var bankIds = Payables.Where(r => r.LinkFileID == 6).Select(r => r.BankOrSaveID).Distinct().ToList();
            var saveIds = Payables.Where(r => r.LinkFileID == 5).Select(r => r.BankOrSaveID).Distinct().ToList();

            var banks = await Unit_Of_Work.bank_Repository.Select_All_With_IncludesById<Bank>(b => bankIds.Contains(b.ID));
            var saves = await Unit_Of_Work.save_Repository.Select_All_With_IncludesById<Save>(s => saveIds.Contains(s.ID));

            List<PayableMasterGetDTO> DTOs = mapper.Map<List<PayableMasterGetDTO>>(Payables);

            foreach (var dto in DTOs)
            {
                if (dto.LinkFileID == 6) // Bank
                {
                    var bank = banks.FirstOrDefault(b => b.ID == dto.BankOrSaveID);
                    dto.BankOrSaveName = bank?.Name;
                }
                else if (dto.LinkFileID == 5) // Save
                {
                    var save = saves.FirstOrDefault(s => s.ID == dto.BankOrSaveID);
                    dto.BankOrSaveName = save?.Name;
                }
            }

            return Ok(DTOs);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Payable", "Accounting" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Enter Doc Type ID");
            }

            PayableMaster PayableMaster = await Unit_Of_Work.payableMaster_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.PayableDocType)
                    );

            if (PayableMaster == null)
            {
                return NotFound();
            }

            string bankOrSaveName = null;

            if (PayableMaster.LinkFileID == 6) // Bank
            {
                var bank = await Unit_Of_Work.bank_Repository.FindByIncludesAsync(b => b.IsDeleted != true && b.ID == PayableMaster.BankOrSaveID);
                bankOrSaveName = bank?.Name;
            }
            else if (PayableMaster.LinkFileID == 5) // Save
            {
                var save = await Unit_Of_Work.save_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == PayableMaster.BankOrSaveID);
                bankOrSaveName = save?.Name;
            }

            PayableMasterGetDTO dto = mapper.Map<PayableMasterGetDTO>(PayableMaster);
            dto.BankOrSaveName = bankOrSaveName;

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Payable", "Accounting" }
        )]
        public IActionResult Add(PayableMasterAddDTO newMaster)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newMaster == null)
            {
                return BadRequest("Payable cannot be null");
            }

            PayableDocType PayableDocType = Unit_Of_Work.payableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newMaster.PayableDocTypeID);
            if (PayableDocType == null)
            {
                return BadRequest("there is no Payable Doc Type with this ID");
            }

            LinkFile LinkFile = Unit_Of_Work.linkFile_Repository.First_Or_Default(t => t.ID == newMaster.LinkFileID);
            if (LinkFile == null)
            {
                return BadRequest("there is no Link File with this ID");
            }

            // Bank
            if (newMaster.LinkFileID == 6)
            {
                Bank Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                if (Bank == null)
                {
                    return BadRequest("There is no Bank with this ID in the database.");
                }
            }
            else if (newMaster.LinkFileID == 5) // Save
            {
                Save Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                if (Save == null)
                {
                    return BadRequest("There is no Save with this ID in the database.");
                }
            }
            else
            {
                return BadRequest("Link File Must be Save or Bank");
            }

            PayableMaster PayableMaster = mapper.Map<PayableMaster>(newMaster);
            if (newMaster.LinkFileID == 6) // Bank
            {
                PayableMaster.BankOrSaveID = newMaster.BankOrSaveID;
                PayableMaster.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
            }
            else if (newMaster.LinkFileID == 5) // Save
            {
                PayableMaster.BankOrSaveID = newMaster.BankOrSaveID;
                PayableMaster.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableMaster.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableMaster.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                PayableMaster.InsertedByUserId = userId;
            }

            Unit_Of_Work.payableMaster_Repository.Add(PayableMaster);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Payable", "Accounting" }
        )]
        public IActionResult Edit(PayableMasterPutDTO newMaster)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID, Type claim not found.");
            }

            if (newMaster == null)
            {
                return BadRequest("Payable cannot be null");
            }

            PayableMaster Payable = Unit_Of_Work.payableMaster_Repository.First_Or_Default(d => d.ID == newMaster.ID && d.IsDeleted != true);
            if (Payable == null)
            {
                return NotFound("There is no Payable with this id");
            }

            PayableDocType PayableDocType = Unit_Of_Work.payableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newMaster.PayableDocTypeID);
            if (PayableDocType == null)
            {
                return BadRequest("there is no Payable Doc Type with this ID");
            }

            LinkFile LinkFile = Unit_Of_Work.linkFile_Repository.First_Or_Default(t => t.ID == newMaster.LinkFileID);
            if (LinkFile == null)
            {
                return BadRequest("there is no Link File with this ID");
            }

            // Bank
            if (newMaster.LinkFileID == 6)
            {
                Bank Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                if (Bank == null)
                {
                    return BadRequest("There is no Bank with this ID in the database.");
                }
            }
            else if (newMaster.LinkFileID == 5) // Save
            {
                Save Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                if (Save == null)
                {
                    return BadRequest("There is no Save with this ID in the database.");
                }
            }
            else
            {
                return BadRequest("Link File Must be Save or Bank");
            }
             

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Payable");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (Payable.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Payable page doesn't exist");
                }
            }

            mapper.Map(newMaster, Payable);
            if (newMaster.LinkFileID == 6) // Bank
            {
                Payable.BankOrSaveID = newMaster.BankOrSaveID;
                Payable.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                Payable.Save = null;
            }
            else if (newMaster.LinkFileID == 5) // Save
            {
                Payable.BankOrSaveID = newMaster.BankOrSaveID;
                Payable.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                Payable.Bank = null;
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Payable.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Payable.UpdatedByOctaId = userId;
                if (Payable.UpdatedByUserId != null)
                {
                    Payable.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                Payable.UpdatedByUserId = userId;
                if (Payable.UpdatedByOctaId != null)
                {
                    Payable.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.payableMaster_Repository.Update(Payable);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Payable", "Accounting" }
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
                return BadRequest("Enter Payable ID");
            }

            PayableMaster Payable = Unit_Of_Work.payableMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (Payable == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Payable");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (Payable.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Payable page doesn't exist");
                }
            }

            Payable.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Payable.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Payable.DeletedByOctaId = userId;
                if (Payable.DeletedByUserId != null)
                {
                    Payable.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                Payable.DeletedByUserId = userId;
                if (Payable.DeletedByOctaId != null)
                {
                    Payable.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.payableMaster_Repository.Update(Payable);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
