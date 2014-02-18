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
    /// This class does not allow you to accept command parameters in the
    /// Execute, ExecuteFinished and CanExecute callback methods.
    /// </summary>
    public class AsyncRelayCommand : RelayCommand
    {
        /// <summary>
        /// Holds the delegate which is called synchronous in the caller thread.
        /// </summary>
        private readonly WeakAction _executeFinished;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="executeAsync">The execution logic for async processing.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="T:System.ArgumentNullException">If the executeAsync argument is null.</exception>
        public AsyncRelayCommand(Action executeAsync, Func<bool> canExecute) : base(executeAsync, canExecute)
        {
            _executeFinished = new WeakAction(DummySyncAction);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="executeAsync">The execution logic for async processing.</param>
        /// <param name="executeFinished">The execution logic to perform when the async operation has finished.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="T:System.ArgumentNullException">If the executeAsync argument is null.</exception>
        public AsyncRelayCommand(Action executeAsync, Action executeFinished, Func<bool> canExecute) : base(executeAsync, canExecute)
        {
            if (executeFinished == null)
            {
                throw new ArgumentNullException("executeFinished");
            }

            _executeFinished = new WeakAction(executeFinished);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        public override void Execute(object parameter)
        {
            if (CheckCanExecute(parameter))
            {
                PerformAsyncOperation();
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
        private async void PerformAsyncOperation()
        {
            try
            {
                Idle = false;
                
                // Perform the async operation.
                await Task.Run(() => ExecuteAction.Execute());
            }
            catch (Exception exception)
            {
                // todo tb: Handle exception
                throw exception;
            } 
            finally
            {
                // Perform the operation in calling thread.
                _executeFinished.Execute();
                Idle = true;
                RaiseCanExecuteChanged();                
            }
        }

        /// <summary>
        /// Dummy operation for async operations with no sync operation.
        /// </summary>
        private void DummySyncAction()
        {
        }
    }
}
