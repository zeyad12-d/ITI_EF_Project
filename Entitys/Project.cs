using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTask.Entitys
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Project Name is Required")]

        public string ProjectName { get; set; }

        public  DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

      
       
        public ICollection<Employee> Employess { get; set; } = new List<Employee>();
    }
}
