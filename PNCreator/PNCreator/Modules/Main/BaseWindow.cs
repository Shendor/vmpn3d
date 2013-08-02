using System.Windows;
using System.Windows.Input;
using Microsoft.Windows.Shell;

namespace PNCreator.Modules.Main
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {
            var closeCommandBinding = new CommandBinding(SystemCommands.CloseWindowCommand, OnWindowClose);
            var minimizeCommandBinding = new CommandBinding(SystemCommands.MinimizeWindowCommand, OnWindowMinimize);
            var maximizeCommandBinding = new CommandBinding(SystemCommands.MaximizeWindowCommand, OnWindowMaximize);

            CommandBindings.Add(closeCommandBinding);
            CommandBindings.Add(minimizeCommandBinding);
            CommandBindings.Add(maximizeCommandBinding);
        }

        protected void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        protected virtual void Exit()
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Exit();
        }

        private void OnWindowMaximize(object sender, ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                SystemCommands.MaximizeWindow(this);
        }

        private void OnWindowMinimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
    }
}
