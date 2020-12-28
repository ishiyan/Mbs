using System.Windows;

// ReSharper disable once CheckNamespace
namespace TheRThemes
{
#pragma warning disable SA1601 // Partial elements should be documented
    // ReSharper disable once UnusedMember.Global
    public partial class LightTheme
#pragma warning restore SA1601
    {
        public static void CloseWind(Window window) => window.Close();

        public static void MaximizeRestore(Window window)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        public static void MinimizeWind(Window window) => window.WindowState = WindowState.Minimized;

        private void CloseWindow_Event(object sender, RoutedEventArgs e)
        {
            if (e.Source != null)
            {
                try
                {
                    CloseWind(Window.GetWindow((FrameworkElement)e.Source));
                }
                catch
                {
                    // Ignore.
                }
            }
        }

        private void AutoMinimize_Event(object sender, RoutedEventArgs e)
        {
            if (e.Source != null)
            {
                try
                {
                    MaximizeRestore(Window.GetWindow((FrameworkElement)e.Source));
                }
                catch
                {
                    // Ignore.
                }
            }
        }

        private void Minimize_Event(object sender, RoutedEventArgs e)
        {
            if (e.Source != null)
            {
                try
                {
                    MinimizeWind(Window.GetWindow((FrameworkElement)e.Source));
                }
                catch
                {
                    // Ignore.
                }
            }
        }
    }
}