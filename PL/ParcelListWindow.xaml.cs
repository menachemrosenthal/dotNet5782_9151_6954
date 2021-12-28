﻿using System;
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

        private void UpdateWindow(object sender, EventArgs e)
        {
            ParcelListView.ItemsSource = BlParcelList.GetParcelList();
            mainView = null;

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
            if (mainView == null)
            {
                mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
                PropertyGroupDescription groupDescription = new("Senderid");
                mainView.GroupDescriptions.Add(groupDescription);
            }
        }

        private void TargetGrouping_Click(object sender, RoutedEventArgs e)
        {
            if (mainView == null)
            {
                mainView = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
                PropertyGroupDescription groupDescription = new("TargetId");
                mainView.GroupDescriptions.Add(groupDescription);
            }
        }

        private void StandartListButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = BlParcelList.GetParcelList();
            weightFilter.SelectedItem = null;
            statusFilter.SelectedItem = null;
            priorityFilter.SelectedItem = null;
            mainView = null;
            mainView = null;
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
