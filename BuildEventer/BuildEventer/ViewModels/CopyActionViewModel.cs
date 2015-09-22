
using BuildEventer.Models;
using System.Collections.Generic;
namespace BuildEventer.ViewModels
{
    public class CopyActionViewModel : SettingsViewModelBase
    {
        #region Constructors

        public CopyActionViewModel(CopyAction CopyAction)
        {
            m_Name = CopyAction.Name;
            m_Type = CopyAction.Type;
            m_Sources = CopyAction.Sources;
            m_Destinations = CopyAction.Destinations;
        }

        #endregion

        #region Properties

        public override string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
                OnPropertyChanged("Name");
            }
        }

        public override string Type
        {
            get
            {
                return m_Type;
            }
            set
            {
                m_Type = value;
                OnPropertyChanged("Type");
            }
        }

        public List<string> Sources
        {
            get
            {
                return m_Sources;
            }
            set
            {
                if (value != m_Sources)
                {
                    m_Sources = value;
                    OnPropertyChanged("Sources");
                }
            }
        }

        public List<string> Destinations
        {
            get
            {
                return m_Destinations;
            }
            set
            {
                if (value != m_Destinations)
                {
                    m_Destinations = value;
                    OnPropertyChanged("Destinations");
                }
            }
        }

        #endregion

        #region Members

        private string m_Name;
        private string m_Type;
        private List<string> m_Sources;
        private List<string> m_Destinations;

        #endregion
    }
}
