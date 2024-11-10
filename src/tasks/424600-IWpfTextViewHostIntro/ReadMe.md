## Objective

1. Introduces **WpfTextViewHost** 

2. This also introduces [subsystems inside Visual Studio Editor](https://learn.microsoft.com/en-us/visualstudio/extensibility/inside-the-editor#overview-of-the-subsystems).

3. We can get `WpfTextViewHost` using the Editor Adapter Factory service as well as follows. But we will see that in subsequent examples, when we see the component model

```cs
vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);
var temp = IVsEditorAdaptersFactoryService.GetWpfTextViewHost(vsTextView);
```

3. If you want to reset the experimental instance, do the following.

![Reset Exp Vs](./../400500-VSixBlankProjectAnalysis/images/57_50_ResetVsExpIntance.jpg)

4. The properties on the control are as follows. We may have to explore the following as we go along

```txt
Property 1 of 27
Property Value type : Microsoft.VisualStudio.Editor.Implementation.VsHiddenTextSessionAdapter
Property Value value: Microsoft.VisualStudio.Editor.Implementation.VsHiddenTextSessionAdapter
Property 2 of 27
Property Value type : Microsoft.VisualStudio.Text.EditorOptions.Implementation.EditorOptions
Property Value value: Microsoft.VisualStudio.Text.EditorOptions.Implementation.EditorOptions
Property 3 of 27
Property Value type : Microsoft.VisualStudio.Text.ChangeTagger.Implementation.ChangeTagger
Property Value value: Microsoft.VisualStudio.Text.ChangeTagger.Implementation.ChangeTagger
Property 4 of 27
Property Value type : Microsoft.TeamFoundation.Git.Provider.ChangeIndication.GitChangeSource
Property Value value: Microsoft.TeamFoundation.Git.Provider.ChangeIndication.GitChangeSource
Property 5 of 27
Property Value type : Microsoft.VisualStudio.Text.Tagging.SimpleTagger`1[Microsoft.VisualStudio.Text.Tagging.TextMarkerTag]
Property Value value: Microsoft.VisualStudio.Text.Tagging.SimpleTagger`1[Microsoft.VisualStudio.Text.Tagging.TextMarkerTag]
Property 6 of 27
Property Value type : System.Collections.Generic.Dictionary`2[Microsoft.VisualStudio.Text.Editor.ITextView,Microsoft.VisualStudio.Editor.Implementation.TextMarkerViewTagger]
Property Value value: System.Collections.Generic.Dictionary`2[Microsoft.VisualStudio.Text.Editor.ITextView,Microsoft.VisualStudio.Editor.Implementation.TextMarkerViewTagger]
Property 7 of 27
Property Value type : Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter
Property Value value: Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter
Property 8 of 27
Property Value type : System.Object
Property Value value: System.Object
Property 9 of 27
Property Value type : Microsoft.VisualStudio.Text.BufferUndoManager.Implementation.TextBufferUndoManager
Property Value value: Microsoft.VisualStudio.Text.BufferUndoManager.Implementation.TextBufferUndoManager
Property 10 of 27
Property Value type : Microsoft.VisualStudio.Editor.Implementation.ShimLanguageNavigator
Property Value value: Microsoft.VisualStudio.Editor.Implementation.ShimLanguageNavigator
Property 11 of 27
Property Value type : Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter
Property Value value: Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter
Property 12 of 27
Property Value type : Microsoft.VisualStudio.Debugger.Parallel.Extension.Tagger
Property Value value: Microsoft.VisualStudio.Debugger.Parallel.Extension.Tagger
Property 13 of 27
Property Value type : Microsoft.VisualStudio.Text.BrokeredServices.Implementation.Diagnostics.DiagnosticTaggerProvider+DiagnosticTagger
Property Value value: Microsoft.VisualStudio.Text.BrokeredServices.Implementation.Diagnostics.DiagnosticTaggerProvider+DiagnosticTagger
Property 14 of 27
Property Value type : Microsoft.VisualStudio.Text.Implementation.TextDocument
Property Value value: Microsoft.VisualStudio.Text.Implementation.TextDocument
Property 15 of 27
Property Value type : System.Collections.Generic.Dictionary`2[Microsoft.VisualStudio.Text.Editor.ITextView,Microsoft.VisualStudio.Debugger.Parallel.Extension.VsDebugMarkers.VsDebugMarkerClassificationTagger]
Property Value value: System.Collections.Generic.Dictionary`2[Microsoft.VisualStudio.Text.Editor.ITextView,Microsoft.VisualStudio.Debugger.Parallel.Extension.VsDebugMarkers.VsDebugMarkerClassificationTagger]
Property 16 of 27
Property Value type : System.Int32
Property Value value: 0
Property 17 of 27
Property Value type : System.Collections.Generic.Dictionary`2[Microsoft.VisualStudio.Text.Editor.ITextView,Microsoft.VisualStudio.Debugger.Parallel.Extension.VsDebugMarkers.VsDebugMarkerTagger]
Property Value value: System.Collections.Generic.Dictionary`2[Microsoft.VisualStudio.Text.Editor.ITextView,Microsoft.VisualStudio.Debugger.Parallel.Extension.VsDebugMarkers.VsDebugMarkerTagger]
Property 18 of 27
Property Value type : Microsoft.VisualStudio.Text.Tagging.SimpleTagger`1[Microsoft.VisualStudio.Text.Tagging.ErrorTag]
Property Value value: Microsoft.VisualStudio.Text.Tagging.SimpleTagger`1[Microsoft.VisualStudio.Text.Tagging.ErrorTag]
Property 19 of 27
Property Value type : System.Boolean
Property Value value: True
Property 20 of 27
Property Value type : Microsoft.VisualStudio.Text.EditorPrimitives.Implementation.DefaultBufferPrimitive
Property Value value: Microsoft.VisualStudio.Text.EditorPrimitives.Implementation.DefaultBufferPrimitive
Property 21 of 27
Property Value type : Microsoft.VisualStudio.Editor.Implementation.MarkerManager
Property Value value: Microsoft.VisualStudio.Editor.Implementation.MarkerManager
Property 22 of 27
Property Value type : Microsoft.VisualStudio.Editor.Implementation.TextViewCreationListenerAdapter+RefCountedTextBufferChangeListener
Property Value value: Microsoft.VisualStudio.Editor.Implementation.TextViewCreationListenerAdapter+RefCountedTextBufferChangeListener
Property 23 of 27
Property Value type : Microsoft.VisualStudio.Text.Tagging.RefCountedSimpleTagger`1[Microsoft.VisualStudio.Text.Tagging.IOutliningRegionTag]
Property Value value: Microsoft.VisualStudio.Text.Tagging.RefCountedSimpleTagger`1[Microsoft.VisualStudio.Text.Tagging.IOutliningRegionTag]
Property 24 of 27
Property Value type : Microsoft.VisualStudio.Text.Projection.Implementation.BufferGraph
Property Value value: Microsoft.VisualStudio.Text.Projection.Implementation.BufferGraph
Property 25 of 27
Property Value type : System.Collections.Immutable.ImmutableHashSet`1[Microsoft.VisualStudio.Text.ITextSnapshot]
Property Value value: System.Collections.Immutable.ImmutableHashSet`1[Microsoft.VisualStudio.Text.ITextSnapshot]
Property 26 of 27
Property Value type : Microsoft.VisualStudio.Debugger.Parallel.Extension.VsDebugMarkers.VsDebugMarkerManager
Property Value value: Microsoft.VisualStudio.Debugger.Parallel.Extension.VsDebugMarkers.VsDebugMarkerManager
Property 27 of 27
Property Value type : Microsoft.VisualStudio.Text.Document.WhitespaceManager
Property Value value: Microsoft.VisualStudio.Text.Document.WhitespaceManager
```

