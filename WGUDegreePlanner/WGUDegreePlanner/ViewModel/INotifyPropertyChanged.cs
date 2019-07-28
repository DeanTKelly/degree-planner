using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WGUDegreePlanner.ViewModel
{
    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
