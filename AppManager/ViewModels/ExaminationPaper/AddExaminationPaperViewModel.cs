using AppManager.Command;
using Business;
using Business.DataModels;
using Caliburn.Micro;
using Panuon.UI;
using Panuon.UI.Utils;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace AppManager.ViewModels.ExaminationPaper
{

    public class AddExaminationPaperViewModel : Screen, IShell
    {
        #region Identity
        private IWindowManager _windowManager;
        private QuetionBankBusiness _quetionBankBusiness = new QuetionBankBusiness();
        private ExaminationPaperBusiness _examinationPaperBusiness = new ExaminationPaperBusiness();
        private TestPaper _testPaper = new TestPaper();
        public TestPaper TestPaper
        {
            get {
                return _testPaper;
            }
            set
            {
                _testPaper = value;
                NotifyOfPropertyChange(() => ExaminationQuestionList);
            }
        }

        public ObservableCollection<ExaminationQuestionModel> ExaminationQuestionList
        {
            get { return _examinationQuestionList; }
            set { _examinationQuestionList = value; NotifyOfPropertyChange(() => ExaminationQuestionList); }
        }
        private ObservableCollection<ExaminationQuestionModel> _examinationQuestionList = new ObservableCollection<ExaminationQuestionModel>();


        private ExaminationQuestionModel _selectedItem;
        public ExaminationQuestionModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }
        public BindableCollection<PUComboBoxItemModel> SubjectItems
        {
            get {
                return _subjectItems;
            }
            set {
                _subjectItems = value;
                NotifyOfPropertyChange(() => SubjectItems);
            }
        }
        private BindableCollection<PUComboBoxItemModel> _subjectItems = new BindableCollection<PUComboBoxItemModel>();

        private BindableCollection<ExaminationQuestionModel> _questionTableList = new BindableCollection<ExaminationQuestionModel>();
        public BindableCollection<ExaminationQuestionModel> QuestionTableList
        {
            get
            {
                return _questionTableList;
            }
            set
            {
                _questionTableList = value;
                NotifyOfPropertyChange(() => QuestionTableList);
            }
        }
        private ExaminationQuestionModel _questionTableSelected;
        public ExaminationQuestionModel QuestionTableSelected
        {
            get
            {
                return _questionTableSelected;
            }
            set
            {
                _questionTableSelected = value;
                NotifyOfPropertyChange(() => QuestionTableSelected);
            }
        }
        public BindableCollection<PUTabItemModel> PaperquestionTypeTabItems
        {
            get {
                return _paperquestionTypeTabItems;
            }
            set {
                _paperquestionTypeTabItems = value;
                NotifyOfPropertyChange(() => PaperquestionTypeTabItems);
            }
        }
        private BindableCollection<PUTabItemModel> _paperquestionTypeTabItems = new BindableCollection<PUTabItemModel>();
        private int _paperquestionTypeSelectValue;
        public int PaperquestionTypeSelectValue
        {
            get
            {
                return _paperquestionTypeSelectValue;
            }
            set
            {
                _paperquestionTypeSelectValue = value;
                NotifyOfPropertyChange(() => PaperquestionTypeSelectValue);
                UpdateQuestionTableList();
            }
        }
        public AddQuestionBackToPaper AddQuestionBackToPaper
        {
            get;
            set;
        }
        public DeleteQuestionBackToPaper DeleteQuestionBackToPaper
        {
            get;
            set;
        }
        private string _paperQuestionTitle;
        public string PaperQuestionTitle
        {
            get
            {
                return _paperQuestionTitle;
            }
            set
            {
                _paperQuestionTitle = value;
                NotifyOfPropertyChange(() => PaperQuestionTitle);
              
            }
        }
        public int _subjectValue;
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
                TestPaper.SubjectId = _subjectValue;
                Init();
            }

        }
        public BindableCollection<PUComboBoxItemModel> QuestionTypesItems
        {
            get { return _questionTypesItems; }
            set { _questionTypesItems = value; NotifyOfPropertyChange(() => QuestionTypesItems); }
        }
        private BindableCollection<PUComboBoxItemModel> _questionTypesItems = new BindableCollection<PUComboBoxItemModel>();

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
                PaperQuestionTitle = QuestionTypesItems.FirstOrDefault(c => c.Value.Equals(QuestionTypesValue))?.Header;
                Init();
            }
        }
        public string SearchText
        {
            get {
                return _searchText;
            }
            set {
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);
                Init();
            }
        }
        private string _searchText;

        public int _currentPage;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                NotifyOfPropertyChange(() => CurrentPage);
                Init();
            }

        }
        public int _totalPage;
        public int TotalPage
        {
            get
            {
                return _totalPage;
            }
            set
            {
                _totalPage = value;
                NotifyOfPropertyChange(() => TotalPage);
            }

        }
        #endregion
        #region Constructor
        public AddExaminationPaperViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            AddQuestionBackToPaper = new AddQuestionBackToPaper(this);
            DeleteQuestionBackToPaper = new DeleteQuestionBackToPaper(this);
            SubjectItems.AddRange(_quetionBankBusiness.GetAllSubject().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.SubjectName }).ToList());
            SubjectValue= Convert.ToInt32(SubjectItems.FirstOrDefault().Value);
            TotalPage = _quetionBankBusiness.GetExaminationQuestionTotalPage(SearchText, QuestionTypesValue, TestPaper.SubjectId);
            CurrentPage = 1;
        }
      
   
        #endregion
        #region Function 
        private void UpdateQuestionTableList()
        {
            List<ExaminationQuestion> examinationQuestions = _quetionBankBusiness.GetExaminationQuestionsByPaperTypeId(PaperquestionTypeSelectValue);
            BindableCollection<ExaminationQuestionModel> eqnlist = new BindableCollection<ExaminationQuestionModel>();
            QuestionTypesItems = new BindableCollection<PUComboBoxItemModel>();
            QuestionTypesItems.AddRange(_quetionBankBusiness.GetAllQuestionType().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.QuestionTypeName }).ToList());
            foreach (var eq in examinationQuestions)
            {
                eqnlist.Add(new ExaminationQuestionModel
                {
                    Content = eq.Content,
                    KnowledgePointName = eq.KnowledgePoint?.KnowledgePointName,
                    QuestionTypeName = eq.QuestionType?.QuestionTypeName,
                    Score = eq.Score,
                    SubjectName = eq.Subject?.SubjectName,
                    ExaminationQuestionId = eq.Id
                });
            }
            QuestionTableList = eqnlist;

        }
        private void Init()
        {
            List<ExaminationQuestion> examinationQuestions = _quetionBankBusiness.GetExaminationQuestionList(SearchText, QuestionTypesValue, TestPaper.SubjectId,CurrentPage);
            ObservableCollection<ExaminationQuestionModel> eqnlist = new ObservableCollection<ExaminationQuestionModel>();
            foreach (var eq in examinationQuestions)
            {
                eqnlist.Add(new ExaminationQuestionModel
                {
                    Content = eq.Content.Length > 50 ? eq.Content.Substring(0, 10) : eq.Content,
                    KnowledgePointName = eq.KnowledgePoint.KnowledgePointName,
                    QuestionTypeName = eq.QuestionType.QuestionTypeName,
                    Score = eq.Score,
                    SubjectName = eq.Subject.SubjectName,
                    ExaminationQuestionId = eq.Id
                });
            }
            ExaminationQuestionList = eqnlist;
            TotalPage = _quetionBankBusiness.GetExaminationQuestionTotalPage(SearchText, QuestionTypesValue, TestPaper.SubjectId);
        }
        public void AddPaperquestionType()
        {
            if (QuestionTypesValue == 0)
            {
                PUMessageBox.ShowDialog("请选择题型");
                return;
            }
            if (TestPaper!= null && TestPaper.Id == 0){
                int testPaperid = _examinationPaperBusiness.AddTestPaper(TestPaper);
                TestPaper.Id = testPaperid;
            }
           int id= _examinationPaperBusiness.AddPaperquestionType(new PaperQuestionType() { PaperQuestionTitle = PaperQuestionTitle, TestPaperId = TestPaper.Id , QuestionTypeId= QuestionTypesValue });
            PaperquestionTypeTabItems.Add(new PUTabItemModel() { Header = PaperQuestionTitle, Value = id,CanDelete=false, Content= null });
        }
        public void AddExaminationQuesionPaperTypes()
        {
            if (PaperquestionTypeSelectValue == 0)
            {
                PUMessageBox.ShowDialog("请选择大题");
                return;
            }
            if (SelectedItem == null) return;
            _examinationPaperBusiness.AddExaminationQuesionPaperTypes(new ExaminationQuestionPaperType() { ExaminationQuestionId= SelectedItem.ExaminationQuestionId, PaperQuestionTypeId= PaperquestionTypeSelectValue });
            UpdateQuestionTableList();
        }
        public void DeleteExaminationQuesionPaperTypes()
        {
            if (PaperquestionTypeSelectValue == 0)
            {
                PUMessageBox.ShowDialog("请选择大题");
                return;
            }
            if(QuestionTableSelected == null)
            {
                return;
            }
            _examinationPaperBusiness.DeleteExaminationQuesionPaperTypes(new ExaminationQuestionPaperType() { ExaminationQuestionId = QuestionTableSelected.ExaminationQuestionId, PaperQuestionTypeId = PaperquestionTypeSelectValue });
            UpdateQuestionTableList();
        }
        public void AddTestPaper()
        {
            if (_examinationPaperBusiness.AddTestPaper(TestPaper)!=1)
            {
                PUMessageBox.ShowDialog("添加成功");
                TestPaper = new TestPaper();
                QuestionTableList = new BindableCollection<ExaminationQuestionModel>();
            } else
            {
                PUMessageBox.ShowDialog("添加失败");
            }
                ;
        
        }
        public void Search()
        {
            Init();
           
        }
        #endregion
    }


}
