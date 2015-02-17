namespace Thinknet.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    using Thinknet.MVVM.Binding;

    /// <summary>
    /// This class serves as abstract base class for all ViewModels.
    /// </summary>
    /// <typeparam name="TModel">
    /// type of the model
    /// </typeparam>
    public abstract class ViewModel<TModel> : BindingBase, IDataErrorInfo, INotifyPropertyChanged
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private TModel _model;

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
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            return SetProperty(ref storage, value, v => true, propertyName);
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary. If typeparam T is a string, it additionally trims the string.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the property.
        /// </typeparam>
        /// <param name="storage">
        /// Reference to a property with both getter and setter.
        /// </param>
        /// <param name="value">
        /// Desired value for the property.
        /// </param>
        /// <param name="validateAction">
        /// An action to validate the value.
        /// </param>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.
        /// </param>
        /// <returns>
        /// True if the value was changed, false if the existing value matched the
        /// desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, Func<T, bool> validateAction, [CallerMemberName] string propertyName = null)
        {

            if (Equals(storage, value))
            {
                return false;
            }

            validateAction(value);

            storage = value;

            // ReSharper disable once ExplicitCallerInfoArgument
            NotifyPropertyChanged(propertyName);
            return true;
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
