using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace SimpleIntraTextAdornment
{
    internal class SimpleColorAdornmentTagger : ITagger<IntraTextAdornmentTag>
    {
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        private ITagAggregator<ColorTag> _colorTagAggregator;

        protected readonly IWpfTextView _view;

        private Dictionary<SnapshotSpan, ColorAdornment> _adornmentCache = new Dictionary<SnapshotSpan, ColorAdornment>();

        protected ITextSnapshot _snapshot { get; private set; }

        private readonly List<SnapshotSpan> _invalidatedSpans = new List<SnapshotSpan>();


        public SimpleColorAdornmentTagger(IWpfTextView view, ITagAggregator<ColorTag> colorTagAggregator) 
        {
            this._colorTagAggregator = colorTagAggregator;
            this._view = view;
            this._snapshot = view.TextBuffer.CurrentSnapshot;
        }

        public IEnumerable<ITagSpan<IntraTextAdornmentTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (spans == null || spans.Count == 0)
                yield break;

            // Translate the request to the snapshot that this tagger is current with.

            ITextSnapshot requestedSnapshot = spans[0].Snapshot;

            var translatedSpans = new NormalizedSnapshotSpanCollection(spans.Select(span => span.TranslateTo(this._snapshot, SpanTrackingMode.EdgeExclusive)));

            // Grab the adornments.
            foreach (var tagSpan in GetAdornmentTagsOnSnapshot(translatedSpans))
            {
                // Translate each adornment to the snapshot that the tagger was asked about.
                SnapshotSpan span = tagSpan.Span.TranslateTo(requestedSnapshot, SpanTrackingMode.EdgeExclusive);

                IntraTextAdornmentTag tag = new IntraTextAdornmentTag(tagSpan.Tag.Adornment, tagSpan.Tag.RemovalCallback, tagSpan.Tag.Affinity);
                yield return new TagSpan<IntraTextAdornmentTag>(span, tag);
            }
        }

        // Produces tags on the snapshot that this tagger is current with.
        private IEnumerable<TagSpan<IntraTextAdornmentTag>> GetAdornmentTagsOnSnapshot(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            ITextSnapshot snapshot = spans[0].Snapshot;

            System.Diagnostics.Debug.Assert(snapshot == this._snapshot);

            // Since WPF UI objects have state (like mouse hover or animation) and are relatively expensive to create and lay out,
            // this code tries to reuse controls as much as possible.
            // The controls are stored in this.adornmentCache between the calls.

            // Mark which adornments fall inside the requested spans with Keep=false
            // so that they can be removed from the cache if they no longer correspond to data tags.
            HashSet<SnapshotSpan> toRemove = new HashSet<SnapshotSpan>();
            foreach (var ar in _adornmentCache)
                if (spans.IntersectsWith(new NormalizedSnapshotSpanCollection(ar.Key)))
                    toRemove.Add(ar.Key);

            var adornmentDataForSpans = GetAdornmentData(spans).ToList();

            foreach (var spanDataPair in adornmentDataForSpans)
            {
                // Look up the corresponding adornment or create one if it's new.
                ColorAdornment adornment;
                SnapshotSpan snapshotSpan = spanDataPair.Item1;
                PositionAffinity? affinity = spanDataPair.Item2;
                ColorTag adornmentData = spanDataPair.Item3;
                if (_adornmentCache.TryGetValue(snapshotSpan, out adornment))
                {
                    if (UpdateAdornment(adornment, adornmentData))
                        toRemove.Remove(snapshotSpan);
                }
                else
                {
                    adornment = CreateAdornment(adornmentData, snapshotSpan);

                    if (adornment == null)
                        continue;

                    // Get the adornment to measure itself. Its DesiredSize property is used to determine
                    // how much space to leave between text for this adornment.
                    // Note: If the size of the adornment changes, the line will be reformatted to accommodate it.
                    // Note: Some adornments may change size when added to the view's visual tree due to inherited
                    // dependency properties that affect layout. Such options can include SnapsToDevicePixels,
                    // UseLayoutRounding, TextRenderingMode, TextHintingMode, and TextFormattingMode. Making sure
                    // that these properties on the adornment match the view's values before calling Measure here
                    // can help avoid the size change and the resulting unnecessary re-format.
                    adornment.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                    _adornmentCache.Add(snapshotSpan, adornment);
                }

                yield return new TagSpan<IntraTextAdornmentTag>(snapshotSpan, new IntraTextAdornmentTag(adornment, null, affinity));
            }

            foreach (var snapshotSpan in toRemove)
                _adornmentCache.Remove(snapshotSpan);
        }

        private ColorAdornment CreateAdornment(ColorTag dataTag, SnapshotSpan span)
        {
            return new ColorAdornment(dataTag);
        }

        private bool UpdateAdornment(ColorAdornment adornment, ColorTag dataTag)
        {
            adornment.Update(dataTag);
            return true;
        }

        private IEnumerable<Tuple<SnapshotSpan, PositionAffinity?, ColorTag>> GetAdornmentData(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            ITextSnapshot snapshot = spans[0].Snapshot;

            IEnumerable<IMappingTagSpan<ColorTag>> colorTags = _colorTagAggregator.GetTags(spans).ToList();

            foreach (IMappingTagSpan<ColorTag> dataTagSpan in colorTags)
            {
                NormalizedSnapshotSpanCollection colorTagSpans = dataTagSpan.Span.GetSpans(snapshot);

                // Ignore data tags that are split by projection.
                // This is theoretically possible but unlikely in current scenarios.
                if (colorTagSpans.Count != 1)
                    continue;

                SnapshotSpan adornmentSpan = new SnapshotSpan(colorTagSpans[0].Start, 0);

                yield return Tuple.Create(adornmentSpan, (PositionAffinity?)PositionAffinity.Successor, dataTagSpan.Tag);
            }
        }
    }
}
