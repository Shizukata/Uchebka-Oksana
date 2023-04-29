using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UchPrakt326.Model;

namespace UchPrakt326
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UchPrakt326Entities DB = new UchPrakt326Entities();
        public static bool godMod = false;
    }
}
