# DTE Events, Build Events

## Objective
1. This example introduces DTE Events.

2. Specifically introduces Build Events.


## References
1. https://learn.microsoft.com/en-us/dotnet/api/envdte.events

2. https://learn.microsoft.com/en-us/dotnet/api/envdte.events.buildevents

3. Earlier example, 501125-ProvideAutoLoad

## How this project is built.
1. Start from the regular VSix project.

2. Then add the following in the initialize method of the package. We get the DTE and then events from it.

```cs
DteTwoInstance = await GetServiceAsync(typeof(DTE)) as DTE2;
BuildEventsInstance = DteTwoInstance.Events.BuildEvents;
BuildEventsInstance.OnBuildDone += BuildEventsInstance_OnBuildDone;
```
And ofcourse there are handler methods

```cs
private void SolutionEvents_AfterClosing()
{
    MessageBox.Show("After Closing");
}
```

3. Also note the  [ProvideAutoLoadAttribute](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.shell.provideautoloadattribute). And I applied it as follows.

```cs
[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[Guid(PackageGuidString)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
public sealed class BuildEventsIntroPackage : AsyncPackage
{..}
```

4. The [ProvideAutoLoadAttribute](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.shell.provideautoloadattribute) is introduced earlier in the example, [501125-ProvideAutoLoad]

5. Without this attribute, the package never loads. This is because the package does not contain any commands. Take a look at [this question.](https://github.com/microsoft/VSExtensibility/issues/272#issuecomment-1772743380) 

## Build and Run
1. Just build, launch the exp instance. 

2. Open any solution. Specifically the SimpleWpf solution. Then try building it. Observe the message box that popup.

