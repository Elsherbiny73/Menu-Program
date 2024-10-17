using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    public class EmployeeModel : Human
    {
        [Key]
        public int ID { get; set; }  // Changed to public set

        public decimal Salary { get; set; }

        // Foreign key for Department
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        // Parameterless constructor for Entity Framework
        public EmployeeModel() : base("", 0, Gender.Male) { }

        public EmployeeModel(string name, int age, Gender gender, decimal salary)
            : base(name, age, gender)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return $"{Name,-15} {ID,-5} {Age,-5} {Gender,-10} {Salary.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))} {Department?.Name ?? "N/A"}";
        }
    }
}