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
            DateCombobox.Items.Add("Scheduled"); DateCombobox.Items.Add("Delivered");
            DateCombobox.Items.Add("Requested"); DateCombobox.Items.Add("PickedUp");

        }
        /// <summary>
        /// update window(refresh)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// open parcel window to add parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(BlParcelList).Show();
        }
        /// <summary>
        /// open parcel window by selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ParcelListView.SelectedItem != null)
            {
                ParcelToList parcelToList = (ParcelToList)ParcelListView.SelectedItem;
                new ParcelWindow(BlParcelList, parcelToList.Id).Show();
            }
        }
        /// <summary>
        /// group by sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SenderGruping_Click(object sender, RoutedEventArgs e)
        {             
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("Senderid");
            mainView.GroupDescriptions.Add(groupDescription);
            senderGruping.Visibility = Visibility.Hidden;
            targetGrouping.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// group by target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetGrouping_Click(object sender, RoutedEventArgs e)
        {            
            mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("TargetId");
            mainView.GroupDescriptions.Add(groupDescription);
            targetGrouping.Visibility = Visibility.Hidden;
            senderGruping.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// refreshes list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandartListButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = BlParcelList.GetParcelList();
            weightFilter.SelectedItem = null;
            statusFilter.SelectedItem = null;
            priorityFilter.SelectedItem = null;
            senderGruping.Visibility = Visibility.Visible;
            targetGrouping.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// filter by weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weightFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Weight == (WeightCategories)weightFilter.SelectedItem
                                             select parcel;
            }
        }
        /// <summary>
        /// filter by status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statusFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Status == (ParcelStatuses)statusFilter.SelectedItem
                                             select parcel;
            }
        }

        /// <summary>
        /// filter by priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priorityFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (priorityFilter.SelectedItem != null)
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where parcel.Priority == (Priorities)priorityFilter.SelectedItem
                                             select parcel;
            }
        }
        /// <summary>
        /// filter by date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterByDate_Click(object sender, RoutedEventArgs e)
        {
            if ((string)DateCombobox.SelectedItem == "Requested")
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where BlParcelList.GetParcel(parcel.Id).Requested >= FirstDate.SelectedDate &&
                                                    BlParcelList.GetParcel(parcel.Id).Requested <= LastDate.SelectedDate
                                             select parcel;
            }
            if ((string)DateCombobox.SelectedItem == "Scheduled")
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where BlParcelList.GetParcel(parcel.Id).Scheduled >= FirstDate.SelectedDate &&
                                                    BlParcelList.GetParcel(parcel.Id).Scheduled <= LastDate.SelectedDate
                                             select parcel;
            }
            if ((string)DateCombobox.SelectedItem == "PickedUp")
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where BlParcelList.GetParcel(parcel.Id).PickedUp >= FirstDate.SelectedDate &&
                                                    BlParcelList.GetParcel(parcel.Id).PickedUp <= LastDate.SelectedDate
                                             select parcel;
            }
            if ((string)DateCombobox.SelectedItem == "Delivered")
            {
                ParcelListView.ItemsSource = from parcel in BlParcelList.GetParcelList()
                                             where BlParcelList.GetParcel(parcel.Id).Delivered >= FirstDate.SelectedDate &&
                                                    BlParcelList.GetParcel(parcel.Id).Delivered <= LastDate.SelectedDate
                                             select parcel;
            }
        }
    }
}
