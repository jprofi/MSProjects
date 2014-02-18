namespace Thinknet.MVVM.Command
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Input;

    /// <summary>
    /// Provides base functionally for all synchronous and asynchronous commands.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        private bool _idle;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand"/> class.
        /// </summary>
        protected BaseCommand()
        {
            Idle = true;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (HasCanExecuteFunction)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (HasCanExecuteFunction)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the running state is idle or not.
        /// </summary>
        public bool Idle
        {
            get { return _idle; }
            protected set { _idle = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the implementing command has a can execute function.
        /// </summary>
        public abstract bool HasCanExecuteFunction { get; }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <inheritdoc />
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc />
        public abstract void Execute(object parameter);
    }
}
