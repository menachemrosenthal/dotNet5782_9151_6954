using IBL.BO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.BO.BL BlDrone;
        int stationId;
        DroneListWindow FatherWindow;
        public DroneWindow(IBL.BO.BL bl, DroneListWindow droneListWindow)
        {
            InitializeComponent();
            BlDrone = bl;
            FatherWindow = droneListWindow;
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
            AddDroneButton.Visibility = Visibility.Visible;

            stationIdLabel.Visibility = Visibility.Visible;
            StationList.Visibility = Visibility.Visible;
        }

        public DroneWindow(IBL.BO.BL bl, DroneToList drone, DroneListWindow droneListWindow)
        {
            InitializeComponent();
            BlDrone = bl;
            FatherWindow = droneListWindow;

            titelLabel.Content = "Choose frome these options";

            Name.Text = drone.Model;
            nameLabel.Content = "Name:";

            ID.Text = $"{drone.Id}";
            ID.IsReadOnly = true;
            IDLabel.Content = "           ID:";

            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightSelector.SelectedValue = drone.MaxWeight;
            WeightSelector.IsEnabled = false;
            weightLabel.Content = "Max weight:";

            Battery.Text = $"{drone.BatteryStatus}%";
            Status.Text = $"{drone.Status}";
            Parcel.Text = $"{drone.DeliveredParcelId}";
            Latitude.Text = $"{drone.CurrentLocation.Latitude}";
            Longitude.Text = $"{drone.CurrentLocation.Longitude}";

            if (drone.Status == DroneStatuses.maintenance)
                ReleaseButton.Visibility = Visibility.Visible;

            if (BlDrone.GetDroneSituation(drone.Id) == "Free")
            {
                ChargeButton.Visibility = Visibility.Visible;
                associateButton.Visibility = Visibility.Visible;
            }

            if (BlDrone.GetDroneSituation(drone.Id) == "Associated")
                pickedUpButton.Visibility = Visibility.Visible;

            if (BlDrone.GetDroneSituation(drone.Id) == "Executing")
                provisionButton.Visibility = Visibility.Visible;
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            _ = int.TryParse(ID.Text, out int id);
            if (id > 0 && !BlDrone.GetDroneList().Any(x => x.Id == id))
            {
                try
                {
                    DroneToList drone = new()
                    {
                        Model = Name.Text,
                        Id = int.Parse(ID.Text),
                        MaxWeight = Enum.Parse<WeightCategories>(WeightSelector.SelectedItem.ToString())
                    };
                   
                    BlDrone.AddDrone(drone, stationId);
                    _ = MessageBox.Show("Drone was added successfully");
                    FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                    Close();

                }

                catch (Exception ex)
                {
                    _ = MessageBox.Show(ex.Message);
                }
            }
            else
            {
                ID.Text = "Wrong ID";
            }
        }

        void StationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var station = (StationToList)StationList.SelectedItem;
            stationId = station.Id;
        }

        private void DroneNameUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.DroneNameUpdate(int.Parse(ID.Text), Name.Text);
                MessageBox.Show("The Name was update successfully");
                FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                new DroneWindow(BlDrone, (DroneToList)FatherWindow.DroneListView.SelectedItem, FatherWindow).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Charge_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ChargeDrone(int.Parse(ID.Text));
                MessageBox.Show("The Drone was sent for charging successfully");
                FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                new DroneWindow(BlDrone, (DroneToList)FatherWindow.DroneListView.SelectedItem, FatherWindow).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ParcelAssociate_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ParcelToDrone(int.Parse(ID.Text));
                MessageBox.Show("The drone associated with a parcel successfully");
                FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                new DroneWindow(BlDrone, (DroneToList)FatherWindow.DroneListView.SelectedItem, FatherWindow).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Pickedup_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ParcelPickedupUptade(int.Parse(ID.Text));
                MessageBox.Show("The parcel was pickedup successfully");
                FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                new DroneWindow(BlDrone, (DroneToList)FatherWindow.DroneListView.SelectedItem, FatherWindow).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Provision_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ParcelProvisionUpdate(int.Parse(ID.Text));
                MessageBox.Show("The parcel provided successfully");
                FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                new DroneWindow(BlDrone, (DroneToList)FatherWindow.DroneListView.SelectedItem, FatherWindow).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ChargingRelease_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ReleaseDrone(int.Parse(ID.Text));
                MessageBox.Show("The drone was release from charging successfully");
                FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
                new DroneWindow(BlDrone, (DroneToList)FatherWindow.DroneListView.SelectedItem, FatherWindow).Show();
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            FatherWindow.DroneListView.ItemsSource = BlDrone.GetDroneList();
            Close();
        }

        private void ID_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _ = int.TryParse(ID.Text, out int id);
            if (id > 0 && !BlDrone.GetDroneList().Any(x => x.Id == id))
            {

                ID.BorderBrush = Brushes.Green;
            }
            else
                ID.BorderBrush = Brushes.Red;
        }
    }
}