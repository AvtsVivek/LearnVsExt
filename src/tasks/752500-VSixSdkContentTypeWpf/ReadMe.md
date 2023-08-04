
# Objective
1. Create a Sdk Style VSix project.
2. Add content type def for foo(.foo)
3. Activate a wpf application when a file with extensioin .foo(example text.foo) is opened.

# How the project is created.
1. Start from 500510-VSixSdkProjectIntro
2. Update the nuget packages.
3. Add the FooContentDefinition
4. Add the FooLanguageClient which impliments ILanguageClient and ILanguageClientCustomMessage2



