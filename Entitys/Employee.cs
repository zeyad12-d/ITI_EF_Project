using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTask.Entitys
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Employess Name is Required")]
        public string FullName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

       
       
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
