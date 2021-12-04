using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL.BO.BL bl = new();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ShowDrones_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show();
        }
    }
}
