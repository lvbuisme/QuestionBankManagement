
using AppManager.ViewModels.ExaminationPaper;
using AppManager.ViewModels.QuestionBank;
using AppManager.ViewModels.UserUnit;
using Business;
using Caliburn.Micro;
using Panuon.UI;
using Panuon.UI.Utils;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace AppManager.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellWindowViewModel : Conductor<IShell>.Collection.OneActive, IShell
    {
        #region Identity
        private UserBusiness _userBusiness = new UserBusiness();
        private PUWindow CurrentWindow
        {
            get
            {
                if (_currentWindow == null)
                    _currentWindow = (GetView()) as PUWindow;
                return _currentWindow;
            }
        }
        public List<RoleRight> _roleRights = new List<RoleRight>();
        private PUWindow _currentWindow;
        private User _user = new User();
        public User CurrentUser
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => CurrentUser);
                _roleRights = _userBusiness.GetRoleRightByRoleId(CurrentUser.RoleId);
            }
        }
  
        private IWindowManager _windowManager;

        private Login_ViewModel _login_ViewModel;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public ShellWindowViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _login_ViewModel=new Login_ViewModel(_windowManager);
            ChoosedValue = "Login";
        }
        #endregion
        #region Bindings
        public string ChoosedValue
        {
            get { return _choosedValue; }
            set
            {
                _choosedValue = value;
                UpdateActivePage();
                NotifyOfPropertyChange(() => ChoosedValue);
            }
        }
        private string  _choosedValue;
        #endregion

        #region Event
        public void UpdateActivePage()
        {
            if (ChoosedValue!="Login" && !_login_ViewModel.IsLogin) return;
            switch (ChoosedValue)
            {
                case "Login":
                   ActivateItem(_login_ViewModel);
                    break;
                case "LoginSucceed":
                    ActivateItem(new LoginSucceedViewModel(CurrentUser));
                    break;
                case "AddUser":
                    if (_roleRights.FirstOrDefault(c => c.RightName.Equals("UserManager")) == null)
                    {
                        PUMessageBox.ShowDialog("你没有访问权限，请联系管理员");
                        return;
                    }
                    ActivateItem(new AddUserViewModel(_windowManager));
                    break;
                case "UserManager":
                    if (_roleRights.FirstOrDefault(c => c.RightName.Equals("UserManager")) == null)
                    {
                        PUMessageBox.ShowDialog("你没有访问权限，请联系管理员");
                        return;
                    }
                    ActivateItem(new UserManagerViewModel(_windowManager));
                    break;
                case "AddQuestions":
                    ActivateItem(new AddQuestionBankViewModel(_windowManager));
                    break;
                case "QuestionsManager":
                    if (_roleRights.FirstOrDefault(c => c.RightName.Equals("QuestionBankManager")) == null){
                        return;
                    }
                    ActivateItem(new QuestionBankManagerViewModel(_windowManager));
                    break;
                case "ExaminationPaperManager":
                    if (_roleRights.FirstOrDefault(c => c.RightName.Equals("ExaminationPaperManager")) == null)
                    {
                        PUMessageBox.ShowDialog("你没有访问权限，请联系管理员");
                        return;
                    }
                    ActivateItem(new ExaminationPaperManagerViewModel(_windowManager));
                    break;
                case "KnowledgePoint":
                    ActivateItem(new KnowledgePointManagerViewModel(_windowManager));
                    break;
                case "QuestionTypes":
                    ActivateItem(new QuestionTypeManagerViewModel(_windowManager));
                    break;
                case "Subject":
                    ActivateItem(new SubjectManagerViewModel(_windowManager));
                    break;
                case "AddExaminationPaper":
                    ActivateItem(new AddExaminationPaperViewModel(_windowManager));
                    break;
                case "AutogenerationExaminationPaper":

                    ActivateItem(new AutoAddExaminationPaperViewModel(_windowManager));
                    break;
                case "ExportExaminationPaper":

                    ActivateItem(new ExaminationPaperGenerateViewModel(_windowManager));
                    break;
                case "AuthorityManagement":
                    if (_roleRights.FirstOrDefault(c => c.RightName.Equals("UserManager")) == null && CurrentUser.Id != 1)
                    {
                        PUMessageBox.ShowDialog("你没有访问权限，请联系管理员");
                        return;
                    }
                    ActivateItem(new RightManagerViewModel(_windowManager));
                    break;
                case "PersonalSettings":
                    ActivateItem(new UpdateUserViewModel(_windowManager,CurrentUser));
                    break;
                case "Logout":
                    if (PUMessageBox.ShowConfirm("是否注销", "提示", Buttons.OKOrCancel, true, AnimationStyles.Gradual) != true)
                    {
                        return;
                    }
                    CurrentUser = new User();
                    _login_ViewModel = new Login_ViewModel(_windowManager);
                    ActivateItem(_login_ViewModel);
                    break;
            }
        }
        #endregion

        #region Function
        #endregion

        #region APIs
        public Window GetCurrentWindow()
        {
            return GetView() as Window;
        }

        /// <summary>
        /// 打开等待控件。
        /// </summary>
        public void ShowAwait()
        {
            CurrentWindow.IsAwaitShow = true;
        }

        /// <summary>
        /// 关闭等待控件。
        /// </summary>
        public void CloseAwait()
        {
            CurrentWindow.IsAwaitShow = false;
        }

        /// <summary>
        /// 打开遮罩层。
        /// </summary>
        public void ShowCoverMask()
        {
            CurrentWindow.IsCoverMaskShow = true;
        }

        /// <summary>
        /// 关闭遮罩层。
        /// </summary>
        public void CloseCoverMask()
        {
            CurrentWindow.IsCoverMaskShow = false;
        }
        public void LoginChange()
        {

        }

        #endregion
    }

}
