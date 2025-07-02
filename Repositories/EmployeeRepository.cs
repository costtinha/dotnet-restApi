using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeApi.Data;
using OfficeApi.Dtos;
using OfficeApi.Models;

namespace OfficeApi.Repositories
{
    public class EmployeeRepository
    {
        private readonly OfficeDbContext _context;
        private IMapper _mapper;

        public EmployeeRepository(OfficeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponseDto>> FindAllEmployeeAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeResponseDto>>(employees);
        }

        public async Task<EmployeeResponseDto> FindEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with id {id} not found");
            }
            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<List<EmployeeResponseDto>> FindEmployeeByName(string name)
        {
            var employees = await _context.Employees.Where(e => e.name == name).ToListAsync();
            return _mapper.Map<List<EmployeeResponseDto>>(employees);

        }

        public async Task<List<EmployeeResponseDto>> FindEmployeeByOfficeCodeAsync(int code)
        {
            List<Employee> employees = await _context.Employees.Where(e => e.OfficeCode == code).ToListAsync();
            return _mapper.Map<List<EmployeeResponseDto>>(employees);
        }

        public async Task SaveEmployeeAsync(EmployeeDto dto)
        {
            Employee employee = _mapper.Map<Employee>(dto);
            _context.Add(employee);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                
            }

        }
    }
}