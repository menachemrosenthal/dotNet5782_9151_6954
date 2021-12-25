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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        BO.BL BlParcelList;
        event EventHandler ParcelListChanged;
        ICollectionView mainView;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="bl"></param>
        public ParcelListWindow(BO.BL bl)
        {
            InitializeComponent();
            BlParcelList = bl;
            ParcelListView.ItemsSource = BlParcelList.GetParcelList();
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(BlParcelList, this).Show();
        }

        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ParcelListView.SelectedItem != null)
            {
                ParcelToList parcelToList = (ParcelToList)ParcelListView.SelectedItem;
                new ParcelWindow(BlParcelList, parcelToList, this).Show();
            }
        }

        private void SenderGruping_Click(object sender, RoutedEventArgs e)
        {
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Senderid");
            mainView.GroupDescriptions.Add(groupDescription);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("TargetId");
            mainView.GroupDescriptions.Add(groupDescription);
        }
    }
}
