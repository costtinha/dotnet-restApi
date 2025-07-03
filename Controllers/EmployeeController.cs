using System.Runtime.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeApi.Dtos;
using OfficeApi.Repositories;

namespace OfficeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")
    ]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private OfficeRepository _officeRepository;

        public EmployeeController(EmployeeRepository employeeRepository, IMapper mapper, OfficeRepository officeRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _officeRepository = officeRepository;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> FindAllEmployees()
        {
            return await _employeeRepository.FindAllEmployeeAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> FindEmployeeById(int id)
        {
            //var employee = _employeeRepository.FindEmployeeById(id);
            //  if (employee == null)
            // {
            //    NotFound($"Employee with id {id} not found");
            //}
            // return _mapper.Map<EmployeeResponseDto>(employee);

            try
            {
                return await _employeeRepository.FindEmployeeById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Employee with id {id} not found");
                
            }
        }

        [HttpGet("/name/{name}")]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> FindEmployeeByName(string name)
        {
            return await _employeeRepository.FindEmployeeByName(name);
        }

        [HttpGet("officecode/{code}")]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> FindEmployeeByOfficeCode(int code)
        {
            return await _employeeRepository.FindEmployeeByOfficeCodeAsync(code);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDto>> SaveEmployee([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _officeRepository.FindOfficeById(dto.OfficeCode);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest($"Office with id {dto.OfficeCode} not found");
            }
            try
            {
                var (employeeId, employeeResponse) = await _employeeRepository.SaveEmployeeAsync(dto);
                return CreatedAtAction(nameof(FindEmployeeById), new { id = employeeId }, employeeResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating employee: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(int id, [FromBody] EmployeeDto dto) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var employeeResponse = await _employeeRepository.UpdateEmployeeAsync(id, dto);
                return Ok(employeeResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
                
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeByIdAsync(id);
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