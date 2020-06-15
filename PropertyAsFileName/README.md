This example demonstrates how to automatically assign path and file name on SOLIDWORKS model save as.

Standard *Save As* dialog will not be shown rather the file will be automatically saved into the nominated folder.

This add-in will override all save as operations in parts, assemblies and drawings.

File will be saved into the temp directory.

File name will be read from the *Title* custom property (either configuration specific or model specific). If this property does not exist, unique file name will be assigned.

To test open or create any file and click *Save As* command.