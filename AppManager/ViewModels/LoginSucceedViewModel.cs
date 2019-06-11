using Business;
using Caliburn.Micro;
using Panuon.UI;
using QuestionBankManagement.Models;
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

    public class LoginSucceedViewModel : Screen, IShell
    {
        private string _userInfoStr;
        public string UserInfoStr
        {
            get
            {
                return _userInfoStr;
            }
            set
            {
                _userInfoStr = value;
                NotifyOfPropertyChange(() => UserInfoStr);
            }
        }
        public LoginSucceedViewModel(User user)
        {
            UserInfoStr = "欢迎您!" + user.UserName;
        }
       
    }
}
