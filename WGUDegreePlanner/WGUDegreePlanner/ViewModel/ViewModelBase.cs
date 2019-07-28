using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace WGUDegreePlanner.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {        
        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
        public void SetNotify(bool enabled, string title, string body, int id, DateTime notifyTime)
        {            
            if(enabled)
            {
                CrossLocalNotifications.Current.Show(title, body, id, notifyTime);
            }
            else
            {
                CrossLocalNotifications.Current.Cancel(id);
            }
        }
        string title = string.Empty;
        public string Title
        {
            get
            {
                return Title;
            }
            set
            {
                SetProperty(ref title, value);
            }            
        }
        bool isBusy = false;
        public bool IsBusy
        {
            set { SetProperty(ref isBusy, value); }
            get { return isBusy; }            
        }  
        
    }
}
