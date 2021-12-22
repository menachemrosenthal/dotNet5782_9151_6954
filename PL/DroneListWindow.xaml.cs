using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        BO.BL BlDroneList;
        public DroneListWindow(BO.BL bl)
        {
            InitializeComponent();
            BlDroneList = (BO.BL)BL.BlFactory.GetBl();
            DroneListView.ItemsSource = bl.GetDroneList();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));            
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                        
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);

            allDronesButton.Visibility = Visibility.Visible;
        }

        public event EventHandler DroneChanged;

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.Status == (DroneStatuses)StatusSelector.SelectedItem);

            allDronesButton.Visibility = Visibility.Visible;
        }       
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDroneList();
            allDronesButton.Visibility = Visibility.Hidden;
            WeightSelector.SelectedItem = -1;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new DroneWindow(BlDroneList, this).Show();
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList drone = (DroneToList)DroneListView.SelectedItem;
            DroneWindow droneWindow = new(BlDroneList, drone.Id);
            droneWindow.Show();
            droneWindow.DroneChanged += UpdateDroneList;
        }

        public void UpdateDroneList(object s, EventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDroneList();            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var drones = from drone in BlDroneList.GetDroneList()
                         orderby drone.Status
                         select drone;
            DroneListView.ItemsSource = drones;
        }
    }
}
