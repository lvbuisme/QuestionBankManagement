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

    public class KnowledgePointManagerViewModel : Screen, IShell
    {
     
        #region Identity
        private IWindowManager _windowManager;
        private PUListBox _listBox;
        private List<KnowledgePoint> _list;
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
                    KnowledgePointName = _list.FirstOrDefault(c => c.Id == SelectedValue)?.KnowledgePointName;
                }
               
            }
        }
        private int _selectedValue;

        private string _knowledgePointName;
        public string KnowledgePointName
        {
            get
            {
                return _knowledgePointName;
            }
            set
            {
                _knowledgePointName = value;
                NotifyOfPropertyChange(() => KnowledgePointName);
            }

        }
        #endregion
        #region Constructor
        public KnowledgePointManagerViewModel(IWindowManager windowManager)
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
            _list = _quetionBankBusiness.GetAllKnowledgePoint();
            foreach (var li in _list)
            {
                listBoxItemModel.Add(new PUListBoxItemModel() { Header = li.KnowledgePointName, Value = li.Id });
            }
            ListBoxItems = listBoxItemModel;

        }
        public void AddKnowledgePoint()
        {
            _quetionBankBusiness.AddKnowledgePoint(KnowledgePointName);
            KnowledgePointName = "";
            PUMessageBox.ShowDialog("添加成功");
            Init();
        }
        public void DeleteKnowledgePoint()
        {
            if (PUMessageBox.ShowConfirm("此操作会将所关联的题目删除,是否确定执行", "提示", Buttons.OKOrCancel, true, AnimationStyles.Gradual) != true)
            {
                return;
            }
            _quetionBankBusiness.DeleteKnowledgePoint(SelectedValue);
            KnowledgePointName = "";
            PUMessageBox.ShowDialog("删除成功");
            Init();
        }
        public void SaveKnowledgePoint()
        {
            _quetionBankBusiness.SaveKnowledgePoint(new KnowledgePoint() { Id=SelectedValue, KnowledgePointName=KnowledgePointName});
            KnowledgePointName = "";
            PUMessageBox.ShowDialog("保存成功");
            Init();
        }
        #endregion
    }

}
