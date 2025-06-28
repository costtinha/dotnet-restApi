using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeApi.Data;
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

        public async Task<List<Office>> FindAllAsync()
        {
            return await _context.Offices.ToListAsync();
        }

        public async Task<Office> FindOfficeById(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            if (office == null)
            {
                throw new KeyNotFoundException($"Office with ID {id} not Found");
            }
            return office;
        }

        public async Task<List<Office>> FindOfficeByCity(string city)
        {
            return await _context.Offices.Where(o => o.City == city).ToListAsync();
        }

        public async Task<List<Office>> FindOfficeByState(string state)
        {
            return await _context.Offices.Where(o => o.State == state).ToListAsync();
        }

        public async Task<List<Office>> FindOfficeByCountry(string country)
        {
            return await _context.Offices.Where(o => o.Country == country).ToListAsync();
        }

        public async Task SaveOfficeAsync(Office office)
        {
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
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