namespace Thinknet.MVVM.Command
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Interface for the async command for execution of tasks.
    /// </summary>
    public interface ITaskRelayCommandBase : ICommand
    {
        /// <summary>
        /// Gets the command for cancellation of a running task.
        /// </summary>
        ICancelTaskRelayCommand CancelCommand { get; }

        /// <summary>
        /// Gets the task to perform asynchronous operation.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The task for the asynch operation.</returns>
        Task ExecuteAsync(object parameter);
    }
}