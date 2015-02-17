namespace Thinknet.MVVM.Command
{
    using System.Threading.Tasks;

    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    /// <typeparam name="TResult">The type of the task result.</typeparam>
    /// <remarks>
    /// Based on Stephen Cleary's implementation from the book: Concurrency in C# Cookbook
    /// </remarks>
    public interface INotifyTaskCompletion<TResult> : INotifyTaskCompletion
    {
        /// <summary>
        /// Gets the task being watched. This property never changes and is never <c>null</c>.
        /// </summary>
        new Task<TResult> Task { get; }

        /// <summary>
        /// Gets the result of the task. Returns the default value of <typeparamref name="TResult"/> if the task has not completed successfully. This property raises a notification when the task completes successfully.
        /// </summary>
        TResult Result { get; }
    }
}