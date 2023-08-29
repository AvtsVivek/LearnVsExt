## Link a content type to a file name extension

## Reference: 
1. https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-linking-a-content-type-to-a-file-name-extension

2. https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-creating-a-margin-glyph

# How this example is created. 
1. This is same as 748510-TodoGlyphTest but in Sdk style.
2. The started from the 400510-VSixSdkProjectIntro
3. Then add the following classes, similar to earlier example. 
   1. TodoGlyphFactory
   2. TodoGlyphFactoryProvider
   3. TodoGlyphTestPackage
   4. TodoTag
   5. TodoTagger
   6. TodoTaggerProvider
4. Ensure the following references are added.
```xml
<ItemGroup>
	<PackageReference Include="Microsoft.VSSDK.BuildTools" Version="17.1.4054">
		<PrivateAssets>all</PrivateAssets>
		<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
	</PackageReference>
	<PackageReference Include="Microsoft.VisualStudio.Shell.15.0" Version="17.1.32210.191" />
	<PackageReference Include="Microsoft.VisualStudio.SDK" Version="17.0.32112.339" />
	<Reference Include="System.Design" />
</ItemGroup>
```

# How to run.
1. Run as before, 748510-TodoGlyphTest
