using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BuildEventer.XMLManager
{
    public static class XMLManager
    {
        //public XMLManager() { }
        // Loading actions

        //public T ObservableCollection<T> LoadActionsManager(string Path)
        //    where T is class
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(savefile_xml);
        //    ObservableCollection<IAction> actions = new ObservableCollection<Action>();
        //    XmlElement objects = xmlDocument.DocumentElement;
        //    XmlNode action = objects.SelectSingleNode("Actions");
        //    XmlNodeList copies = action.SelectNodes("Copy");
        //    int index = 0;
        //    foreach (XmlNode itemcopy in copies)
        //    {
        //        foreach (XmlNode itempara in itemcopy.ChildNodes)
        //        {
        //            if (itempara.Name == "Destination")
        //                actions.Add(new CopySourcesToDestination(new PathTreeNodeData(itempara.InnerText)));
        //            if (itempara.Name == "Sources")
        //            {
        //                XmlNodeList Sources = itempara.SelectNodes("Source");
        //                foreach (XmlNode itemsource in Sources)
        //                    (actions[index] as CopySourcesToDestination).Sources.Add(new PathTreeNodeData(itemsource.InnerText));
        //            }
        //        }
        //        ++index;
        //    }
        //    xmlDocument = null;
        //    return actions;
        //}

        //private XElement AddContentOfPathNodesToXElement(string iParent, string iSub, PathTreeNodeData iRootNode)
        //{
        //    XElement parents = new XElement(iParent);
        //    foreach (PathTreeNodeData sub in iRootNode.Children)
        //        parents.Add(new XElement(iSub, sub.Path));
        //    return parents;
        //}

        public static void GenerateXml(string FilePath)
        {
            XDocument xmlFile = new XDocument(new XElement("Objects"));
            xmlFile.Element("Objects").Add("a");
            xmlFile.Save(FilePath);
            XmlDocument xml = new XmlDocument();
            XmlWriter xmlWriter = XmlWriter.Create("a.xml");
        }

        public static string GetFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml files (*.xml)|*.xml";
            ofd.Title = "Load XML File";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (true == ofd.ShowDialog())
                return ofd.FileName;
            return null;
        }

        public static string GetPathToSaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files (*.xml)|*.xml";
            sfd.FileName = "BuildEventer";
            sfd.Title = "Save XML File";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            if (true == sfd.ShowDialog())
            {
                return sfd.FileName;
            }
            return null;
        }
    }
}
