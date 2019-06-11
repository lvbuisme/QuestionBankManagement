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

namespace AppManager.ViewModels.UserUnit
{
    public class UserManagerViewModel : Screen, IShell
    {
        #region Identity
        private IWindowManager _windowManager;
        private PUListBox _listBox;
        private UserBusiness _userBusiness = new UserBusiness();
        private User _user = new User();
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }
        public Brush SelectedBrush
        {
            get { return _selectedBrush; }
            set { _selectedBrush = value; NotifyOfPropertyChange(() => SelectedBrush); }
        }
        private Brush _selectedBrush;

        public Brush SearchBrush
        {
            get { return _searchBrush; }
            set { _searchBrush = value; NotifyOfPropertyChange(() => SearchBrush); }
        }
        private Brush _searchBrush;

        public Brush CoverBrush
        {
            get { return _coverBrush; }
            set { _coverBrush = value; NotifyOfPropertyChange(() => CoverBrush); }
        }
        public int SelectedValue
        {
            get { return _selectedValue; }
            set {
                _selectedValue = value;
                ChangeUserInfo();
                NotifyOfPropertyChange(() => SelectedValue); }
        }
        private int _selectedValue;
        private Brush _coverBrush;
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
        public void Search()
        {
            if (_listBox == null)
                throw new Exception("未知异常：ListBox为Null。");

            SearchText = SearchText ?? "";

            _listBox.SearchItemByContent(SearchText, true);
        }
        #endregion
        #region Constructor
        public UserManagerViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            SelectedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6E6E6"));
            CoverBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#103056"));
            SearchBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#44444444"));
            Init();
        }
        #endregion
        #region Function
        public void ListBoxLoaded(object sender)
        {
            _listBox = sender as PUListBox;
        }
        private void Init()
        {

            ObservableCollection<PUListBoxItemModel> listBoxItemModel = new ObservableCollection<PUListBoxItemModel>();
            var userList = _userBusiness.GetUserList();
            foreach (var user in userList)
            {
                listBoxItemModel.Add(new PUListBoxItemModel() { Header = user.UserName, Value = user.Id });
            }
            ListBoxItems = listBoxItemModel;

        }
        public void SaveUser()
        {

            if (User == null)
            {
                return;
            }
            User.Id = SelectedValue;
            if(_userBusiness.UpdateUser(User))
            {
                PUMessageBox.ShowDialog("保存成功");
                User = new User();
            }
            else
            {
                PUMessageBox.ShowDialog("保存失败");
            }
            Init();
        }
        private void ChangeUserInfo()
        {
            int userId = Convert.ToInt32(SelectedValue);
            User = _userBusiness.GetUserById(userId);

        }
        public void DeleteUser()
        {
            int userId = Convert.ToInt32(SelectedValue);
            if(_userBusiness.DeleteUser(userId))
            {
                PUMessageBox.ShowDialog("删除成功");
                User = new User();
            }
            else
            {
                PUMessageBox.ShowDialog("删除失败");
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
        #endregion





    }
}
