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

            foreach (Employee employee in context.Employees)
            {
                EmployeeListView.Items.Add(employee);
            }
        }

        public void OnClick_addNewMember(object sender, RoutedEventArgs e)
        {
            EmployeeListView.Visibility = Visibility.Collapsed;
            EmployeeForm.Visibility = Visibility.Visible;
            
            
            
            EmployeeListView.Visibility = Visibility.Visible;
            EmployeeForm.Visibility = Visibility.Collapsed;
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
            MessageBox.Show("Button funktioniert");
        }

        public void OnClick_editMember(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }
    }


}
