# Editor With ToolBox

## Objective
1. 

## How this project is built.
1. Start with regular VSix Project. 

![Starting Template](Images/50_50_VSixStartupProject.png)

2. Add two folders, Resources and Templates, with contents as shown below.

![Resources and Templates Folders](Images/51_50_AddFilesInResourcesAndTemplates.png)

3. Right click each one of those four files and Let the properties be as below(Content, Copy always, True and Default). Its not clear, it should be same for all of the four files as below, but for now, let that be as shown below.

![Properties of the files](Images/52_50_ResourcesProperties.png)

4. Add a GuidList file. Click Seach and in the feature seach, look for Guid and add fresh new guids as follows.

![New Guids](Images/53_50_AddGuidListFileAndCreateGuids.png)

5. Ensure the guids match as follows. The GuidClientPackage must match the one in Templates\EditorWithToolbox.vsdir as follows.

![Guids must match](Images/54_50_EnsureGuidsMatch.png)

6. Add a windows forms custom control, as follows.

![Editor Custom Control](Images/55_50_WindowsFormsCustomControl.png)

7. Remove the backend designer file, and also remove the partial keyword. Let the class be derived from RichTextBox as follows. 

```cs
using System.Windows.Forms;

namespace EditorWithToolBox
{
    public class EditorControl : RichTextBox
    {
        public EditorControl()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            WordWrap = false;
        }
    }
}
```

8. Add a serializable class called ToolboxItemData file. 

10. Add two resource files, Resources.resx and VsPackage.resx files.

![Resource Adding files](Images/57_50_ResourceAdding.png)

11. Adding ico file.

![Adding ico file](Images/58_50_HowToAddIcoFile.png)

12. Add an EditorFactory Class.

13. Add and EditorPane class.

14. Ensure VSixManifest file has the following assets.

![Adding in Manifestfile](Images/59_50_AssetsVsManifest.png)



## Build and Run.
1. This is currently not working. This is throwning exception.


```txt
System.Runtime.InteropServices.COMException
  HResult=0x8000FFFF
  Message=Catastrophic failure (Exception from HRESULT: 0x8000FFFF (E_UNEXPECTED))
  Source=mscorlib
  StackTrace:
   at System.Runtime.InteropServices.Marshal.ThrowExceptionForHRInternal(Int32 errorCode, IntPtr errorInfo)
```

![Exception](Images/50_50_Exception.png)


## References
1. https://joshvarty.com/2014/08/01/ripping-the-visual-studio-editor-apart-with-projection-buffers/

2. https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor

3. https://github.com/JoshVarty/ProjectionBufferTutorial

4. 
