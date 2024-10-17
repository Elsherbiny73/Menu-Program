using Services.AddEmployee;
using Services.DepartmentServic;

namespace Services.MenuServices
{
    public static class DisplayEntities
    {
        public static void DisplayEntity(EmployeeService employeeService, DepartmentServices departmentService)
        {
            Console.Clear();
            Console.WriteLine("What would you like to display?");
            Console.WriteLine("1. Display Employees");
            Console.WriteLine("2. Display Departments");
            Console.Write("Enter your choice (1 or 2): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    employeeService.PrintEmployees(); // Display employees from the database
                    break;

                case "2":
                    departmentService.PrintDepartments(); // Display departments from the database
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
