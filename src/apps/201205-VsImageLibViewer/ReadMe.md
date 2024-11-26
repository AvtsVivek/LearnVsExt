## Notes

1. This is an example to extract Visual Studio Monikers in a Wpf application. 

![List Monikers](images/50_50_ListMonikers.jpg)

2. The objective here is to create a tool something like [Known Moniker example](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.KnownMonikersExplorer2022), as a wpf application, where we can see and download an moniker icon. 

3. As it turns out, this does not seem to be possible. For example, the following code is only possible in a Visual Studio Extension, and not in a wpf app.

```cs
Image = Moniker.ToBitmapSourceAsync(16);
```

4. Take a look at the following. The community tool kit does not seem to work within a wpf application.

![Nuget References](images/51_50_Nuget_References.jpg)

5. 

## References
1. Visual Studio Image Library - Free icons 🖼️ #vslive #tipsandtricks #visualstudio
   1. https://www.youtube.com/watch?v=vRcXK9yhB1k

2. Here we go. 