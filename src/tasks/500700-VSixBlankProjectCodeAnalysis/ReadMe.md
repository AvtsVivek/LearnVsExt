

1. This example is similar to the 500500-VSixBlankProjectAnalysis. 

2. The project is created exactly same way.

3. We will be studying the source code

4. Consider the file VSixBlankProjectCodeAnalysisPackage.cs in the project. 

5. The name of this file is of the format {ProjectName}Package.cs. This class implements
the package that would be exposed by the assembly created by building this project.

6. Any class that implements an **IVsPackage** interface and registers itself with the Visual Studio shell satisfies the minimum criteria to be considered a valid Visual Studio package.

7. Microsoft.VisualStudio.Shell.Package used to be the abstract class that one used to derive from to create a valid package. 

8. These days its Async version of it. With Visual Studio 2015, Microsoft introduced the Microsoft.VisualStudio.Shell.AsyncPackage abstract class that derives from Package class.

9. Leveraging this class, we can opt in asynchronous loading of the extension and reduce performance costs and maintain responsiveness of the UI. 

10. In Visual Studio 2019 and above, synchronous loading of extensions is turned off by default so that Visual Studio starts up faster and performs better while launching, as the UI thread is less rigorously used.

