using DemoEkz.Components;
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
using Microsoft.Win32;
using System.IO;

namespace DemoEkz.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        Service service;
        public AddEditServicePage(Service service)
        {
            InitializeComponent();
            this.service = service;
            this.DataContext = service;
            service.DurationInSeconds /= 60;
            service.Discount *= 100;
            
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            service.DurationInSeconds *= 60;
            service.Discount /= 100;
            if (service.ID == 0)
            {
                DbConnect.db.Service.Add(service);
            }
            DbConnect.db.SaveChanges();
            MessageBox.Show("Успешно");
            Navigation.NextPage(new Nav("Список услуг", new ServicePage()));
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                service.MainImagePath = File.ReadAllBytes(openFileDialog.FileName);
                ServiceImg.Source = new BitmapImage (new Uri(openFileDialog.FileName));
            }
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage--;
            if(numberPage < 0)
                numberPage= 0;
            Update();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage++;
            if(ExcessImageList.Items.Count < 3)
                numberPage--;
            Update();
        }

        private void DeleteIngBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddIngBtn_Click(object sender, RoutedEventArgs e)
        {
            ServicePhoto servicePhoto = new ServicePhoto();
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
               servicePhoto.PhotoPath = File.ReadAllBytes(openFile.FileName);
                servicePhoto.ServiceID = service.ID;
                DbConnect.db.ServicePhoto.Add(servicePhoto);
                DbConnect.db.SaveChanges();
            }
        }
        int numberPage = 0;
        int count = 3;
        private void Update()
        {
            IEnumerable<ServicePhoto> servicePhotoList = DbConnect.db.ServicePhoto.Where(x => x.ServiceID == service.ID);
            servicePhotoList = servicePhotoList.Skip(count * numberPage).Take(count);
            ExcessImageList.ItemsSource = servicePhotoList;
        }
    }
}
