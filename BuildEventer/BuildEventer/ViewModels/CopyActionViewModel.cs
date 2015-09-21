
using BuildEventer.Models;
namespace BuildEventer.ViewModels
{
    public class CopyActionViewModel : SettingsViewModelBase
    {
        public CopyActionViewModel(CopyAction Action)
        {
            this.m_CopyAction = Action;
        }

        private CopyAction m_CopyAction;
        public override string Name { get; set; }

        public override string Type { get; set; }
    }
}
