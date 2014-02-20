namespace Thinknet.ControlLibrary.Utilities
{
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Documents;

    /// <summary>
    /// Formats the RichTextBox text as RTF.
    /// </summary>
    public class RtfFormatter : ITextFormatter
    {
        /// <summary>
        /// Gets the RTF Content of a flow document as UTF-8 Encoded string.
        /// </summary>
        /// <param name="document">The flow document for the RTF extracting.</param>
        /// <returns>The RTF content of the flow document as UTF-8 Encoded string.</returns>
        public string GetText(FlowDocument document)
        {
            TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                tr.Save(ms, DataFormats.Rtf);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// Sets a UTF-8 encoded RTF content to a flow document.
        /// </summary>
        /// <param name="document">The flow document in which the RTF content has to be loaded.</param>
        /// <param name="text">The UTF-8 encoded RTF content.</param>
        public void SetText(FlowDocument document, string text)
        {
            TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                tr.Load(ms, DataFormats.Rtf);
            }
        }
    }
}
