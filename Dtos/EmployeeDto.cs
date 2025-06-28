using System.ComponentModel.DataAnnotations;

namespace OfficeApi.Dtos
{
    public record EmployeeDto()
    {
        [Required]
        public string? name { get; set; }

        [Required]
        public int OfficeCode;
    }    
}