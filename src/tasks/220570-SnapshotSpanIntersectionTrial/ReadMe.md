## Objective

1. Introduces `SnapshotSpan` IntersectsWith method.

## Build and Run
1. Reset Visual Studio Exp instance and then Launch it.

![Reset Visual Studio Exp](../200500-VSixBlankProjectAnalysis/images/57_50_ResetVsExpIntance.jpg)

2. View -> Other Windows -> IntractionTrial

![Extract Numbers](Images/50_50_IntractionTrial_ToolWindow.png)

3. Same buffer

![Same buffer](Images/51_50_IntractionSameBuffer.png)

4. Different buffer

![Different Buffer](Images/52_50_IntractionDifferentBuffer.png)

## Notes

1. We get `ITextBuffer`, from `ITextBufferFactoryService`, 

2. And `ITextSnapshot`, from `ITextBuffer`.

3. `SnapshotSpan` is a subset of `Snapshot`.

4. When the buffers differ, the intersection of `SnapshotSpan`s from those buffers does not make sense. So an exception is thrown. 




