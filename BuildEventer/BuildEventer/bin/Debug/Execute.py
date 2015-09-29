import os
from distutils.dir_util import copy_tree
from distutils.file_util import copy_file
import xml.etree.ElementTree as ET

### Define the action  type
##class ActionType:
##    Copy = "Copy"
##    Delete = "Delete"


def GetActionSources(iAction):
    paths = []
    sources = iAction.find("Sources")
    for path in sources:
        path = path.text.replace(".\\", "", 1)
        paths.append(path.strip())
    return paths

def GetActionDestinations(iAction):
    paths = []
    sources = iAction.find("Destinations")
    for path in sources:
        path = path.text.replace(".\\", "", 1)
        paths.append(path.strip())
    return paths

def PrintCopyTo(iSource, iDestination):
    print "{:>9s} {} to {}".format("Copy", iSource, iDestination)

def PrintCopySuccess():
    print "{:>18s}".format("Copy success.")

def CopyFolder(iSource, iDestination):
    if True == os.path.isdir(iSource):
        newDestination = os.path.join(iDestination, os.path.basename(iSource))
        PrintCopyTo(iSource, newDestination)
        copy_tree(iSource, newDestination)
        PrintCopySuccess()
    else:
        if False == os.path.exists(iDestination):
            os.makedirs(iDestination)
        PrintCopyTo(iSource, iDestination)
        copy_file(iSource, iDestination)
        PrintCopySuccess()

def DoCopyAction(iAction, iSources, iDestinations):
    print "Execute " + str(iAction.tag)
    for source in iSources:
        for dest in iDestinations:
            CopyFolder(source, dest)
    print str(iAction.tag) + " success."


# Main function
if __name__ == "__main__":
    tree = ET.parse("Actions.xml")
    root = tree.getroot()
    
    print "Current working directory is: {0}".format(os.getcwd())
    
    for action in root:
        sources = GetActionSources(action)
        destinations = GetActionDestinations(action)

##        actionType = action.get("type")
##        print (actionType == ActionType.Copy)
        
        DoCopyAction(action, sources, destinations)



