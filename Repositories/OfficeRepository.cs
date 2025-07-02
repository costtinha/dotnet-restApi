using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeApi.Data;
using OfficeApi.Dtos;
using OfficeApi.Models;

namespace OfficeApi.Repositories
{
    public class OfficeRepository
    {
        private readonly OfficeDbContext _context;
        private IMapper _mapper;

        public OfficeRepository(OfficeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OfficeResponseDto>> FindAllAsync()
        {
            var offices = await _context.Offices.ToListAsync();
            return _mapper.Map<List<OfficeResponseDto>>(offices);
        }

        public async Task<OfficeResponseDto> FindOfficeById(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            if (office == null)
            {
                throw new KeyNotFoundException($"Office with ID {id} not Found");
            }
            return _mapper.Map<OfficeResponseDto>(office);
        }

        public async Task<List<OfficeResponseDto>> FindOfficeByCity(string city)
        {

            var offices = await _context.Offices.Where(o => o.City == city).ToListAsync();
            return _mapper.Map<List<OfficeResponseDto>>(offices);
        }

        public async Task<List<OfficeResponseDto>> FindOfficeByState(string state)
        {
            var offices = await _context.Offices.Where(o => o.State == state).ToListAsync();
            return _mapper.Map<List<OfficeResponseDto>>(offices);
        }

        public async Task<List<OfficeResponseDto>> FindOfficeByCountry(string country)
        {
            var offices = await _context.Offices.Where(o => o.Country == country).ToListAsync();
            return _mapper.Map<List<OfficeResponseDto>>(offices);
        }

        public async Task<OfficeResponseDto> SaveOfficeAsync(OfficeDto dto)
        {
            Office office = _mapper.Map<Office>(dto);
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
            return _mapper.Map<OfficeResponseDto>(office);
        }

        public async Task DeleteOfficeById(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            if (office != null)
            {
                _context.Offices.Remove(office);
                await _context.SaveChangesAsync();
            }
        }

    }
}