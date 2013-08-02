namespace WindowsControl
{
    public class DialogWindow
    {
        public static void Alert(string message)
        {
            var alertWnd = new Alert(message);
            alertWnd.ShowDialog();
        }

        public static void Error(string message)
        {
            var errorWnd = new Error(message);
            errorWnd.ShowDialog();
        }

        public static ButtonPressed Confirm(string message)
        {
            var confirmWnd = new Confirm(message);
            confirmWnd.ShowDialog();
            return confirmWnd.ButtonPressed;
        }
    }
}
