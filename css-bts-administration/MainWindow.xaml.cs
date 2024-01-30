using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
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
                FirstName = InputFirstName.Text
            };

            _context.Employees.Add(employee);
            EmployeeListView.Items.Add(employee);

            EmployeeListViewContainer.Visibility = Visibility.Visible;
            EmployeeForm.Visibility = Visibility.Collapsed;
            
            _context.SaveChangesAsync();
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

        public void OnClick_importMembers(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
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
