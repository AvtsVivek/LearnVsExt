using IntraTextAdornmentHideText.Support;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;



namespace IntraTextAdornmentHideText
{
    /// <summary>
    /// Provides color swatch adornments in place of color constants.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a sample usage of the <see cref="IntraTextAdornmentTagTransformer"/> utility class.
    /// </para>
    /// </remarks>
    internal sealed class ColorAdornmentTagger : IntraTextAdornmentTagTransformer<ColorTag, ColorAdornment>

    {
        internal static ITagger<IntraTextAdornmentTag> GetTagger(IWpfTextView view, Lazy<ITagAggregator<ColorTag>> colorTagger)
        {
            return view.Properties.GetOrCreateSingletonProperty(
                () => new ColorAdornmentTagger(view, colorTagger.Value));
        }

        private ColorAdornmentTagger(IWpfTextView view, ITagAggregator<ColorTag> colorTagger)
            : base(view, colorTagger)
        {
        }

        public override void Dispose()
        {
            base.view.Properties.RemoveProperty(typeof(ColorAdornmentTagger));
        }

        protected override ColorAdornment CreateAdornment(ColorTag dataTag, SnapshotSpan span)
        {
            return new ColorAdornment(dataTag);
        }

        protected override bool UpdateAdornment(ColorAdornment adornment, ColorTag dataTag)
        {
            adornment.Update(dataTag);
            return true;
        }
    }
}
