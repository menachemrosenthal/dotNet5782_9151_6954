using System;
using System.Collections.Generic;
using System.ComponentModel;
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
partial class DroneListWindow : Window
{
    BO.BL BlDroneList;
    ICollectionView mainView;
    event EventHandler DroneListChanged;

    public DroneListWindow(BO.BL bl)
    {
        InitializeComponent();
        BlDroneList = (BO.BL)BL.BlFactory.GetBl();
        DroneListView.ItemsSource = bl.GetDroneList();
        WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        DroneListChanged += UpdateWindow;
        BlDroneList.EventRegistration(DroneListChanged, "Drone");
    }

    private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (WeightSelector.SelectedItem != null)
        {
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
            allDronesButton.Visibility = Visibility.Visible;
        }
    }

    private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (StatusSelector.SelectedItem != null)
        {
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.Status == (DroneStatuses)StatusSelector.SelectedItem);
            allDronesButton.Visibility = Visibility.Visible;
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        DroneListView.ItemsSource = BlDroneList.GetDroneList();
        allDronesButton.Visibility = Visibility.Hidden;
        groupingButton.Visibility = Visibility.Visible;
        WeightSelector.SelectedItem = null;
        StatusSelector.SelectedItem = null;
        if (mainView != null)
            mainView.GroupDescriptions.Clear();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        new DroneWindow(BlDroneList).Show();
    }

    private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        DroneToList drone = (DroneToList)DroneListView.SelectedItem;
        new DroneWindow(BlDroneList, drone.Id).Show();
    }

    public void UpdateWindow(object s, EventArgs e)
    {
        DroneListView.ItemsSource = BlDroneList.GetDroneList();

        if (StatusSelector.SelectedItem != null)
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
            (x => x.Status == (DroneStatuses)StatusSelector.SelectedItem);

        if (WeightSelector.SelectedItem != null)
            DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                (x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);

        if (mainView != null)
        {
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            mainView.GroupDescriptions.Add(groupDescription);
        }
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        mainView = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
        mainView.GroupDescriptions.Add(groupDescription);
        groupingButton.Visibility = Visibility.Hidden;
        allDronesButton.Visibility = Visibility.Visible;
    }

    private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
}
