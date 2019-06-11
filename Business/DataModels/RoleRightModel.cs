using System.ComponentModel;

namespace Business.DataModels
{
    public class RoleRightModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void NotifyOfPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyOfPropertyChange("Id");
            }
        }
        private int _id;

        public string RightName
        {
            get { return _rightName; }
            set { _rightName = value; NotifyOfPropertyChange("RightName"); }
        }
        private string _rightName;

        public int RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
                NotifyOfPropertyChange("RoleId");
            }
        }
        private int _roleId;

    }
}
