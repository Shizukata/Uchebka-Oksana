using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            App.header = "Список услуг";
            InitializeComponent();
            LvList.ItemsSource =App.DB.Service.Where(x => x.IsDelete != true).ToList();
            AddBtn.Visibility = Visibility.Collapsed;
            if (App.godMod == true)
                AddBtn.Visibility = Visibility.Visible;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in App.DB.Service)
            {
                item.MainImagePath = item.MainImagePath.Trim();
                item.Logo = File.ReadAllBytes($"C:/Users/progWeb/Desktop/{item.MainImagePath}");
            }
            App.DB.SaveChanges();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditSevicePage(new Service()));
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            var selService = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new AddEditSevicePage(selService));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selService = (sender as Button).DataContext as Service;
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                selService.IsDelete = true;
                App.DB.SaveChanges();
                LvList.ItemsSource = App.DB.Service.Where(x => x.IsDelete != true).ToList();

            }
        }
        public void Refresh()
        {
            IEnumerable<Service> filterService = App.DB.Service.Where(x => x.IsDelete != true).ToList();
            if (SortCb.SelectedIndex == 1)
                filterService = filterService.OrderBy(x => x.CostDiscount);
            else if (SortCb.SelectedIndex == 2)
                filterService = filterService.OrderByDescending(x => x.CostDiscount);
            if (DiscountSortCb.SelectedIndex > 0)
            {
                if (DiscountSortCb.SelectedIndex == 1)
                    filterService = filterService.Where(x => x.Discount >= 0 && x.Discount < 0.05 || x.Discount == null).ToList();
                else if (DiscountSortCb.SelectedIndex == 2)
                    filterService = filterService.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                else if (DiscountSortCb.SelectedIndex == 3)
                    filterService = filterService.Where(x => x.Discount >= 0.15 && x.Discount < 0.30).ToList();
                else if (DiscountSortCb.SelectedIndex == 4)
                    filterService = filterService.Where(x => x.Discount >= 0.30 && x.Discount < 0.70).ToList();
                else if (DiscountSortCb.SelectedIndex == 5)
                    filterService = filterService.Where(x => x.Discount >= 0.70 && x.Discount < 1).ToList();
            }
            if (TitleDescriptionTb.Text.Length > 0)
            {
                filterService = filterService.Where(x => x.Title.ToLower().StartsWith(TitleDescriptionTb.Text.ToLower()) || x.Description.ToLower().StartsWith(TitleDescriptionTb.Text.ToLower()));
            }
            LvList.ItemsSource = filterService.ToList();
        }
        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void DiscountSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TitleDescriptionTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
