//-----------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="My Company">
//     Some info
// </copyright>
//-----------------------------------------------------------------------

namespace WPF_People_lab.ViewModel
{
    using System;
    using System.ComponentModel;
    
    /// <summary>
    /// Abstract base class for view model classes 
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.  
        /// </summary>
        protected ViewModelBase()
        {
        }

        /// <summary>
        /// Event on case if property will be changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method which handle properties' changings
        /// </summary>
        /// <param name="propertyName">which property has been changed</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Frees unmanaged resources
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// How program must free resources
        /// </summary>
        protected virtual void OnDispose()
        {
        }
    }
}
