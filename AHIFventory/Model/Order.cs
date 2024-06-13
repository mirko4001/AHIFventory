using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory.Model
{
    public class Order : INotifyPropertyChanged
    {
        private int orderID;
        public int OrderID
        {
            get
            {
                return orderID;
            }

            set
            {
                if (orderID != value)
                {
                    orderID = value;
                    onPropertyChanged("OrderID");
                }
            }
        }

        private DateOnly orderDate;
        public DateOnly OrderDate
        {
            get
            {
                return orderDate;
            }

            set
            {
                if (orderDate != value)
                {
                    orderDate = value;
                    onPropertyChanged("OrderDate");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
