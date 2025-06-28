using System.ComponentModel.DataAnnotations;

namespace OfficeApi.Dtos
{
    public record OfficeDto
    {
        [Required]
        public string? City { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }


        [Required]
        public string? State { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public int PostalCode { get; set; }

        public string? Territory { get; set; }

    }
}