﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UchPrakt326.Model
{
    public partial class Service
    {
        public Visibility VisibilityBtn
        {
            get
            {
                if (App.godMod == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public string Color
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "#FFFFFF";
                else
                    return "#BDFFBD";
            }
        }
        public string StrCostTime
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return $"{Cost} рублей за {DurationInSeconds / 60} минут";
                else
                    return $"{(double)Cost - (double)Cost * Discount} рублей за {DurationInSeconds / 60} минут";
            }
        }
        public Visibility VisibilityCost
        {
            get
            {
                if (Discount == null || Discount == 0)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public string StrDiscount
        {
            get
            {
                if (Discount != 0 || Discount != null)
                    return $"*скидка {Discount * 100}%";
                else
                    return $"";
            }
        }
        public Visibility VisibilityDiscount
        {
            get
            {
                if (Discount == null || Discount == 0)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public decimal CostDiscount
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Cost;
                else
                    return (decimal)Cost - Convert.ToDecimal(Cost) * Convert.ToDecimal(Discount);
            }
        }
    }
}


