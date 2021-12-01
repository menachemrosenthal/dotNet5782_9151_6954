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
            InitializeComponent();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
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
            WeightSelector = Enum.GetValues(typeof(drone.MaxWeight));
            Battery.Text = $"{drone.BatteryStatus}";
            Status.Text = $"{drone.Status}";
            Parcel.Text = $"{drone.DeliveredParcelId}";
            Latitude.Text = $"{drone.CurrentLocation.Latitude}";
            Longitude.Text = $"{drone.CurrentLocation.Longitude}";
            StationList.UnselectAll();
            ID.IsReadOnly = true;            
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            IBL.BO.DroneToList drone = new()
            {
                Model = Name.Text,
                Id = int.Parse(ID.Text),
                MaxWeight = Weight
            };
            
            BlDrone.AddDrone(drone, stationId);           
        }

        void StationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var station = (IBL.BO.StationToList)StationList.SelectedItem;
            stationId = station.Id;
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight = (WeightCategories)WeightSelector.SelectedItem;
        }
    }
}