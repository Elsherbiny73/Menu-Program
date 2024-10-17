using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services;

namespace MenuPresentation
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite("Data Source=employee_department.db");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }

}
