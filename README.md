# TfsNodeImportExport
This applications helps Import/Export TFS/VSTS Area Paths and Iterations.  

It is a WinForms app based on .NEWT 4.7 and can be used to connect to both TFS and VSTS based systems. 

Note: To import Iterations and Area paths, please make sure you have the correct permissions.

## How to use
The application its self it pretty simple to use

1. Run the application
2. Click the "Select a Project" button and add/connect to your TFS/VSTS instance
3. Select the project you want to import/export
4. Select Area Paths or Iterations
5. Click the operation you want (import or export)
6. Specify a file to read/write

## Main Screen

![Main Screen](https://github.com/ravensorb/TfsNodeImportExport/blob/master/docs/screenshots/main-screen.png?raw=true)

## Connect to TFS/VSTS Screen 

![Connect to Server Screen](https://github.com/ravensorb/TfsNodeImportExport/blob/master/docs/screenshots/connect-vsts.png?raw=true)

## File Structures
The files are simple JSON structure files and provide heirarchical structure and key attributes needed to describe the Area Paths or Iterations

### Area Paths

### Iterations