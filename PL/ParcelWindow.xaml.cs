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
        /// <summary>
        /// BL access
        /// </summary>
        BO.BL BlParcel;

        /// <summary>
        /// parcel for data context
        /// </summary>
        Parcel parcel;

        /// <summary>
        /// when changing happens in parcel
        /// </summary>
        event EventHandler ParcelChanged;

        /// <summary>
        /// costructor
        /// </summary>
        /// <param name="bl"></param>
        public ParcelWindow(BO.BL bl)
        {
            InitializeComponent();
            BlParcel = bl;
            AddParcelButton.Visibility = Visibility.Visible;
            IdLabel.Visibility = Visibility.Hidden;
            PriorityTextbox.ItemsSource = Enum.GetValues(typeof(Priorities));
            WeightTextbox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            IdTextbox.IsReadOnly = false;
        }

        /// <summary>
        /// constructor by selected item
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="parcelId"></param>
        public ParcelWindow(BO.BL bl, int parcelId)
        {
            InitializeComponent();
            BlParcel = bl;
            parcel = BlParcel.GetParcel(parcelId);
            ParcelChanged += UpdateWindow;
            BlParcel.EventRegistration(ParcelChanged, "Parcel");
            PriorityTextbox.ItemsSource = Enum.GetValues(typeof(Priorities));
            PriorityTextbox.SelectedValue = parcel.Priority;
            PriorityTextbox.IsEnabled = false;
            WeightTextbox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightTextbox.SelectedValue = parcel.Weight;
            WeightTextbox.IsEnabled = false;
            UpdateWindow(this, EventArgs.Empty);
        }

        /// <summary>
        /// update window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindow(object sender, EventArgs e)
        {
            if (BlParcel.GetParcel(parcel.Id) != null)
                parcel = BlParcel.GetParcel(parcel.Id);

            DataContext = parcel;

            if (parcel.Drone == null)
                DeleteParcelButton.Visibility = Visibility.Visible;

            else
                DroneInParcelTextbox.Text = parcel.Drone.ToString();
        }

        /// <summary>
        /// add parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Parcel parcel = new()
                {
                    Senderid = int.Parse(SenderIdTextbox.Text),
                    TargetId = int.Parse(TargetIdTextbox.Text),
                    Weight = (WeightCategories)WeightTextbox.SelectedItem,
                    Priority = (Priorities)PriorityTextbox.SelectedItem
                };

                BlParcel.AddParcel(parcel);
                MessageBox.Show("The Parcel was added successfully");
                Close();
            }

            catch (Exception ex)
            {
                if (SenderIdTextbox.Background == Brushes.OrangeRed)
                    SenderIdTextbox.Text = "wrong id";
                if (TargetIdTextbox.Background == Brushes.OrangeRed)
                    TargetIdTextbox.Text = "wrong id";
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// change color by event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneInParcelTextbox_MouseEnter(object sender, MouseEventArgs e)
        {
            DroneInParcelTextbox.Background = Brushes.LightBlue;
        }

        /// <summary>
        /// open drone window by selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneInParcelTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new DroneWindow(BlParcel, parcel.Drone.Id).Show();
        }

        /// <summary>
        /// change color acustomed to mistake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SenderIdTextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!SenderIdTextbox.IsReadOnly)
            {
                _ = int.TryParse(SenderIdTextbox.Text, out int id);
                if (id > 0 && BlParcel.GetCustomerList().Any(x => x.Id == id))
                {
                    SenderIdTextbox.Background = Brushes.LightGreen;
                    return;
                }

                SenderIdTextbox.Background = Brushes.OrangeRed;
            }
        }

        /// <summary>
        /// change color acustomed to mistake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetIdTextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (!TargetIdTextbox.IsReadOnly)
            {
                _ = int.TryParse(TargetIdTextbox.Text, out int id);
                if (id > 0 && BlParcel.GetCustomerList().Any(x => x.Id == id))
                {
                    TargetIdTextbox.Background = Brushes.LightGreen;
                    return;
                }

                TargetIdTextbox.Background = Brushes.OrangeRed;
            }
        }

        /// <summary>
        /// delete parcel if it not associated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteParcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlParcel.EventDelete(ParcelChanged, "Parcel");
                BlParcel.ParcelDelete(parcel.Id);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// open customer window of sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SenderIdTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SenderIdTextbox.IsReadOnly)
                new CostomerWindow(BlParcel, int.Parse(SenderIdTextbox.Text)).Show();
        }

        /// <summary>
        /// open customer window of reciver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetIdTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TargetIdTextbox.IsReadOnly)
                new CostomerWindow(BlParcel, int.Parse(TargetIdTextbox.Text)).Show();
        }
    }
}
