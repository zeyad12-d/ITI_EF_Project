using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTask.Entitys
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Department Name is Required")]
        public string DepartmentName { get; set; }

        public ICollection<Employee> Employess { get; set; } = new List<Employee>();
    }
}
