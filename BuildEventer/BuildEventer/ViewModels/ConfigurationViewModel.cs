
using BuildEventer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace BuildEventer.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        public ConfigurationViewModel()
        {
            TestCreateViewModels();

            TestCreateUserControls();

            m_TypeViewDictionary = GetAllType_View();

        }

        private Dictionary<Type, UserControl> m_TypeViewDictionary = new Dictionary<Type, UserControl>();

        private Dictionary<Type, UserControl> GetAllType_View()
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
            m_Actions = new Collection<IAction>();

            m_Actions.Add(new CopyAction { Name = "Action 1", Sources = new Collection<string> { "A1", "B1", "C1" }, Destinations = new Collection<string> { "D1", "E1", "F1" } });
            m_Actions.Add(new CopyAction { Name = "Action 2", Sources = new Collection<string> { "A2", "B2", "C2" }, Destinations = new Collection<string> { "D2", "E2", "F2" } });
            m_Actions.Add(new CopyAction { Name = "Action 3", Sources = new Collection<string> { "A3", "B3", "C3" }, Destinations = new Collection<string> { "D3", "E3", "F3" } });
            //m_Actions.Add(new CopyAction("Action 1", "Copy"));
            //m_Actions.Add(new CopyAction("Action 2", "Copy"));
            //m_Actions.Add(new CopyAction("Action 3", "Copy"));
            m_Actions.Add(new DeleteAction { Name = "Action 4", Sources = new Collection<string> { "A4", "B4", "C4" }, Destinations = new Collection<string> { "D4", "E4", "F4" } });
        }

        private void TestCreateUserControls()
        {
            //m_UserControls = new Collection<UserControl>();
            //m_UserControls.Add(new CopyActionView());
            m_UserControls = new UserControl();
        }

        private void SettingView()
        {
            m_UserControls = m_TypeViewDictionary[m_SelectedAction.GetType()];
            m_UserControls.DataContext = m_SelectedAction;
        }

        #region Properties

        private ICollection<IAction> m_Actions;
        public ICollection<IAction> Actions
        {
            get
            {
                return m_Actions;
            }
            set
            {
                if (value != m_Actions)
                {
                    m_Actions = value;
                    OnPropertyChanged("Actions");
                }
            }
        }

        private IAction m_SelectedAction;
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
                OnPropertyChanged("UserControls");
            }
        }

        private string m_SelectedActionName;
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

        private UserControl m_UserControls;
        public UserControl UserControls
        {
            get
            {
                return m_UserControls;
            }
        }

        #endregion
    }
}
