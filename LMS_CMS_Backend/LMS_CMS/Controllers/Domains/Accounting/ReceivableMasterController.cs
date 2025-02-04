using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
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
    public class ReceivableMasterController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public ReceivableMasterController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Receivable", "Accounting" }
        )]
        public async Task<IActionResult> GetAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            int totalRecords = await Unit_Of_Work.receivableMaster_Repository
                .CountAsync(f => f.IsDeleted != true);

            List<ReceivableMaster> Receivables = await Unit_Of_Work.receivableMaster_Repository.Select_All_With_IncludesById_Pagination<ReceivableMaster>(
                    t => t.IsDeleted != true ,
                    query => query.Include(Master => Master.ReceivableDocType),
                    query => query.Include(Master => Master.LinkFile)
                    )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (Receivables == null || Receivables.Count == 0)
            {
                return NotFound();
            }

            var bankIds = Receivables.Where(r => r.LinkFileID == 6).Select(r => r.BankOrSaveID).Distinct().ToList();
            var saveIds = Receivables.Where(r => r.LinkFileID == 5).Select(r => r.BankOrSaveID).Distinct().ToList();

            var banks = await Unit_Of_Work.bank_Repository.Select_All_With_IncludesById<Bank>(b => bankIds.Contains(b.ID));
            var saves = await Unit_Of_Work.save_Repository.Select_All_With_IncludesById<Save>(s => saveIds.Contains(s.ID));

            List<ReceivableMasterGetDTO> DTOs = mapper.Map<List<ReceivableMasterGetDTO>>(Receivables);

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

            var paginationMetadata = new
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return Ok(new { Data = DTOs, Pagination = paginationMetadata });
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Receivable", "Accounting" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Enter Doc Type ID");
            } 

            ReceivableMaster ReceivableMaster = await Unit_Of_Work.receivableMaster_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.ReceivableDocType)
                    );

            if (ReceivableMaster == null)
            {
                return NotFound();
            }

            string bankOrSaveName = null;

            if (ReceivableMaster.LinkFileID == 6) // Bank
            {
                var bank = await Unit_Of_Work.bank_Repository.FindByIncludesAsync(b => b.IsDeleted != true && b.ID == ReceivableMaster.BankOrSaveID);
                bankOrSaveName = bank?.Name;
            }
            else if (ReceivableMaster.LinkFileID == 5) // Save
            {
                var save = await Unit_Of_Work.save_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == ReceivableMaster.BankOrSaveID);
                bankOrSaveName = save?.Name;
            }

            ReceivableMasterGetDTO dto = mapper.Map<ReceivableMasterGetDTO>(ReceivableMaster);
            dto.BankOrSaveName = bankOrSaveName;

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Receivable", "Accounting" }
        )]
        public IActionResult Add(ReceivableMasterAddDTO newMaster)
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
                return BadRequest("Receivable cannot be null");
            }

            ReceivableDocType ReceivableDocType = Unit_Of_Work.receivableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newMaster.ReceivableDocTypesID);
            if(ReceivableDocType == null)
            {
                return BadRequest("there is no Receivable Doc Type with this ID"); 
            }

            LinkFile LinkFile = Unit_Of_Work.linkFile_Repository.First_Or_Default(t => t.ID == newMaster.LinkFileID);
            if(LinkFile == null)
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
            } else if(newMaster.LinkFileID == 5) // Save
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

            ReceivableMaster ReceivableMaster = mapper.Map<ReceivableMaster>(newMaster);
            if (newMaster.LinkFileID == 6) // Bank
            {
                ReceivableMaster.BankOrSaveID = newMaster.BankOrSaveID;
                ReceivableMaster.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
            }
            else if (newMaster.LinkFileID == 5) // Save
            {
                ReceivableMaster.BankOrSaveID = newMaster.BankOrSaveID;
                ReceivableMaster.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableMaster.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableMaster.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableMaster.InsertedByUserId = userId;
            }

            Unit_Of_Work.receivableMaster_Repository.Add(ReceivableMaster);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Receivable", "Accounting" }
        )]
        public IActionResult Edit(ReceivablePutDTO newMaster)
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
                return BadRequest("Receivable cannot be null");
            }

            ReceivableMaster Receivable = Unit_Of_Work.receivableMaster_Repository.First_Or_Default(d => d.ID == newMaster.ID && d.IsDeleted != true);
            if (Receivable == null)
            {
                return NotFound("There is no Receivable with this id");
            }

            ReceivableDocType ReceivableDocType = Unit_Of_Work.receivableDocType_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newMaster.ReceivableDocTypesID);
            if (ReceivableDocType == null)
            {
                return BadRequest("there is no Receivable Doc Type with this ID");
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
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Receivable");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (Receivable.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Receivable page doesn't exist");
                }
            }

            mapper.Map(newMaster, Receivable);

            if (newMaster.LinkFileID == 6) // Bank
            {
                Receivable.BankOrSaveID = newMaster.BankOrSaveID;
                Receivable.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                Receivable.Save = null;
            }
            else if (newMaster.LinkFileID == 5) // Save
            {
                Receivable.BankOrSaveID = newMaster.BankOrSaveID;
                Receivable.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newMaster.BankOrSaveID);
                Receivable.Bank = null;
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Receivable.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Receivable.UpdatedByOctaId = userId;
                if (Receivable.UpdatedByUserId != null)
                {
                    Receivable.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                Receivable.UpdatedByUserId = userId;
                if (Receivable.UpdatedByOctaId != null)
                {
                    Receivable.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.receivableMaster_Repository.Update(Receivable);
            Unit_Of_Work.SaveChanges();
            return Ok(newMaster);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Receivable", "Accounting" }
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
                return BadRequest("Enter Receivable ID");
            }

            ReceivableMaster Receivable = Unit_Of_Work.receivableMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);


            if (Receivable == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Receivable");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (Receivable.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Receivable page doesn't exist");
                }
            }

            Receivable.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Receivable.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                Receivable.DeletedByOctaId = userId;
                if (Receivable.DeletedByUserId != null)
                {
                    Receivable.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                Receivable.DeletedByUserId = userId;
                if (Receivable.DeletedByOctaId != null)
                {
                    Receivable.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.receivableMaster_Repository.Update(Receivable);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
