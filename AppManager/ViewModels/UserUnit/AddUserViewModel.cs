using Business;
using Caliburn.Micro;
using Panuon.UI;
using Panuon.UI.Utils;
using QuestionBankManagement.Models;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;

namespace AppManager.ViewModels.UserUnit
{

    public class AddUserViewModel : Screen, IShell
    {
        #region Identity
        private string _verifyPassword;
        public string VerifyPassword
        {
            get
            {
                return _verifyPassword;
            }
            set
            {
                _verifyPassword = value;
                NotifyOfPropertyChange(() => VerifyPassword);
            }
        }
        private User _user = new User();
        private IWindowManager _windowManager;
        #endregion
        #region Constructor
        public AddUserViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        #endregion
      
        public QuestionBankManagement.Models.User User{
            get { return _user; }
            set {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }
        #region Function
        public void AddUser()
        {
            if(User.Password != VerifyPassword)
            {
                PUMessageBox.ShowDialog("两次密码不一致");
                return;
            }

            if (string.IsNullOrEmpty(User.Account)|| string.IsNullOrEmpty(User.UserName) || string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.MobilePhone))
            {
                PUMessageBox.ShowDialog("内容不能为空");
                return;
            }
            UserBusiness userBusiness = new UserBusiness();
            if (userBusiness.VerifyAccountExists(User.Account))
            {
                PUMessageBox.ShowDialog("账号已存在");
            }
            if (userBusiness.AddUser(User))
            {
                PUMessageBox.ShowDialog("添加用户成功");
                User = new User();
            }
            else
            {
                PUMessageBox.ShowDialog("添加用户失败");
            }
          
        }
        public void SetMaskCover(bool toOpen)
        {
            var parent = Parent as ShellWindowViewModel;
            if (toOpen)
                parent.ShowCoverMask();
            else
                parent.CloseCoverMask();
        }

        public void SetAwait(bool toOpen)
        {
            var parent = Parent as ShellWindowViewModel;
            if (toOpen)
                parent.ShowAwait();
            else
                parent.CloseAwait();
        }
        public void ChangeSex(PURadioButton pURadioButton)
        {
            if (pURadioButton.Content.Equals("男"))
            {
                User.Sex = 1;
            }else if (pURadioButton.Content.Equals("女"))
            {
                User.Sex = 2;
            }
            else
            {
                User.Sex = 0;
            }
        }
        #endregion

    }

}
