using Microsoft.EntityFrameworkCore;
using OfficeApi.Data;
using OfficeApi.Dtos;
using OfficeApi.Models;
using OfficeApi.Repositories;
//using OfficeApi.Data;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OfficeDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<OfficeDto, Office>();
    config.CreateMap<Office, OfficeResponseDto>()
    .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address));
    config.CreateMap<EmployeeDto, Employee>();
    config.CreateMap<Employee, EmployeeResponseDto>();
});


builder.Services.AddControllers();
builder.Services.AddScoped<OfficeRepository>();
builder.Services.AddScoped<EmployeeRepository>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


