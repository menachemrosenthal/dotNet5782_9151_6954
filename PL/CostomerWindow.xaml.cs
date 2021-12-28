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
        event EventHandler CustomerChanged;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bl"></param>
        public CostomerWindow(BO.BL bl)
        {
            InitializeComponent();
            blCustomerList = bl;            
            UpdateButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// update window contsractor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customerToList">seledcted item</param>        
        public CostomerWindow(BO.BL bl, int customerId)
        {
            InitializeComponent();
            AddButton.Visibility = Visibility.Hidden;
            blCustomerList = bl;
            Customer = blCustomerList.GetCustomer(customerId);
            nameLabel.Content = "Name";
            CustomerChanged += UpdateWindow;
            blCustomerList.EventRegistration(CustomerChanged, "Customer");
            UpdateWindow(this, EventArgs.Empty);            
            Id.IsReadOnly = true;
        }

        /// <summary>
        /// add a customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// update a customer name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blCustomerList.CustomerUpdate(Customer);                
                MessageBox.Show("The Customer was updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// updates the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindow(object sender, EventArgs e)
        {
            Customer = blCustomerList.GetCustomer(Customer.Id);
            DataContext = Customer;
            RecievedParcels.ItemsSource = Customer.Get;
            SendedParcels.ItemsSource = Customer.Sended;
        }

        /// <summary>
        /// opens sent parcel window for selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendedParcels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelInCustomer parcel = (ParcelInCustomer)SendedParcels.SelectedItem;
            new ParcelWindow(blCustomerList, parcel.Id).Show();
        }

        /// <summary>
        /// opens recieved parcel window for selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecievedParcels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ParcelInCustomer parcel = (ParcelInCustomer)RecievedParcels.SelectedItem;
            new ParcelWindow(blCustomerList, parcel.Id).Show();
        }
    }
}
