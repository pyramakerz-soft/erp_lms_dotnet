using AutoMapper;
using LMS_CMS_BL.DTO.Administration;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers.Domains.Inventory
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryCategoriesController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public InventoryCategoriesController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }


        ///////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Inventory Categories", "Inventory" }
        )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InventoryCategories> InventoryCategories = await Unit_Of_Work.inventoryCategories_Repository.Select_All_With_IncludesById<InventoryCategories>(
                    b => b.IsDeleted != true);

            if (InventoryCategories == null || InventoryCategories.Count == 0)
            {
                return NotFound();
            }

            List<InventoryCategoriesGetDto> inventoryCategoriesGetDto = mapper.Map<List<InventoryCategoriesGetDto>>(InventoryCategories);

            return Ok(inventoryCategoriesGetDto);
        }
    }
}
