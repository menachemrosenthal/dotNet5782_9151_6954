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
            ParcelListChanged += UpdateWindow;
            BlParcelList.EventRegistration(ParcelListChanged, "Parcel");
            weightFilter.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            statusFilter.ItemsSource = Enum.GetValues(typeof(ParcelStatuses));
            priorityFilter.ItemsSource = Enum.GetValues(typeof(Priorities));
        }

        private void UpdateWindow(object sender, EventArgs e)
        {
            ParcelListView.ItemsSource = BlParcelList.GetParcelList();

            if (senderGruping.Visibility == Visibility.Hidden)
            {
                mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Senderid");
                mainView.GroupDescriptions.Add(groupDescription);
            }

            if (targetGrouping.Visibility == Visibility.Visible)
            {
                mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Senderid");
                mainView.GroupDescriptions.Add(groupDescription);
            }

            if (weightFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Weight == (WeightCategories)weightFilter.SelectedItem
                                             select parcel;
            }

            if (statusFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Status == (ParcelStatuses)statusFilter.SelectedItem
                                             select parcel;
            }

            if (priorityFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Priority == (Priorities)priorityFilter.SelectedItem
                                             select parcel;
            }
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(BlParcelList).Show();
        }

        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ParcelListView.SelectedItem != null)
            {
                ParcelToList parcelToList = (ParcelToList)ParcelListView.SelectedItem;
                new ParcelWindow(BlParcelList, parcelToList.Id).Show();
            }
        }

        private void SenderGruping_Click(object sender, RoutedEventArgs e)
        {             
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("Senderid");
            mainView.GroupDescriptions.Add(groupDescription);
            senderGruping.Visibility = Visibility.Hidden;
            targetGrouping.Visibility = Visibility.Hidden;
        }

        private void TargetGrouping_Click(object sender, RoutedEventArgs e)
        {            
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("TargetId");
            mainView.GroupDescriptions.Add(groupDescription);
            targetGrouping.Visibility = Visibility.Hidden;
            senderGruping.Visibility = Visibility.Hidden;
        }

        private void StandartListButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = BlParcelList.GetParcelList();
            weightFilter.SelectedItem = null;
            statusFilter.SelectedItem = null;
            priorityFilter.SelectedItem = null;
            senderGruping.Visibility = Visibility.Visible;
            targetGrouping.Visibility = Visibility.Visible;
        }

        private void WeightFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weightFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Weight == (WeightCategories)weightFilter.SelectedItem
                                             select parcel;
            }
        }

        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statusFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Status == (ParcelStatuses)statusFilter.SelectedItem
                                             select parcel;
            }
        }

        private void priorityFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (priorityFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Priority == (Priorities)priorityFilter.SelectedItem
                                             select parcel;
            }
        }
    }
}
