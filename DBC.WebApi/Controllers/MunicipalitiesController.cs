using AutoMapper;
using DBC.Infrastructure.Domains;
using DBC.Models;
using DBC.WebApi.DtoAndViews;
using DBC.WebApi.ResponseWrappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IMunicipalityDomainLogic _municipalityService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public MunicipalitiesController(IMunicipalityDomainLogic municipalityService,
            IMapper mapper, IUriService uriService)
        {
            _municipalityService = municipalityService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilterDto paginationDto)
        {
            var pagination = _mapper.Map<Pagination>(paginationDto);

            var (municipalities, totalCount) = await _municipalityService.GetMunicipalities(pagination);
            var vws = _mapper.Map<List<MunicipalityApiVw>>(municipalities);

            var route = Request.Path.Value;
            var responce = PaginationHelper.CreatePagedReponse<MunicipalityApiVw>(vws, pagination, totalCount, _uriService, route);

            return Ok(responce);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var municipality = await _municipalityService.GetAsync(id);
            var vw = _mapper.Map<MunicipalityApiVw>(municipality);

            return Ok(new Response<MunicipalityApiVw>(vw));
        }

        [HttpPost]
        public async Task<IActionResult> Post(MunicipalityapiDto dto)
        {
            var (municipality, error) = await _municipalityService.AddAsync(dto.Name);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var vw = _mapper.Map<MunicipalityApiVw>(municipality);

            return CreatedAtAction(nameof(Get), new { vw.Id }, new Response<MunicipalityApiVw>(vw));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, MunicipalityapiDto dto)
        {
            var (municipality, error) = await _municipalityService.EditAsync(id, dto.Name);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            var vw = _mapper.Map<MunicipalityApiVw>(municipality);

            return AcceptedAtAction(nameof(Get), new { vw.Id }, new Response<MunicipalityApiVw>(vw));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, MunicipalityapiDto dto)
        {
            var error = await _municipalityService.DeleteAsync(id);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(error);

            return NoContent();
        }
    }
}
