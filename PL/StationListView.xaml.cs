using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListView.xaml
    /// </summary>
    public partial class StationListView : Window
    {
        BO.BL GetBL;
        public StationListView(BO.BL bL)
        {
            InitializeComponent();
            GetBL = bL;
            stationList.ItemsSource = GetBL.GetBaseStationList();
        }

        private void stationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StationWindow(GetBL, (BO.StationToList)stationList.SelectedItem, this).Show();
        }
    }
}
