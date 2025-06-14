

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeApi.Models
{
    public class Office
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string State { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [StringLength(100)]
        public required string Territory { get; set; }


        [JsonIgnore]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        


    }
}