namespace WpfDemo
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    using Thinknet.ControlLibrary.Native;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _showSpecCursor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnChangeCursorClicked(object sender, RoutedEventArgs e)
        {
            if (!_showSpecCursor)
            {
                _showSpecCursor = true;

                GhostCursor cursor;

                try
                {
                    double scaleX = double.Parse(TxtScaleX.Text);
                    double scaleY = double.Parse(TxtScaleY.Text);
                    double opacity = double.Parse(TxtOpacity.Text);

                    cursor = new GhostCursor((Visual)sender, scaleX, scaleY, opacity, CursorViewBox);
                }
                catch (Exception exception)
                {
                    cursor = new GhostCursor((Visual)sender, CursorViewBox);
                }



                Mouse.OverrideCursor = cursor.Cursor;
            }
            else
            {
                _showSpecCursor = false;
                Mouse.OverrideCursor = null;
            } 
        }
    }
}
