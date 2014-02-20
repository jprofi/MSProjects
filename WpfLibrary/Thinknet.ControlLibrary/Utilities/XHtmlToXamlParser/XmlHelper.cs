namespace Thinknet.ControlLibrary.Utilities.XHtmlToXamlParser
{
    using System.Xml;

    /// <summary>
    /// Helper methods for handle XHtml.
    /// </summary>
    internal static class XmlHelper
    {
        /// <summary>
        /// Returns a value for an attribute by its name (ignoring casing)
        /// </summary>
        /// <param name="element">
        /// XmlElement in which we are trying to find the specified attribute
        /// </param>
        /// <param name="attributeName">
        /// String representing the attribute name to be searched for
        /// </param>
        /// <returns>The value of a specific attribute.</returns>
        internal static string GetAttribute(XmlElement element, string attributeName)
        {
            attributeName = attributeName.ToLower();

            for (int i = 0; i < element.Attributes.Count; i++)
            {
                if (element.Attributes[i].Name.ToLower() == attributeName)
                {
                    return element.Attributes[i].Value;
                }
            }

            return null;
        }
    }
}
