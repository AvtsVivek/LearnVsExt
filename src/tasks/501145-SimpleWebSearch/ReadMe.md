
1. Objective: 

2. Prereqs
   1. 500670-AssignKeyboardShortcut
   2. 500705-AddingMonikerIcon
   3. 501135-InsertGuidMenuCmd
   4. 501140-InsertGuidContextMenu


3. Use IDM_VS_CTXT_CODEWIN instead of IDM_VS_MENU_TOOLS
```cs
<Group guid="guidSimpleWebSearchPackageCmdSet" id="MyMenuGroup" priority="0x0600">
   <Parent guid="guidSHLMainMenu" id="IDM_VS_CTXT_CODEWIN"/>
</Group>
```

4. Add Search Moniker. Next, include the following in the code. By this we are telling the package that we are using the visual studio known moniker icons.
```xml
<Include href="KnownImageIds.vsct"/>
```

5. Now add the following icon tag. So basically we are replacing the above guidImage one with the following.

```xml
<Icon guid="ImageCatalogGuid" id="SearchMember"/>
<CommandFlag>IconIsMoniker</CommandFlag>
```

6. Remove the following. Open VSCT file, the Visual Studio Command Table file. We dont want to use the default png file. So delete the following.

```xml
<GuidSymbol name="guidImages" value="{532d1e76-0759-4f17-8249-a459c36252f1}" >
    <IDSymbol name="bmpPic1" value="1" />
    <IDSymbol name="bmpPic2" value="2" />
    <IDSymbol name="bmpPicSearch" value="3" />
    <IDSymbol name="bmpPicX" value="4" />
    <IDSymbol name="bmpPicArrows" value="5" />
    <IDSymbol name="bmpPicStrikethrough" value="6" />
</GuidSymbol>
```
and 

```xml
<Bitmaps>
    <Bitmap guid="guidImages" href="Resources\MainMenuCommand.png" usedList="bmpPic1, bmpPic2, bmpPicSearch, bmpPicX, bmpPicArrows, bmpPicStrikethrough"/>
</Bitmaps>
```

and finally 

```xml
<Icon guid="guidImages" id="bmpPic1" />
```

