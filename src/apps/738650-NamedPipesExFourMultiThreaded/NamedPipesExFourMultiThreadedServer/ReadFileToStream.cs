
// Contains the method executed in the context of the impersonated user
using System.IO;

public class ReadFileToStream
{
    private string fn;
    private StreamString ss;

    public ReadFileToStream(StreamString str, string filename)
    {
        fn = filename;
        ss = str;
    }

    public void Start()
    {
        string contents = File.ReadAllText(fn);
        ss.WriteString(contents);
    }
}