using Microsoft.EntityFrameworkCore;
using Services.Models;
using System.Globalization;

namespace Services.AddEmployee
{
    public class EmployeeService
    {
        private readonly ApplicationContext _context;

        public EmployeeService(ApplicationContext context)
        {
            _context = context;
        }

        public EmployeeModel AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("Enter Employee Details:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());

            Gender gender;
            while (true)
            {
                Console.Write("Gender (M/F): ");
                string genderInput = Console.ReadLine();
                if (genderInput.Equals("M", StringComparison.OrdinalIgnoreCase))
                {
                    gender = Gender.Male;
                    break;
                }
                else if (genderInput.Equals("F", StringComparison.OrdinalIgnoreCase))
                {
                    gender = Gender.Female;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid gender input. Please enter M for Male or F for Female.");
                }
            }

            Console.Write("Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine());

            var departments = _context.Departments.ToList();
            if (departments.Count == 0)
            {
                Console.WriteLine("No departments available. Please add a department first.");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nSelect a Department:");
            for (int i = 0; i < departments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {departments[i].Name}");
            }

            int departmentChoice;
            while (true)
            {
                Console.Write("Enter the number of the department: ");
                if (int.TryParse(Console.ReadLine(), out departmentChoice) &&
                    departmentChoice > 0 &&
                    departmentChoice <= departments.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid department number.");
                }
            }

            Department selectedDepartment = departments[departmentChoice - 1];

            EmployeeModel employee = new EmployeeModel(name, age, gender, salary)
            {
                DepartmentId = selectedDepartment.ID
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            Console.WriteLine("\nEmployee added successfully!");
            Console.ReadLine();

            return employee;
        }

        public void AddEmployees()
        {
            bool adding = true;

            while (adding)
            {
                AddEmployee();
                Console.Write("\nDo you want to add another employee? (Y/N): ");
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
        public void PrintEmployees()
        {
            var employees = _context.Employees.Include(e => e.Department).ToList();

            Console.Clear();

            Console.WriteLine("Select sorting criteria:");
            Console.WriteLine("1. Sort by ID (Ascending)");
            Console.WriteLine("2. Sort by ID (Descending)");
            Console.WriteLine("3. Sort by Name (Ascending)");
            Console.WriteLine("4. Sort by Name (Descending)");
            Console.WriteLine("5. Sort by Age (Ascending)");
            Console.WriteLine("6. Sort by Age (Descending)");
            Console.WriteLine("7. Sort by Salary (Ascending)");
            Console.WriteLine("8. Sort by Salary (Descending)");
            Console.Write("Enter your choice (1-8): ");
            string choice = Console.ReadLine();

            // Applying sorting based on user choice
            switch (choice)
            {
                case "1":
                    employees = employees.OrderBy(e => e.ID).ToList();
                    break;
                case "2":
                    employees = employees.OrderByDescending(e => e.ID).ToList();
                    break;
                case "3":
                    employees = employees.OrderBy(e => e.Name).ToList();
                    break;
                case "4":
                    employees = employees.OrderByDescending(e => e.Name).ToList();
                    break;
                case "5":
                    employees = employees.OrderBy(e => e.Age).ToList();
                    break;
                case "6":
                    employees = employees.OrderByDescending(e => e.Age).ToList();
                    break;
                case "7":
                    employees = employees.OrderBy(e => e.Salary).ToList();
                    break;
                case "8":
                    employees = employees.OrderByDescending(e => e.Salary).ToList();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Sorting by ID (Ascending) by default.");
                    employees = employees.OrderBy(e => e.ID).ToList();
                    break;
            }
            Console.Clear();

            // Print the sorted list of employees
            Console.WriteLine("{0,-10} {1,-13} {2,-5} {3,-10} {4,-15} {5,-15}", "ID", "Name", "Age", "Gender", "Salary", "Department");
            Console.WriteLine(new string('-', 75));

            foreach (var employee in employees)
            {
                Console.WriteLine("{0,-10} {1,-13} {2,-5} {3,-10} {4,-15} {5,-15}",
                                  employee.ID,
                                  employee.Name,
                                  employee.Age,
                                  employee.Gender,
                                  employee.Salary.ToString("C", CultureInfo.CreateSpecificCulture("en-US")),
                                  employee.Department?.Name ?? "N/A"); // Display department name
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine(); // Wait for the user to press Enter before exiting
        }

        public void SearchEmployee()
        {
            Console.Clear();
            Console.WriteLine("Search Employee by:");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Name");
            Console.Write("Enter your choice (1 or 2): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Employee ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var employee = _context.Employees.Include(e => e.Department)
                                                          .FirstOrDefault(emp => emp.ID == id);

                        if (employee != null)
                        {
                            DisplayEmployeeDetails(employee);
                        }
                        else
                        {
                            Console.WriteLine("\nEmployee not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid ID format.");
                    }
                    break;

                case "2":
                    Console.Write("Enter Employee Name: ");
                    string name = Console.ReadLine();

                    // Convert both to lowercase for a case-insensitive comparison
                    var empByName = _context.Employees.Include(e => e.Department)
                                                       .FirstOrDefault(emp => emp.Name.ToLower() == name.ToLower());

                    if (empByName != null)
                    {
                        DisplayEmployeeDetails(empByName);
                    }
                    else
                    {
                        Console.WriteLine("\nEmployee not found.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select either 1 or 2.");
                    break;
            }
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }


        private void DisplayEmployeeDetails(EmployeeModel employee)
        {
            Console.WriteLine("\nEmployee Found:");
            Console.WriteLine($"ID: {employee.ID}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Age: {employee.Age}");
            Console.WriteLine($"Gender: {employee.Gender}");
            Console.WriteLine($"Salary: {employee.Salary.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");
            Console.WriteLine($"Department: {employee.Department?.Name ?? "N/A"}");
        }

    }
}
