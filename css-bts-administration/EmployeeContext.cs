using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace css_bts_administration
{
    class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

    }
}
