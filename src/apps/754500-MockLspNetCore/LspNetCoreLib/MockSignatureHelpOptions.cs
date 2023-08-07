using Microsoft.VisualStudio.LanguageServer.Protocol;
using System.Runtime.Serialization;

namespace LspNetCoreLib
{
    [DataContract]
    class MockSignatureHelpOptions : SignatureHelpOptions
    {
        [DataMember(Name = "mockSignatureHelp")]
        public bool MockSignatureHelp
        {
            get;
            set;
        }
    }
}
