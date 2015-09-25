
using BuildEventer.Class;
using BuildEventer.Command;
using BuildEventer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

        public int SelectedIndexSources { get; set; }

        public int SelectedIndexDestinations { get; set; }

        #endregion

        #region Command

        public ICommand DeleteSourceItemCommand
        {
            get
            {
                if (m_DeleteSourceItemCommand == null)
                {
                    m_DeleteSourceItemCommand = new RelayCommand(param => DeleteItemSources(SelectedIndexSources));
                }

                return m_DeleteSourceItemCommand;
            }
        }

        public ICommand DeleteDestItemCommand
        {
            get
            {
                if (m_DeleteDestItemCommand == null)
                {
                    m_DeleteDestItemCommand = new RelayCommand(param => DeleteItemDestinations(SelectedIndexDestinations));
                }

                return m_DeleteDestItemCommand;
            }
        }

        public ICommand PreviewDropToSourceCommand
        {
            get
            {
                return m_PreviewDropToSourceCommand ?? (m_PreviewDropToSourceCommand = new RelayCommand(HandlePreviewDropToSource));
            }
        }

        public ICommand PreviewDropToDestinationCommand
        {
            get
            {
                return m_PreviewDropToDestinationCommand ?? (m_PreviewDropToDestinationCommand = new RelayCommand(HandlePreviewDropToDestination));
            }
        }

        #endregion

        #region Private Functions

        private void DeleteItemSources(int Index)
        {
            m_Sources.RemoveAt(Index);
        }

        private void DeleteItemDestinations(int Index)
        {
            m_Destinations.RemoveAt(Index);
        }

        // The method for Sources
        private void HandlePreviewDropToSource(object Object)
        {
            IDataObject ido = Object as IDataObject;

            if (null == ido)
            {
                return;
            }

            DragDropData dropData = (DragDropData)ido.GetData(typeof(DragDropData));
            foreach (string source in m_Sources)
            {
                if (source == dropData.Path)
                {
                    MessageBox.Show(dropData.Path.ToString() + " has already in Sources.");
                    return;
                }
            }
            m_Sources.Add(dropData.Path);
            OnPropertyChanged("Sources");
        }

        private void HandlePreviewDropToDestination(object Object)
        {
            IDataObject ido = Object as IDataObject;

            if (null == ido)
            {
                return;
            }

            DragDropData dropData = (DragDropData)ido.GetData(typeof(DragDropData));

            if (false == dropData.IsFolder)
            {
                MessageBox.Show("Path must be a folder.");
                return;
            }

            foreach (string dest in m_Destinations)
            {
                if (dest == dropData.Path)
                {
                    MessageBox.Show(dropData.Path.ToString() + " has already in Destinations.");
                    return;
                }
            }

            m_Destinations.Add(dropData.Path);
            OnPropertyChanged("Destinations");
        }

        #endregion

        #region Members

        private string m_Name;
        private string m_Type;
        private BindingList<string> m_Sources;
        private BindingList<string> m_Destinations;

        private object _syncLock;
        private ICommand m_PreviewDropToSourceCommand;
        private ICommand m_PreviewDropToDestinationCommand;
        private ICommand m_DeleteDestItemCommand;
        private ICommand m_DeleteSourceItemCommand;

        #endregion
    }
}
