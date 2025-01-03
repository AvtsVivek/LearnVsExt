﻿using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace FilePathToContentTypeProviderIntro
{
    [Export(typeof(IFilePathToContentTypeProvider))]
    [Name("FooAbcdFileToContentTypeProvider")] // I think, this could be any string.
    [FileExtension(CustomContentTypeConstants.FileExtension)]
    internal class CustomFileExtensionToContentTypeProvider : IFilePathToContentTypeProvider
    {
        [Import]
        IContentTypeRegistryService ContentTypeRegistryService { get; set; }

        public bool TryGetContentTypeForFilePath(string filePath,
                  out IContentType contentType)
        {
            // Just assign the content type and then return true.
            // So all that we are doing in here is, for a given extension(in this case its .csabcd
            // just assign the CSharp content type.
            contentType = ContentTypeRegistryService.GetContentType(CustomContentTypeConstants.ContentTypeName);
            return true;
        }
    }
}
