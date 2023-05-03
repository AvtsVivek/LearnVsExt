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
2. https://learn.microsoft.com/en-us/visualstudio/extensibility/?view=vs-2022
3. https://bideveloperextensions.github.io/features/VSIXextensionmodel/

## Useful Links

1. [VSIX Community on GitHub](https://github.com/VsixCommunity)
2. [VSIX Community Samples repo](https://github.com/VsixCommunity/Samples)
3. [Official VS SDK documentation](https://learn.microsoft.com/en-us/visualstudio/extensibility/?view=vs-2022)
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

3. 