# Dependency Injection(DI) using Autofac

## Todo WIP

## Objective
1. This example introduces Dependency Injection configuration for VSix package project.

2. We are using Autofac as the container.


## How this project is built.
1. Starts from regualr VSix Project.
2. Added [Autofac](https://www.nuget.org/packages/Autofac) nuget package
3. Added AutofacEnabledAsyncPackage and BusinessServicesModule
4. The ctor of the package is modified thus

```cs
public AutofacDIIntroPackage()
{
    RegisterModule<BusinessServicesModule>();
}
```

5. Ensure to call the base initialize method as follows. See the thrid line.

```cs
protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
{
    await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
    await AutofacDIIntro.Commands.TrialToolWindowCommand.InitializeAsync(this);
    // Ensure you add the following line. Without this, container will not be built.
    await base.InitializeAsync(cancellationToken, progress);
}
```

6. 


## Notes
1. BusinessServicesModule is the autofac module. We configure services here.
2. We need to derive a class AutofacEnabledAsyncPackage from the Async package. This means, our package class should inherit this AutofacEnabledAsyncPackage, rather than directly AsyncPackage.  
3. This holds the autofac container
4. The command class, here the TrialToolWindowCommand class is not instanciated by the DI infrastructure.
5. But the tool window class, here TrialToolWindow is instanciated by the package by calling GetService method. 
6. This example is still not fully Mvvm, because it does not impliment INotifyPropertyChanged etc.
7. This only demonistrates DI.

## References
1. https://dotnetfalcon.com/dependency-injection-for-visual-studio-extensions/
2. https://github.com/conwid/GistManager/tree/master/GistManager
