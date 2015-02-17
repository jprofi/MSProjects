namespace Thinknet.MVVM.Command
{
    /// <summary>
    /// Interface for task command implementation with result, used for dependency injection.
    /// </summary>
    /// <typeparam name="TResult">The task result</typeparam>
    public interface ITaskRelayCommand<TResult> : ITaskRelayCommandBase
    {
    }
}
