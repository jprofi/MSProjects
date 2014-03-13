namespace Thinknet.ControlLibrary.Behaviors
{
    using System.Diagnostics.Contracts;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// The behavior for setting data rows to a readonly state.
    /// </summary>
    public class DataGridRowReadOnlyBehavior : Behavior<DataGrid>
    {
        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();
            Contract.Assert(AssociatedObject != null);

            AssociatedObject.BeginningEdit += AssociatedObject_BeginningEdit;
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            AssociatedObject.BeginningEdit -= AssociatedObject_BeginningEdit;
        }

        /// <summary>
        /// Is callled while trying to edit a  cell in the datagrid.
        /// Checking for row is readonly and in this case prevent from editing the cell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event for beginning cell edit.</param>
        private static void AssociatedObject_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            DataGridRowViewModel rvm = e.Row.Item as DataGridRowViewModel;

            if (rvm != null && rvm.IsReadOnly)
            {
                e.Cancel = true;
            }
        }
    }
}
