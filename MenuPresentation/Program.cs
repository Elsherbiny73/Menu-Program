using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.AddEmployee;
using Services.DepartmentServic;
using Services.MenuServices;
using ClassLibrary1; // Import ClassLibrary1 to use Employee
using System;
using System.Collections.Generic;

namespace MenuPresentation
{
    internal class Program
    {
        static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate();
            }

            RunMenu();
        }

        static void RunMenu()
        {
            string[] menu = { "New", "Display", "Search", "Sort", "Exit" };
            int width = Console.WindowWidth / 2;
            int height = Console.WindowHeight / (menu.Length + 1);
            int highlight = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                for (int i = 0; i < menu.Length; i++)
                {
                    if (highlight == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    Console.SetCursorPosition(width, (i + 1) * height);
                    Console.Write(menu[i]);
                    Console.ResetColor();
                }

                ConsoleKeyInfo info = Console.ReadKey();
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        highlight--;
                        if (highlight < 0) highlight = menu.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        highlight++;
                        if (highlight >= menu.Length) highlight = 0;
                        break;
                    case ConsoleKey.Home:
                        highlight = 0;
                        break;
                    case ConsoleKey.End:
                        highlight = menu.Length - 1;
                        break;
                    case ConsoleKey.Enter:
                        ExecuteOption(menu[highlight]);
                        break;
                }
            } while (flag);
        }

        static void ExecuteOption(string option)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                var departmentService = scopedProvider.GetRequiredService<DepartmentServices>();
                var employeeService = scopedProvider.GetRequiredService<EmployeeService>();

                switch (option)
                {
                    case "New":
                        // Call AddNewEntity to add a new employee and department
                        Console.Clear();
                        AddNew.AddNewEntity(employeeService, departmentService);
                        Console.WriteLine("New employee and department added successfully.");
                        Console.ReadLine();
                        break;

                    case "Display":
                        // Display entities (employees and departments)
                        Console.Clear();
                        DisplayEntities.DisplayEntity(employeeService, departmentService); // Use display logic
                        Console.WriteLine("Display completed.");
                        Console.ReadLine();
                        break;

                    case "Search":
                        // Search employees or departments
                        Console.Clear();
                        SearchEntities.SearchEntity(employeeService, departmentService); // Use search logic
                        Console.WriteLine("Search completed.");
                        Console.ReadLine();
                        break;

                    case "Sort":
                        Console.Clear();
                        Console.WriteLine("Sort by: 1. Name 2. Salary");
                        int sortOption = int.Parse(Console.ReadLine());

                        Console.WriteLine("Choose: 1. Ascending 2. Descending");
                        int sortOrder = int.Parse(Console.ReadLine());
                        Console.WriteLine("Employees sorted successfully.");
                        Console.ReadLine();
                        break;

                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite("Data Source=employee_department.db"),
                ServiceLifetime.Scoped);

            services.AddScoped<EmployeeService>();
            services.AddScoped<DepartmentServices>();
        }
    }
}
