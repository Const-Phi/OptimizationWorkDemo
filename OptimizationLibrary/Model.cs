using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OptimizationLibrary.Properties;

namespace OptimizationLibrary
{
    public sealed class Model : INotifyPropertyChanged
    {
        #region Range of single dimensional optimization 

        private Range range;

        public Range Range
        {
            get => range;
            set
            {
                range = value ?? throw new ArgumentNullException(nameof(value));

                OnPropertyChanged(nameof(Range));
            }
        }

        #endregion

        #region Some coefficient

        private Double factor;

        public Double Factor
        {
            get => factor;
            set
            {
                factor = value;

                OnPropertyChanged(nameof(Factor));
            }
        }

        #endregion

        public Model()
        {
            Range = new Range();

            Range.PropertyChanged += OnRangePropertyChanged;
        }

        private void OnRangePropertyChanged(Object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChanged?.Invoke(new SenderWrapper(this, Range),
                new PropertyChangedEventArgs(propertyChangedEventArgs.PropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}