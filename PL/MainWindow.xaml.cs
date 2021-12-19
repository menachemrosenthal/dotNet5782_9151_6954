﻿using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BO.BL bl = (BO.BL)BL.BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }

        private void stationsButton_Click(object sender, RoutedEventArgs e)
        {
            new StationListView(bl).Show();
        }

        private void CustomerListButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
        }
    }
}
