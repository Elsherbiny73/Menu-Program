using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Human
    {
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public Human(string name, int age, Gender gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"{Name,-15} {Age,-5} {Gender,-10}";
        }
    }
}
