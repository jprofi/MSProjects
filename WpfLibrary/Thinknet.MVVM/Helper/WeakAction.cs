namespace Thinknet.MVVM.Helper
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     Stores an <see cref="Action" /> without causing a hard reference
    ///     to be created to the Action's owner. The owner can be garbage collected at any time.
    /// </summary>
    public class WeakAction
    {
        private Action _staticAction;
        private WeakReference _reference;
        private MethodInfo _method;
        private WeakReference _actionReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="action"> The action that will be associated to this instance. </param>
        public WeakAction(Action action)
            : this(action.Target, action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakAction"/> class.
        /// </summary>
        /// <param name="target"> The action's owner. </param>
        /// <param name="action"> The action that will be associated to this instance. </param>
        public WeakAction(object target, Action action)
        {
            if (action.Method.IsStatic)
            {
                _staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    Reference = new WeakReference(target);
                }

                return;
            }

            Method = action.Method;
            ActionReference = new WeakReference(action.Target);

            Reference = new WeakReference(target);
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="WeakAction" /> class.
        /// </summary>
        protected WeakAction()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the Action's owner is still alive, or if it was collected
        /// by the Garbage Collector already.
        /// </summary>
        public virtual bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                {
                    return false;
                }

                if (_staticAction != null)
                {
                    return Reference == null || Reference.IsAlive;
                }

                return Reference.IsAlive;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the WeakAction is static or not.
        /// </summary>
        public bool IsStatic
        {
            get { return _staticAction != null; }
        }

        /// <summary>
        /// Gets the name of the method that this WeakAction represents.
        /// </summary>
        public virtual string MethodName
        {
            get
            {
                return _staticAction != null ? _staticAction.Method.Name : Method.Name;
            }
        }

        /// <summary>
        /// Gets the Action's owner. This object is stored as a
        /// <see cref="WeakReference" />.
        /// </summary>
        public object Target
        {
            get
            {
                return Reference == null ? null : Reference.Target;
            }
        }

        /// <summary>
        ///  Gets or sets a WeakReference to this WeakAction's action's target.
        ///  This is not necessarily the same as
        ///  <see cref="Reference" />, for example if the method is anonymous.
        /// </summary>
        protected WeakReference ActionReference
        {
            get { return _actionReference; }
            set { _actionReference = value; }
        }

        /// <summary>
        /// Gets the action target or null.
        /// </summary>
        protected object ActionTarget
        {
            get
            {
                return ActionReference == null ? null : ActionReference.Target;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="MethodInfo" /> corresponding to this WeakAction's method passed in the constructor.
        /// </summary>
        protected MethodInfo Method
        {
            get { return _method; }
            set { _method = value; }
        }

        /// <summary>
        /// Gets or sets a WeakReference to the target passed when constructing
        /// the WeakAction. This is not necessarily the same as
        /// <see cref="ActionReference" />, for example if the
        /// method is anonymous.
        /// </summary>
        protected WeakReference Reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        /// <summary>
        /// Executes the action. This only happens if the action's owner is still alive.
        /// </summary>
        public void Execute()
        {
            if (_staticAction != null)
            {
                _staticAction();
                return;
            }

            object actionTarget = ActionTarget;

            if (IsAlive)
            {
                if (Method != null && ActionReference != null && actionTarget != null)
                {
                    Method.Invoke(ActionTarget, null);
                }
            }
        }

        /// <summary>
        ///     Sets the reference that this instance stores to null.
        /// </summary>
        public void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            Method = null;
            _staticAction = null;
        }
    }
}
