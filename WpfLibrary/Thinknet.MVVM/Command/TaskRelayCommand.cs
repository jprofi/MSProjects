namespace Thinknet.MVVM.Command
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Asynchronous command for execution of tasks.
    /// </summary>
    public class TaskRelayCommand : TaskRelayCommandBase, ITaskRelayCommand
    {
        private readonly Func<CancellationToken, Task> _operation;
        private NotifyTaskCompletion _execution;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRelayCommand{TResult}"/> class.
        /// </summary>
        /// <param name="operation">The operation to proceed.</param>
        /// <param name="canExecute">A function providing value whether the task can be executed or not.</param>
        /// <param name="commandManager">The command manager.</param>
        public TaskRelayCommand(Func<CancellationToken, Task> operation, Func<bool> canExecute, ICommandManager commandManager)
            : base(canExecute, commandManager)
        {
            _operation = operation;
        }

        /// <summary>
        /// Gets the watching on a task with it's bindable properties.
        /// </summary>
        public NotifyTaskCompletion Execution
        {
            get { return _execution; }
            private set
            {
                _execution = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc />
        public override async Task ExecuteAsync(object parameter)
        {
            // todo tb: How we handle exception e.g. Call execute twice, from ui it shouldn't be possible.
            // eventually call an error handler if provided.

            CancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_operation(CancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskCompleted;
            CancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        /// <inheritdoc />
        public override bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && CanExecuteExternalFunc();
        }
    }
}