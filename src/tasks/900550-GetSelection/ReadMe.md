


## Reference
1. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-3-add-context-menu-get-selected-code/


## How this project is built.
1. Started with a VSix project.
2. In the vsct file, changed the IDM_VS_MENU_TOOLS to IDM_VS_CTXT_CODEWIN. 
3. Added the structs
   1. TextViewPosition
   2. TextViewSelection
4. In the command, added the following methods
   1. private async Task<TextViewSelection?> GetSelectionAsync()
   2. private async Task<string> GetActiveDocumentFilePathAsync() 