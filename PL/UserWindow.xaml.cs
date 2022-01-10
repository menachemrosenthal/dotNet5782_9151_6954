using BO;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private BO.BL blUser;

        public UserWindow(BO.BL bl)
        {
            InitializeComponent();
            blUser = bl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CostomerWindow(blUser).Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _ = int.TryParse(IdTextbox.Text, out int id);
            if (blUser.GetCustomerList().Any(x => x.Id == id))
                 new CostomerWindow(blUser, int.Parse(IdTextbox.Text)).Show();
            else
                MessageBox.Show("No customer exists with that Id");
        }
    }
}
