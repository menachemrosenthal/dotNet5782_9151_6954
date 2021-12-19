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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        BO.BL GetBL;
        BO.Station station;
        public StationWindow(BO.BL bl)
        {
            InitializeComponent();
            GetBL = bl;
        }

        public StationWindow(BO.BL bl, BO.StationToList stationToList, StationListView lisetWindow)
        {
            InitializeComponent();
            GetBL = bl;
            station = GetBL.GetStation(stationToList.Id);
            stationView.DataContext = station;
        }
    }
}
