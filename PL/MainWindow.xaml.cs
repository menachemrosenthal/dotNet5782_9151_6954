using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// BL access
        /// </summary>
        private BO.BL bl = (BO.BL)BL.BlFactory.GetBl();

        /// <summary>
        /// constractor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ParcelListButton.Visibility = Visibility.Hidden;
            stationsButton.Visibility = Visibility.Hidden;
            ShowDrones.Visibility = Visibility.Hidden;
            CustomerListButton.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// open drone list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }

        /// <summary>
        /// open station list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stationsButton_Click(object sender, RoutedEventArgs e)
        {
            new StationListView(bl).Show();
        }

        /// <summary>
        /// open customer list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerListButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show();
        }

        /// <summary>
        /// open parcel list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow(bl).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ParcelListButton.Visibility = Visibility.Visible;
            stationsButton.Visibility = Visibility.Visible;
            ShowDrones.Visibility = Visibility.Visible;
            CustomerListButton.Visibility = Visibility.Visible;
            managerbutton.Visibility = Visibility.Hidden;
            userbutton.Visibility = Visibility.Hidden;

        }
    }
}
