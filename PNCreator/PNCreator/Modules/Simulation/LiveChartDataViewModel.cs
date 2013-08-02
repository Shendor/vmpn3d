using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace PNCreator.Modules.Simulation
{
    public class LiveChartDataViewModel<T> : ViewModelBase where T : ChartData
    {
        private const double STEP_RATIO = 0.1;

        private RadObservableCollection<T> goodData;
        private RadObservableCollection<T> badData;
        private double minimumY;
        private double maximumY;
        private double stepY;

        public LiveChartDataViewModel()
        {
            FillData();
        }

        public void ClearData()
        {
            GoodData.Clear();
            BadData.Clear();
        }

        public RadObservableCollection<T> GoodData
        {
            get
            {
                return goodData;
            }
            set
            {
                if (goodData != value)
                {
                    goodData = value;
                    OnPropertyChanged("GoodData");
                }
            }
        }

        public RadObservableCollection<T> BadData
        {
            get
            {
                return badData;
            }
            set
            {
                if (badData != value)
                {
                    badData = value;
                    OnPropertyChanged("BadData");
                }
            }
        }

        public double MinimumY
        {
            get { return minimumY; }
            set
            {
                if (!minimumY.Equals(value))
                {
                    minimumY = value;
                    OnPropertyChanged("MinimumY");
                }
            }
        }

        public double MaximumY
        {
            get
            {
                return maximumY;
            }
            set
            {
                if (!maximumY.Equals(value))
                {
                    maximumY = value;
                    OnPropertyChanged("MaximumY");
                }
            }
        }

        public double StepY
        {
            get
            {
                return stepY;
            }
            set
            {
                if (!stepY.Equals(value))
                {
                    stepY = value;
                    OnPropertyChanged("StepY");
                }
            }
        }

        public void AddGoodData(T data)
        {
            AddData(GoodData, data);
        }


        public void AddBadData(T data)
        {
            AddData(BadData, data);
        }


        private void AddData(RadObservableCollection<T> collection, T data)
        {
            collection.SuspendNotifications();

            if(collection.Count >= 20)
                collection.RemoveAt(0);
            collection.Add(data);
            RecalculateAxisYRange();
            collection.ResumeNotifications();
        }

        private void RecalculateAxisYRange()
        {
            minimumY = double.MaxValue;
            maximumY = 0;
            
            FindMinMaxAxisYValues(GoodData);
            FindMinMaxAxisYValues(BadData);

            StepY = STEP_RATIO * maximumY;
            MinimumY = minimumY - stepY;
            MaximumY = maximumY + stepY;
        }

        private void FindMinMaxAxisYValues(RadObservableCollection<T> collection)
        {
            foreach (var data in collection)
            {
                if (data.Value != null)
                {
                    if (data.Value < minimumY)
                    {
                        minimumY = (double) data.Value;
                    }
                    if (data.Value > maximumY)
                    {
                        maximumY = (double) data.Value;
                    }
                }
            }
        }


        private void FillData()
        {
            GoodData = new RadObservableCollection<T>();
            BadData = new RadObservableCollection<T>();
        }
    }
}

