using AutoMapper;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.Zatca;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using LMS_CMS_PL.Services.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Metrics;
using System.Xml;
using System.Xml.Linq;
using Zatca.EInvoice.SDK.Contracts.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using Zatca.EInvoice.SDK;
//using Zatca.EInvoice.SDK.Contracts;
//using Zatca.EInvoice.SDK.Contracts.Models;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryMasterController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        public  InVoiceNumberCreate _InVoiceNumberCreate;
        private readonly CheckPageAccessService _checkPageAccessService;
        //private readonly IEInvoiceHashGenerator _eInvoiceHashGenerator;
        //private readonly ICsrGenerator _csrGenerator;
        //private readonly RequestResult _requestResult;

        //public InventoryMasterController(DbContextFactoryService dbContextFactory, IMapper mapper  , InVoiceNumberCreate inVoiceNumberCreate, CheckPageAccessService checkPageAccessService, IEInvoiceHashGenerator eInvoiceHashGenerator, ICsrGenerator csrGenerator, RequestResult requestResult)
        public InventoryMasterController(DbContextFactoryService dbContextFactory, IMapper mapper  , InVoiceNumberCreate inVoiceNumberCreate, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this._InVoiceNumberCreate = inVoiceNumberCreate;
            _checkPageAccessService = checkPageAccessService;
            //_eInvoiceHashGenerator = eInvoiceHashGenerator;
            //_csrGenerator = csrGenerator;
            //_requestResult = requestResult;
        }



        [HttpGet("ByFlagId/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Inventory" }
        )]
        public async Task<IActionResult> GetAsync(long id ,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Get total record count
            int totalRecords = await Unit_Of_Work.inventoryMaster_Repository
                .CountAsync(f => f.IsDeleted != true && f.FlagId == id);

            List<InventoryMaster> Data = await Unit_Of_Work.inventoryMaster_Repository.Select_All_With_IncludesById_Pagination<InventoryMaster>(
                    f => f.IsDeleted != true && f.FlagId==id,
                    query => query.Include(store => store.Store),
                    query => query.Include(store => store.Student),
                    query => query.Include(store => store.InventoryFlags),
                    query => query.Include(store => store.Save),
                    query => query.Include(store => store.Bank))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            //if (Data == null || Data.Count == 0)
            //{
            //    return NotFound();
            //}
            InventoryFlags inventoryFlags = Unit_Of_Work.inventoryFlags_Repository.First_Or_Default(i=>i.ID==id);
            InventoryFlagGetDTO Flagdto = mapper.Map<InventoryFlagGetDTO>(inventoryFlags);

            List<InventoryMasterGetDTO> DTO = mapper.Map<List<InventoryMasterGetDTO>>(Data);

            var paginationMetadata = new
            {
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return Ok(new { Data = DTO, Pagination = paginationMetadata , inventoryFlag=Flagdto });
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("Search")]
        [Authorize_Endpoint_(
         allowedTypes: new[] { "octa", "employee" },
         pages: new[] { "Inventory" }
        )]
        public async Task<IActionResult> GetSearch([FromQuery] InventoryMasterSearch obj)
        {
            var unitOfWork = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (obj.FlagIds == null || !obj.FlagIds.Any())
                return BadRequest("FlagIds cannot be null or empty.");

            if (!DateTime.TryParse(obj.DateFrom, out var dateFrom) || !DateTime.TryParse(obj.DateTo, out var dateTo))
                return BadRequest("Invalid date format.");

            if (dateFrom > dateTo)
                return BadRequest("DateFrom cannot be after DateTo.");

            var data = await unitOfWork.inventoryMaster_Repository.Select_All_With_IncludesById<InventoryMaster>(
                f => f.IsDeleted != true && obj.FlagIds.Contains(f.FlagId),
                query => query
                    .Include(x => x.Store)
                    .Include(x => x.Student)
                    .Include(x => x.InventoryFlags)
                    .Include(x => x.InventoryDetails)
                        .ThenInclude(detail => detail.ShopItem)
                            .ThenInclude(shopItem => shopItem.InventorySubCategories)
                                .ThenInclude(subCategory => subCategory.InventoryCategories)
                    .Include(x => x.Save) 
                    .Include(x => x.Bank)
            );

            var filteredData = data.Where(f =>
                DateTime.TryParse(f.Date, out var parsedDate) &&
                parsedDate >= dateFrom && parsedDate <= dateTo &&
                (obj.StoredId == null || (f.Store != null && f.Store.ID == obj.StoredId)) &&
                f.InventoryDetails.Any(detail =>
                    (obj.CategoryId == null || detail.ShopItem.InventorySubCategories.InventoryCategoriesID == obj.CategoryId) &&
                    (obj.SubCategoryId == null || detail.ShopItem.InventorySubCategoriesID == obj.SubCategoryId) &&
                    (obj.ItemId == null || detail.ShopItemID == obj.ItemId)
                )
            ).ToList();

            if (!filteredData.Any())
                return NotFound("No records found matching the search criteria.");

            var allTotal = filteredData.Sum(item => (item.Total) * (item.InventoryFlags?.FlagValue ?? 0));

            var dto = mapper.Map<List<InventoryMasterGetDTO>>(filteredData);

            return Ok(new
            {
                AllTotal = allTotal,
                Data = dto
            });
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("SearchInvoice")]
        [Authorize_Endpoint_(
    allowedTypes: new[] { "octa", "employee" },
    pages: new[] { "Inventory" }
)]
        public async Task<IActionResult> GetSearchInvoice([FromQuery] InventoryMasterSearch obj)
        {
            var unitOfWork = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (obj.FlagIds == null || !obj.FlagIds.Any())
                return BadRequest("FlagIds cannot be null or empty.");

            if (!DateTime.TryParse(obj.DateFrom, out var dateFrom) || !DateTime.TryParse(obj.DateTo, out var dateTo))
                return BadRequest("Invalid date format.");

            if (dateFrom > dateTo)
                return BadRequest("DateFrom cannot be after DateTo.");

            var data = await unitOfWork.inventoryMaster_Repository.Select_All_With_IncludesById<InventoryMaster>(
                f => f.IsDeleted != true && obj.FlagIds.Contains(f.FlagId),
                query => query
                    .Include(x => x.Store)
                    .Include(x => x.Student)
                    .Include(x => x.InventoryFlags)
                    .Include(x => x.InventoryDetails)
                        .ThenInclude(detail => detail.ShopItem)
                            .ThenInclude(shopItem => shopItem.InventorySubCategories)
                                .ThenInclude(subCategory => subCategory.InventoryCategories)
                    .Include(x => x.Save)
                    .Include(x => x.Bank)
            );

            var filteredData = data.Where(f =>
                DateTime.TryParse(f.Date, out var parsedDate) &&
                parsedDate >= dateFrom && parsedDate <= dateTo &&
                (obj.StoredId == null || (f.Store != null && f.Store.ID == obj.StoredId)) &&
                f.InventoryDetails.Any(detail =>
                    (obj.CategoryId == null || detail.ShopItem.InventorySubCategories.InventoryCategoriesID == obj.CategoryId) &&
                    (obj.SubCategoryId == null || detail.ShopItem.InventorySubCategoriesID == obj.SubCategoryId) &&
                    (obj.ItemId == null || detail.ShopItemID == obj.ItemId)
                )
            ).ToList();

            if (!filteredData.Any())
                return NotFound("No records found matching the search criteria.");

            var allTotal = filteredData.Sum(item => item.Total * (item.InventoryFlags?.FlagValue ?? 0));

            // Remove InventoryDetails before returning
            var summaryDtos = filteredData.Select(f => new
            {
                f.ID,
                f.InvoiceNumber,
                f.Date,
                f.Total,
                f.Remaining,
                f.IsCash,
                f.IsVisa,
                f.CashAmount,
                f.VisaAmount,
                f.FlagId,
                FlagValue = f.InventoryFlags?.FlagValue,
                FlagArName = f.InventoryFlags?.arName,
                FlagEnName = f.InventoryFlags?.enName,
                StoreName = f.Store?.Name,
                BankName = f.Bank?.Name,
                StudentName = f.Student?.en_name,
                SaveName = f.Save?.Name,
                f.StoreID,
                f.StudentID,
                f.SaveID,
                f.BankID,
                f.Notes
            }).ToList();


            return Ok(new
            {
                AllTotal = allTotal,
                Data = summaryDtos // ✅ lightweight, safe to serialize
            });

        }


        /////////////////////////////////////////////////////////////////////////////


        [HttpGet("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
          pages: new[] { "Inventory" }
    )]
        public async Task<IActionResult> GetById(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (id == 0)
            {
                return BadRequest("Enter Master ID");
            }

            InventoryMaster Data = await Unit_Of_Work.inventoryMaster_Repository.FindByIncludesAsync(
                    s => s.IsDeleted != true && s.ID == id,
                    query => query.Include(store => store.Store),
                    query => query.Include(store => store.Student),
                    query => query.Include(store => store.Save),
                    query => query.Include(store => store.InventoryFlags),
                    query => query.Include(store => store.Bank)
                    );
            
            if (Data == null)
            {
                return NotFound();
            }

            InventoryMasterGetDTO DTO = mapper.Map<InventoryMasterGetDTO>(Data);
            string serverUrl = $"{Request.Scheme}://{Request.Host}/Uploads/Master";
            //subject.IconLink = $"{serverUrl}{subject.IconLink.Replace("\\", "/")}";
            if (Data.Attachments != null)
            {
                DTO.Attachments = Data.Attachments.Select(filePath =>
                    $"{serverUrl}/{Data.ID}/{Path.GetFileName(filePath)}"
                ).ToList();
            }
            return Ok(DTO);
        }
        /////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
      allowedTypes: new[] { "octa", "employee" },
      pages: new[] { "Inventory" }
  )]
        public async Task<IActionResult> Add([FromForm] InventoryMasterAddDTO newData)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            SchoolPCs pc = Unit_Of_Work.schoolPCs_Repository.First_Or_Default(
                d => d.ID == newData.SchoolPCId && d.IsDeleted != true
            );

            if (pc == null)
            {
                return NotFound("PC not found.");
            }

            if (pc.CertificateDate == null)
            {
                return BadRequest("Please Create the Certificate.");
            }

            if (pc.CertificateDate.Value <= DateOnly.FromDateTime(DateTime.Now.AddDays(1)))
            {
                return BadRequest("Please Update the Certificate.");
            }

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            decimal vat = 0;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userTypeClaim))
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newData == null)
            {
                return BadRequest("Master cannot be null.");
            }

            Store store = Unit_Of_Work.store_Repository.First_Or_Default(b => b.ID == newData.StoreID && b.IsDeleted != true);
            if (store == null)
            {
                return NotFound("Store not found.");
            }
            if (newData.StudentID != 0 && newData.StudentID != null)
            {
                Student student = Unit_Of_Work.student_Repository.First_Or_Default(b => b.ID == newData.StudentID && b.IsDeleted != true);
                if (student == null)
                {
                    return NotFound("Student not found.");
                }

                if (student.Nationality != 148)
                {
                    vat = 0.15m;
                }
            }
            else
            {
                newData.StudentID = null;
            }
            if (newData.SupplierId != 0 && newData.SupplierId != null)
            {
                Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(b => b.ID == newData.SupplierId && b.IsDeleted != true);
                if (supplier == null)
                {
                    return NotFound("Supplier not found.");
                }
            }
            else
            {
                newData.SupplierId = null;
            }

            School school = new();

            if (newData.SchoolId != 0 && newData.SchoolId != null)
            {
                school = Unit_Of_Work.school_Repository.First_Or_Default(b => b.ID == newData.SchoolId && b.IsDeleted != true);
                if (school == null)
                {
                    return NotFound("school not found.");
                }
            }
            else
            {
                newData.SchoolId = null;
            }
            if (newData.SchoolPCId != 0 && newData.SchoolPCId != null)
            {
               SchoolPCs SchoolPCId = Unit_Of_Work.schoolPCs_Repository.First_Or_Default(b => b.ID == newData.SchoolPCId && b.IsDeleted != true);
                if (SchoolPCId == null)
                {
                    return NotFound("SchoolPCId not found.");
                }
            }
            else
            {
                newData.SchoolPCId = null;
            }
            if (newData.StoreToTransformId != 0 && newData.StoreToTransformId != null)
            {
                Store StoreToTransform = Unit_Of_Work.store_Repository.First_Or_Default(b => b.ID == newData.StoreToTransformId && b.IsDeleted != true);
                if (StoreToTransform == null)
                {
                    return NotFound("store not found.");
                }
            }
            else
            {
                newData.StoreToTransformId = null;
            }

            LMS_CMS_DAL.Models.Domains.Inventory.InventoryFlags flag = Unit_Of_Work.inventoryFlags_Repository.First_Or_Default(b => b.ID == newData.FlagId);
            if (flag == null)
            {
                return NotFound("flag not found.");
            }

            if (newData.BankID != 0 && newData.BankID != null)
            {
                Bank bank = Unit_Of_Work.bank_Repository.First_Or_Default(b => b.ID == newData.BankID && b.IsDeleted != true);
                if (bank == null)
                {
                    return NotFound("Bank not found.");
                }
            }
            else
            {
                newData.BankID = null;
            }

            if (newData.SaveID != 0 && newData.SaveID != null)
            {
                Save save = Unit_Of_Work.save_Repository.First_Or_Default(b => b.ID == newData.SaveID && b.IsDeleted != true);
                if (save == null)
                {
                    return NotFound("Save not found.");
                }
            }
            else
            {
                newData.SaveID = null;
            }

            if (newData.InventoryDetails.Count == 0)
            {
                return BadRequest("InventoryDetails IsRequired");
            }

            /// Validations

            if (newData.IsVisa == false)
            {
                newData.VisaAmount = 0;
                newData.BankID = null;
            }
            if (newData.IsCash == false)
            {
                newData.CashAmount = 0;
                newData.SaveID = null;
            }

            if (newData.IsVisa == true && newData.BankID == 0 || newData.IsVisa == true && newData.BankID == null)
            {
                return BadRequest("Bank IsRequired");

            }
            if (newData.IsCash == true && newData.SaveID == 0 || newData.IsCash == true && newData.SaveID == null)
            {
                return BadRequest("Safe IsRequired");

            }

            double expectedItemsPrice = newData.InventoryDetails?.Sum(item => item.TotalPrice) ?? 0;
            if (newData.Total != expectedItemsPrice)
            {
                return BadRequest("Total should be sum up all the totalPrice values in InventoryDetails");

            }
            newData.FlagId = 12;

            if (newData.FlagId == 8 || newData.FlagId == 9 || newData.FlagId == 10 || newData.FlagId == 11 || newData.FlagId == 12)
            {
                double expectedRemaining = (newData.Total) - ((newData.CashAmount ?? 0) + (newData.VisaAmount ?? 0));
                if (expectedRemaining != newData.Remaining)
                {
                    return BadRequest("Total should be sum up all the totalPrice values in InventoryDetails");
                }


            }

            newData.InventoryDetails.RemoveAll(item =>
            {
                var shopItem = Unit_Of_Work.shopItem_Repository.First_Or_Default(s => s.ID == item.ShopItemID && s.IsDeleted != true);
                return shopItem == null;
            });

            /// Create
            InventoryMaster Master = mapper.Map<InventoryMaster>(newData);
            LMS_CMS_Context db = Unit_Of_Work.inventoryMaster_Repository.Database();
            Master.InvoiceNumber = await _InVoiceNumberCreate.GetNextInvoiceNumber(db, newData.StoreID, newData.FlagId);
            if (Master == null)
            {
                return BadRequest("Failed to map sale object.");
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            Master.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                Master.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                Master.InsertedByUserId = userId;
            }

            Unit_Of_Work.inventoryMaster_Repository.Add(Master);
            await Unit_Of_Work.SaveChangesAsync();
            long SaleID = 0;
            SaleID = Master.ID;

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Master");
            var saleFolder = Path.Combine(baseFolder, Master.ID.ToString());

            if (!Directory.Exists(saleFolder))
            {
                Directory.CreateDirectory(saleFolder);
            }

            if (Master.Attachments == null)
            {
                Master.Attachments = new List<string>();
            }

            if (newData.Attachment != null)
            {
                foreach (var item in newData.Attachment)
                {
                    if (item != null)
                    {
                        var filePath = Path.Combine(saleFolder, item.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/Sales/{SaleID}/{item.FileName}";
                        Master.Attachments.Add(fileUrl);
                    }
                }
            }

            Master.uuid = Guid.NewGuid().ToString();
            Master.VatPercent = vat;
            Master.VatAmount = Master.Total * Master.VatPercent;
            Master.TotalWithVat = Master.Total + Master.VatAmount;
            Master.SchoolPCId = newData.SchoolPCId;

            Unit_Of_Work.inventoryMaster_Repository.Update(Master);
            await Unit_Of_Work.SaveChangesAsync();

            if (Master.FlagId == 11 || Master.FlagId == 12)
            {
                Master.School = school;

                List<InventoryMaster> masters = Unit_Of_Work.inventoryMaster_Repository.SelectQuery<InventoryMaster>(i => i.IsDeleted != true).ToList();

                string lastInvoiceHash = "";

                if (masters.Count > 1 || masters is not null)
                {
                    if (Master.FlagId == 11)
                        lastInvoiceHash = masters[masters.Count - 2].InvoiceHash;
                }

                bool result = InvoicingServices.GenerateInvoiceXML(Master, lastInvoiceHash);

                if (!result)
                    return BadRequest("Failed to generate XML file.");

                DateTime invDate = DateTime.Parse(newData.Date);
                string date = invDate.ToString("yyyy-MM-dd");
                string time = invDate.ToString("HH:mm:ss").Replace(":", "");

                string xml = string.Empty;
                if (Master.FlagId == 11)
                    xml = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/XMLInvoices/{Master.School.CRN}_{date.Replace("-", "")}T{time}_{date}-{Master.StoreID}_{Master.FlagId}_{Master.ID}.xml");

                if (Master.FlagId == 12)
                    xml = Path.Combine(Directory.GetCurrentDirectory(), $"Invoices/XMLCredits/{Master.School.CRN}_{date.Replace("-", "")}T{time}_{date}-{Master.StoreID}_{Master.FlagId}_{Master.ID}.xml");

                Master.InvoiceHash = InvoicingServices.GetInvoiceHash(xml);
                Master.QRCode = InvoicingServices.GetQRCode(xml);
                Master.uuid = InvoicingServices.GetUUID(xml);
                Master.XmlInvoiceFile = xml;
                Master.QrImage = InvoicingServices.GenerateQrImage(Master.QRCode);

                Unit_Of_Work.inventoryMaster_Repository.Update(Master);
                await Unit_Of_Work.SaveChangesAsync();
            }

            return Ok(Master.ID);
        }


        /////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        [Consumes("multipart/form-data")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
             pages: new[] { "Inventory" }
        )]
        public async Task<IActionResult> EditAsync([FromForm] InventoryMasterEditDTO newSale )
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


            Store store = Unit_Of_Work.store_Repository.First_Or_Default(b => b.ID == newSale.StoreID && b.IsDeleted != true);
            if (store == null)
            {
                return NotFound("Store cannot be null");
            }

            if (newSale.StudentID != 0 && newSale.StudentID != null)
            {
                Student student = Unit_Of_Work.student_Repository.First_Or_Default(b => b.ID == newSale.StudentID && b.IsDeleted != true);
                if (student == null)
                {
                    return NotFound("There Is No Student With This Id");
                }

            }
            else
            {
                newSale.StudentID = null;
            }

            InventoryFlags flag = Unit_Of_Work.inventoryFlags_Repository.First_Or_Default(b => b.ID == newSale.FlagId);
            if (flag == null)
            {
                return NotFound("There Is No Flag With This Id");
            }

            if (newSale.BankID != 0 && newSale.BankID != null)
            {
                Bank bank = Unit_Of_Work.bank_Repository.First_Or_Default(b => b.ID == newSale.BankID && b.IsDeleted != true);
                if (bank == null)
                {
                    return NotFound("There Is No Bank With This Id");
                }
            }
            else
            {
                newSale.BankID = null;
            }

            if (newSale.SupplierId != 0 && newSale.SupplierId != null)
            {
                Supplier supplier = Unit_Of_Work.supplier_Repository.First_Or_Default(b => b.ID == newSale.SupplierId && b.IsDeleted != true);
                if (supplier == null)
                {
                    return NotFound("Supplier not found.");
                }
            }
            else
            {
                newSale.SupplierId = null;
            }

            if (newSale.SaveID != 0 && newSale.SaveID != null)
            {
                Save save = Unit_Of_Work.save_Repository.First_Or_Default(b => b.ID == newSale.SaveID && b.IsDeleted != true);
                if (save == null)
                {
                    return NotFound("There Is No Save With This Id");
                }
            }
            else
            {
                newSale.SaveID = null;
            }
            if (newSale.StoreToTransformId != 0 && newSale.StoreToTransformId != null)
            {
                Store StoreToTransform = Unit_Of_Work.store_Repository.First_Or_Default(b => b.ID == newSale.StoreToTransformId && b.IsDeleted != true);
                if (StoreToTransform == null)
                {
                    return NotFound("store not found.");
                }
            }
            else
            {
                newSale.StoreToTransformId = null;
            }

            InventoryMaster sale = Unit_Of_Work.inventoryMaster_Repository.First_Or_Default(s => s.ID == newSale.ID && s.IsDeleted != true);
            if (sale == null || sale.IsDeleted == true)
            {
                return NotFound("There Is No InventoryMaster With This Id");
            }

            /// Validations

            if (newSale.IsVisa == false)
            {
                newSale.VisaAmount = 0;
                newSale.BankID = null;
            }
            if (newSale.IsCash == false)
            {
                newSale.CashAmount = 0;
                newSale.SaveID = null;
            }

            if (newSale.IsVisa == true && newSale.BankID == 0 || newSale.IsVisa == true && newSale.BankID == null)
            {
                return BadRequest("Bank IsRequired");

            }
            if (newSale.IsCash == true && newSale.SaveID == 0 || newSale.IsCash == true && newSale.SaveID == null)
            {
                return BadRequest("Safe IsRequired");

            }

            if (newSale.FlagId == 8 || newSale.FlagId == 9 || newSale.FlagId == 10 || newSale.FlagId == 11 || newSale.FlagId == 12)
            {
                double expectedRemaining = (newSale.Total) - ((newSale.CashAmount ?? 0) + (newSale.VisaAmount ?? 0));
                if (expectedRemaining != newSale.Remaining)
                {
                    return BadRequest("Total should be sum up all the totalPrice values in InventoryDetails");
                }


            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Inventory", roleId, userId, sale);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newSale, sale);
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Master");
            var saleFolder = Path.Combine(baseFolder, sale.ID.ToString());

            if (!Directory.Exists(saleFolder))
            {
                Directory.CreateDirectory(saleFolder);
            }

            if (sale.Attachments == null)
            {
                sale.Attachments = new List<string>();
            }


            //Edit Invoice Number if Converted From Purchase Order to Purchases
            if (newSale.IsEditInvoiceNumber==true)
            {
                LMS_CMS_Context db = Unit_Of_Work.inventoryMaster_Repository.Database();
                sale.InvoiceNumber = await _InVoiceNumberCreate.GetNextInvoiceNumber(db, newSale.StoreID, newSale.FlagId);
            }
            // Add new attachments
            if (newSale.NewAttachments != null)
            {
                foreach (var item in newSale.NewAttachments)
                {
                    if (item != null)
                    {
                        var filePath = Path.Combine(saleFolder, item.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/Master/{newSale.ID}/{item.FileName}";
                        sale.Attachments.Add(fileUrl);
                    }
                }
            }

            if (newSale.DeletedAttachments != null)
            {
                foreach (var fileUrl in newSale.DeletedAttachments)
                {
                    var fileName = Path.GetFileName(fileUrl); // Extract just the filename from the URL
                    var filePath = Path.Combine(saleFolder, fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    sale.Attachments.Remove(fileUrl);
                }
            }


            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            sale.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                sale.UpdatedByOctaId = userId;
                if (sale.UpdatedByUserId != null)
                {
                    sale.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                sale.UpdatedByUserId = userId;
                if (sale.UpdatedByOctaId != null)
                {
                    sale.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.inventoryMaster_Repository.Update(sale);
            Unit_Of_Work.SaveChanges();

           
            return Ok(newSale);
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
        allowedTypes: new[] { "octa", "employee" },
        allowDelete: 1,
        pages: new[] { "Inventory" }
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
                return BadRequest("Enter Store ID");
            }

            InventoryMaster sales = Unit_Of_Work.inventoryMaster_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == id);

            if (sales == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Inventory", roleId, userId, sales);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            sales.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            sales.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                sales.DeletedByOctaId = userId;
                if (sales.DeletedByUserId != null)
                {
                    sales.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                sales.DeletedByUserId = userId;
                if (sales.DeletedByOctaId != null)
                {
                    sales.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.inventoryMaster_Repository.Update(sales);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

    
    }
}
