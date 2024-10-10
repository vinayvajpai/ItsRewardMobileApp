using System;
using System.Collections.Generic;
using System.Linq;
using itsRewards.Extensions.Validations.Rules;
using itsRewards.Models.Base;

namespace itsRewards.Extensions.Validations
{
    public class ValidatableObject<T> : IBaseNotifyPropertyChanged, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public event EventHandler<T> ValueChanged;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                SetProperty(ref _errors, value);
            }
        }


        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
                _value = value;
                ValueChanged?.Invoke(this, value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                SetProperty(ref _isValid, value);
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations?.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors?.ToList();
            IsValid = !Errors.Any();
            return this.IsValid;
        }

        public void Reset(T value)
        {
            Value = value;
            Errors = new List<string>();
            IsValid = true;
        }
    }
}