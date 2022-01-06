using BO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        /// <summary>
        /// BL access
        /// </summary>
        BO.BL BlDrone;

        /// <summary>
        /// station id of the station to put the new drone
        /// </summary>
        int stationId;

        /// <summary>
        /// drone for data context
        /// </summary>
        Drone drone;

        /// <summary>
        /// when changing happens in drone
        /// </summary>
        event EventHandler DroneChanged;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bl"></param>
        public DroneWindow(BO.BL bl)
        {
            InitializeComponent();
            BlDrone = bl;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationList.ItemsSource = BlDrone.GetFreeChargingSlotsStationList();
            NameUpdateButton.Visibility = Visibility.Hidden;
            AddDroneButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constractor by selected item
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneId">id of selected drone</param>
        public DroneWindow(BO.BL bl, int droneId)
        {
            InitializeComponent();
            BlDrone = bl;
            drone = BlDrone.GetDrone(droneId);
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightSelector.SelectedValue = drone.MaxWeight;
            WeightSelector.IsEnabled = false;
            nameLabel.Content = "Name:";
            IDLabel.Content = "ID:";
            ID.IsReadOnly = true;
            DroneChanged += UpdateWindow;
            BlDrone.EventRegistration(DroneChanged, "Drone");
            UpdateWindow(this, EventArgs.Empty);
        }

        /// <summary>
        /// update window(refresh)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void UpdateWindow(object s, EventArgs e)
        {
            drone = BlDrone.GetDrone(drone.Id);
            DataContext = drone;

            ReleaseButton.Visibility = Visibility.Hidden;
            ChargeButton.Visibility = Visibility.Hidden;
            associateButton.Visibility = Visibility.Hidden;
            pickedUpButton.Visibility = Visibility.Hidden;
            provisionButton.Visibility = Visibility.Hidden;
            parcel.Visibility = Visibility.Visible;

            if (drone.Status == DroneStatuses.maintenance)
            {
                ReleaseButton.Visibility = Visibility.Visible;
                parcel.Visibility = Visibility.Hidden;
            }

            if (BlDrone.GetDroneSituation(drone.Id) == "Free")
            {
                ChargeButton.Visibility = Visibility.Visible;
                associateButton.Visibility = Visibility.Visible;
                parcel.Visibility = Visibility.Hidden;
            }

            if (BlDrone.GetDroneSituation(drone.Id) == "Associated")
                pickedUpButton.Visibility = Visibility.Visible;

            if (BlDrone.GetDroneSituation(drone.Id) == "Executing")
                provisionButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// add drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    _ = MessageBox.Show("The Drone was added successfully");
                    Close();
                    return;
                }

                catch (Exception ex)
                {
                    _ = MessageBox.Show(ex.Message);
                }
            }
            if (BlDrone.GetDroneList().Any(x => x.Id == id))
            {
                ID.Text = "This ID is alredy exist";
                return;
            }

            ID.Text = "Wrong ID";
        }

        /// <summary>
        /// assign a drone to a station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var station = (StationToList)StationList.SelectedItem;
            stationId = station.Id;
        }

        /// <summary>
        /// update drone name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneNameUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.DroneNameUpdate(int.Parse(ID.Text), Name.Text);
                MessageBox.Show("The Name was update successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// charge drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Charge_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ChargeDrone(int.Parse(ID.Text));
                MessageBox.Show("The Drone was sent for charging successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// associate a parcel to a drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelAssociate_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ParcelToDrone(int.Parse(ID.Text));
                MessageBox.Show("The drone associated with a parcel successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// update that parcel was picked up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pickedup_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ParcelPickedupUptade(int.Parse(ID.Text));
                MessageBox.Show("The parcel was pickedup successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// update that parcel was provided
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Provision_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ParcelProvisionUpdate(int.Parse(ID.Text));
                MessageBox.Show("The parcel provided successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// release drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargingRelease_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                BlDrone.ReleaseDrone(int.Parse(ID.Text));
                MessageBox.Show("The drone was release from charging successfully");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// changes backround acustomed with mistake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ID_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (ID.IsReadOnly == false)
            {
                _ = int.TryParse(ID.Text, out int id);
                if (id > 0 && !BlDrone.GetDroneList().Any(x => x.Id == id))
                {
                    ID.Background = Brushes.LightGreen;
                    return;
                }

                ID.Background = Brushes.OrangeRed;
            }
        }

        /// <summary>
        /// open parcel window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parcel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (drone.Parcel != null)
                new ParcelWindow(BlDrone, drone.Parcel.Id).Show();
        }

        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}