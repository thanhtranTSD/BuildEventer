
using BuildEventer.Command;
using BuildEventer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BuildEventer.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        #region Constructor

        public ConfigurationViewModel()
        {
            TestCreateViewModels();

            //TestCreateUserControls();

            var m_ActionTypes = GetAllActionTypes();

            m_AllViewModels = GetAllViewModels();

        }

        #endregion

        #region Private functions

        private void Apply()
        {

        }

        private void CreateAction()
        {
            int orderAction = m_ViewModels.Count + 1;
            CopyAction action = new CopyAction();
            action.Name = "Action " + orderAction.ToString();
            action.Sources = new BindingList<string> { "A1", "B1", "C1" };
            action.Destinations = new BindingList<string> { "D1", "E1", "F1" };
            CopyActionViewModel actionVM = new CopyActionViewModel(action);
            m_ViewModels.Add(actionVM);
        }

        private void DeleteActionViewModel()
        {
            m_ViewModels.RemoveAt(SelectedIndex);
        }

        private List<Type> GetAllActionTypes()
        {
            List<Type> actionTypes = new List<Type>();

            var entityTypes = from t in System.Reflection.Assembly.GetAssembly(typeof(IAction)).GetTypes()
                              where t.IsSubclassOf(typeof(IAction))
                              select t;

            foreach (var t in entityTypes)
            {
                actionTypes.Add(t);
            }
            return actionTypes;
        }

        private Dictionary<Type, UserControl> GetAllViewModels()
        {
            Dictionary<Type, UserControl> dict = new Dictionary<Type, UserControl>();

            var entityTypes = from t in System.Reflection.Assembly.GetAssembly(typeof(SettingsViewModelBase)).GetTypes()
                              where t.IsSubclassOf(typeof(SettingsViewModelBase))
                              select t;

            foreach (var t in entityTypes)
            {
                Type typeofView = GetViewTypeFromActionType(t);
                UserControl view = (UserControl)Activator.CreateInstance(typeofView);
                dict.Add(t, view);
            }
            return dict;
        }

        private Type GetViewTypeFromActionType(Type TypeName)
        {
            string fullName = TypeName.FullName.Replace("ViewModels", "Views");
            string newFullName = fullName.Remove(fullName.Length - 5);
            return Type.GetType(newFullName);
        }

        private void TestCreateViewModels()
        {
            //m_Actions = new BindingList<IAction>();

            //m_Actions.Add(new CopyAction { Name = "Action 1", Sources = new List<string> { "A1", "B1", "C1" }, Destinations = new List<string> { "D1", "E1", "F1" } });
            //m_Actions.Add(new CopyAction { Name = "Action 2", Sources = new List<string> { "A2", "B2", "C2" }, Destinations = new List<string> { "D2", "E2", "F2" } });
            //m_Actions.Add(new CopyAction { Name = "Action 3", Sources = new List<string> { "A3", "B3", "C3" }, Destinations = new List<string> { "D3", "E3", "F3" } });
            //m_Actions.Add(new DeleteAction { Name = "Action 4", Sources = new List<string> { "A4", "B4", "C4" }, Destinations = new List<string> { "D4", "E4", "F4" } });

            ViewModels.Add(new CopyActionViewModel(new CopyAction { Name = "Action 1", Sources = new BindingList<string> { "A1", "B1", "C1" }, Destinations = new BindingList<string> { "D1", "E1", "F1" } }));
            ViewModels.Add(new DeleteActionViewModel(new DeleteAction { Name = "Action 4", Sources = new BindingList<string> { "A4", "B4", "C4" }, Destinations = new BindingList<string> { "D4", "E4", "F4" } }));
            //ViewModels.Add(

            // CurrentViewModel = ViewModels[0];
            // ((CopyActionViewModel)CurrentViewModel).Name = "action1";
            //CurrentViewModel
        }

        private void TestCreateUserControls()
        {
            m_ViewUI = new UserControl();
        }

        private void SettingView()
        {
            if (null == m_SelectedModel)
            {
                m_ViewUI = null;
            }
            else
            {
                m_ViewUI = m_AllViewModels[m_SelectedModel.GetType()];
                m_ViewUI.DataContext = m_SelectedModel;
            }
        }

        private void ApplyAction()
        {
            
        }

        #endregion

        #region Properties

        public ICommand CreateActionCommand
        {
            get
            {
                if (m_CreateActionCommand == null)
                {
                    m_CreateActionCommand = new RelayCommand(param => CreateAction());
                }

                return m_CreateActionCommand;
            }
        }

        public ICommand ApplyCommand
        {
            get
            {
                return m_ApplyCommand ?? (m_ApplyCommand = new RelayCommand(p => ApplyAction()));
            }
        }            

        public ICommand DeleteSelectedActionCommand
        {
            get
            {
                if (m_DeleteSelectedActionCommand == null)
                {
                    m_DeleteSelectedActionCommand = new RelayCommand(param => DeleteActionViewModel());
                }

                return m_DeleteSelectedActionCommand;
            }
        }

        public BindingList<SettingsViewModelBase> ViewModels
        {
            get
            {
                if (null == m_ViewModels)
                {
                    m_ViewModels = new BindingList<SettingsViewModelBase>();
                }

                return m_ViewModels;
            }
        }

        public SettingsViewModelBase SelectedModel
        {
            get
            {
                return m_SelectedModel;
            }
            set
            {
                m_SelectedModel = value;
                SettingView();
                OnPropertyChanged("ViewUI");
                OnPropertyChanged("SelectedModel");
            }
        }

        public int SelectedIndex { get; set; }

        public UserControl ViewUI
        {
            get
            {
                return m_ViewUI;
            }
        }

        #endregion

        #region Members

        private ICommand m_ApplyCommand;
        private ICommand m_CreateActionCommand;
        private ICommand m_DeleteSelectedActionCommand;

        private BindingList<SettingsViewModelBase> m_ViewModels;


        private Dictionary<Type, UserControl> m_AllViewModels = new Dictionary<Type, UserControl>();

        private SettingsViewModelBase m_SelectedModel;

        private UserControl m_ViewUI;



        #endregion


    }
}
