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
    /// Interaction logic for CostomerWindow.xaml
    /// </summary>
    public partial class CostomerWindow : Window
    {
        private BO.BL blCustomerList;
        Customer customer;
        CustomerListWindow FatherWindow;
        public CostomerWindow(BO.BL bl, CustomerListWindow customerListWindow)
        {
            InitializeComponent();
            
            blCustomerList = bl;
            FatherWindow = customerListWindow;
            
            RecievedParcels.Visibility = Visibility.Hidden;
            SendedParcels.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            SendedLabel.Visibility = Visibility.Hidden;
            RecievedLabel.Visibility = Visibility.Hidden;
        }

        public CostomerWindow(BO.BL bl, CustomerToList customerToList, CustomerListWindow customerListWindow)
        {
            InitializeComponent();
            FatherWindow = customerListWindow;
            AddButton.Visibility = Visibility.Hidden;
            blCustomerList = bl;
            customer = blCustomerList.GetCustomer(customerToList.Id);
            this.DataContext = customer;
            RecievedParcels.ItemsSource = customer.Get;
            SendedParcels.ItemsSource = customer.Sended;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _ = int.TryParse(Id.Text, out int id);
                if (id > 0 && !blCustomerList.GetCustomerList().Any(x => x.Id == id))
                {
                    try
                    {
                    Location l = new() {
                        Longitude = double.Parse(Longitude.Text),
                        Latitude = double.Parse(LatitudeTextBox.Text) };
                    Customer customer1 = new()
                    {
                        Name = Name.Text,
                        Id = int.Parse(Id.Text),
                        Phone = Phone.Text,
                        Location=l
                    };

                        blCustomerList.AddCustumer(customer1);
                        _ = MessageBox.Show("Customer was added successfully");

                        FatherWindow.CustomerListView.ItemsSource = blCustomerList.GetCustomerList();
                        Close();
                        return;
                    }

                    catch (Exception ex)
                    {
                        _ = MessageBox.Show(ex.Message);
                    }
                }
                if (blCustomerList.GetCustomerList().Any(x => x.Id == id))
                {
                    Id.Text = "This ID is alredy exist";
                    return;
                }

                Id.Text = "Wrong ID";
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blCustomerList.CustomerUpdate(customer);
                MessageBox.Show("The Customer was updated successfully");
                FatherWindow.CustomerListView.ItemsSource = blCustomerList.GetCustomerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
