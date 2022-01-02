using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using static NAS_Files_Sender.Enums;

namespace NAS_Files_Sender.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public ViewModelBase()
        {
        }

        #region INotifyPropertyChanged members

        public virtual string DisplayName { get; protected set; }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        public bool HasErrors => _errorsByPropertyName.Any();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected virtual void NotifyPropertyChangedAll(object inOjbect)
        {
            foreach (PropertyInfo pi in inOjbect.GetType().GetProperties())
            {
                NotifyPropertyChanged(pi.Name);
            }
        }

        public virtual void Refresh()
        {
            NotifyPropertyChangedAll(this);
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }

        #endregion

        #region INotifyDataErrorInfo members

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Add specified error to the collection if not already present
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="error"></param>
        /// <param name="isWarning"></param>
        public void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        #endregion

        public void ShowErrorAndLogMessage(Enums.ErrorLevel errorLevel, string title, string message)
        {
            switch (errorLevel)
            {
                case ErrorLevel.Warning:
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;

                case ErrorLevel.Error:
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                case ErrorLevel.Fatal:
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            //TODO : Show a custom message box
        }
    }
}