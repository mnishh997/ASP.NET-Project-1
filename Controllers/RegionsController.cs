using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET http://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync(); 

            // Return the DTOs back to the client 
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }


        //GET Single Region (Get Region by Id)
        //GET http://localhost:portnumber/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {

            //Get Region domain model from the database
            var region = await regionRepository.GetByIdAsync(id);
            //var region = dbContext.Regions.Find(id); // find can only be used with primary key.

            if (region == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(region);


            // Return DTO back to client
            return Ok(regionDto);
        }

        // POST to Creat new Region
        // POST http://localhost:portnumber/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };


            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        // Update Region
        // PUT: http://locahost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = await regionRepository.GetByIdAsync(Id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel = await regionRepository.UpdateAsync(updateRegionRequestDto, regionDomainModel);


            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        //Delete Region
        // DELETE http://locahost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.GetByIdAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            await regionRepository.DeleteAsync(regionDomainModel);
            
            // It is optional to return the deleted object back. 

            return Ok();
        }
    }
}
