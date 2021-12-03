using IBL.BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.BO.BL BlDrone;
        int stationId;
        WeightCategories Weight;
        public DroneWindow(IBL.BO.BL bl)
        {
            BlDrone = bl;
            InitializeComponent();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationList.ItemsSource = BlDrone.GetFreeChargingSlotsStationList();
            
            batteryLabel.Visibility = Visibility.Hidden;
            Battery.Visibility = Visibility.Hidden;
            statusLabel.Visibility = Visibility.Hidden;
            Status.Visibility = Visibility.Hidden;
            parcelLabel.Visibility = Visibility.Hidden;
            Parcel.Visibility = Visibility.Hidden;
            latitudeLabel.Visibility = Visibility.Hidden;
            Latitude.Visibility = Visibility.Hidden;
            longitudeLabel.Visibility = Visibility.Hidden;
            Longitude.Visibility = Visibility.Hidden;
            chargeButton.Visibility = Visibility.Hidden;
            deliveryButton.Visibility = Visibility.Hidden;

            
            
        }

        public DroneWindow(IBL.BO.BL bl, IBL.BO.DroneToList drone)
        {
            InitializeComponent();
            BlDrone = bl;

            Name.Text = drone.Model;
            nameLabel.Content = "Name:";

            ID.Text = $"{drone.Id}";
            ID.IsReadOnly = true;
            IDLabel.Content = "ID:";

            WeightSelector.SelectedItem =  drone.MaxWeight.ToString();
            WeightSelector.IsReadOnly = true;
            weightLabel.Content = "Max weight:";

            Battery.Text = $"{drone.BatteryStatus}";           
            Status.Text = $"{drone.Status}";
            Parcel.Text = $"{drone.DeliveredParcelId}";
            Latitude.Text = $"{drone.CurrentLocation.Latitude}";
            Longitude.Text = $"{drone.CurrentLocation.Longitude}";

            stationIdLabel.Visibility = Visibility.Hidden;
            StationList.Visibility = Visibility.Hidden;
            AddDrone.Visibility = Visibility.Hidden;
           
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IBL.BO.DroneToList drone = new()
                {
                    Model = Name.Text,
                    Id = int.Parse(ID.Text),
                    MaxWeight = Enum.Parse<IBL.BO.WeightCategories>(WeightSelector.SelectedItem.ToString())
                };

                BlDrone.AddDrone(drone, stationId);
                MessageBox.Show("Drone was added successfully");
                //צריך לעדכן בחלון השני
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void StationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var station = (IBL.BO.StationToList)StationList.SelectedItem;
            stationId = station.Id;
        }

        private void DroneNameUpdate_Click(object sender, RoutedEventArgs e)
        {
            BlDrone.DroneNameUpdate(int.Parse(ID.Text),Name.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BlDrone.ChargeDrone(int.Parse(ID.Text));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BlDrone.ParcelToDrone(int.Parse(ID.Text));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BlDrone.ParcelPickedupUptade(int.Parse(ID.Text));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            BlDrone.ParcelProvisionUpdate(int.Parse(ID.Text));
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}