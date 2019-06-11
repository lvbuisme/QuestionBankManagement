using AppManager.ViewModels.ExaminationPaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppManager.Command
{
    public class DeleteQuestionBackToPaper : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private AddExaminationPaperViewModel _addExaminationPaperViewModel;
        public DeleteQuestionBackToPaper(AddExaminationPaperViewModel addExaminationPaperViewModel)
        {
            _addExaminationPaperViewModel = addExaminationPaperViewModel;
        }
        public void Execute(object parameter)
        {
            _addExaminationPaperViewModel.DeleteExaminationQuesionPaperTypes();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
