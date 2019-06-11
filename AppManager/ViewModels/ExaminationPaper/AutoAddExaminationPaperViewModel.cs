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

    public class AutoAddExaminationPaperViewModel : Screen, IShell
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
                NotifyOfPropertyChange(() => TestPaper);
            }
        }

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
            }

        }
        #endregion
        #region Constructor
        public AutoAddExaminationPaperViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            SubjectItems.AddRange(_quetionBankBusiness.GetAllSubject().Select((subject) => new PUComboBoxItemModel { Value = subject.Id, Header = subject.SubjectName }).ToList());
            SubjectValue = Convert.ToInt32(SubjectItems.FirstOrDefault().Value);

        }

        public AddQuestionBackToPaper AddQuestionBackToPaper
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
        public double _progressPercent = 0.5;
        public double ProgressPercent
        {
            get
            {
                return _progressPercent;
            }
            set
            {
                if (value > 1 || value <= 0.1)
                {
                    return;
                }
                _progressPercent = value;
                NotifyOfPropertyChange(() => ProgressPercent);
            }
        }
        #endregion
        #region Function 

        public void AutoAddTestPaper()
        {
            if (string.IsNullOrEmpty(TestPaper.Subtitle) || string.IsNullOrEmpty(TestPaper.Title))
            {
                PUMessageBox.ShowDialog("内容不能为空");
                return;
            }
           
            PUMessageBox.ShowDialog(_examinationPaperBusiness.AutoAddTestPaper(TestPaper, ProgressPercent));
            TestPaper = new TestPaper();
        }
        public void AutoAddTemplateTestPaper()
        {
            if (string.IsNullOrEmpty(TestPaper.Subtitle) || string.IsNullOrEmpty(TestPaper.Title))
            {
                PUMessageBox.ShowDialog("内容不能为空");
                return;
            }
         
            PUMessageBox.ShowDialog(_examinationPaperBusiness.AutoAddTemplateTestPaper(TestPaper, ProgressPercent));
            TestPaper = new TestPaper();
        }
        public void ReducePercent()
        {
            if( ProgressPercent==0 )return;
            ProgressPercent = ProgressPercent - 0.1;
        }
        public void AddPercent()
        {
            if (ProgressPercent == 1) return;
            ProgressPercent = ProgressPercent + 0.1;

        }
        #endregion
    }


}
