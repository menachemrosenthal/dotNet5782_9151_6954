﻿using System;
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
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.WeightCategories weight = (WeightCategories)WeightSelector.SelectedItem;

        }
    }
}
