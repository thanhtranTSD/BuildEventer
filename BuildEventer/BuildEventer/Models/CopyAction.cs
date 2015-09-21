using System.Collections.Generic;

namespace BuildEventer.Models
{
    public class CopyAction : IAction
    {
        public CopyAction()
        {
            this.Name = "Copy Action";
            this.Type = "Copy";
        }

        public CopyAction(string ActionName, string ActionType)
        {
            this.Name = ActionName;
            this.Type = ActionType;
        }

        #region Properties

        private string m_Name = string.Empty;
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

        private string m_Type = string.Empty;
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

        private ICollection<string> m_Sources;
        public ICollection<string> Sources
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

        private ICollection<string> m_Destinations;
        public ICollection<string> Destinations
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
    }
}
