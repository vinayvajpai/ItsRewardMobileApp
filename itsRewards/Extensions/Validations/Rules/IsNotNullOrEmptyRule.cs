namespace itsRewards.Extensions.Validations.Rules
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is string)
            {
                var str = value as string;
                return !string.IsNullOrEmpty(str);
            }
            else
            {
                return true;
            }
        }
    }
}