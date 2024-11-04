using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {

        UOW unitOfWork;
        private readonly IMapper _mapper;
        public DomainController(UOW unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Domain> domains = unitOfWork.domain_Repository.Select_All_With_Includes(d => d.Schools);

            if (domains == null || !domains.Any())
            {
                return NotFound();
            }
            List<DomainDTO> domainDtos = _mapper.Map<List<DomainDTO>>(domains);

            // Return the mapped DTOs
            return Ok(domainDtos);
        }

        //----------Get Client by id----------//
        [HttpGet("{domainId}")]
        public async Task<IActionResult> GetDomain(int domainId)
        {
            var domain = await unitOfWork.domain_Repository.FindByIncludesAsync(
                e => e.Id == domainId, 
                query => query.Include(d => d.Schools) 
                              .ThenInclude(s => s.Employees) 
            );

            if (domain == null)
            {
                return NotFound();
            }

            return Ok(domain);
        }

        ////----------Update Client----------//

        //[HttpPut]

        //public IActionResult EditBreed(BreedGetDTO newBreed)
        //{
        //    if (newBreed == null) { BadRequest(); }
        //    Breed breed = mapper.Map<Breed>(newBreed);
        //    unitOfWork.breedRepository.update(breed);
        //    unitOfWork.SaveChanges();
        //    return Ok(newBreed);
        //}

        ////----------Delete Client----------//

        //[HttpDelete]

        //public IActionResult deleteBreed(int id)
        //{
        //    List<Pet_Breed> PetBreeds = unitOfWork.pet_BreedRepository.FindBy(p => p.BreedID == id);
        //    foreach (var item in PetBreeds)
        //    {
        //        unitOfWork.pet_BreedRepository.deleteEntity(item);
        //    }
        //    unitOfWork.SaveChanges();
        //    unitOfWork.breedRepository.delete(id);
        //    unitOfWork.SaveChanges();
        //    return Ok();

        //}


    }
}
