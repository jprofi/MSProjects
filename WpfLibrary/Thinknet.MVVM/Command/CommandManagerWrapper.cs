namespace Thinknet.MVVM.Command
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Wrapper for testing purposes of the <see cref="CommandManager"/>.
    /// </summary>
    public class CommandManagerWrapper : ICommandManager
    {
        /// <inheritdoc />
        public event EventHandler RequerySuggested
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <inheritdoc />
        public void InvalidateRequerySuggested()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}