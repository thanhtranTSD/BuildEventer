
using BuildEventer.Command;
using BuildEventer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
namespace BuildEventer.ViewModels
{
    public class CopyActionViewModel : SettingsViewModelBase
    {
        #region Constructors

        public CopyActionViewModel(CopyAction CopyAction)
        {
            m_Name = CopyAction.Name;
            m_Type = CopyAction.Type;

            m_Sources = new BindingList<string>();
            m_Destinations = new BindingList<string>();

            m_Sources = CopyAction.Sources;
            m_Destinations = CopyAction.Destinations;
            //BindingOperations.EnableCollectionSynchronization(Sources, _syncLock);
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

        #region Command

        // The RelayCommand that implements ICommand
        public ICommand PreviewDropCommand
        {
            get
            {
                return m_PreviewDropCommand ?? (m_PreviewDropCommand = new RelayCommand(HandlePreviewDrop));
            }
        }

        #endregion

        #region Private Functions

        // The method encapsulated in the relay command
        private void HandlePreviewDrop(object Object)
        {
            // MessageBox.Show("ABC");
            IDataObject ido = Object as IDataObject;
            if (null == ido) return;

            // Get all the possible format
            //string[] formats = ido.GetFormats();

            string data = ido.GetData(DataFormats.Text).ToString();
            m_Sources.Add(data);
            OnPropertyChanged("Sources");
            //string[] formats = ((IDataObject)SelectedModel).GetFormats();
            //(selectedViewModelType)SelectedModel
        }

        #endregion

        #region Members

        private string m_Name;
        private string m_Type;
        private BindingList<string> m_Sources;
        private BindingList<string> m_Destinations;

        private ICommand m_DropCommand;
        private ICommand m_PreviewDropCommand;
        private object _syncLock;

        #endregion
    }
}
