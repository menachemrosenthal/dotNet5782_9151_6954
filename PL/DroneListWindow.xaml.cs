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
<<<<<<< HEAD
    partial class DroneListWindow : Window
    {
        BO.BL BlDroneList;
        ICollectionView mainView;
        event EventHandler DroneListChanged;

        public DroneListWindow(BO.BL bl)
=======
partial class DroneListWindow : Window
{
    BO.BL BlDroneList;
    ICollectionView mainView;
    event EventHandler DroneListChanged;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bl"></param>
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
    /// <summary>
    /// filter by Weight
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (WeightSelector.SelectedItem != null)
>>>>>>> 41e013b9653df63d59a7fa6c076218a25f6c58e1
        {
            InitializeComponent();
            BlDroneList = (BO.BL)BL.BlFactory.GetBl();
            DroneListView.ItemsSource = bl.GetDroneList();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            DroneListChanged += UpdateWindow;
            BlDroneList.EventRegistration(DroneListChanged, "Drone");
        }
<<<<<<< HEAD

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
=======
    }
        /// <summary>
        /// filter by Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (StatusSelector.SelectedItem != null)
>>>>>>> 41e013b9653df63d59a7fa6c076218a25f6c58e1
        {
            if (WeightSelector.SelectedItem != null)
            {
                DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                    (x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
                allDronesButton.Visibility = Visibility.Visible;
            }
        }

<<<<<<< HEAD
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
            WeightSelector.SelectedItem = null;
            StatusSelector.SelectedItem = null;
            mainView = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new DroneWindow(BlDroneList).Show();
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DroneListView.SelectedItem != null)
            {
                DroneToList drone = (DroneToList)DroneListView.SelectedItem;
                new DroneWindow(BlDroneList, drone.Id).Show();
            }
        }

        public void UpdateWindow(object s, EventArgs e)
        {
            DroneListView.ItemsSource = BlDroneList.GetDroneList();
=======
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
    /// <summary>
    /// opens drone window to add dreone
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        new DroneWindow(BlDroneList).Show();
    }
    /// <summary>
    /// opens drone window with sekected item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        DroneToList drone = (DroneToList)DroneListView.SelectedItem;
        new DroneWindow(BlDroneList, drone.Id).Show();
    }
    /// <summary>
    /// updates window(refresh)
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public void UpdateWindow(object s, EventArgs e)
    {
        DroneListView.ItemsSource = BlDroneList.GetDroneList();
>>>>>>> 41e013b9653df63d59a7fa6c076218a25f6c58e1

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
<<<<<<< HEAD

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GroupingByStatus_Click(object sender, RoutedEventArgs e)
        {
            if (mainView == null)
            {
                mainView = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
                mainView.GroupDescriptions.Add(groupDescription);
                allDronesButton.Visibility = Visibility.Visible;
            }
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
=======
    }
    /// <summary>
    /// close window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Close();
    }
    /// <summary>
    /// group by status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
        mainView = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
        mainView.GroupDescriptions.Add(groupDescription);
        groupingButton.Visibility = Visibility.Hidden;
        allDronesButton.Visibility = Visibility.Visible;
>>>>>>> 41e013b9653df63d59a7fa6c076218a25f6c58e1
    }
}
