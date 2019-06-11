using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataModels
{
    public class TestPaperModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void NotifyOfPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public int TestPaperId {
            get
            {
                return _testPaperId;
            }
            set
            {
                _testPaperId = value;
                NotifyOfPropertyChange("TestPaperId");
            }
        }
        private int _testPaperId;

        public string SubjectName
        {
            get { return _subjectName; }
            set { _subjectName = value; NotifyOfPropertyChange("SubjectName"); }
        }
        private string _subjectName;

        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyOfPropertyChange("Title"); }
        }
        private string _title;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; NotifyOfPropertyChange("Subtitle"); }
        }
        private string _subtitle;

        public string Content
        {
            get { return _content; }
            set { _content = value; NotifyOfPropertyChange("Content"); }
        }
        private string _content;

    }
}
