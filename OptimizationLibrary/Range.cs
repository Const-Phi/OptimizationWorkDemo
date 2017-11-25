using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OptimizationLibrary.Exceptions;
using OptimizationLibrary.Properties;

namespace OptimizationLibrary
{
    public sealed class Range : INotifyPropertyChanged
    {
        #region Left bound
        
        private Double? left;

        public Double Left
        {
            get
            {
                if (!left.HasValue)
                    throw new FieldNotSetYetException(nameof(left));

                return left.Value;
            }
            set
            {
                if (left.HasValue && right.HasValue && value > right.Value)
                    throw new Exception("Left bound can not be greater than right.");
                
                left = value; 
                
                OnPropertyChanged(nameof(Left));
                OnPropertyChanged(nameof(Length));
                OnPropertyChanged(nameof(Mean));
            }
        }

        #endregion

        #region Right bound

        private Double? right;

        public Double Right
        {
            get
            {
                if (!right.HasValue)
                    throw new FieldNotSetYetException(nameof(right));

                return right.Value;
            }
            set
            {
                if (left.HasValue && right.HasValue && left.Value > value)
                    throw new Exception("Left bound can not be greater than right.");
                
                right = value; 
                
                OnPropertyChanged(nameof(Right));
                OnPropertyChanged(nameof(Length));
                OnPropertyChanged(nameof(Mean));
            }
        }
        
        #endregion
        
        public Double Length => Right - Left;

        public Double Mean => (Right + Left) / 2;

        public Boolean Contains(Double value) => Left <= value && value <= Right;

        public Range(Double left = 0, Double right = 0)
        {
            Left = left;
            Right = right;
        }
        
        public override String ToString() => $"[{Left}; {Right}]";
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}