namespace Thinknet.MVVM.Command
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Wrapper interface for testing purposes of the <see cref="CommandManager"/>.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Register event handler for requery suggested on command manager.
        /// </summary>
        event EventHandler RequerySuggested;

        /// <summary>
        /// Invokes RequerySuggested listeners registered on the current thread. 
        /// </summary>
        void InvalidateRequerySuggested();
    }
}