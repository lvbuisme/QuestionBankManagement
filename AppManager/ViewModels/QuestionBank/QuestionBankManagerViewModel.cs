using Business;
using Business.DataModels;
using Caliburn.Micro;
using Panuon.UI;
using QuestionBankManagement.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppManager.ViewModels.QuestionBank
{
    public class QuestionBankManagerViewModel : Screen, IShell
    {
        #region Identity
        private IWindowManager _windowManager;
        private QuetionBankBusiness _quetionBankBusiness = new QuetionBankBusiness();
        public int SelectedValue
        {
            get { return _selectedValue; }
            set {
                _selectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue); }
        }
        private int _selectedValue;
        public ObservableCollection<ExaminationQuestionModel> ExaminationQuestionList
        {
            get { return _examinationQuestionList; }
            set { _examinationQuestionList = value; NotifyOfPropertyChange(() => ExaminationQuestionList); }
        }
        private ObservableCollection<ExaminationQuestionModel> _examinationQuestionList = new ObservableCollection<ExaminationQuestionModel>();

        public BindableCollection<PUComboBoxItemModel> SubjectItems
        {
            get { return _subjectItems; }
            set { _subjectItems = value; NotifyOfPropertyChange(() => SubjectItems); }
        }
        private BindableCollection<PUComboBoxItemModel> _subjectItems = new BindableCollection<PUComboBoxItemModel>();

        public BindableCollection<PUComboBoxItemModel> QuestionTypesItems
        {
            get { return _questionTypesItems; }
            set { _questionTypesItems = value; NotifyOfPropertyChange(() => QuestionTypesItems); }
        }
        private BindableCollection<PUComboBoxItemModel> _questionTypesItems = new BindableCollection<PUComboBoxItemModel>();

        public BindableCollection<PUComboBoxItemModel> KnowledgePointItems
        {
            get { return _knowledgePointItems; }
            set { _knowledgePointItems = value; NotifyOfPropertyChange(() => KnowledgePointItems); }
        }
        private BindableCollection<PUComboBoxItemModel> _knowledgePointItems = new BindableCollection<PUComboBoxItemModel>();

       
        public ExaminationQuestion ExaminationQuestion
        {
            get
            {
                return _examinationQuestion;
            }
            set
            {
                _examinationQuestion = value;
                NotifyOfPropertyChange(() => ExaminationQuestion);
            }
        }
        private ExaminationQuestion _examinationQuestion;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyOfPropertyChange(() => SearchText); }
        }
        private string _searchText;

        private ExaminationQuestionModel _selectedItem;
        public ExaminationQuestionModel SelectedItem
        {
            get {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                UpdateExaminationQuestion();
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }
        public int _seachSubjectItem;
        public int SeachSubjectItem {
            get {
                return _seachSubjectItem;
            }
            set
            {
                _seachSubjectItem = value;
                NotifyOfPropertyChange(() => SeachSubjectItem);
            }
        }
        public int _seachQuestionTypeItem;
        public int SeachQuestionTypeItem
        {
            get
            {
                return _seachQuestionTypeItem;
            }
            set
            {
                _seachQuestionTypeItem = value;
                NotifyOfPropertyChange(() => SeachQuestionTypeItem);
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
        public QuestionBankManagerViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            SubjectItems.AddRange(_quetionBankBusiness.GetAllSubject().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.SubjectName }).ToList());
            QuestionTypesItems.AddRange(_quetionBankBusiness.GetAllQuestionType().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.QuestionTypeName }).ToList());
            KnowledgePointItems.AddRange(_quetionBankBusiness.GetAllKnowledgePoint().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.KnowledgePointName }).ToList());
            TotalPage = _quetionBankBusiness.GetExaminationQuestionTotalPage(SearchText, SeachQuestionTypeItem, SeachSubjectItem);
            CurrentPage = 1;
        }
        #endregion
        #region Function
        public void Search()
        {
            Init();
        }
        private void Init()
        {
            List<ExaminationQuestion> examinationQuestions = _quetionBankBusiness.GetExaminationQuestionList(SearchText, SeachQuestionTypeItem, SeachSubjectItem,CurrentPage);

            ObservableCollection<ExaminationQuestionModel> eqnlist = new ObservableCollection<ExaminationQuestionModel>();
            foreach (var eq in examinationQuestions)
            {
                eqnlist.Add(new ExaminationQuestionModel
                {
                    Content = eq.Content.Length > 50 ? eq.Content.Substring(0,10):eq.Content,
                    KnowledgePointName = eq.KnowledgePoint.KnowledgePointName,
                    QuestionTypeName = eq.QuestionType.QuestionTypeName,
                    Score = eq.Score,
                    SubjectName = eq.Subject.SubjectName,
                    ExaminationQuestionId = eq.Id
                });
            }
            ExaminationQuestionList = eqnlist;
            TotalPage = _quetionBankBusiness.GetExaminationQuestionTotalPage(SearchText, SeachQuestionTypeItem, SeachSubjectItem);
        }
        public void DeleteExaminationQuestion()
        {
            if (_quetionBankBusiness.DeleteExaminationQuestion(ExaminationQuestion.Id))
            {
                PUMessageBox.ShowDialog("保存成功");
                ExaminationQuestion = new ExaminationQuestion();
            }
            else
            {
                PUMessageBox.ShowDialog("保存失败");
            }
            ExaminationQuestion = new ExaminationQuestion();
            Init();
        }
        public void SaveExaminationQuestion()
        {
            if (_quetionBankBusiness.SaveExaminationQuestion(ExaminationQuestion))
            {
                PUMessageBox.ShowDialog("保存成功");
                ExaminationQuestion = new ExaminationQuestion();
            }
            else {
                PUMessageBox.ShowDialog("保存失败");
            }       
            Init();
        }
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
        public void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        public void UpdateExaminationQuestion()
        {
            if (SelectedItem == null) return;
            ExaminationQuestion = _quetionBankBusiness.GetExaminationQuestionById(SelectedItem.ExaminationQuestionId);
        }

        #endregion

    }
}
