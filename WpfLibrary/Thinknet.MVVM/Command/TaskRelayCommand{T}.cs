namespace Thinknet.MVVM.Command
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Asynchronous command for execution of tasks.
    /// </summary>
    /// <typeparam name="TResult">The result afert task completion.</typeparam>
    public class TaskRelayCommand<TResult> : TaskRelayCommandBase, ITaskRelayCommand<TResult>
    {
        private readonly Func<CancellationToken, Task<TResult>> _operation;
        private NotifyTaskCompletion<TResult> _execution;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRelayCommand{TResult}"/> class.
        /// </summary>
        /// <param name="operation">The operation to proceed.</param>
        /// <param name="canExecute">A function providing value whether the task can be executed or not.</param>
        /// <param name="commandManager">The command manager.</param>
        public TaskRelayCommand(Func<CancellationToken, Task<TResult>> operation, Func<bool> canExecute, ICommandManager commandManager)
            : base(canExecute, commandManager)
        {
            _operation = operation;
        }

        /// <summary>
        /// Gets the watching on a task with it's bindable properties.
        /// </summary>
        public NotifyTaskCompletion<TResult> Execution
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
            CancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion<TResult>(_operation(CancelCommand.Token));
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