using System;

namespace OptimizationLibrary.Exceptions
{
    public class FieldNotSetYetException : Exception
    {
        private readonly String fieldName = String.Empty;
        
        public override String Message => $"Field {fieldName} not set yet.";

        public FieldNotSetYetException() : base() { }

        public FieldNotSetYetException(String fieldName)
        {
            if (String.IsNullOrEmpty(fieldName) || String.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentOutOfRangeException(nameof(fieldName));
            
            this.fieldName = fieldName;
        }
    }
}