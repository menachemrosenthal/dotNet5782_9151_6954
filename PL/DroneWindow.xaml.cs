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
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.BO.BL BlDrone;
        public DroneWindow(IBL.BO.BL bl)
        {
            InitializeComponent();
            BlDrone = bl;
            StationList.ItemsSource = BlDrone.GetBaseStationList();
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            IBL.BO.DroneToList drone = new()
            {
                Model = Name.Text,
                Id = int.Parse(ID.Text),
                MaxWeight = Enum.Parse<IBL.BO.WeightCategories>(Weight.Text)
            };            
            BlDrone.AddDrone(drone, int.Parse(stationId.Text));           
        }

        void StationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var station = (IBL.BO.StationToList)StationList.SelectedItem;
            stationId.Text = $"{station.Id}";
        }


    }
}