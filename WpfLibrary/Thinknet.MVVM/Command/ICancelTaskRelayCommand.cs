namespace Thinknet.MVVM.Command
{
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// Interface for the implementation of a command which is used to cancel an async relay command.
    /// </summary>
    public interface ICancelTaskRelayCommand : ICommand
    {
        /// <summary>
        /// Gets the cancelleation token for interrupting the task.
        /// </summary>
        CancellationToken Token { get; }

        /// <summary>
        /// Sets the cancel command in the state starting, that means owner command is running it's job and
        /// therefore can be cancelled. 
        /// </summary>
        void NotifyCommandStarting();
        
        /// <summary>
        /// Sets the cancel command in the state finshed, that means owner command has finished it's job and
        /// therefore no cancellation can be done.
        /// </summary>
        void NotifyCommandFinished();
    }
}
