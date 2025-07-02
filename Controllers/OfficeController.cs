using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OfficeApi.Dtos;
using OfficeApi.Models;
using OfficeApi.Repositories;

namespace OfficeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly OfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public OfficeController(OfficeRepository officeRepository, IMapper mapper)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<List<OfficeResponseDto>> GetAllOffices()
        {
            return await _officeRepository.FindAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<OfficeResponseDto> GetOfficeById(int id)
        {
            return await _officeRepository.FindOfficeById(id);
        }

        [HttpGet("city/{city}")]
        public async Task<List<OfficeResponseDto>> GetOfficeByCityName(string city)
        {
            return await _officeRepository.FindOfficeByCity(city);
        }
        [HttpGet("state/{state}")]
        public async Task<List<OfficeResponseDto>> GetOfficeByStateName(string state)
        {
            return await _officeRepository.FindOfficeByState(state);
        }
        [HttpGet("country/{country}")]
        public async Task<List<OfficeResponseDto>> GetOfficeByCountryName(string country)
        {
            return await _officeRepository.FindOfficeByCountry(country);
        }
        [HttpPost]
        public async Task<ActionResult<OfficeResponseDto>> CreateOffice([FromBody] OfficeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var officeResponse = _officeRepository.SaveOfficeAsync(dto);
            Office office = _mapper.Map<Office>(officeResponse);
            return CreatedAtAction(nameof(GetOfficeById), new {id = office.Code}, officeResponse);

        }

    }
}