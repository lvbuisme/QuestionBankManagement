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
    public class GenerateTestPaperWord : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ExaminationPaperGenerateViewModel _examinationPaperGenerateViewModel;
        public GenerateTestPaperWord(ExaminationPaperGenerateViewModel examinationPaperGenerateViewModel)
        {
            _examinationPaperGenerateViewModel = examinationPaperGenerateViewModel;
        }
        public void Execute(object parameter)
        {
            if (_examinationPaperGenerateViewModel.SelectTestPaper == null) return;
            Task task = new Task(() =>
            {
                WordUtility.CreateTestPaperWord(_examinationPaperGenerateViewModel.SelectTestPaper);
            });
            task.Start();
            _examinationPaperGenerateViewModel.AwaitTime(3000);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
