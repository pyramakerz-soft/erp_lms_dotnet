using AutoMapper;
using LMS_CMS_BL.DTO.Inventory;
using LMS_CMS_BL.UOW;
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
    public class InventoryFlagController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;
        public InVoiceNumberCreate _InVoiceNumberCreate;
        private readonly CheckPageAccessService _checkPageAccessService;


        public InventoryFlagController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Inventory" }
       )]
        public async Task<IActionResult> GetAll()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            List<InventoryFlags> Data = Unit_Of_Work.inventoryFlags_Repository.Select_All();

            if (Data == null)
            {
                return NotFound();
            }

            List<InventoryFlagGetDTO> DTO = mapper.Map<List<InventoryFlagGetDTO>>(Data);
            return Ok(DTO);
        }


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
                return BadRequest("Enter ID");
            }

            InventoryFlags Data = Unit_Of_Work.inventoryFlags_Repository.First_Or_Default( s =>s.ID == id);

            if (Data == null)
            {
                return NotFound();
            }

            InventoryFlagGetDTO DTO = mapper.Map<InventoryFlagGetDTO>(Data);
            return Ok(DTO);
        }
    }
}
