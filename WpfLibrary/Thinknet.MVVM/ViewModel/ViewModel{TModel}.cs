namespace Thinknet.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Thinknet.MVVM.Messaging;

    /// <summary>
    /// This class serves as abstract base class for all ViewModels.
    /// </summary>
    /// <typeparam name="TModel">
    /// type of the model
    /// </typeparam>
    public abstract class ViewModel<TModel> : IDataErrorInfo, INotifyPropertyChanged
    {
        private readonly Dispatcher _dispatcher;
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private TModel _model;
        private IMessenger _messengerInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class. 
        /// Constructor
        /// </summary>
        protected ViewModel()
        {
            // Check whether application object is available (e.g. in console applications, 
            // unit tests there is no application object available)
            if (Application.Current != null)
            {
                // Get existing dispatcher
                _dispatcher = Application.Current.Dispatcher;
            }
            else
            {
                // Create new dispatcher and store reference
                _dispatcher = Dispatcher.CurrentDispatcher;
            }
        }

        /// <summary>
        /// Event occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public virtual string Error
        {
            get { return string.Empty; }
        }

        /// <inheritdoc />
        public TModel Model
        {
            get { return _model; }
            set 
            {
                bool updateValue = false;
                if (ReferenceEquals(_model, null))
                {
                    updateValue = true;
                }
                else 
                {
                    updateValue = !_model.Equals(value);
                }

                if (updateValue)
                {
                    _model = value;
                    OnModelChanged();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets an instance of a <see cref="IMessenger" /> used to
        /// broadcast messages to other objects. If null, this class will
        /// attempt to broadcast using the Messenger's default instance.
        /// </summary>
        protected IMessenger MessengerInstance
        {
            get
            {
                return _messengerInstance ?? Messenger.Default;
            }

            set
            {
                _messengerInstance = value;
            }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of a property.</param>
        /// <returns>The error message for the property. The default is an empty string ("")</returns>
        public virtual string this[string propertyName]
        {
            get
            {
                return !_errors.ContainsKey(propertyName) ? null : string.Join(Environment.NewLine, _errors[propertyName]);
            }
        }

        /// <summary>
        /// Determine name of a property function.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="property">
        /// The property function to get the name from.
        /// </param>
        /// <returns>
        /// The name of the property function.
        /// </returns>
        public static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            MemberExpression expression = (MemberExpression)property.Body;
            string propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Gets the name of a property of a specific model.
        /// </summary>
        /// <typeparam name="TModel">The model type containing the property.</typeparam>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="property">The property</param>
        /// <returns>The name of the property.</returns>
        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            MemberExpression expression = (MemberExpression)property.Body;
            string propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Notifies that all properties have changed and have to be updated in UI.
        /// Used for e.g. Language changes on viemodel level.
        /// </summary>
        protected void NotifyAllPropertyChanged()
        {
            NotifyPropertyChangedInternal(string.Empty);
        }

        /// <summary>
        /// Inform all registered event handlers that a property has changed.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="changedProperty">
        /// The property which has changed.
        /// </param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            string propertyName = GetPropertyName(changedProperty);
            NotifyPropertyChangedInternal(propertyName);
        }

        /// <summary>
        /// Raises a property change event without passing the callers method or property name.
        ///     Property name is automatically replaced by the compiler.
        /// </summary>
        /// <param name="propertyName">Holds the compiler generated name of the calling property or method.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            NotifyPropertyChangedInternal(propertyName);
        }

        /// <summary>
        /// Inform all registered event handlers that a property has changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property which has been changed.
        /// </param>
        private void NotifyPropertyChangedInternal(string propertyName)
        {
            // Check whether there are any event handlers registered
            PropertyChangedEventHandler eventHandlers = PropertyChanged;
            if (eventHandlers != null)
            {
                // Raise event
                eventHandlers(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <inheritdoc />
        public void ApplyModel(object model)
        {
            Model = (TModel)model;
        }

        /// <summary>
        /// supply an initialize method which can be overriden by derived classes
        /// </summary>
        public virtual void Initialize()
        {
            _model = CreateModel();
        }

        /// <summary>
        /// Adds a specific error of a property to the errors collection if it is not a already present,
        /// inserting it in the first position if isWarning is false.
        /// Multiple errors and warnings for each property.
        /// The calling property is automatically determined by the compiler.
        /// </summary>
        /// <param name="error">The error or warning text.</param>
        /// <param name="warning">Is it a warning.</param>
        /// <param name="propertyName">The property Name, automatically determined by the compiler.</param>
        public void AddError(string error, bool warning, [CallerMemberName] string propertyName = "")
        {
            AddErrorInternal(propertyName, error, warning);
        }

        /// <summary>
        /// Adds a specific error of a property to the errors collection if it is not a already present,
        /// inserting it in the first position if isWarning is false.
        /// Multiple errors and warnings for each property.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="property">The property for which the error is registered.</param>
        /// <param name="error">The error or warning text.</param>
        /// <param name="warning">Is it a warning.</param>
        public void AddError<T>(Expression<Func<T>> property, string error, bool warning)
        {
            string propertyName = GetPropertyName(property);
            AddErrorInternal(propertyName, error, warning);
        }

        /// <summary>
        /// Removes a specific error of a property from the errors collection if the error is present.
        /// The calling property is automatically determined by the compiler.
        /// </summary>
        /// <param name="error">The error or warning text.</param>
        /// <param name="propertyName">The property Name, automatically determined by the compiler.</param>
        public void RemoveError(string error, [CallerMemberName] string propertyName = "")
        {
            RemoveErrorInternal(propertyName, error);
        }

        /// <summary>
        /// Removes a specific error of a property from the errors collection if the error is present.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="property">The property for which the error has to be removed.</param>
        /// <param name="error">The error or warning text.</param>
        public void RemoveError<T>(Expression<Func<T>> property, string error)
        {
            string propertyName = GetPropertyName(property);
            RemoveErrorInternal(propertyName, error);
        }

        /// <summary>
        /// Removes all errors of a specific property from the errors collection.
        /// The calling property is automatically determined by the compiler.
        /// </summary>
        /// <param name="propertyName">The property Name, automatically determined by the compiler.</param>
        public void RemoveAllErrors([CallerMemberName] string propertyName = "")
        {
            RemoveAllErrorsInternal(propertyName);
        }

        /// <summary>
        /// Removes all errors of a specific property from the errors collection.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="property">The property for which the errors have to be removed.</param>
        public void RemoveAllErrors<T>(Expression<Func<T>> property)
        {
            string propertyName = GetPropertyName(property);
            RemoveAllErrorsInternal(propertyName);
        }

        /// <summary>
        /// Are there any Property Errors in this context.
        /// </summary>
        /// <returns>
        /// The has errors.
        /// </returns>
        public virtual bool HasErrors()
        {
            return _errors.Keys.Count > 0;
        }

        /// <summary>
        /// Run a given action in UI thread with normal priority if neccessary.
        /// </summary>
        /// <param name="action">
        /// The action to run in UI thread
        /// </param>
        public void RunInUiThread(Action action)
        {
            RunInUiThread(DispatcherPriority.Normal, action);
        }

        /// <summary>
        /// Run a given action in UI thread with normal priority, always insert at the end of dispatcher queue.
        /// </summary>
        /// <param name="action">The action to be run.</param>
        protected void RunInUiThreadBeginInvoke(Action action)
        {
            _dispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// Run a given action in UI thread with a given thread priority.
        /// </summary>
        /// <param name="threadPriority">
        /// The thread priority to run the action with.
        /// </param>
        /// <param name="action">
        /// The action to run in UI thread
        /// </param>
        protected void RunInUiThread(DispatcherPriority threadPriority, Action action)
        {
            if (_dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                _dispatcher.BeginInvoke(action);
            }
        }

        /// <summary>
        /// Forces all binded commands to update the can execute state.
        /// </summary>
        protected void NotifyCommandExecutionStatesChanged()
        {
            RunInUiThread(CommandManager.InvalidateRequerySuggested);
        }

        /// <summary>
        /// create or initialize reference to the attached model
        /// </summary>
        /// <returns>created/referenced model</returns>
        protected virtual TModel CreateModel()
        {
            return _model;
        }

        /// <summary>
        /// called when the model has changed
        /// </summary>
        protected virtual void OnModelChanged()
        {
        }

        /// <summary>
        /// override when the parent of the view model is set or has changed
        /// </summary>
        protected virtual void OnParentChanged()
        {
        }

        private void RemoveErrorInternal(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Contains(error))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0)
                {
                    _errors.Remove(propertyName);
                }
            }
        }

        private void AddErrorInternal(string propertyName, string error, bool warning)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            if (!_errors[propertyName].Contains(error))
            {
                if (warning)
                {
                    _errors[propertyName].Add(error);
                }
                else
                {
                    _errors[propertyName].Insert(0, error);
                }
            }
        }

        private void RemoveAllErrorsInternal(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Clear();
                if (_errors[propertyName].Count == 0)
                {
                    _errors.Remove(propertyName);
                }
            }
        }
    }
}
