## Understanding IClassificationTypeRegistryService 


# References
1. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.classification.iclassificationtyperegistryservice
2. https://developercommunity.visualstudio.com/t/vsix-project-with-sdk-style-csproj/1572145

# Notes
1. Tried adding IClassificationTypeRegistryService to a regular VSix project. 

2. The following is not imported correctly. Its always null, not sure why.

```cs
[Import]
private IClassificationTypeRegistryService classificationRegistry;
```