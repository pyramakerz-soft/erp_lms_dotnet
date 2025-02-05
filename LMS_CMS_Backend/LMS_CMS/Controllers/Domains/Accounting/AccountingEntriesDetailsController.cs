using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class AccountingEntriesDetailsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public AccountingEntriesDetailsController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////

        [HttpGet("GetByMasterID/{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Accounting Entries Details", "Accounting" }
        )]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<AccountingEntriesDetails> AccountingEntriesDetails = await Unit_Of_Work.accountingEntriesDetails_Repository.Select_All_With_IncludesById<AccountingEntriesDetails>(
                    t => t.IsDeleted != true && t.AccountingEntriesMasterID == id,
                    query => query.Include(Master => Master.AccountingTreeChart),
                    query => query.Include(Master => Master.AccountingEntriesMaster)
                    );

            if (AccountingEntriesDetails == null || AccountingEntriesDetails.Count == 0)
            {
                return NotFound();
            }

            var suppliersIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 2).Select(r => r.SubAccountingID).Distinct().ToList();
            var debitIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 3).Select(r => r.SubAccountingID).Distinct().ToList();
            var creditsIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 4).Select(r => r.SubAccountingID).Distinct().ToList();
            var saveIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 5).Select(r => r.SubAccountingID).Distinct().ToList();
            var bankIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 6).Select(r => r.SubAccountingID).Distinct().ToList();
            var incomesIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 7).Select(r => r.SubAccountingID).Distinct().ToList();
            var outcomesIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 8).Select(r => r.SubAccountingID).Distinct().ToList();
            var assetsIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 9).Select(r => r.SubAccountingID).Distinct().ToList();
            var employeesIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 10).Select(r => r.SubAccountingID).Distinct().ToList();
            var feeIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 11).Select(r => r.SubAccountingID).Distinct().ToList();
            var discountIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 12).Select(r => r.SubAccountingID).Distinct().ToList();
            var studentIds = AccountingEntriesDetails.Where(r => r.AccountingTreeChart.LinkFileID == 13).Select(r => r.SubAccountingID).Distinct().ToList();

            var banks = await Unit_Of_Work.bank_Repository.Select_All_With_IncludesById<Bank>(b => bankIds.Contains(b.ID));
            var saves = await Unit_Of_Work.save_Repository.Select_All_With_IncludesById<Save>(s => saveIds.Contains(s.ID));
            var Suppliers = await Unit_Of_Work.supplier_Repository.Select_All_With_IncludesById<Supplier>(s => saveIds.Contains(s.ID));
            var Debit = await Unit_Of_Work.debit_Repository.Select_All_With_IncludesById<Debit>(s => saveIds.Contains(s.ID));
            var Credits = await Unit_Of_Work.credit_Repository.Select_All_With_IncludesById<Credit>(s => saveIds.Contains(s.ID));
            var Incomes = await Unit_Of_Work.income_Repository.Select_All_With_IncludesById<Income>(s => saveIds.Contains(s.ID));
            var Outcomes = await Unit_Of_Work.outcome_Repository.Select_All_With_IncludesById<Outcome>(s => saveIds.Contains(s.ID));
            var Assets = await Unit_Of_Work.asset_Repository.Select_All_With_IncludesById<Asset>(s => saveIds.Contains(s.ID));
            var Employees = await Unit_Of_Work.employee_Repository.Select_All_With_IncludesById<Employee>(s => saveIds.Contains(s.ID));
            var Fees = await Unit_Of_Work.tuitionFeesType_Repository.Select_All_With_IncludesById<TuitionFeesType>(s => saveIds.Contains(s.ID));
            var Discount = await Unit_Of_Work.tuitionDiscountType_Repository.Select_All_With_IncludesById<TuitionDiscountType>(s => saveIds.Contains(s.ID));
            var Students = await Unit_Of_Work.student_Repository.Select_All_With_IncludesById<Student>(s => saveIds.Contains(s.ID));

            List<AccountingEntriesDetailsGetDTO> DTOs = mapper.Map<List<AccountingEntriesDetailsGetDTO>>(AccountingEntriesDetails);

            foreach (var dto in DTOs)
            {
                AccountingTreeChart acc = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(
                    ac => ac.ID == dto.AccountingTreeChartID && ac.IsDeleted != true
                    );

                if (acc.LinkFileID == 6) // Bank
                {
                    var bank = banks.FirstOrDefault(b => b.ID == dto.SubAccountingID);
                    dto.SubAccountingName = bank?.Name;
                }
                else if (acc.LinkFileID == 5) // Save
                {
                    var save = saves.FirstOrDefault(s => s.ID == dto.SubAccountingID);
                    dto.SubAccountingName = save?.Name;
                }
                else if (acc.LinkFileID == 2) // Supplier
                {
                    var supplier = Suppliers.FirstOrDefault(s => s.ID == dto.SubAccountingID);
                    dto.SubAccountingName = supplier?.Name;
                }
                else if (acc.LinkFileID == 3) // Debit
                {
                    var debit = Debit.FirstOrDefault(d => d.ID == dto.SubAccountingID);
                    dto.SubAccountingName = debit?.Name;
                }
                else if (acc.LinkFileID == 4) // Credit
                {
                    var credit = Credits.FirstOrDefault(c => c.ID == dto.SubAccountingID);
                    dto.SubAccountingName = credit?.Name;
                }
                else if (acc.LinkFileID == 7) // Income
                {
                    var income = Incomes.FirstOrDefault(i => i.ID == dto.SubAccountingID);
                    dto.SubAccountingName = income?.Name;
                }
                else if (acc.LinkFileID == 8) // Outcome
                {
                    var outcome = Outcomes.FirstOrDefault(o => o.ID == dto.SubAccountingID);
                    dto.SubAccountingName = outcome?.Name;
                }
                else if (acc.LinkFileID == 9) // Asset
                {
                    var asset = Assets.FirstOrDefault(a => a.ID == dto.SubAccountingID);
                    dto.SubAccountingName = asset?.Name;
                }
                else if (acc.LinkFileID == 10) // Employee
                {
                    var employee = Employees.FirstOrDefault(e => e.ID == dto.SubAccountingID);
                    dto.SubAccountingName = employee?.en_name;
                }
                else if (acc.LinkFileID == 11) // Fee
                {
                    var fee = Fees.FirstOrDefault(f => f.ID == dto.SubAccountingID);
                    dto.SubAccountingName = fee?.Name;
                }
                else if (acc.LinkFileID == 12) // Discount
                {
                    var discount = Discount.FirstOrDefault(d => d.ID == dto.SubAccountingID);
                    dto.SubAccountingName = discount?.Name;
                }
                else if (acc.LinkFileID == 13) // Student
                {
                    var student = Students.FirstOrDefault(s => s.ID == dto.SubAccountingID);
                    dto.SubAccountingName = student?.en_name;
                }
            }

            return Ok(DTOs);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting Entries Details", "Accounting" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Enter Accounting Entries Details ID");
            }

            AccountingEntriesDetails AccountingEntriesDetails = await Unit_Of_Work.accountingEntriesDetails_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.AccountingTreeChart),
                    query => query.Include(ac => ac.AccountingEntriesMaster)
                    );

            if (AccountingEntriesDetails == null)
            {
                return NotFound();
            }

            string SubAccountingName = null;

            if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 6) // Bank
            {
                var bank = await Unit_Of_Work.bank_Repository.FindByIncludesAsync(b => b.IsDeleted != true && b.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = bank?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 5) // Save
            {
                var save = await Unit_Of_Work.save_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = save?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 2) // Supplier
            {
                var supplier = await Unit_Of_Work.supplier_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = supplier?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 3) // Debit
            {
                var debit = await Unit_Of_Work.debit_Repository.FindByIncludesAsync(d => d.IsDeleted != true && d.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = debit?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 4) // Credit
            {
                var credit = await Unit_Of_Work.credit_Repository.FindByIncludesAsync(c => c.IsDeleted != true && c.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = credit?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 7) // Income
            {
                var income = await Unit_Of_Work.income_Repository.FindByIncludesAsync(i => i.IsDeleted != true && i.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = income?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 8) // Outcome
            {
                var outcome = await Unit_Of_Work.outcome_Repository.FindByIncludesAsync(o => o.IsDeleted != true && o.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = outcome?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 9) // Asset
            {
                var asset = await Unit_Of_Work.asset_Repository.FindByIncludesAsync(a => a.IsDeleted != true && a.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = asset?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 10) // Employee
            {
                var employee = await Unit_Of_Work.employee_Repository.FindByIncludesAsync(e => e.IsDeleted != true && e.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = employee?.en_name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 11) // Fee
            {
                var fee = await Unit_Of_Work.tuitionFeesType_Repository.FindByIncludesAsync(f => f.IsDeleted != true && f.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = fee?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 12) // Discount
            {
                var discount = await Unit_Of_Work.tuitionDiscountType_Repository.FindByIncludesAsync(d => d.IsDeleted != true && d.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = discount?.Name;
            }
            else if (AccountingEntriesDetails.AccountingTreeChart.LinkFileID == 13) // Student
            {
                var student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == AccountingEntriesDetails.SubAccountingID);
                SubAccountingName = student?.en_name;
            }

            AccountingEntriesDetailsGetDTO dto = mapper.Map<AccountingEntriesDetailsGetDTO>(AccountingEntriesDetails);
            dto.SubAccountingName = SubAccountingName;

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Accounting Entries Details", "Accounting" }
        )]
        public IActionResult Add(AccountingEntriesDetailsAddDTO newDetails)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newDetails == null)
            {
                return BadRequest("Accounting Entries Details cannot be null");
            }

            AccountingEntriesMaster AccountingEntriesMaster = Unit_Of_Work.accountingEntriesMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newDetails.AccountingEntriesMasterID);
            if (AccountingEntriesMaster == null)
            {
                return BadRequest("There is no Accounting Entries Details with this ID");
            }

            AccountingTreeChart AccountingTreeChart = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.ID == newDetails.AccountingTreeChartID && t.SubTypeID == 2);
            if (AccountingTreeChart == null)
            {
                return BadRequest("There is no Sub Accounting Tree Chart with this ID");
            }

            AccountingEntriesDetails AccountingEntriesDetails;

            if (AccountingTreeChart.LinkFileID != null)
            {
                if(newDetails.SubAccountingID != null || newDetails.SubAccountingID != 0)
                { 
                    if (AccountingTreeChart.LinkFileID == 6) // Bank
                    {
                        Bank Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (Bank == null)
                        {
                            return BadRequest("There is no Bank with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 5) // Save
                    {
                        Save Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (Save == null)
                        {
                            return BadRequest("There is no Save with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 2) // Supplier
                    {
                        Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (supplier == null)
                        {
                            return BadRequest("There is no Supplier with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 3) // Debit
                    {
                        Debit debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (debit == null)
                        {
                            return BadRequest("There is no Debit with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 4) // Credit
                    {
                        Credit credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (credit == null)
                        {
                            return BadRequest("There is no Credit with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 7) // Income
                    {
                        Income income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (income == null)
                        {
                            return BadRequest("There is no Income with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 8) // Outcome
                    {
                        Outcome outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (outcome == null)
                        {
                            return BadRequest("There is no Outcome with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 9) // Asset
                    {
                        Asset asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (asset == null)
                        {
                            return BadRequest("There is no Asset with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 10) // Employee
                    {
                        Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (employee == null)
                        {
                            return BadRequest("There is no Employee with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 11) // Fee
                    {
                        TuitionFeesType fee = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (fee == null)
                        {
                            return BadRequest("There is no Fee with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 12) // Discount
                    {
                        TuitionDiscountType discount = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (discount == null)
                        {
                            return BadRequest("There is no Discount with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 13) // Student
                    {
                        Student student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                        if (student == null)
                        {
                            return BadRequest("There is no Student with this ID in the database.");
                        }
                    }
                }


                AccountingEntriesDetails = mapper.Map<AccountingEntriesDetails>(newDetails);

                if (AccountingTreeChart.LinkFileID == 6) // Bank
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 5) // Save
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 2) // Supplier
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 3) // Debit
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 4) // Credit
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 7) // Income
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 8) // Outcome
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 9) // Asset
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 10) // Employee
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 11) // Fee
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.TuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 12) // Discount
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.TuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 13) // Student
                {
                    AccountingEntriesDetails.SubAccountingID = newDetails.SubAccountingID;
                    AccountingEntriesDetails.Student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetails.SubAccountingID);
                }
            }
            else
            {
                AccountingEntriesDetails = mapper.Map<AccountingEntriesDetails>(newDetails);
                AccountingEntriesDetails.SubAccountingID = null;
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AccountingEntriesDetails.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AccountingEntriesDetails.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                AccountingEntriesDetails.InsertedByUserId = userId;
            }

            Unit_Of_Work.accountingEntriesDetails_Repository.Add(AccountingEntriesDetails);
            Unit_Of_Work.SaveChanges();

            return Ok(newDetails);
        }

        //////////////////////////////////////////////////////////////////////////////////


        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Accounting Entries Details", "Accounting" }
        )]
        public IActionResult Edit(AccountingEntriesDetailsPutDTO newDetail)
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

            if (newDetail == null)
            {
                return BadRequest("Accounting Entries Details cannot be null");
            }

            AccountingEntriesDetails AccountingEntriesDetails = Unit_Of_Work.accountingEntriesDetails_Repository.First_Or_Default(d => d.ID == newDetail.ID && d.IsDeleted != true);
            if (AccountingEntriesDetails == null)
            {
                return NotFound("There is no Accounting Entries Details with this id");
            }

            AccountingEntriesMaster AccountingEntriesMaster = Unit_Of_Work.accountingEntriesMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newDetail.AccountingEntriesMasterID);
            if (AccountingEntriesMaster == null)
            {
                return BadRequest("there is no Accounting Entries Master with this ID");
            }

            AccountingTreeChart AccountingTreeChart = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.ID == newDetail.AccountingTreeChartID);
            if (AccountingTreeChart == null)
            {
                return BadRequest("there is no Accounting Tree Chart with this ID");
            }


            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting Entries Details");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (AccountingEntriesDetails.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting Entries Details page doesn't exist");
                }
            }

            if (AccountingTreeChart.LinkFileID != null)
            {
                if (newDetail.SubAccountingID != null || newDetail.SubAccountingID != 0)
                {
                    if (AccountingTreeChart.LinkFileID == 6) // Bank
                    {
                        Bank Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (Bank == null)
                        {
                            return BadRequest("There is no Bank with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 5) // Save
                    {
                        Save Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (Save == null)
                        {
                            return BadRequest("There is no Save with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 2) // Supplier
                    {
                        Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (supplier == null)
                        {
                            return BadRequest("There is no Supplier with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 3) // Debit
                    {
                        Debit debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (debit == null)
                        {
                            return BadRequest("There is no Debit with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 4) // Credit
                    {
                        Credit credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (credit == null)
                        {
                            return BadRequest("There is no Credit with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 7) // Income
                    {
                        Income income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (income == null)
                        {
                            return BadRequest("There is no Income with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 8) // Outcome
                    {
                        Outcome outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (outcome == null)
                        {
                            return BadRequest("There is no Outcome with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 9) // Asset
                    {
                        Asset asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (asset == null)
                        {
                            return BadRequest("There is no Asset with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 10) // Employee
                    {
                        Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (employee == null)
                        {
                            return BadRequest("There is no Employee with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 11) // Fee
                    {
                        TuitionFeesType fee = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (fee == null)
                        {
                            return BadRequest("There is no Fee with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 12) // Discount
                    {
                        TuitionDiscountType discount = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (discount == null)
                        {
                            return BadRequest("There is no Discount with this ID in the database.");
                        }
                    }
                    else if (AccountingTreeChart.LinkFileID == 13) // Student
                    {
                        Student student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                        if (student == null)
                        {
                            return BadRequest("There is no Student with this ID in the database.");
                        }
                    }
                }


                mapper.Map(newDetail, AccountingEntriesDetails);

                if (AccountingTreeChart.LinkFileID == 6) // Bank
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 5) // Save
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 2) // Supplier
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 3) // Debit
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 4) // Credit
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 7) // Income
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 8) // Outcome
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 9) // Asset
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 10) // Employee
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 11) // Fee
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.TuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 12) // Discount
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.TuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
                else if (AccountingTreeChart.LinkFileID == 13) // Student
                {
                    AccountingEntriesDetails.SubAccountingID = newDetail.SubAccountingID;
                    AccountingEntriesDetails.Student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetail.SubAccountingID);
                }
            }
            else
            {
                mapper.Map(newDetail, AccountingEntriesDetails);
                AccountingEntriesDetails.SubAccountingID = null;
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AccountingEntriesDetails.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AccountingEntriesDetails.UpdatedByOctaId = userId;
                if (AccountingEntriesDetails.UpdatedByUserId != null)
                {
                    AccountingEntriesDetails.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                AccountingEntriesDetails.UpdatedByUserId = userId;
                if (AccountingEntriesDetails.UpdatedByOctaId != null)
                {
                    AccountingEntriesDetails.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingEntriesDetails_Repository.Update(AccountingEntriesDetails);
            Unit_Of_Work.SaveChanges();
            return Ok(newDetail);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Accounting Entries Details", "Accounting" }
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
                return BadRequest("Enter Accounting Entries Details ID");
            }

            AccountingEntriesDetails AccountingEntriesDetails = Unit_Of_Work.accountingEntriesDetails_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (AccountingEntriesDetails == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Accounting Entries Details");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (AccountingEntriesDetails.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Accounting Entries Details page doesn't exist");
                }
            }

            AccountingEntriesDetails.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            AccountingEntriesDetails.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                AccountingEntriesDetails.DeletedByOctaId = userId;
                if (AccountingEntriesDetails.DeletedByUserId != null)
                {
                    AccountingEntriesDetails.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                AccountingEntriesDetails.DeletedByUserId = userId;
                if (AccountingEntriesDetails.DeletedByOctaId != null)
                {
                    AccountingEntriesDetails.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.accountingEntriesDetails_Repository.Update(AccountingEntriesDetails);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
