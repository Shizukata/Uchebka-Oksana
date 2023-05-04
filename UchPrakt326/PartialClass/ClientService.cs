﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchPrakt326.Model
{
    public partial class ClientService
    {
        public string Color
        {
            get
            {
                if(StartTime <= DateTime.Now)
                {
                    return "red";
                }
                else 
                {
                    return "White";
                }
            }
        }
        public string remainingTime
        {
            get
            {
                var timer = StartTime - DateTime.Now;
                return $"Начало через: {timer.ToString(@"hh\:mm")}";
            }
        }
    }
}
