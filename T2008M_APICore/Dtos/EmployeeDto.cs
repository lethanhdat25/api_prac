using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T2008M_APICore.Dtos
{
    public class EmployeeDto
    {
        [Required]
        public string FisrtName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        public string PhoneNumber { get; init; }
        [Required]
        public string Email { get; init; }
    }
}
