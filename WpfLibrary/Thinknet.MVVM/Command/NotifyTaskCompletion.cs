namespace Thinknet.MVVM.Command
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Thinknet.MVVM.Binding;

    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    /// <remarks>
    /// Based on Stephen Cleary's implementation from the book: Concurrency in C# Cookbook
    /// </remarks>
    public class NotifyTaskCompletion : BindingBase, INotifyTaskCompletion
    {
        private readonly Task _task;
        private readonly Task _taskCompleted;

        /// <summary>
        /// Initializes a task notifier watching the specified task.
        /// </summary>
        /// <param name="task">
        /// The task to watch.
        /// </param>
        public NotifyTaskCompletion(Task task)
            : this(task, (SynchronizationContext.Current == null) ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext())
        {
        }

        /// <summary>
        /// Initializes a task notifier watching the specified task.
        /// </summary>
        /// <param name="task">
        /// The task to watch.
        /// </param>
        /// <param name="scheduler">
        /// The task scheduler that should be used.
        /// </param>
        public NotifyTaskCompletion(Task task, TaskScheduler scheduler)
        {
              _task = task;
            if (task.IsCompleted)
            {
                return;
            }

            _taskCompleted = task.ContinueWith(t =>
            {
                NotifyPropertyChanged(() => Status);
                NotifyPropertyChanged(() => IsCompleted);
                NotifyPropertyChanged(() => IsNotCompleted);

                if (t.IsCanceled)
                {
                    NotifyPropertyChanged(() => IsCanceled);
                }
                else if (t.IsFaulted)
                {
                    NotifyPropertyChanged(() => IsFaulted);
                    NotifyPropertyChanged(() => Exception);
                    NotifyPropertyChanged(() => InnerException);
                    NotifyPropertyChanged(() => ErrorMessage);
                }
                else
                {
                    NotifyPropertyChanged(() => IsSuccessfullyCompleted);
                }
            }, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, scheduler);            
        }

        /// <inheritdoc />
        public string ErrorMessage
        {
            get { return (InnerException == null) ? null : InnerException.Message; }
        }

        /// <inheritdoc />
        public AggregateException Exception
        {
            get { return _task.Exception; }
        }

        /// <inheritdoc />
        public Exception InnerException
        {
            get { return (Exception == null) ? null : Exception.InnerException; }
        }

        /// <inheritdoc />
        public bool IsCanceled
        {
            get { return _task.IsCanceled; }
        }

        /// <inheritdoc />
        public bool IsCompleted
        {
            get { return _task.IsCompleted; }
        }

        /// <inheritdoc />
        public bool IsFaulted
        {
            get { return _task.IsFaulted; }
        }

        /// <inheritdoc />
        public bool IsNotCompleted
        {
            get { return !_task.IsCompleted; }
        }

        /// <inheritdoc />
        public bool IsSuccessfullyCompleted
        {
            get { return _task.Status == TaskStatus.RanToCompletion; }
        }

        /// <inheritdoc />
        public TaskStatus Status
        {
            get { return _task.Status; }
        }

        /// <inheritdoc />
        public Task Task
        {
            get { return _task; }
        }

        /// <inheritdoc />
        public Task TaskCompleted
        {
            get { return _taskCompleted; }
        }
    }
}
