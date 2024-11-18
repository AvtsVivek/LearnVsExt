## How this project is created.
1. Follow the steps mentioned in VSixBlankProjectAnalysis example earlier.

2. Create a new project as follows.

![New Project Template](../../tasks/400500-VSixBlankProjectAnalysis/images/51_50_NewProjectTemplate50.jpg)

3. Make it a Mef Component as follows. Solution Explorer -> Look for source.extension.vsixmanifest, right click -> Assets -> Edit.

![Make the project an MEF component](Images/50_50_MakeItAnMefComponent.png)

4. Add the two classes, `LineAsyncQuickInfoSource` and its provided, `LineAsyncQuickInfoSourceProvider`

## Build and Run.
1. First Reset VS Exp instance.

![Reset Exp Vs](../../tasks/400500-VSixBlankProjectAnalysis/images/57_50_ResetVsExpIntance.jpg)

2. Do a full rebuild.

3. Press F5 to run. Open any file like the following. Hower the mouse so a quick info is displayed as follows.

![On a cs file](Images/51_50_ShowingLineNumber.png)

4. The text file.

![On a txt file](Images/52_50_SHowingLineNumberTextFile.png)


## Notes

1. Without making it into an MEF component, this is not working. Need to undrestand more about MEF component. 

## References
1. https://github.com/microsoft/VSSDK-Extensibility-Samples/tree/master/AsyncQuickInfo
2. https://learn.microsoft.com/en-us/visualstudio/extensibility/managed-extensibility-framework-in-the-editor
3. https://github.com/Microsoft/vs-editor-api/wiki/Modern-Quick-Info-API
4.  