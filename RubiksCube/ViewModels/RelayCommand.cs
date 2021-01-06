using System;
using System.Runtime.Remoting.Channels;
using System.Windows.Input;

namespace RubiksCube.ViewModels
{
    //Mithilfe dieser Klasse wird aus einer Funktion ein ICommand sodass man diese mit den Buttons verbinden kann
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

        //Funktion die an einen Button gebunden werden soll
        private readonly Action<object> Action;

        //Funktion die überorüft ob die Action ausgeführt werden kann
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

        //Prüft ob die Action ausgeführt werden kann
        public bool CanExecute(object parameter)
        {
            return Predicate == null || Predicate(parameter);
        }

        //Führt die Action aus
        public void Execute(object parameter)
        {
            Action.Invoke(parameter);
        }

        #endregion
    }
}
