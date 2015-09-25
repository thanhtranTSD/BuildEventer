using BuildEventer.Class;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace BuildEventer.Explorer
{
    /// <summary>
    /// Interaction logic for ExplorerView.xaml
    /// </summary>
    public partial class ExplorerView : UserControl, INotifyPropertyChanged
    {
        public ExplorerView()
        {
            InitializeComponent();

            // TreeViewLoaded();
            base.MouseMove += new MouseEventHandler(base_MouseMove);
            base.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(base_PreviewMouseLeftButtonDown);
            base.DragEnter += new DragEventHandler(base_DragEnter);
            base.DragOver += new DragEventHandler(base_DragOver);
            base.DragLeave += new DragEventHandler(base_DragLeave);
            //base.Drop += new DragEventHandler(base_Drop);

            base.Loaded += new RoutedEventHandler(TreeViewLoaded);

            this.DataContext = this;
        }

        private void base_DragLeave(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void base_DragOver(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void base_DragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void base_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_StartPoint = e.GetPosition(null);
        }

        private void base_Drop(object sender, DragEventArgs e)
        {
            //TreeViewItem itemNode;
            //FindDropTarget((TreeView)sender, out itemNode, e);
            //TreeViewItem dropItem = (itemNode != null && itemNode.IsVisible ? itemNode.DataContext as TreeViewItem : null);
            //TreeViewItem dragItem = e.Data.GetData("myFormat") as TreeViewItem;
            //if (dropItem != null)
            //{
            //    //TreeView treeView = sender as TreeView;
            //    //Console.WriteLine("Index: " + (MyData.IndexOf(dropItem) + 1).ToString());
            //    //MyData.Remove(dragItem);
            //    ////MyData.Insert(MyData.IndexOf(dropItem) + 1, dragItem);
            //    //treeView.Insert(MyData.IndexOf(dropItem) >= 1 ? MyData.IndexOf(dropItem) : 0, dragItem);
            //    object newItem = new FolderItem();

            //    tvExplorer.Items.Add(newItem);
            //}
        }

        private void base_MouseMove(object sender, MouseEventArgs e)
        {
            ExplorerView explorerView = sender as ExplorerView;
            if (null != explorerView && e.LeftButton == MouseButtonState.Pressed)
            {
                TreeViewItem val = FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                //var mousePos = e.GetPosition(null);
                //var diff = m_StartPoint - mousePos;

                //if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                //    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                if (null != val)
                {
                    string fullPath = (string)val.Tag;

                    DragDropData data = new DragDropData();
                    data.IsFolder = Directory.Exists(fullPath);

                    string relativePath = fullPath.Substring(m_CurrentDirectory.Length, fullPath.Length - m_CurrentDirectory.Length);

                    if (string.Empty == relativePath)
                    {
                        return;
                    }
                    data.Path = relativePath;

                    DragDrop.DoDragDrop(tvExplorer, data, DragDropEffects.Copy);
                }

            }
        }


        // Helper to search up the VisualTree
        private static T FindAncestor<T>(DependencyObject current)
          where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void FindDropTarget(TreeView tv, out TreeViewItem pItemNode, DragEventArgs pDragEventArgs)
        {
            pItemNode = null;

            DependencyObject k = VisualTreeHelper.HitTest(tv, pDragEventArgs.GetPosition(tv)).VisualHit;

            while (k != null)
            {
                if (k is TreeViewItem)
                {
                    TreeViewItem treeNode = k as TreeViewItem;
                    //if (treeNode.DataContext is Family)
                    //{
                    pItemNode = treeNode;
                    //}
                }
                else if (k == tv)
                {
                    Console.WriteLine("Found treeview instance");
                    return;
                }

                k = VisualTreeHelper.GetParent(k);
            }
        }

        private void TreeViewLoaded()
        {
            string currentDirectory = @"D:\Working";
            bool hasDir = Directory.Exists(currentDirectory);

            string s = Path.GetFullPath(currentDirectory);

            m_ExpolrerTreeViewItems = new BindingList<TreeViewItem>();

            TreeViewItem item = new TreeViewItem();
            item.Header = s;
            item.Tag = s;
            item.FontWeight = FontWeights.Normal;
            item.Items.Add(dummyNode);
            item.Expanded += new RoutedEventHandler(folder_Expanded);

            m_ExpolrerTreeViewItems.Add(item);
        }

        private void TreeViewLoaded(object sender, RoutedEventArgs e)
        {
            m_CurrentDirectory = @"D:\Working";

            bool hasDir = Directory.Exists(m_CurrentDirectory);

            string s = Path.GetFullPath(m_CurrentDirectory);

            m_ExpolrerTreeViewItems = new BindingList<TreeViewItem>();

            TreeViewItem item = new TreeViewItem();
            item.Header = s;
            item.Tag = s;
            item.FontWeight = FontWeights.Normal;
            item.Items.Add(dummyNode);
            item.Expanded += new RoutedEventHandler(folder_Expanded);

            m_ExpolrerTreeViewItems.Add(item);
        }

        public string SelectedImagePath { get; set; }

        public string SelectedPath
        {
            get
            {
                return m_SelectedPath;
            }
            set
            {
                m_SelectedPath = value;
                OnPropertyChanged("SelectedPath");
            }
        }

        public BindingList<TreeViewItem> ExpolrerTreeViewItems
        {
            get
            {
                return m_ExpolrerTreeViewItems;
            }
            set
            {
                m_ExpolrerTreeViewItems = value;
                OnPropertyChanged("ExpolrerTreeViewItems");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string currentDirectory = @"D:\";
            //bool hasDir = Directory.Exists(currentDirectory);

            //string s = Path.GetFullPath(currentDirectory);

            //TreeViewItem item = new TreeViewItem();
            //item.Header = s;
            //item.Tag = s;
            //item.FontWeight = FontWeights.Normal;
            //item.Items.Add(dummyNode);
            //item.Expanded += new RoutedEventHandler(folder_Expanded);
            //foldersItem.Items.Add(item);


            //foreach (string s in Directory.GetDirectories(currentDirectory))
            //{
            //    TreeViewItem item = new TreeViewItem();
            //    item.Header = s;
            //    item.Tag = s;
            //    item.FontWeight = FontWeights.Normal;
            //    item.Items.Add(dummyNode);
            //    item.Expanded += new RoutedEventHandler(folder_Expanded);
            //    foldersItem.Items.Add(item);
            //}
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);

                    }

                    foreach (var file in Directory.GetFiles(item.Tag.ToString()))
                    {
                        TreeViewItem fileItem = new TreeViewItem();
                        fileItem.Header = file.Substring(file.LastIndexOf("\\") + 1);
                        fileItem.Tag = file;
                        fileItem.FontWeight = FontWeights.Normal;
                        item.Items.Add(fileItem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = (string)temp.Tag;
            //string temp1 = "";
            //string temp2 = "";
            //while (true)
            //{
            //    temp1 = temp.Header.ToString();
            //    if (temp1.Contains(@"\"))
            //    {
            //        temp2 = "";
            //    }
            //    SelectedImagePath = temp1 + temp2 + SelectedImagePath;
            //    if (temp.Parent.GetType().Equals(typeof(TreeView)))
            //    {
            //        break;
            //    }
            //    temp = ((TreeViewItem)temp.Parent);
            //    temp2 = @"\";
            //}
            //show user selected path

            //MessageBox.Show(SelectedImagePath);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private string m_SelectedPath;
        private BindingList<TreeViewItem> m_ExpolrerTreeViewItems;

        /// <summary>
        /// Warns the developer if this object does not have a public property with
        /// the specified name. This method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                Debug.Fail("Invalid property name: " + propertyName);
            }
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            //PropertyChangedEventHandler handler = PropertyChanged;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private object dummyNode;
        private Point m_StartPoint;
        private string m_CurrentDirectory;

    }
}
