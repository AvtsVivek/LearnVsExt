
# VSixCommunity Command Example.

## References
 
https://github.com/VsixCommunity/Samples/tree/master/InsertGuid

https://learn.microsoft.com/en-us/visualstudio/extensibility/vsix/get-started/first-extension

https://www.vsixcookbook.com/getting-started/your-first-extension.html

https://www.vsixcookbook.com/recipes/menus-buttons-commands.html#define-the-command

## How this project is built.

Use the following.

![Visual Studio Command Community Project](./images/50VSixCommandCommunityProject50.jpg)

Once installed, my command.

![My Command Visual Studio](./images/55MyCommand50.jpg)

Solution Explorer.

![Solution Explorer](./images/57SolutionExplorer50.jpg)

The File changes to the VSCommandTable.vsct are as follows.

![File Changes](./images/58CommandTableFileChanges50.jpg)

## Build and Run

Invoke the Command without any file opened in the editor. 
![Invoke the command](images/60_50InvokeCommandFromEditMenu.jpg)

You will get a Message as follows.
![Message Box](images/70_50MessageBox.jpg)

Now open any text file, then place the curser where you want to insert a randonly generated Guid.

Insert Guid
![Insert Guid](images/80_50InsertGuid.jpg)

## Notes.

1. If you want the end user to have the option of assigning a keyboard shortcut to the command, you need to follow this. 
2. The <LocCanonicalName> will have the technical name of the command - this is the name shown users when they assign custom keyboard shortcuts to your command in the Tools -> Options -> Environment -> Keyboard dialog.

3. See the following for <LocCanonicalName> 
   1. https://www.vsixcookbook.com/getting-started/your-first-extension.html#modify-command
   
   2. https://learn.microsoft.com/en-us/visualstudio/extensibility/vsix/get-started/first-extension#modifying-the-command

4. the Tools -> Options -> Environment -> Keyboard dialog is as follows.

![Tools Options](images/90_50ToolsOptions.jpg)

4. The end user can now assign a keyboard shortcut to the command in this way.



