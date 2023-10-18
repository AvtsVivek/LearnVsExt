
# VsixCommunity Tool Window.
1. Demos Tool window using community template and tool kit.

2. This also shows how to use a dialog window. See the class MyToolWindowDialog that is derived from **DialogWindow**. 

3. Once you have it, showing the dialog is as simple as calling the following method.

```cs
var dialog = new MyToolWindowDialog();
dialog.ShowDialog();
```

4. This also demoistrates Theme. Look for the following in the xaml files.

```xaml
toolkit:Themes.UseVsTheme="True"
```

5. 

## References
 
1. https://github.com/VsixCommunity/Samples/tree/master/ToolWindow

2. https://learn.microsoft.com/en-us/visualstudio/extensibility/vsix/recipes/custom-tool-windows

3. https://www.vsixcookbook.com/recipes/custom-tool-windows.html

4. https://learn.microsoft.com/en-us/visualstudio/extensibility/vsix/recipes/use-themes

5. https://www.vsixcookbook.com/recipes/theming.html



## How this project is built.

Use the following.

1. Create a new projct.
   ![Visual Studio Tool Window Community Project](./images/50_50CreateProject.jpg)

2. Configure the project.
   ![Configure the project](./images/60_50ConfigureNewProject.jpg)

3. Update the nuget packages.
   1. https://www.nuget.org/packages/Community.VisualStudio.VSCT
   2. https://www.nuget.org/packages/Community.VisualStudio.Toolkit.17
   3. https://www.nuget.org/packages/Microsoft.VSSDK.BuildTools

4. 

## Build and Run

1. The exp instance is as follows.

   ![The exp instance](images/70_50BuildAndRunExpInstance.jpg)

2. The tool window

   ![The tool windows](images/80_50ToolWindow.jpg)

3. 


