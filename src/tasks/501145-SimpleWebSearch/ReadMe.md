
1. Objective:
2. 
3. Prereqs
   1. 500670-AssignKeyboardShortcut
   2. 500705-AddingMonikerIcon
   3. 501140-InsertGuidContextMenu
4. Use IDM_VS_CTXT_CODEWIN instead of IDM_VS_MENU_TOOLS
```cs
<Group guid="guidSimpleWebSearchPackageCmdSet" id="MyMenuGroup" priority="0x0600">
   <Parent guid="guidSHLMainMenu" id="IDM_VS_CTXT_CODEWIN"/>
</Group>
```
5. 