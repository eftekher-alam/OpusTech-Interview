using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpusTechInterview.Models
{
    public class EmployeeVM
    {
        [Key]
        [StringLength(4, ErrorMessage = "Length must be 4 char", MinimumLength = 4)]
        [Display(Name = "Employee Code")]
        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required, Column(TypeName = "date"), Display(Name = "Date of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        public IFormFile Photo { get; set; }
    }
}
