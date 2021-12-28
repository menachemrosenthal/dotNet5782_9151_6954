using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BO.BL bl = (BO.BL)BL.BlFactory.GetBl();
        /// <summary>
        /// constractor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
