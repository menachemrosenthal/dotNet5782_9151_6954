using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBL.BO.BL BlDroneList;
        public DroneListWindow(IBL.BO.BL bl)
        {
            InitializeComponent();
            BlDroneList = bl;
            DroneListView.ItemsSource = bl.GetDroneList();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.MaxWeight == (WeightCategories)StatusSelector.SelectedItem);

            allDronesButton.Visibility = Visibility.Visible;
        }

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
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new DroneWindow(BlDroneList, this).Show();
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            new DroneWindow(BlDroneList, (DroneToList)DroneListView.SelectedItem, this).Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
