using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataModels
{
    public class UserModels : INotifyPropertyChanged
    {
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public UserModels()
        {
            Uid = Guid.NewGuid().ToString("N");
        }
        private object _uid;
        internal object Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private object _userName;
        public object UserName {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged("UserName"); }
        }
        private object _account;
        public object Account
        {
            get { return _account; }
            set { _account = value; OnPropertyChanged("Account"); }
        }
        private object _mobilePhone;
        public object MobilePhone
        {
            get { return _mobilePhone; }
            set { _mobilePhone = value; OnPropertyChanged("MobilePhone"); }
        }
        public object Header
        {
            get { return _header; }
            set
            {
                _header = value; OnPropertyChanged("Header");
            }
        }
        private object _header = "";


    }
}
