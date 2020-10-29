using System;
using System.Runtime.Remoting.Channels;
using System.Windows.Input;

namespace RubiksCube.ViewModels
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        private Action Action;

        public RelayCommand(Action action)
        {
            this.Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action.Invoke();
        }
    }
}
