using Services.Models;

namespace Services.Sorting
{
    public static class EmployeeSorting
    {
        // Sorting by different properties using List<T>.Sort(Comparison<T>)
        public static void SortByID(List<EmployeeModel> employees, bool ascending = true)
        {
            employees.Sort((x, y) => ascending ? x.ID.CompareTo(y.ID) : y.ID.CompareTo(x.ID));
        }

        public static void SortByName(List<EmployeeModel> employees, bool ascending = true)
        {
            employees.Sort((x, y) => ascending ? x.Name.CompareTo(y.Name) : y.Name.CompareTo(x.Name));
        }

        public static void SortByAge(List<EmployeeModel> employees, bool ascending = true)
        {
            employees.Sort((x, y) => ascending ? x.Age.CompareTo(y.Age) : y.Age.CompareTo(x.Age));
        }

        public static void SortBySalary(List<EmployeeModel> employees, bool ascending = true)
        {
            employees.Sort((x, y) => ascending ? x.Salary.CompareTo(y.Salary) : y.Salary.CompareTo(x.Salary));
        }
    }
}
