
namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class ProgressEventArgs : AbstractEventArgs
    {
        public ProgressEventArgs(double progress)
        {
            Progress = progress;
            MinimumProgress = 0;
        }

        public double Progress
        {
            get; set;
        }

        public double MinimumProgress
        {
            get; set;
        }

        public double MaximumProgress
        {
            get; set;
        }
    }
}
