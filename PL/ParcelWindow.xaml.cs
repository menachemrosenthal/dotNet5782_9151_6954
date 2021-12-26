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
        public ParcelWindow(BO.BL bl)
        {
            InitializeComponent();
            BlParcel = bl;
            UpdateParcelButton.Visibility = Visibility.Hidden;
            UpdateDeliveryButton.Visibility = Visibility.Hidden;
            UpdatePickedUpButton.Visibility = Visibility.Hidden;
            DeleteParcelButton.Visibility = Visibility.Hidden;
            AddParcelButton.Visibility = Visibility.Visible;
        }
        public ParcelWindow(BO.BL bl, int parcelId)
        {
            InitializeComponent();
            BlParcel = bl;
            AddParcelButton.Visibility = Visibility.Hidden;
            parcel = BlParcel.GetParcel(parcelId);
            ParcelChanged += UpdateWindow;
            BlParcel.EventRegistration(ParcelChanged, "Parcel");
            DataContext = parcel;
            if (parcel.Drone != null)
                DroneInParcelTextbox.Text = parcel.Drone.ToString();
        }

        private void UpdateWindow(object sender, EventArgs e)
        {
            parcel = BlParcel.GetParcel(parcel.Id);
            DataContext = parcel;
            if (parcel.Drone != null)
                DroneInParcelTextbox.Text = parcel.Drone.ToString();
        }

        private void AddParcelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DroneInParcelTextbox_MouseLeave(object sender, MouseEventArgs e)
        {
            DroneInParcelTextbox.Background = Brushes.White;
        }

        private void DroneInParcelTextbox_MouseEnter(object sender, MouseEventArgs e)
        {
            DroneInParcelTextbox.Background = Brushes.LightBlue;
        }

        private void DroneInParcelTextbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new DroneWindow(BlParcel, parcel.Drone.Id).Show();
        }
    }
}
