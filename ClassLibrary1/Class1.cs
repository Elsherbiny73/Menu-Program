namespace ClassLibrary1;

public class Class1
{
    public enum Gender  
    {
        Male,
        Female,
        Other
    }

    public class Employee : human
    {
        public int ID { get; set; }
        public int Salary { get; set; }

        public Employee(string name, int id, int salary, Gender gender, int age) 
            : base(name, age, gender)
        {
            ID = id;
            Salary = salary;
        }

        // Override 
        public override string ToString()
        {
            return base.ToString() + $", ID: {ID}, Salary: {Salary}";
        }

        public void DisplayData()
        {
            Console.WriteLine("Employee Details:");
            Console.WriteLine(this.ToString());
        }

        public static Employee ReadInfo()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Salary: ");
            int salary = int.Parse(Console.ReadLine());

            Console.Write("Select Gender (0 for Male, 1 for Female): ");
            Gender gender = (Gender)int.Parse(Console.ReadLine());

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            return new Employee(name, id, salary, gender, age);
        }
    }
}