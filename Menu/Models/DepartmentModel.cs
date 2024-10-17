using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<EmployeeModel> Employees { get; set; }
    }
}
