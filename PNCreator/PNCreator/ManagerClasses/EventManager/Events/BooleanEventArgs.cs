
namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class BooleanEventArgs : AbstractEventArgs
    {
        public BooleanEventArgs(bool isChecked)
        {
            IsChecked = isChecked;
        }

        public bool IsChecked
        {
            get;
            set;
        }
    }
}
