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
        public async Task<ActionResult<IEnumerable<OfficeResponseDto>>> GetAllOffices()
        {
            var offices = await _officeRepository.FindAllAsync();
            if (!offices.Any())
            {
                return NotFound("No offices found");
            }
            return offices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeResponseDto>> GetOfficeById(int id)
        {
            return await _officeRepository.FindOfficeById(id);
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<OfficeResponseDto>>> GetOfficeByCityName(string city)
        {
            return await _officeRepository.FindOfficeByCity(city);
        }
        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<OfficeResponseDto>>> GetOfficeByStateName(string state)
        {
            return await _officeRepository.FindOfficeByState(state);
        }
        [HttpGet("country/{country}")]
        public async Task<ActionResult<IEnumerable<OfficeResponseDto>>> GetOfficeByCountryName(string country)
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

            var (officeCode , officeResponse) = await _officeRepository.SaveOfficeAsync(dto);
            return CreatedAtAction(nameof(GetOfficeById), new { id = officeCode }, officeResponse);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OfficeResponseDto>> UpdateOffice(int id, [FromBody] OfficeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var officeResponse = await _officeRepository.UpdateOfficeAsync(id, dto);
                return Ok(officeResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            try
            {
                await _officeRepository.DeleteOfficeById(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}