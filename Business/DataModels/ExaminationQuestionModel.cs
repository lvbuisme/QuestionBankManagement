using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataModels
{
    public class ExaminationQuestionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void NotifyOfPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public int ExaminationQuestionId {
            get
            {
                return _examinationQuestionId;
            }
            set
            {
                _examinationQuestionId = value;
                NotifyOfPropertyChange("ExaminationQuestionId");
            }
        }
        private int _examinationQuestionId;
        public string QuestionTypeName
        {
            get { return _questionTypeName; }
            set { _questionTypeName = value; NotifyOfPropertyChange("QuestionTypeName"); }
        }
        private string _questionTypeName;

        public string SubjectName
        {
            get { return _subjectName; }
            set { _subjectName = value; NotifyOfPropertyChange("SubjectName"); }
        }
        private string _subjectName;

        public int Score
        {
            get { return _score; }
            set { _score = value; NotifyOfPropertyChange("Score"); }
        }
        private int _score;

        public string KnowledgePointName
        {
            get { return _knowledgePointName; }
            set { _knowledgePointName = value; NotifyOfPropertyChange("KnowledgePointName"); }
        }
        private string _knowledgePointName;

        public string Content
        {
            get { return _content; }
            set { _content = value; NotifyOfPropertyChange("Content"); }
        }
        private string _content;

    }
}
