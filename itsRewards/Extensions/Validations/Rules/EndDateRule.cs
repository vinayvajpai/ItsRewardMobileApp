using System;

namespace itsRewards.Extensions.Validations.Rules
{
    public class EndDateRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public Func<T> CompareFunction { get; set; }

        public bool Check(T value)
        {
            if (CompareFunction == null)
                throw new ArgumentException();

            var compareValue = CompareFunction();

            if (value == null && compareValue == null)
                return true;

            if (value != null && compareValue == null)
                return false;

            if (value == null && compareValue != null)
                return false;

            var enddate = value as DateTime?;
            var startdate = compareValue as DateTime?;

            return enddate > startdate;
        }
    }
}