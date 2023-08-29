﻿using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace OokLanguage.Intellisence
{
    [Export(typeof(IIntellisenseControllerProvider))]
    [Name("Template QuickInfo Controller")]
    [ContentType("text")]
    internal class TemplateQuickInfoControllerProvider : IIntellisenseControllerProvider
    {
        #region Asset Imports

        [Import]
        internal IQuickInfoBroker QuickInfoBroker { get; set; }

        #endregion

        #region IIntellisenseControllerFactory Members

        public IIntellisenseController TryCreateIntellisenseController(ITextView textView,
            IList<ITextBuffer> subjectBuffers)
        {
            return new TemplateQuickInfoController(textView, subjectBuffers, this);
        }

        #endregion

    }

}
