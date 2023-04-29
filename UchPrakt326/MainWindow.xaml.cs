using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using UchPrakt326.Pages;

namespace UchPrakt326
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ServicePage());
            Header.Text = App.header.ToString();

        }

        private void Button_Click_GM(object sender, RoutedEventArgs e)
        {
            if (TbGodCode.Text == "0000")
            {
                App.godMod = true;
                MainFrame.Navigate(new ServicePage());
            }
            else
                MessageBox.Show("Код неверный");
        }
    }
}
