using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace css_bts_administration
{
    class Employee
    {
        public Employee() { }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public long CompanyEntry { get; set; }
        public float Salary { get; set; }
        public long PensionStart { get; set; }
    }
}
