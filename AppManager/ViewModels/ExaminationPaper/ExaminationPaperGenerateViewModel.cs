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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AppManager.ViewModels.ExaminationPaper
{

    public class ExaminationPaperGenerateViewModel : Screen, IShell
    {
        #region Identity
        private IWindowManager _windowManager;
        private ExaminationPaperBusiness _examinationPaperBusiness = new ExaminationPaperBusiness();
        private TestPaper _selectTestPaper;
        public TestPaper SelectTestPaper
        {
            get
            {
                return _selectTestPaper;
            }
            set
            {
                _selectTestPaper = value;
                NotifyOfPropertyChange(() => SelectTestPaper);
            }
        }

       
        public ObservableCollection<TestPaperModel> TesPaperModelList
        {
            get { return _tesPaperModelList; }
            set { _tesPaperModelList = value; NotifyOfPropertyChange(() => TesPaperModelList); }
        }
        private ObservableCollection<TestPaperModel> _tesPaperModelList = new ObservableCollection<TestPaperModel>();

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);
            }
        }
        public string _searchText;
        public TestPaperModel _selectedItem;
        public TestPaperModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
                if (SelectedItem.TestPaperId != 0)
                {
                    SelectTestPaper = _examinationPaperBusiness.GetTestPaperById(SelectedItem.TestPaperId);
                }
            }
        }
        #endregion
        #region Constructor
        public ExaminationPaperGenerateViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            GenerateTestPaperWord = new GenerateTestPaperWord(this);
            LookTestPaperWord = new LookTestPaperWord(this);
            PrintTestPaperWord = new PrintTestPaperWord(this);
            Init();
        }
        public GenerateTestPaperWord GenerateTestPaperWord
        {
            get;set;
        }
        public LookTestPaperWord LookTestPaperWord
        {
            get;set;
        }
        public PrintTestPaperWord PrintTestPaperWord
        {
            get;set;
        }
        #endregion
        #region Function 

        private void Init()
        {
            var testPaperList = _examinationPaperBusiness.GetAllTestPaper(SearchText);
            ObservableCollection<TestPaperModel> list = new ObservableCollection<TestPaperModel>();
            if (testPaperList == null) return;
            foreach(var testPaper in testPaperList)
            {
                string content = string.Empty;
                if (testPaper.PaperQuestionTypes != null)
                {
                    bool isFirst = true;
                    foreach (var raperQuestionType in testPaper.PaperQuestionTypes)
                    {
                        if (!isFirst)
                        {
                            content += ",";
                        }
                        content += raperQuestionType.PaperQuestionTitle;
                        isFirst = false;
                    }
                }
                list.Add(new TestPaperModel
                {
                    Subtitle = testPaper.Subtitle,
                    Title = testPaper.Title,
                    SubjectName = testPaper.Subject.SubjectName,
                    TestPaperId = testPaper.Id,
                    Content = content
                });
            }
            TesPaperModelList = list;
        }
       
        public void Search()
        {
            Init();
        }
        public void AwaitTime(int millisecond)
        {
            SetAwait(true);
            Task.Run(() =>
            {
                Thread.Sleep(millisecond);
                App.Current.Dispatcher.Invoke(() =>
                {
                    SetAwait(false);
                });
            });
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
        #endregion
    }


}
