
namespace BuildEventer.ViewModels
{
    public abstract class SettingsViewModelBase : ViewModelBase
    {
        #region Properties

        public abstract string Name { get; set; }
        public abstract string Type { get; set; }

        #endregion
    }
}
