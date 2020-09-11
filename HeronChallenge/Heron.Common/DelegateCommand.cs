using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Heron.Common
{
    public class DelegateCommandBase : ICommand
    {
        protected Action _execute;
        protected Func<bool> _canExecute;

        public DelegateCommandBase()
        {
        }

        public DelegateCommandBase(Action execute)
        {
            this._execute = execute;
        }

        public DelegateCommandBase(Action execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public virtual event EventHandler CanExecuteChanged = delegate { };

        public virtual void OnExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual bool CanExecute(object parameter)
        {
            if (this._canExecute != null)
            {
                return this._canExecute();
            }

            if (this._execute != null)
            {
                return true;
            }

            return false;
        }

        public virtual void Execute(object parameter)
        {
            this._execute?.Invoke();
        }
    }

    public class DelegateCommand : DelegateCommandBase
    {
        public DelegateCommand()
        {
        }

        public DelegateCommand(Action execute)
        {
            this._execute = execute;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
    }

    public class DelegateCommand<T> : DelegateCommand
    {
        private new Action<T> _execute;
        private new Func<T, bool> _canExecute;

        public DelegateCommand()
        { }

        public DelegateCommand(Action<T> execute)
        {
            this._execute = execute;
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public override event EventHandler CanExecuteChanged = delegate { };

        public override void OnExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public override bool CanExecute(object parameter)
        {
            if (this._canExecute != null)
            {
                return this._canExecute((T)parameter);
            }

            if (this._execute != null)
            {
                return true;
            }

            return false;
        }

        public override void Execute(object parameter)
        {
            this._execute?.Invoke((T)parameter);
        }
    }
}