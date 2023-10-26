
## Simple Tool Window Extension

1. This is based on the following You tube video.
   1. https://www.youtube.com/watch?v=u0pRDM8qW04

2. Reference.
   1. https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/guids-and-ids-of-visual-studio-menus#submenus-of-visual-studio-menus

3. Build the project and start debugging. The Visual Studio experimental instance should appear.
   
4. On the View / Other Windows menu, click ToolWindowWithButton.

5. ![Simple Tool Window](./images/50_50SimpleToolWindow.jpg)

6. Note the attribute over the package class.

```cs
[ProvideToolWindow(typeof(SimpleToolWindow.Commands.ToolWindowWithButton), Orientation = ToolWindowOrientation.Left, Style = VsDockStyle.Tabbed, Window = EnvDTE.Constants.vsWindowKindServerExplorer
)]
public sealed class SimpleToolWindowPackage : AsyncPackage
{

}
```
The Window is set to **EnvDTE.Constants.vsWindowKindServerExplorer**, you dock the window to left. You can set to **EnvDTE.Constants.vsWindowKindOutput** to dock it down. Or **EnvDTE.Constants.vsWindowKindSolutionExplorer** when you want to dock right.

7. https://stackoverflow.com/a/13515072/1977871



