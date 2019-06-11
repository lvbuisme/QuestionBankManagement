using Business;
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

namespace AppManager.ViewModels.QuestionBank
{

    public class QuestionTypeManagerViewModel : Screen, IShell
    {
     
        #region Identity
        private IWindowManager _windowManager;
        private PUListBox _listBox;
        private List<QuestionType> _list;
        private QuetionBankBusiness _quetionBankBusiness = new QuetionBankBusiness();
        private ObservableCollection<PUListBoxItemModel> _listBoxItems = new ObservableCollection<PUListBoxItemModel>();
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
   
        public int SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue);
                if (SelectedValue != 0)
                {
                    var model = _list.FirstOrDefault(c => c.Id == SelectedValue);
                    if (model != null)
                    {
                        QuestionTypeName= model.QuestionTypeName;
                        HasOption= model.HasOption;
                    }
                 
                }
               
            }
        }
        private int _selectedValue;

        private string _questionTypeName;
        public string  QuestionTypeName 
        {
            get
            {
                return _questionTypeName;
            }
            set
            {
                _questionTypeName = value;
                NotifyOfPropertyChange(() =>  QuestionTypeName );
            }

        }
        public bool _hasOption;
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
     
        #endregion
        #region Constructor
        public QuestionTypeManagerViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Init();
        }
        #endregion
        #region Function 
        public void Search()
        {
            if (_listBox == null)
                throw new Exception("未知异常：ListBox为Null。");

            SearchText = SearchText ?? "";

            _listBox.SearchItemByContent(SearchText, true);
        }
        public void ListBoxLoaded(object sender)
        {
            _listBox = sender as PUListBox;
        }
        private void Init()
        {

            ObservableCollection<PUListBoxItemModel> listBoxItemModel = new ObservableCollection<PUListBoxItemModel>();
            _list = _quetionBankBusiness.GetAllQuestionType();
            foreach (var li in _list)
            {
                listBoxItemModel.Add(new PUListBoxItemModel() { Header = li.QuestionTypeName, Value = li.Id });
            }
            ListBoxItems = listBoxItemModel;

        }
        public void AddQuestionType()
        {
            if (HasOption)
            {

            }
            bool bl=_quetionBankBusiness.AddQuestionType(new QuestionType()
            {
                HasOption = HasOption,
                QuestionTypeName = QuestionTypeName,
                IsFixedScore = false,
                Score = 0,
            });
            if (bl)
            {
                PUMessageBox.ShowDialog("添加成功");
                QuestionTypeName = "";
            }
            else
            {
                PUMessageBox.ShowDialog("添加失败");
            }
            Init();
        }
        public void DeleteQuestionType()
        {
    
            if(PUMessageBox.ShowConfirm("此操作会将所关联的题目删除,是否确定执行", "提示", Buttons.OKOrCancel, true, AnimationStyles.Gradual) != true)
            {
                return;
            }
            _quetionBankBusiness.DeleteQuestionType(SelectedValue);
            QuestionTypeName = "";
            PUMessageBox.ShowDialog("删除成功");
            Init();
        }
        public void SaveQuestionType()
        {

            _quetionBankBusiness.SaveQuestionType(new QuestionType() { Id=SelectedValue, QuestionTypeName= QuestionTypeName });
            QuestionTypeName = "";
            PUMessageBox.ShowDialog("保存成功");
            Init();
        }
        #endregion
    }

}
