//using Microsoft.VisualStudio.Text;
//using System.Collections.ObjectModel;
//using System.Windows.Media;

//namespace SmartTagExOne
//{
//    internal class UpperCaseSmartTagAction : ISmartTagAction 
//    {
//        private ITrackingSpan m_span;
//        private string m_upper;
//        private string m_display;
//        private ITextSnapshot m_snapshot;
//        public string DisplayText
//        {
//            get { return m_display; }
//        }
//        public ImageSource Icon
//        {
//            get { return null; }
//        }
//        public bool IsEnabled
//        {
//            get { return true; }
//        }

//        public ISmartTagSource Source
//        {
//            get;
//            private set;
//        }

//        public ReadOnlyCollection<SmartTagActionSet> ActionSets
//        {
//            get { return null; }
//        }
//        public void Invoke()
//        {
//            m_span.TextBuffer.Replace(m_span.GetSpan(m_snapshot), m_upper);
//        }
//    }
//}
