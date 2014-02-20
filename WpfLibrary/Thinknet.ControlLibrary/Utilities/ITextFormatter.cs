namespace Thinknet.ControlLibrary.Utilities
{
    using System.Windows.Documents;

    /// <summary>
    /// Interface for formatting the content of a RichtTextBox.
    /// Usually converts a flowdocument to a specific string representation and vice versa.
    /// </summary>
    public interface ITextFormatter
    {
        /// <summary>
        /// Converts the content of a flow document to a string, usually this value will be written back to the viewmodel.
        /// </summary>
        /// <param name="document">The flow document with content.</param>
        /// <returns>The flow document content in a specific string format.</returns>
        string GetText(FlowDocument document);

        /// <summary>
        /// Writes the text content usually provided by viewmodel to the flow document.
        /// </summary>
        /// <param name="document">The flow document in which the text has to be written.</param>
        /// <param name="text">The text to write to the flow document.</param>
        void SetText(FlowDocument document, string text);
    }
}
