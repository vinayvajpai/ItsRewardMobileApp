using System.Text.RegularExpressions;

namespace itsRewards.Extensions.Validations.Rules
{
    public class PasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            Regex regex = new Regex(@"^((?!.*[\s])(?=.*[a-z])(?=.{8,})(?=.*[A-Z])(?=.*\w)(?=.*[^\da-zA-Z]).{8,150})$");
            Match match = regex.Match(str);
            return match.Success;
        }
    }
}