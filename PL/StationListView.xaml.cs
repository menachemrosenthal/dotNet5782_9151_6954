using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListView.xaml
    /// </summary>
    public partial class StationListView : Window
    {
        BO.BL GetBL;
        ICollectionView mainView;
        event EventHandler StationListChanged;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bL"></param>
        public StationListView(BO.BL bL)
        {
            InitializeComponent();
            GetBL = bL;
            StationListChanged += UpdateWindow;
            GetBL.EventRegistration(StationListChanged, "Station");
            stationList.ItemsSource = GetBL.GetBaseStationList();
        }
        /// <summary>
        /// open station window by selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.StationToList station = (BO.StationToList)stationList.SelectedItem;
            new StationWindow(GetBL, station.Id).Show();                        
        }
        /// <summary>
        /// grouping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupingButton_Click(object sender, EventArgs e)
        {
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(stationList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("FreeChargeSlots");
            mainView.GroupDescriptions.Add(groupDescription);
            standartListButton.Visibility = Visibility.Visible;
            groupingButton.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// return to standared list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandartListButton_Click(object sender, EventArgs e)
        {
            stationList.ItemsSource = GetBL.GetBaseStationList();
            mainView = null;
            groupingButton.Visibility = Visibility.Visible;
            standartListButton.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindow(object sender, EventArgs e)
        {
            stationList.ItemsSource = GetBL.GetBaseStationList();

            if (mainView != null)
            {
                mainView = (CollectionView)CollectionViewSource.GetDefaultView(stationList.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("FreeChargeSlots");
                mainView.GroupDescriptions.Add(groupDescription);
            }
        }
        /// <summary>
        /// open station window to add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow (GetBL).Show();                        
        }
    }
}
