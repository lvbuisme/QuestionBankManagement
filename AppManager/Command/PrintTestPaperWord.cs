using AppManager.Common;
using AppManager.ViewModels.ExaminationPaper;
using Panuon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppManager.Command
{
    public class PrintTestPaperWord : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ExaminationPaperGenerateViewModel _examinationPaperGenerateViewModel;
        public PrintTestPaperWord(ExaminationPaperGenerateViewModel examinationPaperGenerateViewModel)
        {
            _examinationPaperGenerateViewModel = examinationPaperGenerateViewModel;
        
        }
        public void Execute(object parameter)
        {
            string pathStr = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathStr = pathStr + @"\QBM\TestPaper" + _examinationPaperGenerateViewModel.SelectTestPaper.Id + ".doc";
            Task task = new Task(() =>{

                WordUtility.PrintWord(pathStr);
            });
            task.Start();
            _examinationPaperGenerateViewModel.AwaitTime(2000);

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
