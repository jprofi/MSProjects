namespace WpfDemo.Tooltip
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    using Thinknet.MVVM.Command;
    using Thinknet.MVVM.ViewModel;

    public class TooltipViewModel : ViewModel
    {
        private RelayCommand<Uri> _hyperLinkCommand;

        public string XHtml
        {
            get { return LoadXmlFromAssembly("TestForTooltip.xml").ToString(); }
        }


        /// <summary>
        /// Gets the command for processing Hyperlink Actions.
        /// </summary>
        public RelayCommand<Uri> HyperLinkCommand
        {
            get
            {
                return _hyperLinkCommand = _hyperLinkCommand ?? new RelayCommand<Uri>(ProcessHyperlink, CanProcessHyperlink);
            }
        }

        /// <summary>
        /// Should the specific hyperlink be processed.
        /// </summary>
        /// <param name="uri">The uri for the hyperlink action.</param>
        /// <returns>Whether the hyperlink should be processed or not.</returns>
        protected virtual bool CanProcessHyperlink(Uri uri)
        {
            return true;
        }

        /// <summary>
        /// Default action for a hyperlink action.
        /// Starts the browser of the application with the specifi uri.
        /// Can be overriden for customized behavior.
        /// </summary>
        /// <param name="uri">The uri to process.</param>
        protected virtual void ProcessHyperlink(Uri uri)
        {
            System.Diagnostics.Process.Start(uri.ToString());
        }

        /// <summary>
        /// Loads an xml file out of the assembly
        /// </summary>
        /// <param name="referenceXmlFileName">The xml file name without package.</param>
        /// <returns>The loaded xml file as XDocument.</returns>
        private static XDocument LoadXmlFromAssembly(string referenceXmlFileName)
        {
            Stream inputStream = typeof(TooltipViewModel).Module.Assembly.GetManifestResourceStream(string.Format("WpfDemo.Tooltip.{0}", referenceXmlFileName));
            XDocument referenceXml = XDocument.Load(inputStream);
            return referenceXml;
        }
    }
}
