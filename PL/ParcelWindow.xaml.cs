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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        BO.BL BlParcel;
        Parcel parcel;
        public ParcelWindow(BO.BL bl, ParcelListWindow droneListWindow)
        {
            InitializeComponent();
            BlParcel = bl;
            UpdateParcelButton.Visibility = Visibility.Hidden;
            UpdateDeliveryButton.Visibility = Visibility.Hidden;
            UpdatePickedUpButton.Visibility = Visibility.Hidden;
            DeleteParcelButton.Visibility = Visibility.Hidden;
        }
        public ParcelWindow(BO.BL bl, ParcelToList parcelToList, ParcelListWindow droneListWindow)
        {
            InitializeComponent();
            BlParcel = bl;
            AddParcelButton.Visibility = Visibility.Hidden;
            parcel = BlParcel.GetParcel(parcelToList.Id);
            DataContext = parcel;
        }
    }
}
