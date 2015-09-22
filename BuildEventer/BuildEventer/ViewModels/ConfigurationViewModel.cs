
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

            TestCreateUserControls();

            m_TypeViewDictionary = GetAllActionTypes();

        }

        #endregion

        #region Private functions

        private void Apply()
        { 
        
        }

        private void CreateAction()
        {
            int orderAction = m_Actions.Count + 1;
            m_Actions.Add(new DeleteAction { Name = "Action " + orderAction.ToString(), Sources = new List<string> { "A5" }, Destinations = new List<string> { "D5" } });
        }

        private Dictionary<Type, UserControl> GetAllActionTypes()
        {
            Dictionary<Type, UserControl> dict = new Dictionary<Type, UserControl>();

            var entityTypes = from t in System.Reflection.Assembly.GetAssembly(typeof(IAction)).GetTypes()
                              where t.IsSubclassOf(typeof(IAction))
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
            string fullName = TypeName.FullName.Replace("Models", "Views");
            fullName = string.Format("{0}View", fullName);
            return Type.GetType(fullName);
        }

        private void TestCreateViewModels()
        {
            m_Actions = new BindingList<IAction>();

            m_Actions.Add(new CopyAction { Name = "Action 1", Sources = new List<string> { "A1", "B1", "C1" }, Destinations = new List<string> { "D1", "E1", "F1" } });
            m_Actions.Add(new CopyAction { Name = "Action 2", Sources = new List<string> { "A2", "B2", "C2" }, Destinations = new List<string> { "D2", "E2", "F2" } });
            m_Actions.Add(new CopyAction { Name = "Action 3", Sources = new List<string> { "A3", "B3", "C3" }, Destinations = new List<string> { "D3", "E3", "F3" } });
            m_Actions.Add(new DeleteAction { Name = "Action 4", Sources = new List<string> { "A4", "B4", "C4" }, Destinations = new List<string> { "D4", "E4", "F4" } });

            ViewModels.Add(new CopyActionViewModel(new CopyAction { Name = "Action 1", Sources = new List<string> { "A1", "B1", "C1" }, Destinations = new List<string> { "D1", "E1", "F1" } }));
            ViewModels.Add(new DeleteActionViewModel(new DeleteAction { Name = "Action 4", Sources = new List<string> { "A4", "B4", "C4" }, Destinations = new List<string> { "D4", "E4", "F4" } }));

            CurrentViewModel = ViewModels[0];
            ((CopyActionViewModel)CurrentViewModel).Name = "action1";
            //CurrentViewModel
        }

        private void TestCreateUserControls()
        {
            m_Views = new UserControl();
        }

        private void SettingView()
        {
            m_Views = m_TypeViewDictionary[m_SelectedAction.GetType()];
            m_Views.DataContext = m_SelectedAction;
            //m_Views.DataContext = CurrentViewModel;// 
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
                if (m_ApplyCommand == null)
                {
                    m_ApplyCommand = new RelayCommand(param => Apply(), true);
                }

                return null;
            }
        }

        public List<SettingsViewModelBase> ViewModels
        {
            get
            {
                if (null == m_ViewModels)
                {
                    m_ViewModels = new List<SettingsViewModelBase>();
                }

                return m_ViewModels;
            }
        }

        public SettingsViewModelBase CurrentViewModel
        {
            get
            {
                return m_CurrentViewModel;
            }
            set
            {
                if (m_CurrentViewModel != value)
                {
                    m_CurrentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }

        public BindingList<IAction> Actions
        {
            get
            {
                return m_Actions;
            }
        }

        public IAction SelectedAction
        {
            get
            {
                return m_SelectedAction;
            }
            set
            {
                m_SelectedAction = value;
                SettingView();
                OnPropertyChanged("SelectedAction");
                OnPropertyChanged("SelectedActionName");
                OnPropertyChanged("Views");
            }
        }

        public string SelectedActionName
        {
            get
            {
                if (null == m_SelectedAction)
                {
                    return null;
                }
                else
                {
                    return m_SelectedAction.Type;
                }
            }
            set
            {
                m_SelectedActionName = value;
                OnPropertyChanged("SelectedAction");
            }
        }

        public UserControl Views
        {
            get
            {
                return m_Views;
            }
        }

        #endregion

        #region Members

        private ICommand m_CreateActionCommand;

        private List<SettingsViewModelBase> m_ViewModels;

        private SettingsViewModelBase m_CurrentViewModel;

        private Dictionary<Type, UserControl> m_TypeViewDictionary = new Dictionary<Type, UserControl>();
        private BindingList<IAction> m_Actions;
        private IAction m_SelectedAction;
        private string m_SelectedActionName;
        private UserControl m_Views;
        private RelayCommand m_ApplyCommand;

        #endregion


    }
}
