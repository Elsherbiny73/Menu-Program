
using Services.Models;

namespace Services.Sorting
{
    public static class DepartmentSorting
    {
        // Sort by ID
        public static void SortByID(List<Department> departments, bool ascending = true)
        {
            departments.Sort((x, y) => ascending ? x.ID.CompareTo(y.ID) : y.ID.CompareTo(x.ID));
        }

        // Sort by Name
        public static void SortByName(List<Department> departments, bool ascending = true)
        {
            departments.Sort((x, y) => ascending ? x.Name.CompareTo(y.Name) : y.Name.CompareTo(x.Name));
        }
    }
}
