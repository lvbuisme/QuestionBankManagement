using AppManager.Command;
using Business;
using Business.DataModels;
using Caliburn.Micro;
using Panuon.UI;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppManager.ViewModels.ExaminationPaper
{
    public class ExaminationPaperManagerViewModel : Screen, IShell
    {
        #region Identity
        private IWindowManager _windowManager;
        private PUListBox _listBox;
        private ExaminationPaperBusiness _examinationPaperBusiness = new ExaminationPaperBusiness();
        private QuetionBankBusiness _quetionBankBusiness = new QuetionBankBusiness();

        public int SelectedValue
        {
            get { return _selectedValue; }
            set {
                _selectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue);
                ChangeTestPaper();
            }
        }
        private int _selectedValue;
  
        private ObservableCollection<PUListBoxItemModel> _listBoxItems= new ObservableCollection<PUListBoxItemModel>();
        public ObservableCollection<PUListBoxItemModel> ListBoxItems
        {
            get { return _listBoxItems; }
            set { _listBoxItems = value; NotifyOfPropertyChange(() => ListBoxItems); }
        }
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyOfPropertyChange(() => SearchText); }
        }
        private string _searchText;

        private TestPaper _testPaper = new TestPaper();
        public TestPaper TestPaper
        {
            get
            {
                return _testPaper;
            }
            set
            {
                _testPaper = value;
                NotifyOfPropertyChange(() => TestPaper);
            }
        }

        public ObservableCollection<ExaminationQuestionModel> ExaminationQuestionList
        {
            get { return _examinationQuestionList; }
            set { _examinationQuestionList = value; NotifyOfPropertyChange(() => ExaminationQuestionList); }
        }
        private ObservableCollection<ExaminationQuestionModel> _examinationQuestionList = new ObservableCollection<ExaminationQuestionModel>();


     
        public BindableCollection<PUComboBoxItemModel> SubjectItems
        {
            get
            {
                return _subjectItems;
            }
            set
            {
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
            get
            {
                return _paperquestionTypeTabItems;
            }
            set
            {
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
                PaperQuestionTitle = QuestionTypesItems.FirstOrDefault(c=>c.Value.Equals(QuestionTypesValue))?.Header;
                InitExamination();
            }
        }
        public string SearchExaminationText
        {
            get
            {
                return _searchExaminationText;
            }
            set
            {
                _searchExaminationText = value;
                NotifyOfPropertyChange(() => SearchExaminationText);
                InitExamination();
            }
        }
        private string _searchExaminationText;
        public AddQuestionBackToManagerPaper AddQuestionBackToManagerPaper
        {
            get;
            set;
        }
        public DeleteQuestionBackToManagerPaper DeleteQuestionBackToManagerPaper
        {
            get;
            set;
        }
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
                InitExamination();
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
        public ExaminationPaperManagerViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            AddQuestionBackToManagerPaper = new AddQuestionBackToManagerPaper(this);
            DeleteQuestionBackToManagerPaper = new DeleteQuestionBackToManagerPaper(this);
            TotalPage = _quetionBankBusiness.GetExaminationQuestionTotalPage(SearchText, QuestionTypesValue, TestPaper?.SubjectId??0);
            CurrentPage = 1;
            Init();
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
        #endregion
        #region Function
        private void SetMaskCover(bool toOpen)
        {
            var parent = Parent as ShellWindowViewModel;
            if (toOpen)
                parent.ShowCoverMask();
            else
                parent.CloseCoverMask();
        }

        private void SetAwait(bool toOpen)
        {
            var parent = Parent as ShellWindowViewModel;
            if (toOpen)
                parent.ShowAwait();
            else
                parent.CloseAwait();
        }
        private void Init()
        {
            ObservableCollection<PUListBoxItemModel> listBoxItemModel = new ObservableCollection<PUListBoxItemModel>();
            var list = _examinationPaperBusiness.GetAllTestPaperTitle();
            foreach (var li in list)
            {
                listBoxItemModel.Add(new PUListBoxItemModel() { Header = li.Title, Value = li.Id });
            }
            ListBoxItems = listBoxItemModel;
        }
        private void InitExamination()
        {
            List<ExaminationQuestion> examinationQuestions = _quetionBankBusiness.GetExaminationQuestionList(SearchText, QuestionTypesValue, TestPaper?.SubjectId??0,CurrentPage);
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
            TotalPage = _quetionBankBusiness.GetExaminationQuestionTotalPage(SearchText, QuestionTypesValue, TestPaper?.SubjectId ?? 0);

        }
        public void Search()
        {
            if (_listBox == null)
                throw new Exception("未知异常：ListBox为Null。");

            SearchText = SearchText ?? "";

            _listBox.SearchItemByContent(SearchText, true);
        }
        public void SeachExaminationQuestion()
        {
            InitExamination();
          
        }
        public void ListBoxLoaded(object sender)
        {
            _listBox = sender as PUListBox;
        }
        private void UpdateQuestionTableList()
        {
            List<ExaminationQuestion> examinationQuestions = _quetionBankBusiness.GetExaminationQuestionsByPaperTypeId(PaperquestionTypeSelectValue);
            PaperQuestionType paperQuestionTyp = _quetionBankBusiness.GetPaperQuestionTypeById(PaperquestionTypeSelectValue);
            BindableCollection<ExaminationQuestionModel> eqnlist = new BindableCollection<ExaminationQuestionModel>();
            QuestionTypesItems = new BindableCollection<PUComboBoxItemModel>();
            QuestionTypesItems.AddRange(_quetionBankBusiness.GetAllQuestionType().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.QuestionTypeName }).ToList());
            foreach (var eq in examinationQuestions)
            {
                eqnlist.Add(new ExaminationQuestionModel
                {

                    SubjectName= paperQuestionTyp.PaperQuestionTitle,
                    Content = eq.Content.Length>10?eq.Content.Substring(0,10):eq.Content,
                    Score = eq.Score,
                    ExaminationQuestionId = eq.Id
                });
            }
            QuestionTableList = eqnlist;

        }
        public void ChangeTestPaper()
        {
            TestPaper = _examinationPaperBusiness.GetTestPapperById(SelectedValue);
            if (TestPaper == null) return;
            if (TestPaper.PaperQuestionTypes == null) return;
            BindableCollection<PUTabItemModel> list = new BindableCollection<PUTabItemModel>();
            foreach(var li in TestPaper.PaperQuestionTypes)
            {
                list.Add(new PUTabItemModel() { Header = li.PaperQuestionTitle, Value = li.Id, Content = null });
            }
            PaperquestionTypeTabItems = list;
        }
        public void AddExaminationQuesionPaperTypes()
        {
            if (PaperquestionTypeSelectValue == 0)
            {
                PUMessageBox.ShowDialog("请选择大题");
                return;
            }

            _examinationPaperBusiness.AddExaminationQuesionPaperTypes(new ExaminationQuestionPaperType() { ExaminationQuestionId = SelectedItem.ExaminationQuestionId, PaperQuestionTypeId = PaperquestionTypeSelectValue });
            UpdateQuestionTableList();
        }
        public void DeleteExaminationQuesionPaperTypes()
        {
            if (PaperquestionTypeSelectValue == 0)
            {
                PUMessageBox.ShowDialog("请选择大题");
                return;
            }
            if (QuestionTableSelected == null)
            {
                return;
            }
            _examinationPaperBusiness.DeleteExaminationQuesionPaperTypes(new ExaminationQuestionPaperType() { ExaminationQuestionId = QuestionTableSelected.ExaminationQuestionId, PaperQuestionTypeId = PaperquestionTypeSelectValue });
            UpdateQuestionTableList();
        }
        public void SaveTestPaper()
        {
            _examinationPaperBusiness.AddTestPaper(TestPaper);
            TestPaper = new TestPaper();
            Init();
        }
        public void DeleteTestPaper()
        {
            if (TestPaper == null)
            {
                PUMessageBox.ShowDialog("请选择试卷");
                return;
            }
            _examinationPaperBusiness.DeleteTestPaper(TestPaper.Id);
            TestPaper = new TestPaper();
            Init();
        }
        public void AddPaperquestionType()
        {
            if (TestPaper == null)
            {
                PUMessageBox.ShowDialog("请选择试卷");
                return;
            }
            if (QuestionTypesValue == 0)
            {
                PUMessageBox.ShowDialog("请选择题型");
                return;
            }
            if (TestPaper != null && TestPaper.Id == 0)
            {
                int testPaperid = _examinationPaperBusiness.AddTestPaper(TestPaper);
                TestPaper.Id = testPaperid;
            }
            int id = _examinationPaperBusiness.AddPaperquestionType(new PaperQuestionType() { PaperQuestionTitle = PaperQuestionTitle, TestPaperId = TestPaper.Id, QuestionTypeId = QuestionTypesValue });
            ChangeTestPaper();
           // PaperquestionTypeTabItems.Add(new PUTabItemModel() { Header = PaperQuestionTitle, Value = id, CanDelete = false, Content = null });
        }
        #endregion
    }
}
