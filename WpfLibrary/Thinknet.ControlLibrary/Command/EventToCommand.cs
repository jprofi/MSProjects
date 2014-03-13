namespace Thinknet.ControlLibrary.Command
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    using EventTrigger = System.Windows.Interactivity.EventTrigger;

    /// <summary>
    ///     This <see cref="System.Windows.Interactivity.TriggerAction" /> can be
    ///     used to bind any event on any FrameworkElement to an <see cref="ICommand" />.
    ///     Typically, this element is used in XAML to connect the attached element
    ///     to a command located in a ViewModel. This trigger can only be attached
    ///     to a FrameworkElement or a class deriving from FrameworkElement.
    ///     <para>
    ///         To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
    ///         and leave the CommandParameter and CommandParameterValue empty!
    ///     </para>
    /// </summary>
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        /// <summary>
        ///     Identifies the <see cref="CommandParameter" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", 
            typeof(object), 
            typeof(EventToCommand), 
            new PropertyMetadata(
                null, 
                (s, e) =>
                    {
                        EventToCommand sender = s as EventToCommand;
                        if (sender == null)
                        {
                            return;
                        }

                        if (sender.AssociatedObject == null)
                        {
                            return;
                        }

                        sender.EnableDisableElement();
                    }));

        /// <summary>
        ///     Identifies the <see cref="Command" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", 
            typeof(ICommand), 
            typeof(EventToCommand), 
            new PropertyMetadata(
                null, 
                (s, e) => OnCommandChanged(s as EventToCommand, e)));

        /// <summary>
        ///     Identifies the <see cref="MustToggleIsEnabled" /> dependency property
        /// </summary>
        public static readonly DependencyProperty MustToggleIsEnabledProperty = DependencyProperty.Register(
            "MustToggleIsEnabled", 
            typeof(bool), 
            typeof(EventToCommand), 
            new PropertyMetadata(
                false, 
                (s, e) =>
                    {
                        EventToCommand sender = s as EventToCommand;
                        if (sender == null)
                        {
                            return;
                        }

                        if (sender.AssociatedObject == null)
                        {
                            return;
                        }

                        sender.EnableDisableElement();
                    }));

        private object _commandParameterValue;

        private bool? _mustToggleValue;

        /// <summary>
        ///     Gets or sets the ICommand that this trigger is bound to. This
        ///     is a DependencyProperty.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }

            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        ///     Gets or sets an object that will be passed to the <see cref="Command" />
        ///     attached to this trigger. This is a DependencyProperty.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }

            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        ///     Gets or sets an object that will be passed to the <see cref="Command" />
        ///     attached to this trigger. This property is here for compatibility
        ///     with the Silverlight version. This is NOT a DependencyProperty.
        ///     For databinding, use the <see cref="CommandParameter" /> property.
        /// </summary>
        public object CommandParameterValue
        {
            get { return _commandParameterValue ?? CommandParameter; }

            set
            {
                _commandParameterValue = value;
                EnableDisableElement();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the attached element must be
        ///     disabled when the <see cref="Command" /> property's CanExecuteChanged
        ///     event fires. If this property is true, and the command's CanExecute
        ///     method returns false, the element will be disabled. If this property
        ///     is false, the element will not be disabled when the command's
        ///     CanExecute method changes. This is a DependencyProperty.
        /// </summary>
        public bool MustToggleIsEnabled
        {
            get { return (bool)GetValue(MustToggleIsEnabledProperty); }

            set { SetValue(MustToggleIsEnabledProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the attached element must be
        ///     disabled when the <see cref="Command" /> property's CanExecuteChanged
        ///     event fires. If this property is true, and the command's CanExecute
        ///     method returns false, the element will be disabled. This property is here for
        ///     compatibility with the Silverlight version. This is NOT a DependencyProperty.
        ///     For databinding, use the <see cref="MustToggleIsEnabled" /> property.
        /// </summary>
        public bool MustToggleIsEnabledValue
        {
            get
            {
                return _mustToggleValue == null
                           ? MustToggleIsEnabled
                           : _mustToggleValue.Value;
            }

            set
            {
                _mustToggleValue = value;
                EnableDisableElement();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the EventArgs passed to the
        ///     event handler will be forwarded to the ICommand's Execute method
        ///     when the event is fired (if the bound ICommand accepts an argument
        ///     of type EventArgs).
        ///     <para>
        ///         For example, use a RelayCommand&lt;MouseEventArgs&gt; to get
        ///         the arguments of a MouseMove event.
        ///     </para>
        /// </summary>
        public bool PassEventArgsToCommand { get; set; }

        /// <summary>
        /// create a new EventToCommand and wires it to the actions of the given dependency object
        /// </summary>
        /// <param name="element">dependency object</param>
        /// <param name="command">command source</param>
        /// <param name="action">action to execute</param>
        /// <returns>new EventToCommand</returns>
        public static EventToCommand CreateEventToCommandBinding(DependencyObject element, ICommand command, string action)
        {
            return CreateEventToCommandBinding(element, command, action, null);
        }

        /// <summary>
        /// create a new EventToCommand and wires it to the actions of the given dependency object
        /// </summary>
        /// <param name="element">dependency object</param>
        /// <param name="command">command source</param>
        /// <param name="action">action to execute</param>
        /// <param name="commandParameter">command parameter</param>
        /// <returns>new EventToCommand</returns>
        public static EventToCommand CreateEventToCommandBinding(DependencyObject element, ICommand command, string action, object commandParameter)
        {
            EventToCommand cmd = new EventToCommand();
            cmd.CommandParameterValue = commandParameter;
            EventTrigger trigger = new EventTrigger { EventName = action };
            cmd.Command = command;
            trigger.Actions.Add(cmd);
            Interaction.GetTriggers(element).Add(trigger);

            return cmd;
        }

        /// <summary>
        ///     Provides a simple way to invoke this trigger programatically
        ///     without any EventArgs.
        /// </summary>
        public void Invoke()
        {
            Invoke(null);
        }

        /// <summary>
        /// Executes the trigger.
        ///     <para>
        /// To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
        ///         and leave the CommandParameter and CommandParameterValue empty!
        ///     </para>
        /// </summary>
        /// <param name="parameter">
        /// The EventArgs of the fired event.
        /// </param>
        protected override void Invoke(object parameter)
        {
            if (AssociatedElementIsDisabled())
            {
                return;
            }

            ICommand command = GetCommand();
            object commandParameter = CommandParameterValue;

            if (commandParameter == null
                && PassEventArgsToCommand)
            {
                commandParameter = parameter;
            }

            if (command != null
                && command.CanExecute(commandParameter))
            {
                command.Execute(commandParameter);
            }
        }

        /// <summary>
        ///     Called when this trigger is attached to a DependencyObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            EnableDisableElement();
        }

        private static void OnCommandChanged(
            EventToCommand element, 
            DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;
            }

            ICommand command = (ICommand)e.NewValue;

            if (command != null)
            {
                command.CanExecuteChanged += element.OnCommandCanExecuteChanged;
            }

            element.EnableDisableElement();
        }

        private bool AssociatedElementIsDisabled()
        {
            FrameworkElement element = GetAssociatedObject();
            return AssociatedObject == null
                   || (element != null
                       && !element.IsEnabled);
        }

        private void EnableDisableElement()
        {
            FrameworkElement element = GetAssociatedObject();

            if (element == null)
            {
                return;
            }

            ICommand command = GetCommand();

            if (MustToggleIsEnabledValue
                && command != null)
            {
                element.IsEnabled = command.CanExecute(CommandParameterValue);
            }
        }

        /// <summary>
        ///     This method is here for compatibility
        ///     with the Silverlight version.
        /// </summary>
        /// <returns>
        ///     The object to which this trigger
        ///     is attached casted as a FrameworkElement.
        /// </returns>
        private FrameworkElement GetAssociatedObject()
        {
            return AssociatedObject as FrameworkElement;
        }

        /// <summary>
        ///     This method is here for compatibility
        ///     with the Silverlight version.
        /// </summary>
        /// <returns>
        ///     The command that must be executed when
        ///     this trigger is invoked.
        /// </returns>
        private ICommand GetCommand()
        {
            return Command;
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            EnableDisableElement();
        }
    }
}
