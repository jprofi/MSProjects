namespace Thinknet.MVVM.Command
{
    using System;
    using System.Threading.Tasks;

    using Thinknet.MVVM.Helper;

    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking a delegate asynchronous. After asynchronous operation completion,
    /// the executeFinished delegate will be called synchronous in the caller thread.
    /// The default return value for the CanExecute method is 'true'.
    /// This class does accept command parameters in the Execute, ExecuteFinished and CanExecute callback methods.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public class AsyncRelayCommand<T> : RelayCommand<T>
    {
        /// <summary>
        ///     Holds the delegate which is called synchronous in the caller thread.
        /// </summary>
        private readonly WeakAction<T> _executeFinished;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="executeAsync">The execution logic for async processing.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">executeAsync is null</exception>
        /// <exception cref="ArgumentNullException">executeFinished is null</exception>
        public AsyncRelayCommand(Action<T> executeAsync, Func<T, bool> canExecute) : base(executeAsync, canExecute)
        {
            _executeFinished = new WeakAction<T>(DummySyncAction);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="executeAsync">The execution logic for async processing.</param>
        /// <param name="executeFinished">The execution logic sync operation after async operation has completed.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">executeAsync is null</exception>
        /// <exception cref="ArgumentNullException">executeFinished is null</exception>
        public AsyncRelayCommand(Action<T> executeAsync, Action<T> executeFinished, Func<T, bool> canExecute) : base(executeAsync, canExecute)
        {
            if (executeFinished == null)
            {
                throw new ArgumentNullException("executeFinished");
            }

            _executeFinished = new WeakAction<T>(executeFinished);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        public override void Execute(object parameter)
        {
            if (CheckCanExecute(parameter))
            {
                T val = TryConvertParameter(parameter);
                PerformAsyncOperation(val);
            }
        }

        /// <inheritdoc />
        protected override bool CheckCanExecute(object parameter)
        {
            return base.CheckCanExecute(parameter) && _executeFinished != null && (_executeFinished.IsStatic || _executeFinished.IsAlive);
        }

        /// <summary>
        /// Performs the long duration operation asychronous.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        private async void PerformAsyncOperation(T parameter)
        {
            try
            {
                Idle = false;

                // Perform the async operation.
                await Task.Run(() => ExecuteAction.Execute(parameter));
            }
            catch (Exception exception)
            {
                // todo tb: Exception handling
                throw exception;
            }
            finally
            {
                // Perform the operation in calling thread.
                _executeFinished.Execute(parameter);
                Idle = true;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Dummy operation for async operations with no sync operation.
        /// </summary>
        /// <param name="obj">The parameter object.</param>
        private void DummySyncAction(T obj)
        {
        }
    }
}
