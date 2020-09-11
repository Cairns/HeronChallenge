using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Heron.Common
{
    public class ValidatableBase : BindableBase, IValidatableBase
    {
        #region BindableBase Members
        public override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            base.SetProperty<T>(ref storage, value, propertyName);
            ValidateProperty<T>(value, propertyName);
            IsChanged = true;
            return true;
        }
        #endregion

        #region INotifyDataErrorInfo Members
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();

        public event EventHandler<System.ComponentModel.DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual void OnPropertyErrorsChanged(string propertyName)
        {
            var handler = this.ErrorsChanged;
            if (handler != null)
            {
                handler.Invoke(this, new System.ComponentModel.DataErrorsChangedEventArgs(propertyName));
            }
        }

        public System.Collections.Generic.IEnumerable<string> GetErrors()
        {
            return this.GetErrors(null) as IEnumerable<string>;
        }

        public System.Collections.IEnumerable GetErrors(string propertyName = null)
        {
            List<string> errorsList = new List<string>();
            if (propertyName != null)
            {
                _errorMessages.TryGetValue(propertyName, out errorsList);
                return errorsList;
            }
            else
            {
                foreach (KeyValuePair<string, List<string>> keyValuePair in this._errorMessages)
                {
                    errorsList.AddRange(keyValuePair.Value);
                }
                return errorsList;
            }
        }

        public bool HasErrors
        {
            get
            {
                try
                {
                    var errorsCount = _errorMessages.Values.FirstOrDefault(r => r.Count > 0);
                    if (errorsCount != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                { }
                return true;
            }
        }

        public Dictionary<string, List<string>> Errors
        {
            get
            {
                return this._errorMessages;
            }
        }
        #endregion

        public string ErrorMessage
        {
            get
            {
                return String.Join<string>(Environment.NewLine, this.GetErrors());
            }
        }

        public virtual void Validate()
        {
            //var results = new List<ValidationResult>();
            //ValidationContext context = new ValidationContext(this);
            //Validator.TryValidateObject(this, context, results);

            //if (results.Any())
            //{
            //    Errors[""] = results.Select(v => v.ErrorMessage).ToList();
            //}
            //else
            //{
            //    Errors.Remove("");
            //}

            //this.OnPropertyErrorsChanged("");
        }

        public void ValidateProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            var results = new List<ValidationResult>();

            ValidationContext context = new ValidationContext(this);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {
                Errors[propertyName] = results.Select(v => v.ErrorMessage).ToList();
            }
            else
            {
                Errors.Remove(propertyName);
            }

            this.OnPropertyErrorsChanged(propertyName);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
