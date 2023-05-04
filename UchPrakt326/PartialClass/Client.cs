using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchPrakt326.Model
{
    public partial class Client
    {
        public string fio
        {
            get
            {
                return $"{FirstName} {LastName} {Patronymic}";
            }
        }

    }
}
