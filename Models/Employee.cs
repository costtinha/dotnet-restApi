

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OfficeApi.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public required string name { get; set; }

        [ForeignKey("Office")]
        [Required]
        public int OfficeCode { get; set; }

        [JsonIgnore]
        public Office? Office { get; set; }
    }
    
}