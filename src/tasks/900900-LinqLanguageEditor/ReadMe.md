# Linq Language Editor Example

## References
1. https://github.com/SFC-Sarge/LinqLanguageEditor2022

2. https://www.vsixcookbook.com/recipes/Walkthrough-Create-Language-Editor.html

## How to run.

1. Create VSix Community Project with Tool Window as follows.
   ![Visual Studio Tool Window Community Project](./images/50_50CreateProject.jpg)

2. Configure the project.
   ![Configure the project](./images/60_50ConfigureNewProject.jpg)

3. Update the nuget packages to the latest. 

4. Changed MyCommand to LinqToolWindowCommand in the file VSCommandTable.cs
```cs
internal sealed partial class PackageIds
{
   public const int LinqToolWindowCommand = 0x0100;
}
```
5. Changed MyToolWindow to LinqToolWindow

6. Changed MyToolWindowCommand to LinqToolWindowCommand

7. Changed the tool window button fromthe following

```xml
<Strings>
   <ButtonText>My Tool Window</ButtonText>
   <LocCanonicalName>.View.MyToolWindow</LocCanonicalName>
</Strings>
```
to the following.
```xml
<Strings>
   <ButtonText>LINQ Query Tool Window</ButtonText>
   <LocCanonicalName>.View.LinqToolWindow</LocCanonicalName>
</Strings>
```

8. Changed the MyToolWindowControl UserControl to LinqToolWindowControl.



