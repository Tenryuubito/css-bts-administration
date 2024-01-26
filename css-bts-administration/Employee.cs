using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace css_bts_administration
{
    class Employee
    {

        private string first_name;
        protected string last_name;
        protected string adress;
        protected string phone_number;
        protected string email;
        protected string position;
        protected long company_entry;
        protected float salary;
        protected long pension_start;

        public Employee(string first_name, string last_name, string adress, string phone_number, string email, string position, long company_entry, float salary, long pension_start)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.adress = adress;
            this.phone_number = phone_number;
            this.email = email;
            this.position = position;
            this.company_entry = company_entry;
            this.salary = salary;
            this.pension_start = pension_start;
        }

        string Name { get => first_name; set => first_name = value; }
         string Surname { get => last_name; set => last_name = value; }

         string Adress { get => adress; set => adress = value; }

         string Phone_Number { get => phone_number; set => phone_number = value; }

         string Email { get => email; set => email = value; }
         string Position { get => position; set => position = value; }

         long Company_Entry { get => company_entry; set => company_entry = value; }

         float Salary { get => salary; set => salary = value; }

         long Pension_Start { get => pension_start; set => pension_start = value; }


    }
}
