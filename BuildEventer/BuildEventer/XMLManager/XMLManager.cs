using BuildEventer.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;

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
            List<IAction> Actions = new List<IAction>();
            Actions.Add(new CopyAction { Name = "Action_1", Type = "Copy", Sources = new System.ComponentModel.BindingList<string> { "a1", "a2" }, Destinations = new System.ComponentModel.BindingList<string> { "b1", "c1" } });
            Actions.Add(new CopyAction { Name = "Action_2", Type = "Copy", Sources = new System.ComponentModel.BindingList<string> { "a2", "a3" }, Destinations = new System.ComponentModel.BindingList<string> { "b2" } });

            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration(XML_VERSION, XML_ENCODING, null);
            doc.AppendChild(declaration);

            XmlElement actionsNode = doc.CreateElement(NODE_ACTIONS);

            foreach (IAction action in Actions)
            {
                XmlElement actionNode = doc.CreateElement(action.Name);
                actionNode.SetAttribute(ATTRIBUTE_TYPE, action.Type);
                actionNode.AppendChild(PathsToXMLNode(action.Sources, NODE_SOURCES, ref doc));
                actionNode.AppendChild(PathsToXMLNode(action.Destinations, NODE_DESTINATIONS, ref doc));

                actionsNode.AppendChild(actionNode);
            }
            doc.AppendChild(actionsNode);
            doc.Save(FilePath);
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

        private static XmlNode PathsToXMLNode(BindingList<string> Paths, string NodeName, ref XmlDocument Document)
        {
            XmlElement xmlNode = Document.CreateElement(NodeName);

            foreach (string path in Paths)
            {
                XmlElement xmlPath = Document.CreateElement("Path");
                xmlPath.InnerText = RELATIVE_PATH_SIGNATURE + path;
                xmlNode.AppendChild(xmlPath);
            }

            return xmlNode;
        }

        private const string RELATIVE_PATH_SIGNATURE = ".\\";
        private const string NODE_ACTIONS = "Actions";
        private const string NODE_SOURCES = "Sources";
        private const string NODE_DESTINATIONS = "Destinations";
        private const string ATTRIBUTE_TYPE = "type";
        private const string XML_VERSION = "1.0";
        private const string XML_ENCODING ="UTF-8";        
    }
}
