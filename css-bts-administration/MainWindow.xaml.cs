using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace css_bts_administration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeContext context = new EmployeeContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnClick_addNewMember(object sender, RoutedEventArgs e)
        {
            context = new EmployeeContext();
            Employee employee = new Employee();
            employee.FirstName = "Test";
            employee.LastName = "Test";
            employee.PhoneNumber = "Test";
            employee.Salary = 12.34f;
            employee.Position = "Test";
            employee.Address = "Test";
            employee.Email = "Test";
            employee.CompanyEntry = 242424242;
            employee.PensionStart = 3525141414;

            context.Employees.Add(employee);
            context.SaveChanges();
            MessageBox.Show("Add new Member completed.");
        }


        public void OnClick_deleteMember(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        public void OnClick_exportMembers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        public void OnClick_importMembers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        public void OnClick_searchMember(object sender, RoutedEventArgs e)
        {
            string var1;
            var1 = input.Text;

        }

        public void OnClick_editMember(object sender, RoutedEventArgs e)
        {
            foreach (Employee employee in context.Employees)
                MessageBox.Show(employee.Email);
        }
    }


}
