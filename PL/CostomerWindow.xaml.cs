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
        Customer Customer;
        public event EventHandler CustomerChanged;
        public CostomerWindow(BO.BL bl, CustomerListWindow customerListWindow)
        {
            InitializeComponent();

            blCustomerList = bl;
            UpdateButton.Visibility = Visibility.Hidden;
        }

        public CostomerWindow(BO.BL bl, CustomerToList customerToList)
        {
            InitializeComponent();
            AddButton.Visibility = Visibility.Hidden;
            blCustomerList = bl;
            Customer = blCustomerList.GetCustomer(customerToList.Id);
            CustomerChanged += UpdateWindow;
            CustomerChanged(this, EventArgs.Empty);
            Id.IsReadOnly = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _ = int.TryParse(Id.Text, out int id);
            if (id > 0 && !blCustomerList.GetCustomerList().Any(x => x.Id == id))
            {
                try
                {
                    Location l = new()
                    {
                        Longitude = double.Parse(Longitude.Text),
                        Latitude = double.Parse(LatitudeTextBox.Text)
                    };
                    Customer customer1 = new()
                    {
                        Name = Name.Text,
                        Id = int.Parse(Id.Text),
                        Phone = Phone.Text,
                        Location = l
                    };

                    blCustomerList.AddCustumer(customer1);
                    _ = MessageBox.Show("Customer was added successfully");

                    CustomerChanged(this, EventArgs.Empty);
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
                blCustomerList.CustomerUpdate(Customer);
                CustomerChanged(this, EventArgs.Empty);
                MessageBox.Show("The Customer was updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateWindow(object sender, EventArgs e)
        {
            Customer = blCustomerList.GetCustomer(Customer.Id);
            DataContext = Customer;
            RecievedParcels.ItemsSource = Customer.Get;
            SendedParcels.ItemsSource = Customer.Sended;
        }
    }
}
