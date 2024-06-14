using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory
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

        private DateTime orderDate;
        public DateTime OrderDate
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

        private string supplier;
        public string Supplier
        {
            get
            {
                return supplier;
            }

            set
            {
                if (supplier != value)
                {
                    supplier = value;
                    onPropertyChanged("Supplier");
                }
            }
        }

        private int productID;
        public int ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                if (productID != value)
                {
                    productID = value;
                    onPropertyChanged("ProductID");
                }
            }
        }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    onPropertyChanged("Quantity");
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
