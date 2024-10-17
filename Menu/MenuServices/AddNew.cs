using Services.AddEmployee;
using Services.DepartmentServic;

namespace Services.MenuServices
{
    public static class AddNew
    {
        public static void AddNewEntity(EmployeeService employeeService, DepartmentServices departmentService)
        {
            Console.Clear();
            Console.WriteLine("What would you like to add?");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Add Department");
            Console.Write("Enter your choice (1 or 2): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    employeeService.AddEmployees(); // Ensure this method adds to the database
                    break;

                case "2":
                    departmentService.AddDepartments(); // This should now work seamlessly
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

}
