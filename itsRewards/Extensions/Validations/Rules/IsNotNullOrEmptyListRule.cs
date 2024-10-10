using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace itsRewards.Extensions.Validations.Rules
{
    public class IsNotNullOrEmptyListRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is List<object>)
            {
                var str = value as IList<object>;
                return str.Count() > 0;
            }
            if (value is ICollection<object>)
            {
                var str = value as ICollection<object>;
                return str.Count() > 0;
            }
            if (value is ObservableCollection<object>)
            {
                var str = value as ObservableCollection<object>;
                return str.Count() > 0;
            }
            else if (value is List<byte[]>)
            {
                var str = value as List<byte[]>;
                return str.Count() > 0;
            }
            else
            {
                return true;
            }
        }
    }
}
