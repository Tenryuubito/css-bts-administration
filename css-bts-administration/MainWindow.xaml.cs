using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
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

            ReloadEmployeeList();
        }

        public void OnClick_addNewMember(object sender, RoutedEventArgs e)
        {
            EmployeeListView.Visibility = Visibility.Collapsed;
            EmployeeForm.Visibility = Visibility.Visible;
            
            EmployeeListView.Visibility = Visibility.Visible;
            EmployeeForm.Visibility = Visibility.Collapsed;
        }

        private void ReloadEmployeeList()
        {
            EmployeeListView.Items.Clear();
            foreach (Employee employee in context.Employees)
            {
                EmployeeListView.Items.Add(employee);
            }
        }

        public void OnClick_deleteMember(object sender, RoutedEventArgs e)
        {
            foreach (Employee employee in EmployeeListView.SelectedItems)
            {
                context.Employees.Remove(employee);
            }

            context.SaveChanges();
            
            ReloadEmployeeList();
        }

        public void OnClick_exportMembers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        public void OnClick_importMembers(object sender, RoutedEventArgs e) //TODO: Duplicates Entries if clicked again
        {
            string line;
            StreamReader sr = new StreamReader("C:\\Users\\Erik\\RiderProjects\\itprojekt2\\importEmployees.txt");

                                        
            while (sr.Peek() >= 0)
            {
                Employee employee = new Employee();
                employee.FirstName = sr.ReadLine();
                employee.LastName = sr.ReadLine();
                employee.Address = sr.ReadLine();
                employee.PhoneNumber = sr.ReadLine();
                employee.Email = sr.ReadLine();
                employee.Position = sr.ReadLine();
                employee.CompanyEntry = 123;
                employee.Salary = 123;
                employee.PensionStart = 123;
                context.Employees.Add(employee);
                context.SaveChanges();
            }
            
            sr.Close();
            ReloadEmployeeList(); 
            Console.WriteLine("HERE");
            
           
        }

        public void OnClick_searchMember(object sender, RoutedEventArgs e)
        {
            string text = input.Text; 
            var found_employees = from b in context.Employees
                where b.FirstName.Contains(text) || b.LastName.Contains(text) || b.Address.Contains(text) || b.Salary.ToString().Contains(text) || b.Email.Contains(text)
                || b.Position.Contains(text) || b.CompanyEntry.ToString().Contains(text) || b.PhoneNumber.Contains(text) || b.CompanyEntry.ToString().Contains(text) ||
                b.PensionStart.ToString().Contains(text)
                select b;

            EmployeeListView.Items.Clear();
            foreach (Employee employee in found_employees)
            {
                EmployeeListView.Items.Add(employee);
            }
            
        }

        public void OnClick_editMember(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }
    }


}
