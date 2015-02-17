namespace Thinknet.MVVM.Command
{
    using System;
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// Command for the cancellation of an async command.
    /// </summary>
    public sealed class CancelTaskRelayCommand : ICancelTaskRelayCommand
    {
        private readonly ICommandManager _commandManager;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _commandExecuting;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelTaskRelayCommand"/> class.
        /// </summary>
        /// <param name="commandManager">The command manager.</param>
        public CancelTaskRelayCommand(ICommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add { _commandManager.RequerySuggested += value; }
            remove { _commandManager.RequerySuggested -= value; }
        }

        /// <inheritdoc />
        public CancellationToken Token
        {
            get { return _cts.Token; }
        }

        /// <inheritdoc />
        public void NotifyCommandStarting()
        {
            _commandExecuting = true;
            if (!_cts.IsCancellationRequested)
            {
                return;
            }

            _cts = new CancellationTokenSource();
            RaiseCanExecuteChanged();
        }

        /// <inheritdoc />
        public void NotifyCommandFinished()
        {
            _commandExecuting = false;
            RaiseCanExecuteChanged();
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object parameter)
        {
            return _commandExecuting && !_cts.IsCancellationRequested;
        }

        /// <inheritdoc />
        void ICommand.Execute(object parameter)
        {
            _cts.Cancel();
            RaiseCanExecuteChanged();
        }

        private void RaiseCanExecuteChanged()
        {
            _commandManager.InvalidateRequerySuggested();
        }
    }
}