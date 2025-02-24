using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace SimpleIntraTextAdornment
{
    /// <summary>
    /// Determines which spans of text likely refer to color values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a data-only component. The tagging system is a good fit for presenting data-about-text.
    /// The <see cref="ColorAdornmentTagger"/> takes color tags produced by this tagger and creates corresponding UI for this data.
    /// </para>
    /// <para>
    /// This class is a sample usage of the <see cref="RegexTagger"/> utility base class.
    /// </para>
    /// </remarks>
    internal class ColorTagger<T> : ITagger<T> where T : ITag  // : RegexTagger<ColorTag>
    {
        private readonly IEnumerable<Regex> matchExpressions;
        internal ColorTagger(ITextBuffer buffer) // : base(buffer, new[] { new Regex(@"\b(0[xX])?([0-9a-fA-F])+\b", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase) })
        //: base(buffer, new[] { new Regex(@"\b[\dA-F]{6}\b", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase) })
        {
            this.matchExpressions = new[] { new Regex(@"\b(0[xX])?([0-9a-fA-F])+\b", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase) };
        }

        public IEnumerable<ITagSpan<T>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            // Here we grab whole lines so that matches that only partially fall inside the spans argument are detected.
            // Note that the spans argument can contain spans that are sub-spans of lines or intersect multiple lines.
            foreach (ITextSnapshotLine textSnapshotLine in GetIntersectingLines(spans))
            {
                string text = textSnapshotLine.GetText();

                foreach (Regex regex in matchExpressions)
                {
                    foreach (Match match in regex.Matches(text).Cast<Match>())
                    {
                        T tag = (T)(TryCreateTagForMatch(match) as ITag);
                        if (tag != null)
                        {
                            SnapshotSpan snapshotSpan = new SnapshotSpan(textSnapshotLine.Start + match.Index, match.Length);
                            yield return new TagSpan<T>(snapshotSpan, tag);
                        }
                    }
                }
            }
        }

        private ColorTag TryCreateTagForMatch(Match match)
        {
            Color color = ParseColor(match.ToString());

            if (match.Length == 6 || match.Length == 8)
            {
                return new ColorTag(color);
            }

            return null;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        private IEnumerable<ITextSnapshotLine> GetIntersectingLines(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            int lastVisitedLineNumber = -1;

            ITextSnapshot snapshot = spans[0].Snapshot;

            foreach (var span in spans)
            {
                int firstLine = snapshot.GetLineNumberFromPosition(span.Start);
                int lastLine = snapshot.GetLineNumberFromPosition(span.End);

                for (int i = Math.Max(lastVisitedLineNumber, firstLine); i <= lastLine; i++)
                {
                    ITextSnapshotLine textSnapshotLine = snapshot.GetLineFromLineNumber(i);
                    yield return textSnapshotLine;
                }

                lastVisitedLineNumber = lastLine;
            }
        }

        private static Color ParseColor(string hexColor)
        {
            int number;

            //Rule out any any '0x' prefixes
            if (hexColor.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase))
            {
                hexColor = hexColor.Substring(2);
            }

            if (!int.TryParse(hexColor, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out number))
            {
                Debug.Fail("unable to parse " + hexColor);
                return Colors.Transparent;
            }

            byte r = (byte)(number >> 16);
            byte g = (byte)(number >> 8);
            byte b = (byte)(number >> 0);

            return Color.FromRgb(r, g, b);
        }
    }
}
