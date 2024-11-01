# Introduces SDK Project for VSix

## New Project creation

1. The starting project is [taken from here](https://github.com/microsoft/VSSDK-Extensibility-Samples/blob/master/LanguageServerProtocol/MockLanguageExtension/MockLanguageExtension.csproj). 

```sh
devenv /build Debug ./VSixSdkProjectIntro.sln
```

2. You may get the following error when you run the above. 

```txt
Microsoft Visual Studio 2022 Version 17.11.5.                                                                                                                    
Copyright (C) Microsoft Corp. All rights reserved.                                                                                                               
Build started at 12:30 PM...                                                                                                                                     
1>------ Build started: Project: VSixSdkProjectIntro, Configuration: Debug Any CPU ------                                                                        
1>C:\Program Files\dotnet\sdk\8.0.403\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(266,5): 
error NETSDK1004: Assets file 'C:\Trials\Ex\LearnVsExt\src\apps\400510-VSixSdkProjectIntro\obj\project.assets.json' not found. 
Run a NuGet package restore to generate this file.                      
1>Done building project "VSixSdkProjectIntro.csproj" -- FAILED.                                                                                                  
========== Build: 0 succeeded, 1 failed, 0 up-to-date, 0 skipped ==========                                                                                      
========== Build completed at 12:31 PM and took 01.088 seconds ==========
```

3. So we need to restore the nuget packages.

```sh
nuget restore ./VSixSdkProjectIntro.sln
```

4. If you run the above, you may get nuget not found error.

5. So we need to install nuget command line as follows. This will install at this location C:\Program Files\PackageManagement\NuGet\Packages on windows.

```ps
Install-Package NuGet.CommandLine
```

6. You may also have to add the nuget.exe location(in this case C:\Program Files\PackageManagement\NuGet\Packages\NuGet.CommandLine.6.11.1\tools) to the path variable. 



## References.

1. https://github.com/Microsoft/VSSDK-Extensibility-Samples/tree/master/LanguageServerProtocol

2. https://github.com/microsoft/VSSDK-Extensibility-Samples/blob/master/LanguageServerProtocol/MockLanguageExtension/MockLanguageExtension.csproj

3. https://developercommunity.visualstudio.com/t/vsix-project-with-sdk-style-csproj/1572145

4. https://stackoverflow.com/q/68548433/1977871

5. https://stackoverflow.com/a/68548802/1977871

6. https://learn.microsoft.com/en-us/visualstudio/extensibility/adding-an-lsp-extension

7. https://github.com/AArnott/VSIXProjectWithPackageReferences

8. https://github.com/AArnott/VSIXProjectWithPackageReferences/blob/netsdk/VSIXProject11/VSIXProject11.csproj



