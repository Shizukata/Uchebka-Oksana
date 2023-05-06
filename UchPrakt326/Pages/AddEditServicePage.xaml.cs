using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddEditSevicePage.xaml
    /// </summary>
    public partial class AddEditSevicePage : Page
    {
        Service serv;
        public AddEditSevicePage(Service serv)
        {
            InitializeComponent();
            this.serv = serv;
            this.DataContext = serv;
            serv.DurationInSeconds /= 60;
            serv.Discount *= 100;
            ExcessImageList.ItemsSource = serv.ServicePhoto;
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            serv.DurationInSeconds *= 60;
            serv.Discount /= 100;
            if (serv.ID == 0)
            {
                if (App.DB.Service.FirstOrDefault(x => x.Title == TbName.Text) == null)
                    App.DB.Service.Add(serv);
                else
                {
                    MessageBox.Show("Такое наименование уже существует, попробуйте иное");
                    return;
                }
                    
                
            }
            App.DB.SaveChanges();
            MessageBox.Show("Успешно");
            NavigationService.Navigate(new ServicePage());
        }
        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                serv.Logo = File.ReadAllBytes(openFileDialog.FileName);
                ServiceImg.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage--;
            if (numberPage < 0)
                numberPage = 0;
            Update();
        }
        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage++;
            if (ExcessImageList.Items.Count < 3)
                numberPage--;
            Update();
        }
        private void AddAdditImgBtn_Click(object sender, RoutedEventArgs e)
        {
            ServicePhoto servicePhoto = new ServicePhoto();
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                servicePhoto.PhotoPath = File.ReadAllBytes(openFile.FileName);
                servicePhoto.ServiceID = serv.ID;
                App.DB.ServicePhoto.Add(servicePhoto);
                App.DB.SaveChanges();
            }
            ExcessImageList.ItemsSource = serv.ServicePhoto;
        }
        int numberPage = 0;
        int count = 3;
        private void Update()
        {
            IEnumerable<ServicePhoto> servicePhotoList = App.DB.ServicePhoto.Where(x => x.ServiceID == serv.ID);
            servicePhotoList = servicePhotoList.Skip(count * numberPage).Take(count);
            ExcessImageList.ItemsSource = servicePhotoList;
        }
        private void DltImageBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteIngBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteAdditImgBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я]") == false)
                e.Handled = true;
        }

        private void TbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
                e.Handled = true;
        }

        private void TbTimeInSec_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
                e.Handled = true;
        }

        private void TbDescription_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я]") == false)
                e.Handled = true;
        }

        private void TbDiscount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0.0-9]") == false)
                e.Handled = true;
        }
    }
}
