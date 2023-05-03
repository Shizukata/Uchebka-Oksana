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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UchPrakt326.Model;

namespace UchPrakt326.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientServicePage.xaml
    /// </summary>
    public partial class ClientServicePage : Page
    {
        Service order;
        public ClientServicePage(Service order)
        {
            this.order = order;
            this.DataContext = order;
            InitializeComponent();
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (order.ID == 0)
            {
                App.DB.Service.Add(order);
            }
            App.DB.SaveChanges();
            MessageBox.Show("Успешно");
            NavigationService.Navigate(new ServicePage());
        }
    }
}
