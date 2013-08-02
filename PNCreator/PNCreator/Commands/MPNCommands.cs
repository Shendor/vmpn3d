using System.Windows.Input;

namespace PNCreator.Commands
{
    public static class MPNCommands
    {
        private static readonly RoutedUICommand perspectiveViewCommand;
        private static readonly RoutedUICommand frontViewCommand;

        static MPNCommands()
        {
            //ApplicationCommands.SaveAs.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control & ModifierKeys.Shift));

            perspectiveViewCommand = new RoutedUICommand("PerspectiveView","PerspectiveView",typeof(MPNCommands));
            perspectiveViewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));

            frontViewCommand = new RoutedUICommand("FrontView", "FrontView", typeof(MPNCommands));
            frontViewCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));

            
        }

        public static RoutedUICommand PerspectiveView
        {
            get { return perspectiveViewCommand; }
        }

        public static RoutedUICommand FrontView
        {
            get { return frontViewCommand; }
        }
    }
}
