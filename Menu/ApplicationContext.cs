using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<EmployeeModel>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);


        }
    }
}
