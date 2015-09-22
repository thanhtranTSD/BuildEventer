using BuildEventer.ViewModels;
using System.Windows.Controls;

namespace BuildEventer.Views
{
    /// <summary>
    /// Interaction logic for ActionConfigurationView.xaml
    /// </summary>
    public partial class ActionConfigurationView : UserControl
    {
        public ActionConfigurationView()
        {
            InitializeComponent();
            this.DataContext = new ConfigurationViewModel();
        }
    }
}
