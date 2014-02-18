namespace Thinknet.MVVM.Command
{
    using System;

    using Thinknet.MVVM.Helper;

    /// <summary>
    /// A generic command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'. This class allows you to accept command parameters in the
    /// Execute and CanExecute callback methods.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    //// [ClassInfo(typeof(RelayCommand)]
    public class RelayCommand<T> : BaseCommand
    {
        private readonly WeakFunc<T, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            Idle = true;

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            ExecuteAction = new WeakAction<T>(execute);

            if (canExecute != null)
            {
                _canExecute = new WeakFunc<T, bool>(canExecute);
            }
        }

        /// <inheritdoc />
        public override bool HasCanExecuteFunction
        {
            get { return _canExecute != null; }
        }

        /// <summary>
        /// Gets the execute action.
        /// </summary>
        protected WeakAction<T> ExecuteAction { get; private set; }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            if (Idle && (_canExecute.IsStatic || _canExecute.IsAlive))
            {
                T val = TryConvertParameter(parameter);
                return _canExecute.Execute(val);
            }

            return false;
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
                Idle = false;
                ExecuteAction.Execute(val);
                Idle = true;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Try to convert a command Parameter to the Type of <typeparamref name="T"/>.
        /// If the parameter is null and the <typeparamref name="T"/> 
        /// is typeof ValueType than a default instance of <typeparamref name="T"/> will be created.
        /// If the parameter is not typeof <typeparamref name="T"/> and the parameter implements <see cref="IConvertible"/> a conversion will be tried.
        /// </summary>
        /// <param name="parameter"> The parameter for conversion.</param>
        /// <returns>A value of <typeparamref name="T"/> or null or class cast exception.</returns>
        protected static T TryConvertParameter(object parameter)
        {
            object val = parameter;
            if (parameter != null && parameter.GetType() != typeof(T))
            {
                if (parameter is IConvertible)
                {
                    val = Convert.ChangeType(parameter, typeof(T), null);
                }
            }

            if (parameter == null && typeof(T).IsValueType)
            {
                val = default(T);
            }

            return (T)val;
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
                // todo tb: Log warning.
                //_log.Warn("Command is still running, shouldn't be called more than once from multiple threads.");
            }

            return Idle && CanExecute(parameter) && ExecuteAction != null && (ExecuteAction.IsStatic || ExecuteAction.IsAlive);
        }
    }
}