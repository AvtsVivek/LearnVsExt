## Introducing Image Manifest Tools Extension

1. References 
   1. [Create a Tools Options custom page](https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-options-page#create-a-tools-options-custom-page)

2. This is extends 500810-ToolsOptionsCustomPage

3. Now add a command. But this is not resulting a vsct file. We need to author or create a visual studio command table, vsct. So we have to follow
   1. https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/visual-studio-command-table-dot-vsct-files
   2. https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/authoring-dot-vsct-files
   3. https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/how-to-create-a-dot-vsct-file

4. Instead of authoring vsct file ourselves, we take the other alternative.

5. In this other alternative, we will start with a VSix project, then add a command. Then add a dialog page.

6. 