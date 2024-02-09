using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace css_bts_administration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeContext _context = new EmployeeContext();

        public MainWindow()
        {
            InitializeComponent();

            ReloadEmployeeList();
        }

        private void OnClick_addNewMember(object sender, RoutedEventArgs e)
        {
            EmployeeListViewContainer.Visibility = Visibility.Collapsed;
            EmployeeForm.Visibility = Visibility.Visible;
        }

        private void OnClick_addMember(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee()
            {
                FirstName = InputFirstName.Text,
                LastName = InputLastName.Text,
                CompanyEntry = InputCompanyEntry.Text,
                Address = InputAddress.Text,
                Email = InputEmail.Text,
                Position = InputPosition.Text,
                Salary = InputSalary.Text,
                PensionStart = InputPensionStart.Text,
                PhoneNumber = InputFirstName.Text
            };

            if (!ValidateEmployeeData(ref employee))
            {
                MessageBox.Show("Validation failed!");
                return;
            }

            _context.Employees.Add(employee);
            EmployeeListView.Items.Add(employee);

            EmployeeListViewContainer.Visibility = Visibility.Visible;
            EmployeeForm.Visibility = Visibility.Collapsed;
            
            _context.SaveChangesAsync();
        }

        private bool ValidateEmployeeData(ref Employee employee)
        {
            return new Regex("/[^a-zA-ZäöüÄÖÜß]/g").IsMatch(employee.FirstName)
                && new Regex("/[^a-zA-ZäöüÄÖÜß]/g").IsMatch(employee.LastName)
                && new Regex("/^([a-zäöüß\\s\\d.,-]+?)\\s*([\\d\\s]+(?:\\s?[-|+/]\\s?\\d+)?\\s*[a-z]?)?\\s*(\\d{5})\\s*(.+)?$/i\n").IsMatch(employee.Address)
                && new Regex("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$").IsMatch(employee.PhoneNumber)
                && new Regex("\\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,}\\b").IsMatch(employee.Email)
                && new Regex("/[^a-zA-ZäöüÄÖÜß]/g").IsMatch(employee.Position)
                && new Regex("/^(([^0]{1})([0-9])*|(0{1}))(\\,\\d{2}){0,1}\u20ac?$/").IsMatch(employee.Salary);
        }
        
        private void ReloadEmployeeList()
        {
            EmployeeListView.Items.Clear();
            foreach (Employee employee in _context.Employees)
            {
                EmployeeListView.Items.Add(employee);
            }
        }

        private void OnClick_deleteMember(object sender, RoutedEventArgs e)
        {
            foreach (Employee employee in EmployeeListView.SelectedItems)
            {
                _context.Employees.Remove(employee);
            }

            _context.SaveChanges();
            
            ReloadEmployeeList();
        }

        private void OnClick_exportMembers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        private void OnClick_importMembers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        private void OnClick_searchMember(object sender, RoutedEventArgs e)
        {
            string searchInputText = searchInput.Text; 
            var foundEmployees = from b in _context.Employees
                where b.FirstName.Contains(searchInputText) || b.LastName.Contains(searchInputText) || b.Address.Contains(searchInputText) || b.Salary.Contains(searchInputText) || b.Email.Contains(searchInputText)
                || b.Position.Contains(searchInputText) || b.CompanyEntry.Contains(searchInputText) || b.PhoneNumber.Contains(searchInputText) || b.CompanyEntry.Contains(searchInputText) ||
                b.PensionStart.Contains(searchInputText)
                select b;

            EmployeeListView.Items.Clear();
            foreach (Employee employee in foundEmployees)
            {
                EmployeeListView.Items.Add(employee);
            }
            
        }

        private void OnClick_editMember(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }
    }


}
