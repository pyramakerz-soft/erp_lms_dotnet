using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public AssetController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Asset" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<Asset> Assets = await Unit_Of_Work.asset_Repository.Select_All_With_IncludesById<Asset>(
                    f => f.IsDeleted != true,
            query => query.Include(emp => emp.AccountNumber));

            if (Assets == null || Assets.Count == 0)
            {
                return NotFound();
            }

            List<AssetGetDTO> AssetGetDTOs = mapper.Map<List<AssetGetDTO>>(Assets);

            return Ok(AssetGetDTOs);
        }

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Asset" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Asset ID");
            }

            Asset asset = await Unit_Of_Work.asset_Repository.FindByIncludesAsync(
                    asset => asset.IsDeleted != true && asset.ID == id,
                    query => query.Include(asset => asset.AccountNumber));

            if (asset == null)
            {
                return NotFound();
            }

            AssetGetDTO assetGetDTO = mapper.Map<AssetGetDTO>(asset);

            return Ok(assetGetDTO);
        }

        [HttpPost]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Asset" }
       )]
        public IActionResult Add(AssetAddDTO NewAsset)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewAsset == null)
            {
                return BadRequest("Asset cannot be null");
            }

            if (NewAsset.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewAsset.AccountNumberID);

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

                if (account.LinkFileID != 9)
                {
                    return BadRequest("Wrong Link File, it should be Asset file link ");
                }
            }

            Asset asset = mapper.Map<Asset>(NewAsset);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            asset.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                asset.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                asset.InsertedByUserId = userId;
            }

            Unit_Of_Work.asset_Repository.Add(asset);
            Unit_Of_Work.SaveChanges();
            return Ok(NewAsset);
        }

        [HttpPut]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           allowEdit: 1,
           pages: new[] { "Asset" }
       )]
        public IActionResult Edit(AssetPutDTO EditedAsset)
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

            if (EditedAsset == null)
            {
                return BadRequest("Asset cannot be null");
            }

            if (EditedAsset.Name == null)
            {
                return BadRequest("the name cannot be null");
            }

            Asset AssetExists = Unit_Of_Work.asset_Repository.First_Or_Default(s => s.ID == EditedAsset.ID && s.IsDeleted != true);
            if (AssetExists == null || AssetExists.IsDeleted == true)
            {
                return NotFound("No Asset with this ID");
            }

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedAsset.AccountNumberID);

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

                if (account.LinkFileID != 9)
                {
                    return BadRequest("Wrong Link File, it should be Asset file link");
                }
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Asset", roleId, userId, AssetExists);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(EditedAsset, AssetExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AssetExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AssetExists.UpdatedByOctaId = userId;
                if (AssetExists.UpdatedByUserId != null)
                {
                    AssetExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                AssetExists.UpdatedByUserId = userId;
                if (AssetExists.UpdatedByOctaId != null)
                {
                    AssetExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.asset_Repository.Update(AssetExists);
            Unit_Of_Work.SaveChanges();
            return Ok(EditedAsset);
        }

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Asset" }
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
                return BadRequest("Enter Asset ID");
            }

            Asset asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (asset == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Asset", roleId, userId, asset);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            asset.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            asset.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                asset.DeletedByOctaId = userId;
                if (asset.DeletedByUserId != null)
                {
                    asset.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                asset.DeletedByUserId = userId;
                if (asset.DeletedByOctaId != null)
                {
                    asset.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.asset_Repository.Update(asset);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
