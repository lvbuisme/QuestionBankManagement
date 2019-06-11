using AppManager.ViewModels.ExaminationPaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppManager.Command
{
    public class AddQuestionBackToPaper : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private AddExaminationPaperViewModel _addExaminationPaperViewModel;
        public AddQuestionBackToPaper(AddExaminationPaperViewModel addExaminationPaperViewModel)
        {
            _addExaminationPaperViewModel = addExaminationPaperViewModel;
        }
        public void Execute(object parameter)
        {
            _addExaminationPaperViewModel.AddExaminationQuesionPaperTypes();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
