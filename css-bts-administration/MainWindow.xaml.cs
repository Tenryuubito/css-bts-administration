using System;
using System.Collections.Generic;
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
        

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnClick_addNewMember(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        public void OnClick_editMember(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
