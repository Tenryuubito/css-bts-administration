using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace css_bts_administration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeContext _context = new();

        private Employee? _employeeToEdit;

        private string _placeholderText = "Suchen ...";

        public MainWindow()
        {
            InitializeComponent();

            ReloadEmployeeList();

            searchInput.Text = _placeholderText;
            searchInput.Foreground = Brushes.Gray;
        }

        private void OnClick_addNewMember(object sender, RoutedEventArgs e)
        {
            ChangeFormState(FormState.AddNewEmployee);
        }

        private void ChangeFormState(FormState formState)
        {
            ClearForm();
            switch (formState)
            {
                case (FormState.ListEmployees):
                    EmployeeListViewContainer.Visibility = Visibility.Visible;
                    EmployeeForm.Visibility = Visibility.Collapsed;
                    break;
                case (FormState.AddNewEmployee):
                    EmployeeListViewContainer.Visibility = Visibility.Collapsed;
                    EmployeeForm.Visibility = Visibility.Visible;
                    AddMemberButton.Visibility = Visibility.Visible;
                    SaveChangesButton.Visibility = Visibility.Hidden;
                    break;
                case (FormState.EditExistingEmployee):
                    EmployeeListViewContainer.Visibility = Visibility.Collapsed;
                    EmployeeForm.Visibility = Visibility.Visible;
                    AddMemberButton.Visibility = Visibility.Hidden;
                    SaveChangesButton.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ClearForm()
        {
            InputFirstName.Text = "";
            InputLastName.Text = "";
            InputAddress.Text = "";
            InputPhoneNumber.Text = "";
            InputEmail.Text = "";
            InputPosition.Text = "";
            InputCompanyEntry.Text ="";
            InputSalary.Text = "";
            InputPensionStart.Text = "";
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
                PhoneNumber = InputPhoneNumber.Text
            };

            if (!ValidateEmployeeData(ref employee))
            {
                MessageBox.Show("Validierung fehlgeschlagen!");
                return;
            }

            _context.Employees.Add(employee);
            EmployeeListView.Items.Add(employee);
            
            _context.SaveChangesAsync();
            
            ChangeFormState(FormState.ListEmployees);
        }

        private bool ValidateEmployeeData(ref Employee employee)
        {
            return new Regex("^[a-zA-ZäÄöÖüÜß ]{2,}$").IsMatch(employee.FirstName)
                   && new Regex("^[a-zA-Z.äÄöÖüÜ ]{2,}$").IsMatch(employee.LastName)
                   && new Regex("^[a-zA-Z.äÄöÖüÜß ]+ \\d+$").IsMatch(employee.Address)
                   && new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$").IsMatch(employee.Email)
                   && new Regex("^[a-zA-ZäöüÄÖÜß ]+$").IsMatch(employee.Position)
                   && new Regex("^[0-9]+[.][0-9]{2}[€]$").IsMatch(employee.Salary);
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
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Title = "Speichern der Exportdatei";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != true)
            {
                MessageBox.Show("Beim speichern der Exportdatei ist wohl etwas schiefgelaufen ;)");
                return;
            }
            
            StringBuilder stringBuilder = new();
            StringWriter stringWriter = new(stringBuilder);

            using (JsonWriter writer = new JsonTextWriter(stringWriter))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartArray();
                
                foreach (Employee employee in _context.Employees)
                {
                    writer.WriteStartObject();
                    foreach (PropertyInfo property in typeof(Employee).GetProperties())
                    {
                        writer.WritePropertyName(property.Name);
                        writer.WriteValue(property.GetValue(employee));
                    }
                    writer.WriteEndObject();
                }
                
                writer.WriteEndArray();
                stringWriter.Close();
                
                File.WriteAllText(saveFileDialog.FileName, stringBuilder.ToString());
            }
        }

        private void OnClick_importMembers(object sender, RoutedEventArgs e)
        {
            for (int dummyNr = 0; dummyNr < 1000; ++dummyNr)
            {
                Employee employee = new Employee()
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    CompanyEntry = DateTime.Now.ToString("dd.MM.yyyy"),
                    Address = Faker.Address.StreetAddress(),
                    Email = Faker.Internet.Email(),
                    Position = "Dummy",
                    Salary = Faker.RandomNumber.Next(2000, 3000) + ".00€",
                    PensionStart = Faker.Identification.DateOfBirth().ToString("dd.MM.yyyy"),
                    PhoneNumber = Faker.Phone.Number()
                };
                _context.Employees.Add(employee);
            }

            _context.SaveChanges();
            ReloadEmployeeList();
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
            _employeeToEdit = EmployeeListView.SelectedItem as Employee;

            if (_employeeToEdit == null)
            {
                MessageBox.Show("Es muss ein Mitarbeiter zum Bearbeiten ausgewählt sein.");
                return;
            }

            ChangeFormState(FormState.EditExistingEmployee);

            SetEmployeeDataToForm(ref _employeeToEdit);
        }

        private void SetEmployeeDataToForm(ref Employee employee)
        {
            _employeeToEdit = employee;
            InputFirstName.Text = employee.FirstName;
            InputLastName.Text = employee.LastName;
            InputAddress.Text = employee.Address;
            InputPhoneNumber.Text = employee.PhoneNumber;
            InputEmail.Text = employee.Email;
            InputPosition.Text = employee.Position;
            InputCompanyEntry.Text = employee.CompanyEntry;
            InputSalary.Text = employee.Salary;
            InputPensionStart.Text = employee.PensionStart;
        }


        private void OnCLick_saveChanges(object sender, RoutedEventArgs e)
        {
            if (_employeeToEdit == null) throw new NullReferenceException();

            
            _employeeToEdit.FirstName = InputFirstName.Text;
            _employeeToEdit.LastName = InputLastName.Text;
            _employeeToEdit.Address = InputAddress.Text;
            _employeeToEdit.PhoneNumber = InputPhoneNumber.Text;
            _employeeToEdit.Email = InputEmail.Text;
            _employeeToEdit.Position = InputPosition.Text;
            _employeeToEdit.CompanyEntry = InputCompanyEntry.Text;
            _employeeToEdit.Salary = InputSalary.Text;
            _employeeToEdit.PensionStart = InputPensionStart.Text;
            
            if (!ValidateEmployeeData(ref _employeeToEdit))
            {
                MessageBox.Show("Ungültige Eingabe im Formular.");
                return;
            }
            _context.Employees.AddOrUpdate(_employeeToEdit);
            _context.SaveChanges();
            
            ReloadEmployeeList();
            ChangeFormState(FormState.ListEmployees);
        }

        private void OnClick_backToListView(object sender, RoutedEventArgs e)
        {
            ClearForm();
            ChangeFormState(FormState.ListEmployees);
        }

        private void OnLostFocus_fillPlaceholder(object sender, RoutedEventArgs e)
        {
            if (searchInput.Text == String.Empty)
            {
                searchInput.Text = _placeholderText;
                searchInput.Foreground = Brushes.Gray;
            }
        }

        private void GotFocus_emptyBox(object sender, RoutedEventArgs e)
        {
            if (searchInput.Text == _placeholderText)
            {
                searchInput.Text = String.Empty;
            }

            searchInput.Foreground = Brushes.Black;
        }
    }
}
