# Trails With ToolBox

## Objective
1. To understand more about toolbox, how to add or remove Items to tool box. 
2. Currently this example is not working. 

## How this project is built.
1. Start with regular VSix project and add a tool window. Change the UI of the xaml to have two buttons.
2. In the add item button handler, have the following code. 
```cs
private void AddToToolBox_Click(object sender, RoutedEventArgs e)
{
    ThreadHelper.ThrowIfNotOnUIThread();
    IVsToolbox toolbox = (IVsToolbox) Package.GetGlobalService(typeof(SVsToolbox));

    if (toolbox == null)
    {
        MessageBox.Show("Add attempted, but Toolbox is null. Cannot continue",
        "Toolbox null");
        return;
    }

    TBXITEMINFO[] itemInfo = new TBXITEMINFO[1];
    itemInfo[0].bstrText = "Toolbox Sample Item one";
    itemInfo[0].hBmp = IntPtr.Zero;
    itemInfo[0].dwFlags = (uint)__TBXITEMINFOFLAGS.TBXIF_DONTPERSIST;
    var toolboxData = new OleDataObject();

    toolboxData.SetData(typeof(ToolboxItemData), new ToolboxItemData("Test string one"));

    toolbox.AddItem(toolboxData, itemInfo, "Toolbox Test one");

    toolbox.UpdateToolboxUI();
}
```
3. But this is not working. Tried commenting out the line `toolbox.UpdateToolboxUI();`. But still its not working. Not clear why. There is no changed in the tool box. 

4. This example is same as previous one, This is step as a step by step guide.

5. I try to break it down to just deal with the tool box, so created another example `901010-ToolboxTrailOne` but even that is having problem


## References
1.  


