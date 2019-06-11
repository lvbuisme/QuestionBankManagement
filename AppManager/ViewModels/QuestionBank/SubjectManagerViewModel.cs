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

    public class SubjectManagerViewModel : Screen, IShell
    {
     
        #region Identity
        private IWindowManager _windowManager;
        private PUListBox _listBox;
        private List<Subject> _list;
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
                    SubjectName = _list.FirstOrDefault(c => c.Id == SelectedValue)?.SubjectName;
                }
               
            }
        }
        private int _selectedValue;

        private string _subjectName;
        public string SubjectName
        {
            get
            {
                return _subjectName;
            }
            set
            {
                _subjectName = value;
                NotifyOfPropertyChange(() => SubjectName);
            }

        }
        #endregion
        #region Constructor
        public SubjectManagerViewModel(IWindowManager windowManager)
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
            _list = _quetionBankBusiness.GetAllSubject();
            foreach (var li in _list)
            {
                listBoxItemModel.Add(new PUListBoxItemModel() { Header = li.SubjectName, Value = li.Id });
            }
            ListBoxItems = listBoxItemModel;

        }
        public void AddSubject()
        {
            _quetionBankBusiness.AddSubject(SubjectName);
            SubjectName = "";
            PUMessageBox.ShowDialog("添加成功");
            Init();
        }
        public void DeleteSubject()
        {
            if (PUMessageBox.ShowConfirm("此操作会将所关联的题目删除,是否确定执行", "提示", Buttons.OKOrCancel, true, AnimationStyles.Gradual) != true)
            {
                return;
            }
            _quetionBankBusiness.DeleteSubject(SelectedValue);
            SubjectName = "";
            PUMessageBox.ShowDialog("删除成功");
            Init();
        }
        public void SaveSubject()
        {
            _quetionBankBusiness.SaveSubject(new Subject() { Id=SelectedValue, SubjectName=SubjectName});
            SubjectName = "";
            PUMessageBox.ShowDialog("保存成功");
            Init();
        }
        #endregion
    }

}
