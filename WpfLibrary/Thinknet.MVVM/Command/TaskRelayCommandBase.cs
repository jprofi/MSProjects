namespace Thinknet.MVVM.Command
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Thinknet.MVVM.Binding;

    /// <summary>
    /// Base for async commands.
    /// </summary>
    public abstract class TaskRelayCommandBase : BindingBase, ITaskRelayCommandBase
    {
        private readonly Func<bool> _canExecute;
        private readonly ICommandManager _commandManager;
        private readonly ICancelTaskRelayCommand _cancelCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRelayCommandBase"/> class.
        /// </summary>
        /// <param name="canExecute">A function providing value whether the task can be executed or not.</param>
        /// <param name="commandManager">The command manager.</param>
        protected TaskRelayCommandBase(Func<bool> canExecute, ICommandManager commandManager)
        {
            _canExecute = canExecute;
            _commandManager = commandManager;
            _cancelCommand = new CancelTaskRelayCommand(commandManager);
        }
        
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add { _commandManager.RequerySuggested += value; }
            remove { _commandManager.RequerySuggested -= value; }
        }

        /// <inheritdoc />
        public ICancelTaskRelayCommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        /// <inheritdoc />
        public abstract Task ExecuteAsync(object parameter);

        /// <inheritdoc />
        public abstract bool CanExecute(object parameter);


        /// <inheritdoc />
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /// <summary>
        /// Calls command manager for requesting update command states.
        /// </summary>
        protected void RaiseCanExecuteChanged()
        {
            _commandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Gets the evaluated value of the external passed function CanExecute(..).
        /// </summary>
        /// <returns>The evaluated value of the external function CanExecute().</returns>
        protected bool CanExecuteExternalFunc()
        {
            return _canExecute == null || _canExecute.Invoke();
        }
    }
}