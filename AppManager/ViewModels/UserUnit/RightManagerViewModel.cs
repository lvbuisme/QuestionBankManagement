using Business;
using Business.DataModels;
using Caliburn.Micro;
using Panuon.UI;
using QuestionBankManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppManager.ViewModels.UserUnit
{
    public class RightManagerViewModel : Screen, IShell
    {
        private IWindowManager _windowManager;
        private UserBusiness _userBusiness = new UserBusiness(); 
        public RightManagerViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Init();
        }
        public int _selectedValue;
        public int SelectedValue
        {
            get
            {
              return  _selectedValue;
            }
            set
            {
                _selectedValue = value;
                NotifyOfPropertyChange(() => SelectedValue);
                Init();
            }

        }
        public bool _userMamagerRight;
       public bool UserMamagerRight
        {
            get
            {
                return _userMamagerRight;
            }
            set
            {
                _userMamagerRight = value;
                NotifyOfPropertyChange(() => UserMamagerRight);
                _userBusiness.UpdateRoleRight(UserMamagerRight, "UserManager", SelectedValue);
            }
        }
        public bool _questionBankMamagerRight;
        public bool QuestionBankMamagerRight
        {
            get
            {
                return _questionBankMamagerRight;
            }
            set
            {
                _questionBankMamagerRight = value;
                NotifyOfPropertyChange(() => QuestionBankMamagerRight);
                _userBusiness.UpdateRoleRight(QuestionBankMamagerRight, "QuestionBankManager", SelectedValue);
            }
        }
        public bool _examinationPaperMamagerRight;
        public bool ExaminationPaperMamagerRight
        {
            get
            {
                return _examinationPaperMamagerRight;
            }
            set
            {
                _examinationPaperMamagerRight = value;
                NotifyOfPropertyChange(() => ExaminationPaperMamagerRight);
                _userBusiness.UpdateRoleRight(ExaminationPaperMamagerRight,  "ExaminationPaperManager",SelectedValue);
            }
        }
        public void Init()
        {
            List<RoleRight> roleRights = _userBusiness.GetRoleRightByRoleId(SelectedValue);
            if (roleRights.FirstOrDefault(c => c.RightName.Equals("UserManager"))==null)
            {
                UserMamagerRight = false;
            }
            else
            {
                UserMamagerRight = true;
            }
            if (roleRights.FirstOrDefault(c => c.RightName.Equals("QuestionBankManager")) == null)
            {
                QuestionBankMamagerRight = false;
            }
            else
            {
                QuestionBankMamagerRight = true;
            }
            if (roleRights.FirstOrDefault(c => c.RightName.Equals("ExaminationPaperManager")) == null)
            {
                ExaminationPaperMamagerRight = false;
            }
            else
            {
                ExaminationPaperMamagerRight = true;

            }
        }
    }
}
