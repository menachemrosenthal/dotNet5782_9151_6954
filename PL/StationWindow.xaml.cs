﻿using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        /// <summary>
        /// BL access
        /// </summary>
        BO.BL GetBL;

        /// <summary>
        /// station for data context
        /// </summary>
        BO.Station Station;

        /// <summary>
        /// when staion is changed
        /// </summary>
        public event EventHandler StationChanged;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bl"></param>
        public StationWindow(BO.BL bl)
        {
            InitializeComponent();
            GetBL = bl;
            updateButton.Visibility = Visibility.Hidden;
            addBUtton.Visibility = Visibility.Visible;
            drones.Visibility = Visibility.Hidden;
            slotsLabel.Content = "Charging slots:";
            slots.VerticalAlignment = VerticalAlignment.Center;
        }

        /// <summary>
        /// construtor by selected item
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="stationId">selected item</param>
        public StationWindow(BO.BL bl, int stationId)
        {
            InitializeComponent();
            GetBL = bl;
            Station = GetBL.GetStation(stationId);
            StationChanged += UpdateWindow;            
            UpdateWindow(this, EventArgs.Empty);
            id.IsReadOnly = false;
        }

        /// <summary>
        /// update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindow(object sender, EventArgs e)
        {
            Station = GetBL.GetStation(Station.Id);
            DataContext = Station;

            if (Station.DronesCharging.Count > 0)
            {
                drones.ItemsSource = Station.DronesCharging;
                return;
            }

            drones.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// open drone window by selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneInCharging drone = (BO.DroneInCharging)drones.SelectedItem;
            DroneWindow droneWindow = new DroneWindow(GetBL, drone.Id);
            droneWindow.DroneChanged += StationChanged;
            droneWindow.Show();
        }

         /// <summary>
         /// change color of text box by the correct of the input data
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void Id_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (id.IsReadOnly == false)
            {
                _ = int.TryParse(id.Text, out int Id);
                if (Id > 0 && !GetBL.GetBaseStationList().Any(x => x.Id == Id))
                {
                    id.Background = Brushes.LightGreen;
                    return;
                }

                id.Background = Brushes.OrangeRed;
            }
        }

        /// <summary>
        /// update station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetBL.StationUpdate(int.Parse(id.Text), name.Text, slots.Text);
                StationChanged(this, EventArgs.Empty);
                _ = MessageBox.Show("The Station was updated successfully");
            }
            catch (Exception ex)
            {
                if (slots.Background == Brushes.OrangeRed)
                    slots.Text = "wrong num of charging slots";

                _ = MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// add station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBUtton_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                BO.Station station = new()
                {
                    Name = name.Text,
                    Id = int.Parse(id.Text),
                    ChargeSlots = int.Parse(slots.Text),
                    LocationOfStation = new()
                    {
                        Latitude = double.Parse(latitud.Text),
                        Longitude = double.Parse(longitude.Text)
                    }
                };

                GetBL.AddStation(station);
                StationChanged(this, EventArgs.Empty);
                _ = MessageBox.Show("The Station was added successfully");
                Close();
            }
            catch (Exception ex)
            {
                if (id.Background == Brushes.OrangeRed)
                    id.Text = "wrong ID";

                if (latitud.Background == Brushes.OrangeRed)
                    latitud.Text = "wrong Latitude";

                if (longitude.Background == Brushes.OrangeRed)
                    longitude.Text = "wrong Longitude";

                if (slots.Background == Brushes.OrangeRed)
                    slots.Text = "wrong num of charging slots";

                _ = MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// change color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Latitude_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (latitud.IsReadOnly == false)
            {
                _ = double.TryParse(latitud.Text, out double lat);
                if (lat is >= 31.5898 and <= 32.802)
                {
                    latitud.Background = Brushes.LightGreen;
                    return;
                }

                latitud.Background = Brushes.OrangeRed;
            }
        }

        /// <summary>
        /// change color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Longitude_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (longitude.IsReadOnly == false)
            {                
                _ = double.TryParse(longitude.Text, out double lon);
                if (lon is >= 34.5 and <= 35.9)
                {
                    longitude.Background = Brushes.LightGreen;
                    return;
                }
                longitude.Background = Brushes.OrangeRed;
            }
        }

        /// <summary>
        /// change color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slots_SelectionChanged(object sender, RoutedEventArgs e)
        {            
            if (_ = int.TryParse(slots.Text, out int chargSlts) && chargSlts > 0)
            {
                slots.Background = Brushes.LightGreen;
                return;
            }

            slots.Background = Brushes.OrangeRed;
        }
    }
}
