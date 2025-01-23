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

            AccountingTreeChart accountExists = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedAccountingTreeChart.ID);
            if (accountExists == null)
            {
                return BadRequest("No Accounting Tree with this ID");
            }

            // 1) If He want to change from main to sub or from sub to main
            if(accountExists.SubTypeID == 1 && EditedAccountingTreeChart.SubTypeID == 2)
            {
                return BadRequest("You can't change subType from Main to Sub");
            }

            if(EditedAccountingTreeChart.SubTypeID == 1 || (accountExists.SubTypeID == 2 && EditedAccountingTreeChart.SubTypeID == 1))
            {
                EditedAccountingTreeChart.LinkFileID = null;
            }

            // If Account Main Changed
            List<AccountingTreeChart> Children = new List<AccountingTreeChart>();
            Children = await GetAllChildrenAsync(EditedAccountingTreeChart.ID);

            bool differentEndTypeID = false;
            bool differentMotionID = false;

            if (EditedAccountingTreeChart.MainAccountNumberID != accountExists.MainAccountNumberID)
            {
                if(Children.Any(child => child.ID == EditedAccountingTreeChart.MainAccountNumberID))
                {
                    return BadRequest("You can't choose this main account as it's one of this account children");
                }
                 
                AccountingTreeChart mainAccountExists = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.SubTypeID == 1 && t.ID == EditedAccountingTreeChart.MainAccountNumberID);
                if (accountExists == null)
                {
                    return BadRequest("No Main Accounting Tree with this ID");
                }

                if (mainAccountExists.EndTypeID != accountExists.EndTypeID || mainAccountExists.MotionTypeID != accountExists.MotionTypeID)
                {
                    if (EditedAccountingTreeChart.MotionTypeID != mainAccountExists.MotionTypeID)
                    {
                        differentMotionID = true;
                        EditedAccountingTreeChart.MotionTypeID = mainAccountExists.MotionTypeID;
                    }

                    if (EditedAccountingTreeChart.EndTypeID != mainAccountExists.EndTypeID)
                    {
                        differentEndTypeID = true;
                        EditedAccountingTreeChart.EndTypeID = mainAccountExists.EndTypeID;
                    }

                    EditedAccountingTreeChart.MotionTypeID = mainAccountExists.MotionTypeID;
                    EditedAccountingTreeChart.EndTypeID = mainAccountExists.EndTypeID;

                    for(int i = 0; i < Children.Count; i++)
                    {
                        Children[i].EndTypeID = mainAccountExists.EndTypeID;
                        Children[i].MotionTypeID = mainAccountExists.MotionTypeID;
                        Children[i].Level = mainAccountExists.Level + 1;
                         
                        TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
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

            }

            return Ok(Children.Count);

            
            


            //int level = 0;
            //bool isExistsMainAccountNumberID = false;

            //if (EditedAccountingTreeChart.Name == null)
            //{
            //    return BadRequest("Name cannot be null");
            //}

            //if (EditedAccountingTreeChart.SubTypeID != 0 || EditedAccountingTreeChart.SubTypeID != null)
            //{
            //    SubType subType = Unit_Of_Work.subType_Repository.Select_By_Id(EditedAccountingTreeChart.SubTypeID);
            //    if (subType == null)
            //    {
            //        return NotFound("No SubType with this Id");
            //    }
            //    if (EditedAccountingTreeChart.SubTypeID == 2)
            //    {
            //        if (EditedAccountingTreeChart.LinkFileID == 0 || EditedAccountingTreeChart.LinkFileID == null)
            //        {
            //            return BadRequest("Link File cannot be null");
            //        }

            //        if (EditedAccountingTreeChart.MainAccountNumberID == null || EditedAccountingTreeChart.MainAccountNumberID == 0)
            //        {
            //            return BadRequest("Main Account Number ID cannot be null");
            //        }

            //        LinkFile linkFile = Unit_Of_Work.linkFile_Repository.Select_By_Id(EditedAccountingTreeChart.LinkFileID);
            //        if (linkFile == null)
            //        {
            //            return NotFound("No Link File with this Id");
            //        }
            //    }
            //}

            //if (EditedAccountingTreeChart.MainAccountNumberID == null || EditedAccountingTreeChart.MainAccountNumberID == 0)
            //{
            //    if (EditedAccountingTreeChart.MotionTypeID == null || EditedAccountingTreeChart.MotionTypeID == 0)
            //    {
            //        return BadRequest("Motion type cannot be null");
            //    }

            //    if (EditedAccountingTreeChart.EndTypeID == null || EditedAccountingTreeChart.EndTypeID == 0)
            //    {
            //        return BadRequest("End type cannot be null");
            //    }

            //    MotionType motion = Unit_Of_Work.motionType_Repository.Select_By_Id(EditedAccountingTreeChart.MotionTypeID);
            //    if (motion == null)
            //    {
            //        return NotFound("No Motion Type with this Id");
            //    }

            //    EndType endType = Unit_Of_Work.endType_Repository.Select_By_Id(EditedAccountingTreeChart.EndTypeID);
            //    if (endType == null)
            //    {
            //        return NotFound("No End Type with this Id");
            //    }

            //    level = 1;
            //    motionID = EditedAccountingTreeChart.MotionTypeID;
            //    endTypeID = EditedAccountingTreeChart.EndTypeID;
            //    EditedAccountingTreeChart.MainAccountNumberID = null;
            //}
            //else
            //{
            //    isExistsMainAccountNumberID = true;

            //    AccountingTreeChart MainAccount = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == EditedAccountingTreeChart.MainAccountNumberID);
            //    if (MainAccount == null)
            //    {
            //        return NotFound("No Account chart with this Id");
            //    }

            //    level = MainAccount.Level + 1;
            //    motionID = MainAccount.MotionTypeID;
            //    endTypeID = MainAccount.EndTypeID;

            //    if (EditedAccountingTreeChart.EndTypeID != 0 || EditedAccountingTreeChart.EndTypeID != null)
            //    {
            //        if (EditedAccountingTreeChart.EndTypeID != endTypeID)
            //        {
            //            differentEndTypeID = true;
            //        }
            //    }

            //    if (EditedAccountingTreeChart.MotionTypeID != 0 || EditedAccountingTreeChart.MotionTypeID != null)
            //    {
            //        if (EditedAccountingTreeChart.MotionTypeID != motionID)
            //        {
            //            differentMotionID = true;
            //        }
            //    }
            //}

            //mapper.Map(EditedAccountingTreeChart, accountExists);

            //if (accountExists.SubTypeID == 1)
            //{
            //    accountExists.LinkFileID = null;
            //}
            //accountExists.Level = level;
            //accountExists.MotionTypeID = (long)motionID;
            //accountExists.EndTypeID = (long)endTypeID;

            //if (userTypeClaim == "employee")
            //{
            //    Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting");
            //    if (page != null)
            //    {
            //        Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
            //        if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
            //        {
            //            if (accountExists.InsertedByUserId != userId)
            //            {
            //                return Unauthorized();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return BadRequest("Accounting page doesn't exist");
            //    }
            //}

            //TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //accountExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            //if (userTypeClaim == "octa")
            //{
            //    accountExists.UpdatedByOctaId = userId;
            //    if (accountExists.UpdatedByUserId != null)
            //    {
            //        accountExists.UpdatedByUserId = null;
            //    }
            //}
            //else if (userTypeClaim == "employee")
            //{
            //    accountExists.UpdatedByUserId = userId;
            //    if (accountExists.UpdatedByOctaId != null)
            //    {
            //        accountExists.UpdatedByOctaId = null;
            //    }
            //}

            //Unit_Of_Work.accountingTreeChart_Repository.Update(accountExists);
            //Unit_Of_Work.SaveChanges();

            //if (isExistsMainAccountNumberID)
            //{
            //    if (differentMotionID && differentEndTypeID)
            //    {
            //        return Ok(new { message = "Done But We Chosed Motion Type and End Type from Parent.", data = accountExists });
            //    }
            //    else if (differentMotionID)
            //    {
            //        return Ok(new { message = "Done But We Chosed Motion Type from Parent.", data = accountExists });
            //    }
            //    else if (differentEndTypeID)
            //    {
            //        return Ok(new { message = "Done But We Chosed End Type from Parent.", data = accountExists });
            //    }
            //    else
            //    {
            //        return Ok(accountExists);
            //    }
            //}
            //else
            //{
            //    return Ok(accountExists);
            //}
        }

        private async Task<List<AccountingTreeChart>> GetAllChildrenAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            
            var node = await Unit_Of_Work.accountingTreeChart_Repository.FindByIncludesAsync(
                tree => tree.IsDeleted != true && tree.ID == id,
                query => query.Include(tree => tree.ChildAccountingTreeCharts)
            ); 

            if (node == null)
            {
                return new List<AccountingTreeChart>(); 
            }

            if (!node.ChildAccountingTreeCharts.Any())
            {
                // Log or handle the case where there are no direct children
                Console.WriteLine($"Node with ID {id} has no children.");
            }

            var children = new List<AccountingTreeChart>();
            AddChildren(node, children);
            return children;
        }

        private void AddChildren(AccountingTreeChart node, List<AccountingTreeChart> children)
        {
            // Add direct children
            children.AddRange(node.ChildAccountingTreeCharts);

            // Recursively add each child's children
            foreach (var child in node.ChildAccountingTreeCharts)
            {
                AddChildren(child, children);
            }
        }
    }
}
