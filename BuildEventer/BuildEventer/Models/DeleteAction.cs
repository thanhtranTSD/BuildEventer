using System.Collections.Generic;

namespace BuildEventer.Models
{
    public class DeleteAction : IAction
    {
        #region Constructor

        public DeleteAction()
        {
            this.m_Name = "Delete Action";
            this.m_Type = "Delete";
        }

        public DeleteAction(string DeleteName, string DeleteType)
        {
            this.m_Name = DeleteName;
            this.m_Type = DeleteType;
        }

        public DeleteAction(string DeleteName, string DeleteType, List<string> DeleteSources, List<string> DeleteDestinations)
        {
            this.m_Name = DeleteName;
            this.m_Type = DeleteType;
            this.m_Sources = DeleteSources;
            this.m_Destinations = DeleteDestinations;
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
