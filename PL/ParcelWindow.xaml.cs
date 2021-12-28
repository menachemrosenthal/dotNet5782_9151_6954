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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        BO.BL BlParcel;
        Parcel parcel;
        event EventHandler ParcelChanged;
        /// <summary>
        /// costructor
        /// </summary>
        /// <param name="bl"></param>
        public ParcelWindow(BO.BL bl)
        {
            InitializeComponent();
            BlParcel = bl;
            UpdateParcelButton.Visibility = Visibility.Hidden;
            UpdateDeliveryButton.Visibility = Visibility.Hidden;
            UpdatePickedUpButton.Visibility = Visibility.Hidden;
            DeleteParcelButton.Visibility = Visibility.Hidden;
            AddParcelButton.Visibility = Visibility.Visible;
            PriorityTextbox.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightTextbox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            IdTextbox.IsReadOnly = false;
        }
        /// <summary>
        /// constructor by selected item
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="parcelId"></param>
        public ParcelWindow(BO.BL bl, int parcelId)
        {
            InitializeComponent();
            BlParcel = bl;
            AddParcelButton.Visibility = Visibility.Hidden;
            parcel = BlParcel.GetParcel(parcelId);
            ParcelChanged += UpdateWindow;
            BlParcel.EventRegistration(ParcelChanged, "Parcel");
            DataContext = parcel;
            PriorityTextbox.ItemsSource = Enum.GetValues(typeof(Priorities));
            PriorityTextbox.SelectedValue = parcel.Priority;
            PriorityTextbox.IsEnabled = false;
            WeightTextbox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightTextbox.SelectedValue = parcel.Weight;
            WeightTextbox.IsEnabled = false;
        }
        /// <summary>
        /// update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindow(object sender, EventArgs e)
        {
            parcel = BlParcel.GetParcel(parcel.Id);
            DataContext = parcel;

            if (parcel.Drone != null)
                DroneInParcelTextbox.Text = parcel.Drone.ToString();
        }
        /// <summary>
        /// add parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Parcel parcel = new()
                {
                    Senderid = int.Parse(SenderIdTextbox.Text),
                    TargetId = int.Parse(TargetIdTextbox.Text),
                    Weight = (WeightCategories)WeightTextbox.SelectedItem,
                    Priority = (Priorities)PriorityTextbox.SelectedItem
                };

                BlParcel.AddParcel(parcel);
                MessageBox.Show("The Parcel was added successfully");
                Close();
            }

            catch (Exception ex)
            {
                if (SenderIdTextbox.Background == Brushes.OrangeRed)
                    SenderIdTextbox.Text = "wrong id";
                if (TargetIdTextbox.Background == Brushes.OrangeRed)
                    TargetIdTextbox.Text = "wrong id";
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// change color by event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneInParcelTextbox_MouseLeave(object sender, MouseEventArgs e)
        {
            DroneInParcelTextbox.Background = Brushes.White;
        }
        /// <summary>
        /// change color by event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneInParcelTextbox_MouseEnter(object sender, MouseEventArgs e)
        {
            DroneInParcelTextbox.Background = Brushes.LightBlue;
        }
        /// <summary>
        /// open drone window by selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneInParcelTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            new DroneWindow(BlParcel, parcel.Drone.Id).Show();
        }
        /// <summary>
        /// change color acustomed to mistake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SenderIdTextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!SenderIdTextbox.IsReadOnly)
            {
                _ = int.TryParse(SenderIdTextbox.Text, out int id);
                if (id > 0 && BlParcel.GetCustomerList().Any(x => x.Id == id))
                {
                    SenderIdTextbox.Background = Brushes.LightGreen;
                    return;
                }

                SenderIdTextbox.Background = Brushes.OrangeRed;
            }
        }
        /// <summary>
        /// change color acustomed to mistake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetIdTextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (!TargetIdTextbox.IsReadOnly)
            {
                _ = int.TryParse(TargetIdTextbox.Text, out int id);
                if (id > 0 && BlParcel.GetCustomerList().Any(x => x.Id == id))
                {
                    TargetIdTextbox.Background = Brushes.LightGreen;
                    return;
                }

                TargetIdTextbox.Background = Brushes.OrangeRed;
            }
        }
    }
}
