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
