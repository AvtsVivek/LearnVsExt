


## Reference
1. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-4-show-a-popup-window/


1. Add BaseDialogWindow class which inherits DialogWindow.
2. Add a wpf user control named AddDocumentationWindow
3. Add a text block to the user control. 
```xml
<TextBlock Text="Hello World!"/>
```
4. Change the user control to Window by replacing UserControl with BaseDialogWindow

```xml
<local:BaseDialogWindow x:Class="GetSelectionShowPopup.AddDocumentationWindow"
             xmlns:local="clr-namespace:GetSelectionShowPopup" >

</local:BaseDialogWindow>
```

5. Also correct the backend AddDocumentationWindow.xaml.cs file base class to BaseDialogWindow.

```cs
public partial class AddDocumentationWindow : BaseDialogWindow
{
}
```

6. Add some properties to the xaml window as follows.
```xaml
mc:Ignorable="d" Title="Add Documentation" WindowStyle="ToolWindow"
Padding="5" Background="LightYellow"
Width="500" Height="400"
```
8. Also change the content of the window. See the AddDocumentationWindow.xaml file. Also its backend file AddDocumentationWindow.xaml.cs
9. Added, DocumentationFileHandler, DocumentationFileSerializer, DocumentationFragment, FileDocumentation and so on.
