using System.Data.Entity;

namespace css_bts_administration
{
    class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

    }
}
