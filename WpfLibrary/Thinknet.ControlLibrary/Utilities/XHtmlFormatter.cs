namespace Thinknet.ControlLibrary.Utilities
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Documents;
    using System.Xml.Linq;

    using Thinknet.ControlLibrary.Utilities.XHtmlToXamlParser;

    /// <summary>
    /// Formats a text containing XHtml UTF-8 content to a flow Document and vice Versa.
    /// <b>Actually conversion from XAML to XHtml is not implemented, therefore original unmodified XHtml Content will be returned!</b>
    /// </summary>
    public class XHtmlFormatter : ITextFormatter
    {
        private string _xHtml = string.Empty;

        /// <summary>
        /// Gets the XHtml Content of a flow document as unicode UTF-8 string.
        /// <b>Actually conversion from XAML to XHtml is not implemented, therefore original unmodified XHtml Content will be returned!</b>
        /// </summary>
        /// <param name="document">The flow document.</param>
        /// <returns>The XHtml content as Unicode UTF-8 string.</returns>
        public string GetText(FlowDocument document)
        {
            // If implementing a XAML to XHTML parser has to be used, actually not implemented, for writing modified content back.
            //TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    tr.Save(ms, DataFormats.Xaml);
            //    string text = Encoding.UTF8.GetString(ms.ToArray());
            //    return text;
            //}

            // Return same XHtml as read before, because converting from Xaml to XHtml is not supported yet.
            // The problem occurs for example, when a hyperlink is clicked, properties of the color should change.
            // This forces to write modified Xaml back to the viewvodel.
            return _xHtml;
        }

        /// <summary>
        /// Converts the XHtml content to XAML and sets the converted content in a flow document.
        /// </summary>
        /// <param name="document">The flow document in which the XHtml content has to be loaded.</param>
        /// <param name="text">The Unicode UTF-8 coded XHtml text to load.</param>
        /// <exception cref="InvalidDataException">When an error occured while parsing xaml code.</exception>
        public void SetText(FlowDocument document, string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    XDocument xDocument = XDocument.Parse(text);
                    string xaml = HtmlToXamlConverter.ConvertXHtmlToXaml(xDocument, false);

                    TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xaml)))
                    {
                        tr.Load(ms, DataFormats.Xaml);
                    }

                    _xHtml = text;
                }
            }
            catch (Exception e)
            {
                // todo tb: Logging
                //log.Error("Data provided is not in the correct Xaml or XHtml format: {0}", e);
                throw e;
            }
        }
    }
}
