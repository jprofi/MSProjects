namespace Thinknet.MVVM.Command
{
    using System;

    using Thinknet.MVVM.Helper;

    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'.  This class does not allow you to accept command parameters in the
    /// Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommand : BaseCommand
    {
        private readonly WeakFunc<bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            Idle = true;

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            ExecuteAction = new WeakAction(execute);

            if (canExecute != null)
            {
                _canExecute = new WeakFunc<bool>(canExecute);
            }
        }

        /// <inheritdoc />
        public override bool HasCanExecuteFunction
        {
            get { return _canExecute != null; }
        }

        /// <summary>
        /// Gets the Excecution Action.
        /// </summary>
        protected WeakAction ExecuteAction { get; private set; }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            if (Idle && (_canExecute.IsStatic || _canExecute.IsAlive))
            {
                return _canExecute.Execute();
            }

            return false;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        public override void Execute(object parameter)
        {
            if (CheckCanExecute(parameter))
            {
                Idle = false;
                ExecuteAction.Execute();
                Idle = true;
            }
        }

        /// <summary>
        /// Checks the state of this command whether the execute action can be excecuted or not.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        /// <returns>The <see cref="bool"/>, is the command in a state for executing the action.</returns>
        protected virtual bool CheckCanExecute(object parameter)
        {
            if (!Idle)
            {
                // todo tb: Do log warn message.
                //log.Warn("Command is still running, shouldn't be called more than once from multiple threads.");
            }

            return Idle && CanExecute(parameter) && ExecuteAction != null && (ExecuteAction.IsStatic || ExecuteAction.IsAlive);
        }
    }
}