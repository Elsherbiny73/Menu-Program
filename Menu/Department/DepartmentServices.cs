using Services.Models;
using Services.Sorting;

namespace Services.DepartmentServic
{
    public class DepartmentServices
    {
        private readonly ApplicationContext _context;

        // Constructor accepting ApplicationContext directly
        public DepartmentServices(ApplicationContext context)
        {
            _context = context;
        }

        public Department AddDepartment()
        {
            Console.Clear();
            Console.WriteLine("Enter Department Details:");

            Console.Write("Department Name: ");
            string departmentName = Console.ReadLine();

            // Create a new Department object
            Department department = new Department { Name = departmentName };

            // Save the department to the database
            _context.Departments.Add(department);
            _context.SaveChanges(); // Save changes to the database

            Console.WriteLine("\nDepartment added successfully!");
            Console.ReadLine();
            return department;
        }

        public void AddDepartments()
        {
            bool adding = true;

            while (adding)
            {
                AddDepartment(); // Call the method to add a single department

                Console.Write("\nDo you want to add another department? (Y/N): ");
                string input = Console.ReadLine().Trim().ToUpper();

                if (input != "Y")
                {
                    adding = false;
                }
                else
                {
                    Console.Clear();
                }
            }
        }
        public void PrintDepartments()
        {
            Console.Clear(); // Clear the console before displaying sorted data

            var departments = _context.Departments.ToList();

            if (departments.Count == 0)
            {
                Console.WriteLine("No departments available to display.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Select sorting criteria:");
            Console.WriteLine("1. Sort by ID (Ascending)");
            Console.WriteLine("2. Sort by ID (Descending)");
            Console.WriteLine("3. Sort by Name (Ascending)");
            Console.WriteLine("4. Sort by Name (Descending)");
            Console.Write("Enter your choice (1-4): ");
            string choice = Console.ReadLine();

            // Apply sorting based on user choice
            switch (choice)
            {
                case "1":
                    DepartmentSorting.SortByID(departments, ascending: true);
                    break;
                case "2":
                    DepartmentSorting.SortByID(departments, ascending: false);
                    break;
                case "3":
                    DepartmentSorting.SortByName(departments, ascending: true);
                    break;
                case "4":
                    DepartmentSorting.SortByName(departments, ascending: false);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Sorting by ID (Ascending) by default.");
                    DepartmentSorting.SortByID(departments);
                    break;
            }
            Console.Clear();

            // Display the sorted departments
            Console.WriteLine("{0,-10} {1,-20}", "ID", "Department Name");
            Console.WriteLine(new string('-', 40));

            foreach (var department in departments)
            {
                Console.WriteLine("{0,-10} {1,-20}", department.ID, department.Name);
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine(); // Wait for the user to press Enter before exiting
        }

        public void SearchDepartment()
        {
            Console.Clear();
            Console.WriteLine("Search Department by:");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Name");
            Console.Write("Enter your choice (1 or 2): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Department ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var department = _context.Departments.FirstOrDefault(dep => dep.ID == id);

                        if (department != null)
                        {
                            DisplayDepartmentDetails(department);
                        }
                        else
                        {
                            Console.WriteLine("\nDepartment not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid ID format.");
                    }
                    break;

                case "2":
                    Console.Write("Enter Department Name: ");
                    string name = Console.ReadLine();

                    var depByName = _context.Departments
                                            .FirstOrDefault(dep => dep.Name.ToLower() == name.ToLower());

                    if (depByName != null)
                    {
                        DisplayDepartmentDetails(depByName);
                    }
                    else
                    {
                        Console.WriteLine("\nDepartment not found.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select either 1 or 2.");
                    break;
            }
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }


        private void DisplayDepartmentDetails(Department department)
        {
            Console.WriteLine("\nDepartment Found:");
            Console.WriteLine($"ID: {department.ID}");
            Console.WriteLine($"Name: {department.Name}");
        }

    }
}
