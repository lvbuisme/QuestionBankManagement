using AppManager.Common;
using AppManager.ViewModels.ExaminationPaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppManager.Command
{
    public class LookTestPaperWord : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ExaminationPaperGenerateViewModel _examinationPaperGenerateViewModel;
        public LookTestPaperWord(ExaminationPaperGenerateViewModel examinationPaperGenerateViewModel)
        {
            _examinationPaperGenerateViewModel = examinationPaperGenerateViewModel;
        
        }
        public void Execute(object parameter)
        {
            string pathStr = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathStr = pathStr + @"\QBM\TestPaper" + _examinationPaperGenerateViewModel.SelectTestPaper.Id + ".doc";
            WordUtility.OpenWord(pathStr);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
