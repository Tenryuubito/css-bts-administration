﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

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
            ChangeFormState(true);
        }

        private void ChangeFormState(bool show)
        {
            EmployeeListViewContainer.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
            EmployeeForm.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
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
            
            _context.SaveChangesAsync();
            
            ChangeFormState(false);
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
                employee.CompanyEntry = sr.ReadLine() ;
                employee.Salary = sr.ReadLine();
                employee.PensionStart = sr.ReadLine();
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            
            sr.Close();
            ReloadEmployeeList(); 
            Console.WriteLine("HERE");
            
           
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
            Employee employee = EmployeeListView.SelectedItem as Employee;

            if (employee == null)
            {
                MessageBox.Show("You need to select an employee to edit.");
                return;
            }

            ChangeFormState(true);
        }
    }


}
