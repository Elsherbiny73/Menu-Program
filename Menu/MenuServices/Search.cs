using Services.AddEmployee;
using Services.DepartmentServic;

namespace Services.MenuServices
{
    public static class SearchEntities
    {
        public static void SearchEntity(EmployeeService employeeService, DepartmentServices departmentService)
        {
            Console.Clear();
            Console.WriteLine("What would you like to Search?");
            Console.WriteLine("1. Search Employees");
            Console.WriteLine("2. Search Departments");
            Console.Write("Enter your choice (1 or 2): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    employeeService.SearchEmployee(); // Display employees from the database
                    break;

                case "2":
                    departmentService.SearchDepartment(); // Display departments from the database
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
