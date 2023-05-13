## Objective

1. This builds on [task](https://github.com/AvtsVivek/LearnVsExt/tree/main/src/tasks/501100-AddMenuVsMainMenuBar) and [app](https://github.com/AvtsVivek/LearnVsExt/tree/main/src/apps/501100-AddMenuVsMainMenuBar).  

2. This example adds and demos [ProvideAutoLoad attribute](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.shell.provideautoloadattribute?view=visualstudiosdk-2022). 

3. References
   1. https://learn.microsoft.com/en-us/visualstudio/extensibility/how-to-use-asyncpackage-to-load-vspackages-in-the-background?view=vs-2022#create-an-asyncpackage

   2. https://learn.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2017/extensibility/how-to-use-asyncpackage-to-load-vspackages-in-the-background?view=vs-2017#create-an-asyncpackage
   
   3. https://learn.microsoft.com/en-us/visualstudio/extensibility/adding-a-menu-to-the-visual-studio-menu-bar?view=vs-2022
   
   4. https://www.youtube.com/watch?v=p328QcgZObs&t=526s

4. Asdf

```cs
[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[Guid(ProvideAutoLoadPackage.PackageGuidString)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
public sealed class ProvideAutoLoadPackage : AsyncPackage
{

}
``` 

5. Consider the attributes applied above. Take a look at the YouTube ref: https://www.youtube.com/watch?v=p328QcgZObs&t=526s
```cs
[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
```

   So they are not events. Think of them as states. https://www.youtube.com/watch?v=p328QcgZObs&t=565s

6. Try running debugging this app with and without the above attributes. Each time you run the app, ensure to reset the exp vs as follows.

![Reset Visual Studio Exp](https://github.com/AvtsVivek/LearnVsExt/blob/main/src/tasks/500500-VSixBlankProjectAnalysis/images/110ResetVsExpIntance50.jpg)

Visual studio doesn’t load the Command until first use. Which means, our Command’s constructor of the package won’t be called.

If we need to make our Command to initialize at Visual Studio startup, that is, if we need our ctor to be called at start up, we need to do a bit more. To do that, we need to make our VSPackage to initialize at startup. This is done with the ProvideAutoLoad attribute in the package class file.

We have the attribute twice: When there’s no Solution, and when Solution exists. Which covers all cases. Also, note the PackageAutoLoadFlags.BackgroundLoad flag. This important flag states that our packages can initialize asynchronously on a background thread.



