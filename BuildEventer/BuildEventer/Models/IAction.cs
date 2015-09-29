using BuildEventer.ViewModels;
using System;
using System.ComponentModel;

namespace BuildEventer.Models
{
    public abstract class IAction : ViewModelBase, ICloneable
    {
        #region Properties

        public abstract string Name { get; set; }
        public abstract string Type { get; set; }
        public abstract BindingList<string> Sources { get; set; }
        public abstract BindingList<string> Destinations { get; set; }

        public abstract object Clone();

        #endregion
    }
}
