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

namespace UchPrakt326.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            App.header = "Список услуг";
            LvList.ItemsSource = App.DB.Service.ToList();

        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
