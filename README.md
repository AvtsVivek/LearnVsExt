# LearnVsExt
Repository to learn Visual Studio Extension development. 


# Some references

## Book References.

1. https://github.com/Apress/visual-studio-extensibility-development
2. https://link.springer.com/book/10.1007/978-1-4842-5853-8
3. https://www.amazon.in/Visual-Studio-Extensibility-Development-Productivity/dp/1484258525
4. https://www.oreilly.com/library/view/visual-studio-extensibility/9781484258538/

## Cook Book reference

1. https://www.vsixcookbook.com/
2. https://github.com/VsixCommunity/docs/blob/main/docs/index.md

## Other references

1. https://visualstudio.microsoft.com/vs/features/extend/
2. https://learn.microsoft.com/en-us/visualstudio/extensibility
3. https://bideveloperextensions.github.io/features/VSIXextensionmodel/

## Useful Links

1. [VSIX Community on GitHub](https://github.com/VsixCommunity)
2. [VSIX Community Samples repo](https://github.com/VsixCommunity/Samples)
3. [Official VS SDK documentation](https://learn.microsoft.com/en-us/visualstudio/extensibility)
4. [VS SDK Samples repo](https://github.com/Microsoft/VSSDK-Extensibility-Samples)
5. [Extensibility chatroom on Gitter.im](https://gitter.im/Microsoft/extendvs)


## Notes

1. If you get an error something like the following, 
> Extension 'AddMenuButtonVsMainMenuBar.d4c83f53-acb7-473a-8726-498e86ebae56' could not be found. Please make sure the extension has been installed.	AddMenuButtonVsMainMenuBar			

then take a look at the [so answer](https://stackoverflow.com/a/76134788/1977871).


```txt
C:\"Program Files"\"Microsoft Visual Studio"\2022\Professional\VSSDK\VisualStudioIntegration\Tools\Bin\CreateExpInstance.exe /Reset /VSInstance=17.0_c9ef2fd3 /RootSuffix=Exp && PAUSE
```

2. If you want to reset the experimental instance, do the following.

![Reset Exp Vs](./src/tasks/500500-VSixBlankProjectAnalysis/images/110ResetVsExpIntance50.jpg)

3. Also ensure you install [Clear MEF Component Cache](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ClearMEFComponentCache) extension. This is a part of [Extensibility Essentials](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ExtensibilityEssentials2022). The current latest version is 2022, and do check for the latest version
   1. Typical errors that can be solved by clearing the component cache are:
      1. Could not load package exception
      2. Could not load assembly exception
      3. Composition error when opening files
      4. Missing syntax highlighting of some languages in VS
 
   2. Clear Cache
      ![Clear Cache](./images/50_50_VsExtensionEssentials_ClearMef.jpg)

   3. HEre we go


## A tutorial Series that I found on the net. 

1. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-1/
2. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-2-add-menu-item/
3. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-3-add-context-menu-get-selected-code/
4. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-4-show-a-popup-window/
5. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-highlight-code-in-editor/
6. https://michaelscodingspot.com/visual-studio-2017-extension-development-tutorial-part-6-follow-span-code-trackingspan/
7. https://michaelscodingspot.com/visual-studio-2017-extension-tutorial-7-insert-buttons-text-characters-adornments/
8. https://michaelscodingspot.com/visual-studio-2017-extension-tutorial-8-add-ui-in-the-indicator-margin-with-glyphs/

And the code samples are here.

1. https://github.com/michaelscodingspot/CodyDocs


##  YouTube Playlist
1. https://www.youtube.com/playlist?list=PLReL099Y5nRdG2n1PrY_tbCsUznoYvqkS

2. https://www.youtube.com/watch?v=2c-4uZc0rq0

3. Writing Visual Studio Extensions with Mads - Writing your first extension
   1. https://www.youtube.com/watch?v=u0pRDM8qW04
   2. https://www.youtube.com/watch?v=Pk7jdsvEhfc
   3. https://www.youtube.com/watch?v=VVaGOxdvYSw
   4. https://www.youtube.com/watch?v=haTDm8Qkips
   5. https://www.youtube.com/watch?v=03M_4v0I1Gk
   6. https://www.youtube.com/playlist?list=PLReL099Y5nReXKzeX10TZF3BfLdOZXxix




## Blog posts
1. https://devblogs.microsoft.com/visualstudio/getting-started-writing-visual-studio-extensions/
2. https://devblogs.microsoft.com/visualstudio/writing-extensions-just-got-easier/


## To do 
1. https://learn.microsoft.com/en-us/visualstudio/extensibility/adding-a-menu-controller-to-a-toolbar