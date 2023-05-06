using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            ClientCb.ItemsSource = App.DB.Client.ToList();
            OrderDp.Text = DateTime.Now.ToString();
            OrderTb.Text = DateTime.Now.ToString("t");
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string TimeOut = OrderDp.Text + " " + OrderTb.Text;
            if (ClientCb.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали клиента");
                return;
            }
            if (string.IsNullOrEmpty(OrderDp.Text)||DateTime.Parse(TimeOut) <= DateTime.Now)
            {
                MessageBox.Show("Вы ввели неверную дату");
                return;
            }
            if (order.ID != 0)
            {
                ClientService clientService = new ClientService();
                clientService.ServiceID = order.ID;
                var client = ClientCb.SelectedItem as Client;
                clientService.ClientID = client.ID;
                clientService.StartTime = DateTime.Parse(TimeOut);
                App.DB.ClientService.Add(clientService);
            }
            App.DB.SaveChanges();
            MessageBox.Show("Успешно");
            NavigationService.GoBack();
        }

        private void OrderTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[00-23:00-59]") == false)
                e.Handled = true;
        }
    }
}
