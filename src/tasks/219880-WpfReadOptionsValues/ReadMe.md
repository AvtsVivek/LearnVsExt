# Demos Reading of Options values. 

1. This example uses Wpf user control instead. 
2. This is not much different from earlier ReadOptionsValue. The only difference is as follows.
   1. MyWpfUserControl replaces MyUserControl
    ```cs
    public partial class MyWpfUserControl : UserControl { }
    ```
   2. OptionsPageCustom changes from `OptionPageCustom : DialogPage` to `OptionPageCustom : UIElementDialogPage`

3. If you want to reset the experimental instance, do the following.

![Reset Exp Vs](./../400500-VSixBlankProjectAnalysis/images/57_50_ResetVsExpIntance.jpg)

## References


