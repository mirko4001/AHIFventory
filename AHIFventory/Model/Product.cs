using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory.Model
{
    public class Product : INotifyPropertyChanged
    {

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

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    onPropertyChanged("Name");
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (description != value)
                {
                    description = value;
                    onPropertyChanged("Description");
                }
            }
        }

        private string category;
        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                if (category != value)
                {
                    category = value;
                    onPropertyChanged("Category");
                }
            }
        }

        private double price;
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (price != value)
                {
                    price = value;
                    onPropertyChanged("Price");
                }
            }
        }

        private int stock;
        public int Stock
        {
            get
            {
                return stock;
            }

            set
            {
                if (stock != value)
                {
                    stock = value;
                    onPropertyChanged("Price");
                }
            }
        }

        private int stockWarning;
        public int StockWarning
        {
            get
            {
                return stockWarning;
            }

            set
            {
                if (stockWarning != value)
                {
                    stockWarning = value;
                    onPropertyChanged("StockWarning");
                }
            }
        }

        public bool LowOnStock
        {
            get
            {
                return Stock <= StockWarning;
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

        public Product() { }
    }
}
