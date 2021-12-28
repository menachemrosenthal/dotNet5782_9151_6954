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
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        BO.BL BlCustomerList;
        event EventHandler CustomerListChanged;
        /// <summary>
        /// consractor
        /// </summary>
        /// <param name="bl"></param>
        public CustomerListWindow(BO.BL bl)
        {
            InitializeComponent();
            BlCustomerList = bl;
            CustomerListView.ItemsSource = bl.GetCustomerList();
            CustomerListChanged += UpdateWindow;
            BlCustomerList.EventRegistration(CustomerListChanged, "Customer");
        }
        /// <summary>
        /// opens customer window by selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CostomerWindow(BlCustomerList, (CustomerToList)CustomerListView.SelectedItem).Show();                        
        }
        /// <summary>
        /// opens add customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new CostomerWindow(BlCustomerList).Show();
        }
        /// <summary>
        /// updates the window(refresh)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindow(object sender, EventArgs e)
        {
            CustomerListView.ItemsSource = BlCustomerList.GetCustomerList();
        }
    }
}
