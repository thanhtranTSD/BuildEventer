using System.Collections.Generic;
using System.ComponentModel;

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

        public DeleteAction(string DeleteName, string DeleteType, BindingList<string> DeleteSources, BindingList<string> DeleteDestinations)
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

        public BindingList<string> Sources
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

        public BindingList<string> Destinations
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

        private BindingList<string> m_Sources;

        private BindingList<string> m_Destinations;

        #endregion
    }
}
