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
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = BlDroneList.get
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
