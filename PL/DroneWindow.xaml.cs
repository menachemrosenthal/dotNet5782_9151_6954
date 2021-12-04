using IBL.BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        DroneListWindow droneListWindow1;
        public DroneWindow(IBL.BO.BL bl, DroneListWindow droneListWindow)
        {
            BlDrone = bl;
            droneListWindow1 = droneListWindow;
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

            NameUpdateButton.Visibility = Visibility.Hidden;
            pickedUpButton.Visibility = Visibility.Hidden;
            ChargeButton.Visibility = Visibility.Hidden;
            deliveryButton.Visibility = Visibility.Hidden;
            previsionButton.Visibility = Visibility.Hidden;
            ReleaseButton.Visibility = Visibility.Hidden;
            ChargingTimeLabel.Visibility = Visibility.Hidden;
            ChargingTime.Visibility = Visibility.Hidden;
        }

        public DroneWindow(IBL.BO.BL bl, IBL.BO.DroneToList drone, DroneListWindow droneListWindow)
        {
            InitializeComponent();
            BlDrone = bl;
            droneListWindow1 = droneListWindow;

            titelLabel.Content = "Choose frome these options";

            Name.Text = drone.Model;
            nameLabel.Content = "Name:";

            ID.Text = $"{drone.Id}";
            ID.IsReadOnly = true;
            IDLabel.Content = "ID:";

            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightSelector.SelectedItem = drone.MaxWeight.ToString();
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

            if (drone.Status == DroneStatuses.maintenance)
            {
                ChargeButton.Visibility = Visibility.Hidden;
                deliveryButton.Visibility = Visibility.Hidden;
                pickedUpButton.Visibility = Visibility.Hidden;
                previsionButton.Visibility = Visibility.Hidden;
            }

            if (drone.Status != DroneStatuses.maintenance)
            {
                ReleaseButton.Visibility = Visibility.Hidden;
                ChargingTimeLabel.Visibility = Visibility.Hidden;
                ChargingTime.Visibility = Visibility.Hidden;

                if (BlDrone.GetDroneSituation(drone.Id) == "Free")
                {
                    pickedUpButton.Visibility = Visibility.Hidden;
                    previsionButton.Visibility = Visibility.Hidden;
                }

                if (BlDrone.GetDroneSituation(drone.Id) == "Associated")
                {
                    ChargeButton.Visibility = Visibility.Hidden;
                    deliveryButton.Visibility = Visibility.Hidden;
                    previsionButton.Visibility = Visibility.Hidden;
                }

                if (BlDrone.GetDroneSituation(drone.Id) == "Executing")
                {
                    ChargeButton.Visibility = Visibility.Hidden;
                    deliveryButton.Visibility = Visibility.Hidden;
                    pickedUpButton.Visibility = Visibility.Hidden;
                }
            }

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
                droneListWindow1.DroneListView.ItemsSource = BlDrone.GetDroneList();
                Close();

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
            BlDrone.DroneNameUpdate(int.Parse(ID.Text), Name.Text);
        }

        private void Charge_Button(object sender, RoutedEventArgs e)
        {
            BlDrone.ChargeDrone(int.Parse(ID.Text));
        }

        private void ParcelAssociate_Button(object sender, RoutedEventArgs e)
        {
            BlDrone.ParcelToDrone(int.Parse(ID.Text));
        }

        private void Pickedup_Button(object sender, RoutedEventArgs e)
        {
            BlDrone.ParcelPickedupUptade(int.Parse(ID.Text));
        }

        private void Provision_Button(object sender, RoutedEventArgs e)
        {
            BlDrone.ParcelProvisionUpdate(int.Parse(ID.Text));
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChargingRelease_Button(object sender, RoutedEventArgs e)
        {
            BlDrone.ReleaseDrone(int.Parse(ID.Text), TimeSpan.Parse(ChargingTime.Text));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            droneListWindow1.DroneListView.ItemsSource = BlDrone.GetDroneList();
            Close();
        }
    }
}