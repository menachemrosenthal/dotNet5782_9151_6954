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
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
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

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDroneList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new DroneWindow(BlDroneList).Show();
        }
    }
}
