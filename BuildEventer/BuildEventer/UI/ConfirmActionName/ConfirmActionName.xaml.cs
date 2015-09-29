using BuildEventer.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BuildEventer.UI.ConfirmActionName
{
    /// <summary>
    /// Interaction logic for Confirm_Action.xaml
    /// </summary>
    public partial class ConfirmActionName : Window, IDataErrorInfo
    {
        public ConfirmActionName(string DefaultActionName)
        {
            DefaultName = DefaultActionName;
            InitializeComponent();
            this.DataContext = this;
        }

        public string DefaultName { get; set; }
        public string NewName { get; set; }

        public ICommand NewNameCommand
        {
            get
            {
                return m_NewNameCommand ?? (m_NewNameCommand = new RelayCommand(p => ApplyNewName(), p => CanExecute()));
            }
        }

        private bool CanExecute()
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(NewName) || ("AAA" == NewName))
            {
                result = false;
            }
            return result;
        }

        private void ApplyNewName()
        {

            this.DialogResult = false;
        }

        private ICommand m_NewNameCommand;

        #region IDataErrorInfo

        string IDataErrorInfo.Error
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }



        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        #endregion

        #region Validation

        public static readonly string[] s_ValidateProperties =
        {
            "NewName"
        };

        public bool IsValid
        {
            get
            {
                foreach (string property in s_ValidateProperties)
                {
                    if (GetValidationError(property) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "NewName":
                    error = ValidateNewName();
                    break;
            }
            return error;
        }

        private string ValidateNewName()
        {
            if (true == string.IsNullOrWhiteSpace(NewName))
            {
                return "New name can not be empty";
            }
            return null;
        }


        #endregion
    }
}
