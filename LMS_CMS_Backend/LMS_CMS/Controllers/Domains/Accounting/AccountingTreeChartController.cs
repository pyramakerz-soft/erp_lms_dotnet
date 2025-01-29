using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Page = LMS_CMS_DAL.Models.Domains.Page;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountingTreeChartController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public AccountingTreeChartController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        private async Task<List<AccountingTreeChart>> GetAllChildren(long parentId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var children = new List<AccountingTreeChart>();

            var parentNode = await Unit_Of_Work.accountingTreeChart_Repository.FindByIncludesAsync(
                tree => tree.IsDeleted != true && tree.ID == parentId,
                query => query.Include(tree => tree.ChildAccountingTreeCharts) 
            );

            if (parentNode != null)
            { 
                await AddChildrenRecursively(parentNode.ID, children, Unit_Of_Work);
            }

            return children;
        }

        private async Task AddChildrenRecursively(long parentId, List<AccountingTreeChart> allChildren, UOW Unit_Of_Work)
        {
            var directChildren = await Unit_Of_Work.accountingTreeChart_Repository
                .Select_All_With_IncludesById<AccountingTreeChart>(
                    tree => tree.IsDeleted != true && tree.MainAccountNumberID == parentId,
                    query => query.Include(tree => tree.ChildAccountingTreeCharts)
                );

            foreach (var child in directChildren)
            {
                allChildren.Add(child);

                await AddChildrenRecursively(child.ID, allChildren, Unit_Of_Work);
            }
        }

        private List<AccountingTreeChartGetDTO> BuildHierarchy(List<AccountingTreeChart> accountingTreeCharts, long? parentId = null)
        {
            return accountingTreeCharts
                .Where(ac => ac.MainAccountNumberID == parentId)
                .Select(ac => new AccountingTreeChartGetDTO
                {
                    ID = ac.ID,
                    Name = ac.Name,
                    Level = ac.Level,
                    SubTypeID = ac.SubTypeID,
                    SubTypeName = ac.SubType?.Name ?? "",
                    EndTypeID = ac.EndTypeID,
                    EndTypeName = ac.EndType?.Name ?? "",
                    MainAccountNumberID = ac.MainAccountNumberID ?? 0,
                    MainAccountNumberName = ac.Parent?.Name ?? "",
                    LinkFileID = ac.LinkFileID ?? 0,
                    LinkFileName = ac.LinkFile?.Name ?? "",
                    MotionTypeID = ac.MotionTypeID,
                    MotionTypeName = ac.MotionType?.Name ?? "",
                    InsertedByUserId = ac.InsertedByUserId ?? 0,
                    Children = BuildHierarchy(accountingTreeCharts, ac.ID) 
                }).ToList();
        }

        private List<AccountingTreeChartGetDTO> RemoveChildById(List<AccountingTreeChartGetDTO> children, long id)
        {
            return children
                .Where(child => child.ID != id)
                .Select(child => new AccountingTreeChartGetDTO
                {
                    ID = child.ID,
                    Name = child.Name,
                    Level = child.Level,
                    SubTypeID = child.SubTypeID,
                    SubTypeName = child.SubTypeName,
                    EndTypeID = child.EndTypeID,
                    EndTypeName = child.EndTypeName,
                    MainAccountNumberID = child.MainAccountNumberID,
                    MainAccountNumberName = child.MainAccountNumberName,
                    LinkFileID = child.LinkFileID,
                    LinkFileName = child.LinkFileName,
                    MotionTypeID = child.MotionTypeID,
                    MotionTypeName = child.MotionTypeName,
                    InsertedByUserId = child.InsertedByUserId,
                    Children = RemoveChildById(child.Children, id)  
                }).ToList();
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting" }
        )]
        public async Task<IActionResult> GetAll()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<AccountingTreeChart> accountingTreeCharts = await Unit_Of_Work.accountingTreeChart_Repository.Select_All_With_IncludesById<AccountingTreeChart>(
                    f => f.IsDeleted != true,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.MotionType),
                    query => query.Include(ac => ac.SubType),
                    query => query.Include(ac => ac.EndType),
                    query => query.Include(ac => ac.Parent)
                    );

            if (accountingTreeCharts == null || accountingTreeCharts.Count == 0)
            {
                return NotFound();
            }

            List<AccountingTreeChartGetDTO> accountingTreeChartsDTO = BuildHierarchy(accountingTreeCharts);

            return Ok(accountingTreeChartsDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting" }
        )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Account Tree Chart ID");
            }

            AccountingTreeChart accountingTreeChart = await Unit_Of_Work.accountingTreeChart_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.MotionType),
                    query => query.Include(ac => ac.SubType),
                    query => query.Include(ac => ac.EndType),
                    query => query.Include(ac => ac.Parent)
                    );

            if (accountingTreeChart == null)
            {
                return NotFound();
            }

            AccountingTreeChartGetDTO accountingTreeChartGetDTO = mapper.Map<AccountingTreeChartGetDTO>(accountingTreeChart);

            return Ok(accountingTreeChartGetDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetBySubAndLinkFileId/{linkFileID}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting" }
        )]
        public async Task<IActionResult> GetBySubAndLinkFileId(long linkFileID)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            
            if (linkFileID == 0)
            {
                return BadRequest("Enter Link File ID");
            } 

            LinkFile linkFile = Unit_Of_Work.linkFile_Repository.Select_By_Id(linkFileID);
            if (linkFile == null)
            {
                return NotFound("No Link File with this Id");
            }

            List<AccountingTreeChart> accountingTreeCharts = await Unit_Of_Work.accountingTreeChart_Repository.Select_All_With_IncludesById<AccountingTreeChart>(
                    f => f.IsDeleted != true && f.SubTypeID == 2 && f.LinkFileID == linkFileID,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.MotionType),
                    query => query.Include(ac => ac.SubType),
                    query => query.Include(ac => ac.EndType),
                    query => query.Include(ac => ac.Parent)
                    );

            if (accountingTreeCharts == null || accountingTreeCharts.Count == 0)
            {
                return NotFound();
            }

            List<AccountingTreeChartGetDTO> accountingTreeChartsDTO = mapper.Map<List<AccountingTreeChartGetDTO>>(accountingTreeCharts);

            return Ok(accountingTreeChartsDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByMainId")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting" }
        )]
        public async Task<IActionResult> GetByMainId()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<AccountingTreeChart> accountingTreeCharts = await Unit_Of_Work.accountingTreeChart_Repository.Select_All_With_IncludesById<AccountingTreeChart>(
                    f => f.IsDeleted != true && f.SubTypeID == 1,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.MotionType),
                    query => query.Include(ac => ac.SubType),
                    query => query.Include(ac => ac.EndType),
                    query => query.Include(ac => ac.Parent)
                    );

            if (accountingTreeCharts == null || accountingTreeCharts.Count == 0)
            {
                return NotFound();
            }

            List<AccountingTreeChartGetDTO> accountingTreeChartsDTO = mapper.Map<List<AccountingTreeChartGetDTO>>(accountingTreeCharts);

            return Ok(accountingTreeChartsDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetMainDataChildFiltered/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting" }
        )]
        public async Task<IActionResult> GetMainDataChildFiltered(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<AccountingTreeChart> accountingTreeCharts = await Unit_Of_Work.accountingTreeChart_Repository.Select_All_With_IncludesById<AccountingTreeChart>(
                    f => f.IsDeleted != true && f.SubTypeID == 1,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.MotionType),
                    query => query.Include(ac => ac.SubType),
                    query => query.Include(ac => ac.EndType),
                    query => query.Include(ac => ac.Parent)
                    );

            if (accountingTreeCharts == null || accountingTreeCharts.Count == 0)
            {
                return NotFound();
            }

            List<AccountingTreeChartGetDTO> accountingTreeChartsDTO = BuildHierarchy(accountingTreeCharts);

            accountingTreeChartsDTO = accountingTreeChartsDTO
                .Where(ac => ac.ID != id)
                .Select(ac => new AccountingTreeChartGetDTO
                {
                    ID = ac.ID,
                    Name = ac.Name,
                    Level = ac.Level,
                    SubTypeID = ac.SubTypeID,
                    SubTypeName = ac.SubTypeName,
                    EndTypeID = ac.EndTypeID,
                    EndTypeName = ac.EndTypeName,
                    MainAccountNumberID = ac.MainAccountNumberID,
                    MainAccountNumberName = ac.MainAccountNumberName,
                    LinkFileID = ac.LinkFileID,
                    LinkFileName = ac.LinkFileName,
                    MotionTypeID = ac.MotionTypeID,
                    MotionTypeName = ac.MotionTypeName,
                    InsertedByUserId = ac.InsertedByUserId,
                    Children = RemoveChildById(ac.Children, id)
                }).ToList();

            List<AccountingTreeChartGetDTO> flatList = new List<AccountingTreeChartGetDTO>();

            void FlattenHierarchy(IEnumerable<AccountingTreeChartGetDTO> nodes)
            {
                foreach (var node in nodes)
                {
                    flatList.Add(new AccountingTreeChartGetDTO
                    {
                        ID = node.ID,
                        Name = node.Name,
                        Level = node.Level,
                        SubTypeID = node.SubTypeID,
                        SubTypeName = node.SubTypeName,
                        EndTypeID = node.EndTypeID,
                        EndTypeName = node.EndTypeName,
                        MainAccountNumberID = node.MainAccountNumberID,
                        MainAccountNumberName = node.MainAccountNumberName,
                        LinkFileID = node.LinkFileID,
                        LinkFileName = node.LinkFileName,
                        MotionTypeID = node.MotionTypeID,
                        MotionTypeName = node.MotionTypeName,
                        InsertedByUserId = node.InsertedByUserId
                    });

                    // Recursively process children
                    if (node.Children != null && node.Children.Any())
                    {
                        FlattenHierarchy(node.Children);
                    }
                }
            }

            // Initiate flattening process
            FlattenHierarchy(accountingTreeChartsDTO);

            return Ok(flatList);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Accounting" }
        )]
        public IActionResult Add(AccountingTreeChartAddDTO NewAccountingTreeChart)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (NewAccountingTreeChart == null)
            {
                return BadRequest("Accounting tree chart cannot be null");
            }

            int level = 0;
            long? motionID = null;
            long? endTypeID = null;
            bool differentEndTypeID = false;
            bool differentMotionID = false;
            bool isExistsMainAccountNumberID = false;

            AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == NewAccountingTreeChart.ID);
            if (account != null)
            {
                return BadRequest("ID already exists");
            }
             
            if (NewAccountingTreeChart.Name == null)
            {
                return BadRequest("Name cannot be null");
            }

            if (NewAccountingTreeChart.SubTypeID != 0 || NewAccountingTreeChart.SubTypeID != null)
            {
                SubType subType = Unit_Of_Work.subType_Repository.Select_By_Id(NewAccountingTreeChart.SubTypeID);
                if (subType == null)
                {
                    return NotFound("No SubType with this Id");
                }
                if (NewAccountingTreeChart.SubTypeID == 2)
                {
                    if (NewAccountingTreeChart.LinkFileID == 0 || NewAccountingTreeChart.LinkFileID == null)
                    {
                        return BadRequest("Link File cannot be null");
                    }

                    if(NewAccountingTreeChart.MainAccountNumberID == null || NewAccountingTreeChart.MainAccountNumberID == 0)
                    {
                        return BadRequest("Main Account Number ID cannot be null");
                    }

                    LinkFile linkFile = Unit_Of_Work.linkFile_Repository.Select_By_Id(NewAccountingTreeChart.LinkFileID);
                    if (linkFile == null)
                    {
                        return NotFound("No Link File with this Id");
                    }
                }
            }

            if (NewAccountingTreeChart.MainAccountNumberID == null || NewAccountingTreeChart.MainAccountNumberID == 0)
            {
                if (NewAccountingTreeChart.MotionTypeID == null || NewAccountingTreeChart.MotionTypeID == 0)
                {
                    return BadRequest("Motion type cannot be null");
                }
                
                if (NewAccountingTreeChart.EndTypeID == null || NewAccountingTreeChart.EndTypeID == 0)
                {
                    return BadRequest("End type cannot be null");
                }

                MotionType motion = Unit_Of_Work.motionType_Repository.Select_By_Id(NewAccountingTreeChart.MotionTypeID);
                if (motion == null) 
                {
                    return NotFound("No Motion Type with this Id");
                }
                
                EndType endType = Unit_Of_Work.endType_Repository.Select_By_Id(NewAccountingTreeChart.EndTypeID);
                if (endType == null) 
                {
                    return NotFound("No End Type with this Id");
                }

                level = 1;
                motionID = NewAccountingTreeChart.MotionTypeID;
                endTypeID = NewAccountingTreeChart.EndTypeID;
                NewAccountingTreeChart.MainAccountNumberID = null;
            }
            else
            {
                isExistsMainAccountNumberID = true;

                AccountingTreeChart MainAccount = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.SubTypeID == 1 && t.ID == NewAccountingTreeChart.MainAccountNumberID);
                if (MainAccount == null)
                {
                    return NotFound("No Account chart with this Id");
                }

                level = MainAccount.Level + 1;
                motionID = MainAccount.MotionTypeID;
                endTypeID = MainAccount.EndTypeID;

                if(NewAccountingTreeChart.EndTypeID != 0 || NewAccountingTreeChart.EndTypeID != null)
                {
                    if(NewAccountingTreeChart.EndTypeID != endTypeID)
                    {
                        differentEndTypeID = true;
                    }
                }

                if(NewAccountingTreeChart.MotionTypeID != 0 || NewAccountingTreeChart.MotionTypeID != null)
                {
                    if(NewAccountingTreeChart.MotionTypeID != motionID)
                    {
                        differentMotionID = true;
                    }
                }
            }

            AccountingTreeChart accountingTreeChart = mapper.Map<AccountingTreeChart>(NewAccountingTreeChart);

            if(accountingTreeChart.SubTypeID == 1)
            {
                accountingTreeChart.LinkFileID = null;
            }
            accountingTreeChart.Level = level;
            accountingTreeChart.MotionTypeID = (long)motionID;
            accountingTreeChart.EndTypeID = (long)endTypeID;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            accountingTreeChart.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                accountingTreeChart.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                accountingTreeChart.InsertedByUserId = userId;
            }

            Unit_Of_Work.accountingTreeChart_Repository.Add(accountingTreeChart);
            Unit_Of_Work.SaveChanges();

            if (isExistsMainAccountNumberID)
            {
                if (differentMotionID && differentEndTypeID)
                {
                    return Ok(new { message = "Done But We Chosed Motion Type and End Type from Parent.", data = NewAccountingTreeChart });
                }
                else if (differentMotionID)
                {
                    return Ok(new { message = "Done But We Chosed Motion Type from Parent.", data = NewAccountingTreeChart });
                }
                else if (differentEndTypeID)
                {
                    return Ok(new { message = "Done But We Chosed End Type from Parent.", data = NewAccountingTreeChart });
                }
                else
                {
                    return Ok(NewAccountingTreeChart);
                }
            }
            else
            {
                return Ok(NewAccountingTreeChart);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowEdit: 1,
          pages: new[] { "Accounting" }
        )]
        public async Task<IActionResult> Edit(AccountingTreeChartAddDTO EditedAccountingTreeChart)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (EditedAccountingTreeChart == null)
            {
                return BadRequest("Accounting tree chart cannot be null");
            }

            if (EditedAccountingTreeChart.Name == null)
            {
                return BadRequest("Name cannot be null");
            }

            AccountingTreeChart accountExists = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedAccountingTreeChart.ID);
            var oldAccExMain = accountExists.MainAccountNumberID;

            if (accountExists == null)
            {
                return BadRequest("No Accounting Tree with this ID");
            }

            SubType subtype = Unit_Of_Work.subType_Repository.Select_By_Id(EditedAccountingTreeChart.SubTypeID);
            if (subtype == null)
            {
                return BadRequest("No SubType with this ID");
            }

            if (EditedAccountingTreeChart.ID == EditedAccountingTreeChart.MainAccountNumberID)
            {
                return BadRequest("Main Accounting ID Can't be same as ID");
            }
            // 1) If He want to change from main to sub or from sub to main
            if (accountExists.SubTypeID == 1 && EditedAccountingTreeChart.SubTypeID == 2)
            {
                return BadRequest("You can't change subType from Main to Sub");
            }

            if (EditedAccountingTreeChart.SubTypeID == 1 || (accountExists.SubTypeID == 2 && EditedAccountingTreeChart.SubTypeID == 1))
            {
                EditedAccountingTreeChart.LinkFileID = null;
            }

            if (EditedAccountingTreeChart.SubTypeID == 2)
            {
                if (EditedAccountingTreeChart.LinkFileID == 0 || EditedAccountingTreeChart.LinkFileID == null)
                {
                    return BadRequest("Link File cannot be null");
                }

                if (EditedAccountingTreeChart.MainAccountNumberID == null || EditedAccountingTreeChart.MainAccountNumberID == 0)
                {
                    return BadRequest("Main Accounting Number ID cannot be null");
                }

                LinkFile linkFile = Unit_Of_Work.linkFile_Repository.Select_By_Id(EditedAccountingTreeChart.LinkFileID);
                if (linkFile == null)
                {
                    return NotFound("No Link File with this Id");
                }
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (accountExists.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting page doesn't exist");
                }
            }


            bool isEndTypeDiff = false;
            bool isMotionTypeDiff = false;

            // If Account Main Changed
            List<AccountingTreeChart> Children = new List<AccountingTreeChart>();
            Children = await GetAllChildren(EditedAccountingTreeChart.ID);
            Children = Children.OrderBy(c => c.Level).ToList();

            if ((EditedAccountingTreeChart.MainAccountNumberID ?? 0) != (accountExists.MainAccountNumberID ?? 0))
            {
                if (Children.Any(child => child.ID == EditedAccountingTreeChart.MainAccountNumberID))
                {
                    return BadRequest("You can't choose this main account as it's one of this account children");
                }

                // From main = value ---> main = null
                if (accountExists.MainAccountNumberID != null && (EditedAccountingTreeChart.MainAccountNumberID == null || EditedAccountingTreeChart.MainAccountNumberID == 0))
                {
                    if (EditedAccountingTreeChart.MotionTypeID == null || EditedAccountingTreeChart.MotionTypeID == 0)
                    {
                        return BadRequest("Motion Type Can't be null");
                    }
                    if (EditedAccountingTreeChart.EndTypeID == null || EditedAccountingTreeChart.EndTypeID == 0)
                    {
                        return BadRequest("End Type Can't be null");
                    }

                    // Can Change End Type and Motion type But change children also
                    if (accountExists.MotionTypeID != EditedAccountingTreeChart.MotionTypeID || accountExists.EndTypeID != EditedAccountingTreeChart.EndTypeID)
                    {
                        for (int i = 0; i < Children.Count; i++)
                        {
                            Children[i].EndTypeID = (long)EditedAccountingTreeChart.EndTypeID;
                            Children[i].MotionTypeID = (long)EditedAccountingTreeChart.MotionTypeID;

                            Children[i].UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                            if (userTypeClaim == "octa")
                            {
                                Children[i].UpdatedByOctaId = userId;
                                if (Children[i].UpdatedByUserId != null)
                                {
                                    Children[i].UpdatedByUserId = null;
                                }
                            }
                            else if (userTypeClaim == "employee")
                            {
                                Children[i].UpdatedByUserId = userId;
                                if (Children[i].UpdatedByOctaId != null)
                                {
                                    Children[i].UpdatedByOctaId = null;
                                }
                            }

                            Unit_Of_Work.accountingTreeChart_Repository.Update(Children[i]);
                        }
                        Unit_Of_Work.SaveChanges();
                    }
                }
                else
                {
                    // From main = null ---> main = value
                    // From main = value ---> main = new value
                    AccountingTreeChart mainAccountNewExists = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.SubTypeID == 1 && t.ID == EditedAccountingTreeChart.MainAccountNumberID);
                    if (mainAccountNewExists == null)
                    {
                        return BadRequest("No Main Accounting Tree with this ID");
                    }

                    if (mainAccountNewExists.EndTypeID != EditedAccountingTreeChart.EndTypeID)
                    {
                        EditedAccountingTreeChart.EndTypeID = mainAccountNewExists.EndTypeID;
                        isEndTypeDiff = true;
                    }
                    if (mainAccountNewExists.MotionTypeID != EditedAccountingTreeChart.MotionTypeID)
                    {
                        EditedAccountingTreeChart.MotionTypeID = mainAccountNewExists.MotionTypeID;
                        isMotionTypeDiff = true;
                    }

                    for (int i = 0; i < Children.Count; i++)
                    {
                        if (accountExists.EndTypeID != mainAccountNewExists.EndTypeID || accountExists.MotionTypeID != mainAccountNewExists.MotionTypeID)
                        {
                            Children[i].EndTypeID = mainAccountNewExists.EndTypeID;
                            Children[i].MotionTypeID = mainAccountNewExists.MotionTypeID;

                            Children[i].UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                            if (userTypeClaim == "octa")
                            {
                                Children[i].UpdatedByOctaId = userId;
                                if (Children[i].UpdatedByUserId != null)
                                {
                                    Children[i].UpdatedByUserId = null;
                                }
                            }
                            else if (userTypeClaim == "employee")
                            {
                                Children[i].UpdatedByUserId = userId;
                                if (Children[i].UpdatedByOctaId != null)
                                {
                                    Children[i].UpdatedByOctaId = null;
                                }
                            }

                        }
                        Unit_Of_Work.accountingTreeChart_Repository.Update(Children[i]);
                    }
                    Unit_Of_Work.SaveChanges();
                }
            }
            else
            {
                if (EditedAccountingTreeChart.MainAccountNumberID == null || EditedAccountingTreeChart.MainAccountNumberID == 0)
                {
                    if (EditedAccountingTreeChart.MotionTypeID == null || EditedAccountingTreeChart.MotionTypeID == 0)
                    {
                        return BadRequest("Motion Type Can't be null");
                    }
                    if (EditedAccountingTreeChart.EndTypeID == null || EditedAccountingTreeChart.EndTypeID == 0)
                    {
                        return BadRequest("End Type Can't be null");
                    }

                    EndType endType = Unit_Of_Work.endType_Repository.Select_By_Id(EditedAccountingTreeChart.EndTypeID);
                    if (endType == null)
                    {
                        return NotFound("No End Type with this Id");
                    }

                    MotionType motionType = Unit_Of_Work.motionType_Repository.Select_By_Id(EditedAccountingTreeChart.MotionTypeID);
                    if (motionType == null)
                    {
                        return NotFound("No Motion Type with this Id");
                    }

                    // Can Change End Type and Motion type But change children also
                    if (accountExists.MotionTypeID != EditedAccountingTreeChart.MotionTypeID || accountExists.EndTypeID != EditedAccountingTreeChart.EndTypeID)
                    {
                        for (int i = 0; i < Children.Count; i++)
                        {
                            Children[i].EndTypeID = (long)EditedAccountingTreeChart.EndTypeID;
                            Children[i].MotionTypeID = (long)EditedAccountingTreeChart.MotionTypeID;

                            Children[i].UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                            if (userTypeClaim == "octa")
                            {
                                Children[i].UpdatedByOctaId = userId;
                                if (Children[i].UpdatedByUserId != null)
                                {
                                    Children[i].UpdatedByUserId = null;
                                }
                            }
                            else if (userTypeClaim == "employee")
                            {
                                Children[i].UpdatedByUserId = userId;
                                if (Children[i].UpdatedByOctaId != null)
                                {
                                    Children[i].UpdatedByOctaId = null;
                                }
                            }

                            Unit_Of_Work.accountingTreeChart_Repository.Update(Children[i]);
                        }
                        Unit_Of_Work.SaveChanges();
                    }
                }
                else if (EditedAccountingTreeChart.MainAccountNumberID != null && EditedAccountingTreeChart.MainAccountNumberID != 0)
                {
                    AccountingTreeChart accountingTreeChartEx = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(
                    acc => acc.IsDeleted != true && acc.ID == EditedAccountingTreeChart.MainAccountNumberID);

                    if (accountingTreeChartEx == null)
                    {
                        return NotFound("No Main Acc with this ID");
                    }

                    if (accountingTreeChartEx.EndTypeID != EditedAccountingTreeChart.EndTypeID)
                    {
                        EditedAccountingTreeChart.EndTypeID = accountingTreeChartEx.EndTypeID;
                        isEndTypeDiff = true;
                    }
                    if (accountingTreeChartEx.MotionTypeID != EditedAccountingTreeChart.MotionTypeID)
                    {
                        EditedAccountingTreeChart.MotionTypeID = accountingTreeChartEx.MotionTypeID;
                        isMotionTypeDiff = true;
                    }
                }
            }

            mapper.Map(EditedAccountingTreeChart, accountExists);

            if (accountExists.MainAccountNumberID != null && accountExists.MainAccountNumberID != 0)
            {
                AccountingTreeChart acc = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(
                    acc => acc.IsDeleted != true && acc.ID == accountExists.MainAccountNumberID
                    );
                if (acc != null)
                {
                    accountExists.Level = acc.Level + 1;
                }
                else
                {
                    return BadRequest("No Main Accounting with this ID");
                }
            }
            else
            {
                accountExists.Level = 1;
                accountExists.MainAccountNumberID = null;
            }

            if ((EditedAccountingTreeChart.MainAccountNumberID ?? 0) != (oldAccExMain ?? 0))
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    AccountingTreeChart acc = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(
                        acc => acc.IsDeleted != true && acc.ID == Children[i].MainAccountNumberID
                        );
                    Children[i].Level = acc.Level + 1;

                    Children[i].UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                    if (userTypeClaim == "octa")
                    {
                        Children[i].UpdatedByOctaId = userId;
                        if (Children[i].UpdatedByUserId != null)
                        {
                            Children[i].UpdatedByUserId = null;
                        }
                    }
                    else if (userTypeClaim == "employee")
                    {
                        Children[i].UpdatedByUserId = userId;
                        if (Children[i].UpdatedByOctaId != null)
                        {
                            Children[i].UpdatedByOctaId = null;
                        }
                    }

                    Unit_Of_Work.accountingTreeChart_Repository.Update(Children[i]);
                }
                Unit_Of_Work.SaveChanges();
            }

            accountExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                accountExists.UpdatedByOctaId = userId;
                if (accountExists.UpdatedByUserId != null)
                {
                    accountExists.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                accountExists.UpdatedByUserId = userId;
                if (accountExists.UpdatedByOctaId != null)
                {
                    accountExists.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingTreeChart_Repository.Update(accountExists);
            Unit_Of_Work.SaveChanges();

            if (isEndTypeDiff && isMotionTypeDiff)
            {
                return Ok(new { message = "Done But We Chosed Motion Type and End Type from Parent." });
            }
            else if (isMotionTypeDiff)
            {
                return Ok(new { message = "Done But We Chosed Motion Type from Parent." });
            }
            else if (isEndTypeDiff)
            {
                return Ok(new { message = "Done But We Chosed End Type from Parent." });
            }
            else
            {
                return Ok(new { message = "Done" });
            }
        }
    }
}
