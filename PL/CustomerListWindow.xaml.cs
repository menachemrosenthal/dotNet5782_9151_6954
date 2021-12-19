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
using System.Windows.Shapes;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        BO.BL BlCustomerList;
        public CustomerListWindow(BO.BL bl)
        {
            InitializeComponent();
            BlCustomerList = bl;
            CustomerListView.ItemsSource = bl.GetCustomerList();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CostomerWindow(BlCustomerList, (CustomerToList)CustomerListView.SelectedItem,this).Show();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new CostomerWindow(BlCustomerList,this).Show();
        }
    }
}
