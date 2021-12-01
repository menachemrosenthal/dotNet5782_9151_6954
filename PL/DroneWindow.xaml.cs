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
        public DroneWindow(IBL.BO.BL bl)
        {
            InitializeComponent();
            BlDrone = bl;
            Battery.Text = "0";
            Status.Text = "0";
            Parcel.Text = "0";
            Latitude.Text = "0";
            Longitude.Text = "0";
            Battery.IsReadOnly = true;
            Status.IsReadOnly = true;
            Parcel.IsReadOnly = true;
            Latitude.IsReadOnly = true;
            Longitude.IsReadOnly = true;
            StationList.ItemsSource = BlDrone.GetFreeChargingSlotsStationList();
            
        }

        public DroneWindow(IBL.BO.BL bl, IBL.BO.DroneToList drone)
        {
            InitializeComponent();
            BlDrone = bl;
            Name.Text = drone.Model;
            ID.Text = $"{drone.Id}";
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            Battery.Text = $"{drone.BatteryStatus}";
            Status.Text = $"{drone.Status}";
            Parcel.Text = $"{drone.DeliveredParcelId}";
            Latitude.Text = $"{drone.CurrentLocation.Latitude}";
            Longitude.Text = $"{drone.CurrentLocation.Longitude}";
            StationList.UnselectAll();
            AddDrone.Visibility = Visibility.Hidden;
            ID.IsReadOnly = true;
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

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}