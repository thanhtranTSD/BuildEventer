using BuildEventer.ViewModels;

namespace BuildEventer.Models
{
    public abstract class IAction : ViewModelBase
    {
        #region Properties

        public abstract string Name { get; set; }
        public abstract string Type { get; set; }

        #endregion
    }
}
