namespace WpfDemo.Command
{
    using System.Windows.Controls;

    using Thinknet.MVVM.Command;

    /// <summary>
    /// Interaction logic for CommandPanel.xaml
    /// </summary>
    public partial class CommandPanel : UserControl
    {
        public CommandPanel()
        {
            InitializeComponent();
            DataContext = new CommandViewModel(new CommandManagerWrapper());
        }
    }
}
