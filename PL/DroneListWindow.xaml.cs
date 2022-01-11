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
        /// <summary>
        /// BL access
        /// </summary>
        BO.BL BlDroneList;

        /// <summary>
        /// ienumerable for grouping
        /// </summary>
        ICollectionView mainView;

        /// <summary>
        /// when drone list get changes
        /// </summary>
        event EventHandler DroneListChanged;

        private object WindowLock = new();

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bl"></param>
        public DroneListWindow(BO.BL bl)
        {
            InitializeComponent();
            lock (bl)
            {
                BlDroneList = (BO.BL)BL.BlFactory.GetBl();          
                DroneListView.ItemsSource = bl.GetDroneList();
            }
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            //DroneListChanged += UpdateWindow;
            //BlDroneList.EventRegistration(DroneListChanged, "Drone");
        }

        /// <summary>
        /// filter by Weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem != null)

            {
                InitializeComponent();
                BlDroneList = (BO.BL)BL.BlFactory.GetBl();
                lock (BlDroneList)
                    DroneListView.ItemsSource = BlDroneList.GetDroneList();
                WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
                StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
                //DroneListChanged += UpdateWindow;
                //BlDroneList.EventRegistration(DroneListChanged, "Drone");
            }
        }

        /// <summary>
        /// filter by Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (WeightSelector.SelectedItem != null)
            {
                lock (BlDroneList)
                    DroneListView.ItemsSource = BlDroneList.GetDronesByCondition
                        (x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
                allDronesButton.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// back to standart list after filtering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandartList_Click(object sender, RoutedEventArgs e)
        {
            lock (BlDroneList)
                DroneListView.ItemsSource = BlDroneList.GetDroneList();
            allDronesButton.Visibility = Visibility.Hidden;
            WeightSelector.SelectedItem = null;
            StatusSelector.SelectedItem = null;
            mainView = null;
        }

        /// <summary>
        /// opens drone window with sekected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DroneListView.SelectedItem != null)
            {
                DroneToList drone = (DroneToList)DroneListView.SelectedItem;
                DroneWindow droneWindow = new DroneWindow(BlDroneList, drone.Id);
                droneWindow.Show();
                droneWindow.DroneChanged += UpdateWindow;
            }
        }

        /// <summary>
        /// updates window(refresh)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void UpdateWindow(object sender, EventArgs e)
        {
            lock (BlDroneList)
             Dispatcher.Invoke(()=> DroneListView.ItemsSource = BlDroneList.GetDroneList());
        }

        /// <summary>
        /// opens drone window to add dreone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDroneButton_Click_1(object sender, RoutedEventArgs e)
        {
            new DroneWindow(BlDroneList).Show();
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// grouping by status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
