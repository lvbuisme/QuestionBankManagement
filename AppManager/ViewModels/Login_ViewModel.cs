using Business;
using Caliburn.Micro;
using Panuon.UI;
using Panuon.UI.Utils;
using QuestionBankManagement.Models;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;

namespace AppManager.ViewModels
{

    public class Login_ViewModel : Screen, IShell
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
        private User _user;
        private IWindowManager _windowManager;
        #endregion
        #region Constructor
        public Login_ViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        #endregion
        public bool IsLogin=false;
        public QuestionBankManagement.Models.User User{
            get { return _user; }
            set {
                _user = value;
                NotifyOfPropertyChange(() => User);
            }
        }
        private string _userName="lvbu";
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
        private string _password="123456";
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        #region Function
        public void Login()
        {

            UserBusiness userBusiness = new UserBusiness();
            //ShellWindowViewModel shellWindowViewModel = GetView();
            //_windowManager.ShowWindow(shellWindowViewModel);

            if(_userName==null || _password == null)
            {
                PUMessageBox.ShowDialog("请输入正确的账号密码");
            }
            User = userBusiness.login(_userName, _password.ToString());
            if (User == null)
            {
                PUMessageBox.ShowDialog("密码或账号错误");
            }
            else
            {
                IsLogin = true;

                var parent= Parent as ShellWindowViewModel;
                parent.CurrentUser = User;
                parent.ChoosedValue = "LoginSucceed";
             
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
    
        #endregion

    }

}
