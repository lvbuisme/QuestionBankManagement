using Business;
using Caliburn.Micro;
using Panuon.UI;
using Panuon.UI.Utils;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace AppManager.ViewModels.QuestionBank
{

    public class AddQuestionBankViewModel : Screen, IShell
    {
        private QuetionBankBusiness _quetionBankBusiness = new QuetionBankBusiness();
        private List<QuestionType> questionTypes = null;
        #region Identity
        private IWindowManager _windowManager;
     
        public BindableCollection<PUComboBoxItemModel> SubjectItems
        {
            get { return _subjectItems; }
            set { _subjectItems = value; NotifyOfPropertyChange(() => SubjectItems); }
        }
        private BindableCollection<PUComboBoxItemModel> _subjectItems = new BindableCollection<PUComboBoxItemModel>();

        public BindableCollection<PUComboBoxItemModel> QuestionTypesItems
        {
            get {
                return _questionTypesItems;
            }
            set {
                _questionTypesItems = value;
                NotifyOfPropertyChange(() => QuestionTypesItems);
               
            }
        }
        private BindableCollection<PUComboBoxItemModel> _questionTypesItems = new BindableCollection<PUComboBoxItemModel>();

        public BindableCollection<PUComboBoxItemModel> KnowledgePointItems
        {
            get { return _knowledgePointItems; }
            set { _knowledgePointItems = value; NotifyOfPropertyChange(() => KnowledgePointItems); }
        }
        private BindableCollection<PUComboBoxItemModel> _knowledgePointItems = new BindableCollection<PUComboBoxItemModel>();

        private int _questionTypesValue;
        public int QuestionTypesValue
        {
            get
            {
                return _questionTypesValue;
            }
            set
            {
                _questionTypesValue = value;
                NotifyOfPropertyChange(() => QuestionTypesValue);
                HasOption= questionTypes.FirstOrDefault(c => c.Id == value).HasOption;
                IsFixedScore = !questionTypes.FirstOrDefault(c => c.Id == value).IsFixedScore;
                if (IsFixedScore)
                {
                    Score = questionTypes.FirstOrDefault(c => c.Id == value).Score;
                }
            }
        }
        private int _subjectValue;
        public int SubjectValue
        {
            get
            {
                return _subjectValue;
            }
            set
            {
                _subjectValue = value;
                NotifyOfPropertyChange(() => SubjectValue);
            }
        }
        private int _knowledgePointValue;
        public int KnowledgePointValue
        {
            get
            {
                return _knowledgePointValue;
            }
            set
            {
                _knowledgePointValue = value;
                NotifyOfPropertyChange(() => KnowledgePointValue);
            }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {

                _score = value;
                NotifyOfPropertyChange(() => Score);
            }
        }
        private bool _hasOption;
        public bool HasOption
        {
            get
            {
                return _hasOption;
            }
            set
            {
                _hasOption = value;
                NotifyOfPropertyChange(() => HasOption);
            }
        }
        private string _option1;
        public string Option1
        {
            get
            {
                return _option1;
            }
            set
            {
                _option1 = value;
                NotifyOfPropertyChange(() => Option1);
            }
        }
        private string _option2;
        public string Option2
        {
            get
            {
                return _option2;
            }
            set
            {
                _option2 = value;
                NotifyOfPropertyChange(() => Option2);
            }
        }
        private string _option3;
        public string Option3
        {
            get
            {
                return _option3;
            }
            set
            {
                _option3 = value;
                NotifyOfPropertyChange(() => Option3);
            }
        }
        private string _option4;
        public string Option4
        {
            get
            {
                return _option4;
            }
            set
            {
                _option4 = value;
                NotifyOfPropertyChange(() => Option4);
            }
        }
        private int _gradeDifficulty;
        public int GradeDifficulty
        {
            get
            {
                return _gradeDifficulty;

            }
            set
            {
                _gradeDifficulty = value;
                NotifyOfPropertyChange(() => GradeDifficulty);
            }
        }
        private bool _isFixedScore;
        public bool IsFixedScore
        {
            get
            {
                return _isFixedScore;
            }
            set
            {
                _isFixedScore = value;
                NotifyOfPropertyChange(() => IsFixedScore);
            }
        }
        #endregion
        #region Constructor
        public AddQuestionBankViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            questionTypes = _quetionBankBusiness.GetAllQuestionType();
            SubjectItems.AddRange(_quetionBankBusiness.GetAllSubject().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.SubjectName }).ToList());
            QuestionTypesItems.AddRange(questionTypes.Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.QuestionTypeName }).ToList());
            KnowledgePointItems.AddRange(_quetionBankBusiness.GetAllKnowledgePoint().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.KnowledgePointName }).ToList());
            GradeDifficulty = 2;
        }
        #endregion
        #region Function 
        public void AddExaminationQuestion()
        {
            if(Content==null)
            {
                PUMessageBox.ShowDialog("内容不能为空");
                return;
            }
            OptionItem[] optionItems = null;
            if (HasOption)
            {
                optionItems = new OptionItem[] {
                    new OptionItem() { OptionContent = Option1 } ,
                    new OptionItem() { OptionContent = Option2 },
                    new OptionItem() { OptionContent = Option3 },
                    new OptionItem() { OptionContent = Option4 }
                };
            }
            if (_quetionBankBusiness.AddExaminationQuestion(
                 new ExaminationQuestion
                 {
                     DifficultyGrade= GradeDifficulty,
                     Content = Content,
                     KnowledgePointId = KnowledgePointValue,
                     QuestionTypeId = QuestionTypesValue,
                     SubjectId = SubjectValue,
                     Score = Score,
                     OptionItems = optionItems,
                 }))
            {
                Content = null;
                KnowledgePointValue = 1;
                QuestionTypesValue = 1;
                SubjectValue = 1;
                Score = 0;
                Option1 = null;
                Option2 = null;
                Option3 = null;
                Option4 = null;
                PUMessageBox.ShowDialog("添加成功");
            }
            else
            {
                PUMessageBox.ShowDialog("添加失败");

            }
        }
    
        #endregion

    }

}
