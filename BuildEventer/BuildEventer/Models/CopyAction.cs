using System.Collections.Generic;
using System.ComponentModel;

namespace BuildEventer.Models
{
    public class CopyAction : IAction
    {

        #region Constructors

        public CopyAction()
        {
            this.m_Name = "Copy Action";
            this.m_Type = "Copy";
        }

        public CopyAction(string Name, string Type)
        {
            this.m_Name = Name;
            this.m_Type = Type;
        }

        public CopyAction(string Name, string Type, BindingList<string> Sources, BindingList<string> Destinations)
        {
            this.m_Name = Name;
            this.m_Type = Type;
            this.m_Sources = Sources;
            this.m_Destinations = Destinations;
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

        public override BindingList<string> Sources
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

        public override BindingList<string> Destinations
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

        #region Public Methods

        public override object Clone()
        {
            return (CopyAction)this.MemberwiseClone();
        }

        #endregion

        #region Members

        private string m_Name = string.Empty;
        private string m_Type = string.Empty;
        private BindingList<string> m_Sources;
        private BindingList<string> m_Destinations;

        #endregion
    }
}
