## Objective 

1. This example is similar to ITagOne. This example has ITaggerProvider also.
2. 


## How the project is created. 

1. Make this into a MEF component.

![Mef Component](images/50_50_MakeMEFComponent.png)

  

## Build and Run

1. Reset the Visual Studio
![Reset Visual Studio Exp](../200500-VSixBlankProjectAnalysis/images/57_50_ResetVsExpIntance.jpg)

2. Lauch the exp insance. Open `HelloTagOne.TagOne` file. Ensure the `HelloUrlTaggerOneProvider.cs` is invoked.

![Output](images/51_50_TagOneOpenVsOutput.png)

3. Try with `HelloTagTwo.TagTwo` and `HelloTagThree.TagThree` as well.

## Todo

1. Need to understand the GetTags method in all the tagger classes. Its explained here.

![Explanation of Tagger](images/52_50_TaggerClass.png)

2. https://mihailromanov.wordpress.com/2022/06/19/json-on-steroids-2-3-visual-studio-editor-tags-classifiers-and-text-formating-part-1/

3.  