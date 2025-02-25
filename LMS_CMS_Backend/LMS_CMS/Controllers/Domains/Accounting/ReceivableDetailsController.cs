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
    public class ReceivableDetailsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public ReceivableDetailsController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet("GetByMasterID/{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Receivable Details" }
        )]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<ReceivableDetails> ReceivableDetails = await Unit_Of_Work.receivableDetails_Repository.Select_All_With_IncludesById<ReceivableDetails>(
                    t => t.IsDeleted != true && t.ReceivableMasterID == id,
                    query => query.Include(Master => Master.ReceivableMaster),
                    query => query.Include(Master => Master.LinkFile)
                    );

            if (ReceivableDetails == null || ReceivableDetails.Count == 0)
            {
                return NotFound();
            }

            var suppliersIds = ReceivableDetails.Where(r => r.LinkFileID == 2).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var debitIds = ReceivableDetails.Where(r => r.LinkFileID == 3).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var creditsIds = ReceivableDetails.Where(r => r.LinkFileID == 4).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var saveIds = ReceivableDetails.Where(r => r.LinkFileID == 5).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var bankIds = ReceivableDetails.Where(r => r.LinkFileID == 6).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var incomesIds = ReceivableDetails.Where(r => r.LinkFileID == 7).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var outcomesIds = ReceivableDetails.Where(r => r.LinkFileID == 8).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var assetsIds = ReceivableDetails.Where(r => r.LinkFileID == 9).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var employeesIds = ReceivableDetails.Where(r => r.LinkFileID == 10).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var feeIds = ReceivableDetails.Where(r => r.LinkFileID == 11).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var discountIds = ReceivableDetails.Where(r => r.LinkFileID == 12).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var studentIds = ReceivableDetails.Where(r => r.LinkFileID == 13).Select(r => r.LinkFileTypeID).Distinct().ToList();

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

            List<ReceivableDetailsGetDTO> DTOs = mapper.Map<List<ReceivableDetailsGetDTO>>(ReceivableDetails);

            foreach (var dto in DTOs)
            {
                if (dto.LinkFileID == 6) // Bank
                {
                    var bank = banks.FirstOrDefault(b => b.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = bank?.Name;
                }
                else if (dto.LinkFileID == 5) // Save
                {
                    var save = saves.FirstOrDefault(s => s.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = save?.Name;
                }
                else if (dto.LinkFileID == 2) // Supplier
                {
                    var supplier = Suppliers.FirstOrDefault(s => s.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = supplier?.Name;
                }
                else if (dto.LinkFileID == 3) // Debit
                {
                    var debit = Debit.FirstOrDefault(d => d.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = debit?.Name;
                }
                else if (dto.LinkFileID == 4) // Credit
                {
                    var credit = Credits.FirstOrDefault(c => c.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = credit?.Name;
                }
                else if (dto.LinkFileID == 7) // Income
                {
                    var income = Incomes.FirstOrDefault(i => i.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = income?.Name;
                }
                else if (dto.LinkFileID == 8) // Outcome
                {
                    var outcome = Outcomes.FirstOrDefault(o => o.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = outcome?.Name;
                }
                else if (dto.LinkFileID == 9) // Asset
                {
                    var asset = Assets.FirstOrDefault(a => a.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = asset?.Name;
                }
                else if (dto.LinkFileID == 10) // Employee
                {
                    var employee = Employees.FirstOrDefault(e => e.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = employee?.en_name;
                }
                else if (dto.LinkFileID == 11) // Fee
                {
                    var fee = Fees.FirstOrDefault(f => f.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = fee?.Name;
                }
                else if (dto.LinkFileID == 12) // Discount
                {
                    var discount = Discount.FirstOrDefault(d => d.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = discount?.Name;
                }
                else if (dto.LinkFileID == 13) // Student
                {
                    var student = Students.FirstOrDefault(s => s.ID == dto.LinkFileTypeID);
                    dto.LinkFileTypeName = student?.en_name;
                }
            }

            return Ok(DTOs);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Receivable Details" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Enter Receivable Details ID");
            }

            ReceivableDetails ReceivableDetails = await Unit_Of_Work.receivableDetails_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.ReceivableMaster)
                    );

            if (ReceivableDetails == null)
            {
                return NotFound();
            }

            string LinkFileTypeName = null;

            if (ReceivableDetails.LinkFileID == 6) // Bank
            {
                var bank = await Unit_Of_Work.bank_Repository.FindByIncludesAsync(b => b.IsDeleted != true && b.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = bank?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 5) // Save
            {
                var save = await Unit_Of_Work.save_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = save?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 2) // Supplier
            {
                var supplier = await Unit_Of_Work.supplier_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = supplier?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 3) // Debit
            {
                var debit = await Unit_Of_Work.debit_Repository.FindByIncludesAsync(d => d.IsDeleted != true && d.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = debit?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 4) // Credit
            {
                var credit = await Unit_Of_Work.credit_Repository.FindByIncludesAsync(c => c.IsDeleted != true && c.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = credit?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 7) // Income
            {
                var income = await Unit_Of_Work.income_Repository.FindByIncludesAsync(i => i.IsDeleted != true && i.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = income?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 8) // Outcome
            {
                var outcome = await Unit_Of_Work.outcome_Repository.FindByIncludesAsync(o => o.IsDeleted != true && o.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = outcome?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 9) // Asset
            {
                var asset = await Unit_Of_Work.asset_Repository.FindByIncludesAsync(a => a.IsDeleted != true && a.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = asset?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 10) // Employee
            {
                var employee = await Unit_Of_Work.employee_Repository.FindByIncludesAsync(e => e.IsDeleted != true && e.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = employee?.en_name;
            }
            else if (ReceivableDetails.LinkFileID == 11) // Fee
            {
                var fee = await Unit_Of_Work.tuitionFeesType_Repository.FindByIncludesAsync(f => f.IsDeleted != true && f.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = fee?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 12) // Discount
            {
                var discount = await Unit_Of_Work.tuitionDiscountType_Repository.FindByIncludesAsync(d => d.IsDeleted != true && d.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = discount?.Name;
            }
            else if (ReceivableDetails.LinkFileID == 13) // Student
            {
                var student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == ReceivableDetails.LinkFileTypeID);
                LinkFileTypeName = student?.en_name;
            }

            ReceivableDetailsGetDTO dto = mapper.Map<ReceivableDetailsGetDTO>(ReceivableDetails);
            dto.LinkFileTypeName = LinkFileTypeName;

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Receivable Details" }
        )]
        public IActionResult Add(ReceivableDetailsAddDTO newDetails)
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
                return BadRequest("Receivable Details cannot be null");
            }

            ReceivableMaster ReceivableMaster = Unit_Of_Work.receivableMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newDetails.ReceivableMasterID);
            if (ReceivableMaster == null)
            {
                return BadRequest("There is no Receivable with this ID");
            }

            LinkFile LinkFile = Unit_Of_Work.linkFile_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileID);
            if (LinkFile == null)
            {
                return BadRequest("There is no Link File with this ID");
            }

            // Validate and assign LinkFileType based on LinkFileID
            if (newDetails.LinkFileID == 6) // Bank
            {
                Bank Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (Bank == null)
                {
                    return BadRequest("There is no Bank with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 5) // Save
            {
                Save Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (Save == null)
                {
                    return BadRequest("There is no Save with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 2) // Supplier
            {
                Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (supplier == null)
                {
                    return BadRequest("There is no Supplier with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 3) // Debit
            {
                Debit debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (debit == null)
                {
                    return BadRequest("There is no Debit with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 4) // Credit
            {
                Credit credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (credit == null)
                {
                    return BadRequest("There is no Credit with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 7) // Income
            {
                Income income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (income == null)
                {
                    return BadRequest("There is no Income with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 8) // Outcome
            {
                Outcome outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (outcome == null)
                {
                    return BadRequest("There is no Outcome with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 9) // Asset
            {
                Asset asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (asset == null)
                {
                    return BadRequest("There is no Asset with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 10) // Employee
            {
                Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (employee == null)
                {
                    return BadRequest("There is no Employee with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 11) // Fee
            {
                TuitionFeesType fee = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (fee == null)
                {
                    return BadRequest("There is no Fee with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 12) // Discount
            {
                TuitionDiscountType discount = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (discount == null)
                {
                    return BadRequest("There is no Discount with this ID in the database.");
                }
            }
            else if (newDetails.LinkFileID == 13) // Student
            {
                Student student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
                if (student == null)
                {
                    return BadRequest("There is no Student with this ID in the database.");
                }
            }

            // Map and insert the ReceivableDetails object
            ReceivableDetails ReceivableDetails = mapper.Map<ReceivableDetails>(newDetails);

            // Set up the corresponding LinkFileType (based on LinkFileID)
            if (newDetails.LinkFileID == 6) // Bank
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 5) // Save
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 2) // Supplier
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 3) // Debit
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 4) // Credit
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 7) // Income
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 8) // Outcome
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 9) // Asset
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 10) // Employee
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 11) // Fee
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.TuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 12) // Discount
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.TuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 13) // Student
            {
                ReceivableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                ReceivableDetails.Student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
             
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableDetails.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableDetails.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableDetails.InsertedByUserId = userId;
            }
             
            Unit_Of_Work.receivableDetails_Repository.Add(ReceivableDetails);
            Unit_Of_Work.SaveChanges();

            return Ok(newDetails);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Receivable Details" }
        )]
        public IActionResult Edit(ReceivableDetailsPutDTO newDetail)
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
                return BadRequest("Receivable Details cannot be null");
            }

            ReceivableDetails ReceivableDetails = Unit_Of_Work.receivableDetails_Repository.First_Or_Default(d => d.ID == newDetail.ID && d.IsDeleted != true);
            if (ReceivableDetails == null)
            {
                return NotFound("There is no Receivable Details with this id");
            }

            ReceivableMaster ReceivableMaster = Unit_Of_Work.receivableMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newDetail.ReceivableMasterID);
            if (ReceivableMaster == null)
            {
                return BadRequest("there is no Receivable Master with this ID");
            }

            LinkFile LinkFile = Unit_Of_Work.linkFile_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileID);
            if (LinkFile == null)
            {
                return BadRequest("there is no Link File with this ID");
            }

            if (newDetail.LinkFileID == 6) // Bank
            {
                Bank Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (Bank == null)
                {
                    return BadRequest("There is no Bank with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 5) // Save
            {
                Save Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (Save == null)
                {
                    return BadRequest("There is no Save with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 2) // Supplier
            {
                Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (supplier == null)
                {
                    return BadRequest("There is no Supplier with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 3) // Debit
            {
                Debit debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (debit == null)
                {
                    return BadRequest("There is no Debit with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 4) // Credit
            {
                Credit credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (credit == null)
                {
                    return BadRequest("There is no Credit with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 7) // Income
            {
                Income income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (income == null)
                {
                    return BadRequest("There is no Income with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 8) // Outcome
            {
                Outcome outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (outcome == null)
                {
                    return BadRequest("There is no Outcome with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 9) // Asset
            {
                Asset asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (asset == null)
                {
                    return BadRequest("There is no Asset with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 10) // Employee
            {
                Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (employee == null)
                {
                    return BadRequest("There is no Employee with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 11) // Fee
            {
                TuitionFeesType fee = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (fee == null)
                {
                    return BadRequest("There is no Fee with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 12) // Discount
            {
                TuitionDiscountType discount = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (discount == null)
                {
                    return BadRequest("There is no Discount with this ID in the database.");
                }
            }
            else if (newDetail.LinkFileID == 13) // Student
            {
                Student student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
                if (student == null)
                {
                    return BadRequest("There is no Student with this ID in the database.");
                }
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Receivable Details", roleId, userId, ReceivableDetails);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newDetail, ReceivableDetails);

            if (newDetail.LinkFileID == 6) // Bank
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 5) // Save
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 2) // Supplier
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 3) // Debit
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 4) // Credit
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 7) // Income
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 8) // Outcome
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 9) // Asset
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 10) // Employee
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 11) // Fee
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.TuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 12) // Discount
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.TuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 13) // Student
            {
                ReceivableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                ReceivableDetails.Student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableDetails.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableDetails.UpdatedByOctaId = userId;
                if (ReceivableDetails.UpdatedByUserId != null)
                {
                    ReceivableDetails.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableDetails.UpdatedByUserId = userId;
                if (ReceivableDetails.UpdatedByOctaId != null)
                {
                    ReceivableDetails.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.receivableDetails_Repository.Update(ReceivableDetails);
            Unit_Of_Work.SaveChanges();
            return Ok(newDetail);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Receivable Details" }
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

            ReceivableDetails ReceivableDetails = Unit_Of_Work.receivableDetails_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);
             
            if (ReceivableDetails == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Receivable Details", roleId, userId, ReceivableDetails);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            ReceivableDetails.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            ReceivableDetails.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                ReceivableDetails.DeletedByOctaId = userId;
                if (ReceivableDetails.DeletedByUserId != null)
                {
                    ReceivableDetails.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                ReceivableDetails.DeletedByUserId = userId;
                if (ReceivableDetails.DeletedByOctaId != null)
                {
                    ReceivableDetails.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.receivableDetails_Repository.Update(ReceivableDetails);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
