using System;
using System.Runtime.Remoting.Channels;
using System.Windows.Input;

namespace RubiksCube.ViewModels
{
    class RelayCommand : ICommand
    {
        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Private Members

        private readonly Action<object> Action;
        private readonly Predicate<object> Predicate;

        #endregion

        #region Constructors

        public RelayCommand(Action<object> action, Predicate<object> predicate)
        {
            this.Action = action ?? throw new NullReferenceException("action");
            this.Predicate = predicate;
        }

        public RelayCommand(Action<object> action) : this(action, null) { }

        #endregion

        #region Public Methods


        public bool CanExecute(object parameter)
        {
            return Predicate == null || Predicate(parameter);
        }

        public void Execute(object parameter)
        {
            Action.Invoke(parameter);
        }

        #endregion
    }
}
