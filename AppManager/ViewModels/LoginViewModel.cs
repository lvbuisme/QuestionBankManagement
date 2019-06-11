using Business;
using Caliburn.Micro;
using Panuon.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppManager.ViewModels
{

    public class LoginViewModel : Screen, IShell
    {
        private string _userName;
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
        private string _password;
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
  
        private IWindowManager _windowManager;
      
        public LoginViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
       
        public void Login()
        {
            
            UserBusiness userBusiness = new UserBusiness();
            //ShellWindowViewModel shellWindowViewModel = GetView();
            //_windowManager.ShowWindow(shellWindowViewModel);
            
        
            //var user = userBusiness.login(_userName, _password.ToString());
            //if (user == null)
            //{
            //    PUMessageBox.ShowDialog("密码或账号错误");
            //}
            //else
            //{
            //    _windowManager.ShowWindow(new ShellWindowViewModel(_windowManager, user));

            //}



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
       
    }
}
