using IBL.BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                (x => x.MaxWeight == (IBL.BO.WeightCategories)StatusSelector.SelectedItem);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.Status == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDroneList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new DroneWindow(BlDroneList, this).Show();
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IBL.BO.DroneToList drone = (IBL.BO.DroneToList)DroneListView.SelectedItem;
            new DroneWindow(BlDroneList, drone, this).Show();
        }
    }
}
