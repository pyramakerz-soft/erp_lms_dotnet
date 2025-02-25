using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.LMS;
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
    public class PayableDetailsController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        private readonly CheckPageAccessService _checkPageAccessService;

        public PayableDetailsController(DbContextFactoryService dbContextFactory, IMapper mapper, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////

        [HttpGet("GetByMasterID/{id}")]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Payable Details" }
        )]
        public async Task<IActionResult> GetAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<PayableDetails> PayableDetails = await Unit_Of_Work.payableDetails_Repository.Select_All_With_IncludesById<PayableDetails>(
                    t => t.IsDeleted != true && t.PayableMasterID == id,
                    query => query.Include(Master => Master.PayableMaster),
                    query => query.Include(Master => Master.LinkFile)
                    );

            if (PayableDetails == null || PayableDetails.Count == 0)
            {
                return NotFound();
            }

            var suppliersIds = PayableDetails.Where(r => r.LinkFileID == 2).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var debitIds = PayableDetails.Where(r => r.LinkFileID == 3).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var creditsIds = PayableDetails.Where(r => r.LinkFileID == 4).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var saveIds = PayableDetails.Where(r => r.LinkFileID == 5).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var bankIds = PayableDetails.Where(r => r.LinkFileID == 6).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var incomesIds = PayableDetails.Where(r => r.LinkFileID == 7).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var outcomesIds = PayableDetails.Where(r => r.LinkFileID == 8).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var assetsIds = PayableDetails.Where(r => r.LinkFileID == 9).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var employeesIds = PayableDetails.Where(r => r.LinkFileID == 10).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var feeIds = PayableDetails.Where(r => r.LinkFileID == 11).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var discountIds = PayableDetails.Where(r => r.LinkFileID == 12).Select(r => r.LinkFileTypeID).Distinct().ToList();
            var studentIds = PayableDetails.Where(r => r.LinkFileID == 13).Select(r => r.LinkFileTypeID).Distinct().ToList();

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

            List<PayableDetailsGetDTO> DTOs = mapper.Map<List<PayableDetailsGetDTO>>(PayableDetails);

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
            pages: new[] { "Payable Details" }
        )]
        public async Task<IActionResult> GetbyIdAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0 || id == null)
            {
                return BadRequest("Enter Payable Details ID");
            }

            PayableDetails PayableDetails = await Unit_Of_Work.payableDetails_Repository.FindByIncludesAsync(
                    acc => acc.IsDeleted != true && acc.ID == id,
                    query => query.Include(ac => ac.LinkFile),
                    query => query.Include(ac => ac.PayableMaster)
                    );

            if (PayableDetails == null)
            {
                return NotFound();
            }

            string LinkFileTypeName = null;

            if (PayableDetails.LinkFileID == 6) // Bank
            {
                var bank = await Unit_Of_Work.bank_Repository.FindByIncludesAsync(b => b.IsDeleted != true && b.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = bank?.Name;
            }
            else if (PayableDetails.LinkFileID == 5) // Save
            {
                var save = await Unit_Of_Work.save_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = save?.Name;
            }
            else if (PayableDetails.LinkFileID == 2) // Supplier
            {
                var supplier = await Unit_Of_Work.supplier_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = supplier?.Name;
            }
            else if (PayableDetails.LinkFileID == 3) // Debit
            {
                var debit = await Unit_Of_Work.debit_Repository.FindByIncludesAsync(d => d.IsDeleted != true && d.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = debit?.Name;
            }
            else if (PayableDetails.LinkFileID == 4) // Credit
            {
                var credit = await Unit_Of_Work.credit_Repository.FindByIncludesAsync(c => c.IsDeleted != true && c.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = credit?.Name;
            }
            else if (PayableDetails.LinkFileID == 7) // Income
            {
                var income = await Unit_Of_Work.income_Repository.FindByIncludesAsync(i => i.IsDeleted != true && i.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = income?.Name;
            }
            else if (PayableDetails.LinkFileID == 8) // Outcome
            {
                var outcome = await Unit_Of_Work.outcome_Repository.FindByIncludesAsync(o => o.IsDeleted != true && o.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = outcome?.Name;
            }
            else if (PayableDetails.LinkFileID == 9) // Asset
            {
                var asset = await Unit_Of_Work.asset_Repository.FindByIncludesAsync(a => a.IsDeleted != true && a.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = asset?.Name;
            }
            else if (PayableDetails.LinkFileID == 10) // Employee
            {
                var employee = await Unit_Of_Work.employee_Repository.FindByIncludesAsync(e => e.IsDeleted != true && e.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = employee?.en_name;
            }
            else if (PayableDetails.LinkFileID == 11) // Fee
            {
                var fee = await Unit_Of_Work.tuitionFeesType_Repository.FindByIncludesAsync(f => f.IsDeleted != true && f.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = fee?.Name;
            }
            else if (PayableDetails.LinkFileID == 12) // Discount
            {
                var discount = await Unit_Of_Work.tuitionDiscountType_Repository.FindByIncludesAsync(d => d.IsDeleted != true && d.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = discount?.Name;
            }
            else if (PayableDetails.LinkFileID == 13) // Student
            {
                var student = await Unit_Of_Work.student_Repository.FindByIncludesAsync(s => s.IsDeleted != true && s.ID == PayableDetails.LinkFileTypeID);
                LinkFileTypeName = student?.en_name;
            }

            PayableDetailsGetDTO dto = mapper.Map<PayableDetailsGetDTO>(PayableDetails);
            dto.LinkFileTypeName = LinkFileTypeName;

            return Ok(dto);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Payable Details" }
        )]
        public IActionResult Add(PayableDetailsAddDTO newDetails)
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
                return BadRequest("Payable Details cannot be null");
            }

            PayableMaster PayableMaster = Unit_Of_Work.payableMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newDetails.PayableMasterID);
            if (PayableMaster == null)
            {
                return BadRequest("There is no Payable with this ID");
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

            PayableDetails PayableDetails = mapper.Map<PayableDetails>(newDetails);

            // Set up the corresponding LinkFileType (based on LinkFileID)
            if (newDetails.LinkFileID == 6) // Bank
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 5) // Save
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 2) // Supplier
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 3) // Debit
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 4) // Credit
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 7) // Income
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 8) // Outcome
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 9) // Asset
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 10) // Employee
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 11) // Fee
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.TuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 12) // Discount
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.TuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }
            else if (newDetails.LinkFileID == 13) // Student
            {
                PayableDetails.LinkFileTypeID = newDetails.LinkFileTypeID;
                PayableDetails.Student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetails.LinkFileTypeID);
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableDetails.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableDetails.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                PayableDetails.InsertedByUserId = userId;
            }

            Unit_Of_Work.payableDetails_Repository.Add(PayableDetails);
            Unit_Of_Work.SaveChanges();

            return Ok(newDetails);
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Payable Details" }
        )]
        public IActionResult Edit(PayableDetailsPutDTO newDetail)
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
                return BadRequest("Payable Details cannot be null");
            }

            PayableDetails PayableDetails = Unit_Of_Work.payableDetails_Repository.First_Or_Default(d => d.ID == newDetail.ID && d.IsDeleted != true);
            if (PayableDetails == null)
            {
                return NotFound("There is no Payable Details with this id");
            }

            PayableMaster PayableMaster = Unit_Of_Work.payableMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newDetail.PayableMasterID);
            if (PayableMaster == null)
            {
                return BadRequest("there is no Payable Master with this ID");
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
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Payable Details", roleId, userId, PayableDetails);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newDetail, PayableDetails);

            if (newDetail.LinkFileID == 6) // Bank
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Bank = Unit_Of_Work.bank_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 5) // Save
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Save = Unit_Of_Work.save_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 2) // Supplier
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 3) // Debit
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Debit = Unit_Of_Work.debit_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 4) // Credit
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Credit = Unit_Of_Work.credit_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 7) // Income
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Income = Unit_Of_Work.income_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 8) // Outcome
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Outcome = Unit_Of_Work.outcome_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 9) // Asset
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Asset = Unit_Of_Work.asset_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 10) // Employee
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Employee = Unit_Of_Work.employee_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 11) // Fee
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.TuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 12) // Discount
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.TuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }
            else if (newDetail.LinkFileID == 13) // Student
            {
                PayableDetails.LinkFileTypeID = newDetail.LinkFileTypeID;
                PayableDetails.Student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newDetail.LinkFileTypeID);
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableDetails.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableDetails.UpdatedByOctaId = userId;
                if (PayableDetails.UpdatedByUserId != null)
                {
                    PayableDetails.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                PayableDetails.UpdatedByUserId = userId;
                if (PayableDetails.UpdatedByOctaId != null)
                {
                    PayableDetails.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.payableDetails_Repository.Update(PayableDetails);
            Unit_Of_Work.SaveChanges();
            return Ok(newDetail);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Payable Details" }
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

            PayableDetails PayableDetails = Unit_Of_Work.payableDetails_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (PayableDetails == null)
            {
                return NotFound();
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Payable Details", roleId, userId, PayableDetails);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            PayableDetails.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            PayableDetails.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                PayableDetails.DeletedByOctaId = userId;
                if (PayableDetails.DeletedByUserId != null)
                {
                    PayableDetails.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                PayableDetails.DeletedByUserId = userId;
                if (PayableDetails.DeletedByOctaId != null)
                {
                    PayableDetails.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.payableDetails_Repository.Update(PayableDetails);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}
