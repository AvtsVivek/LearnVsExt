using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Projection;
using Microsoft.VisualStudio.Utilities;

namespace ProjectionBufferTut
{
    internal class ProjectionTextViewModel : ITextViewModel
    {
        private readonly ITextDataModel _dataModel;
        private readonly IProjectionBuffer _projectionBuffer;
        private readonly PropertyCollection _properties;

        //The underlying source buffer from which the projection was created
        public ITextBuffer DataBuffer
        {
            get
            {
                return _dataModel.DataBuffer;
            }
        }

        public ITextDataModel DataModel
        {
            get
            {
                return _dataModel;
            }
        }

        public ITextBuffer EditBuffer
        {
            get
            {
                return _projectionBuffer;
            }
        }

        // Displays our projection 
        public ITextBuffer VisualBuffer
        {
            get
            {
                return _projectionBuffer;
            }
        }

        public PropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }

        public void Dispose()
        {

        }

        public ProjectionTextViewModel(ITextDataModel dataModel, IProjectionBuffer projectionBuffer)
        {
            this._dataModel = dataModel;
            this._projectionBuffer = projectionBuffer;
            this._properties = new PropertyCollection();
        }

        public SnapshotPoint GetNearestPointInVisualBuffer(SnapshotPoint editBufferPoint)
        {
            return editBufferPoint;
        }

        public SnapshotPoint GetNearestPointInVisualSnapshot(SnapshotPoint editBufferPoint, ITextSnapshot targetVisualSnapshot, PointTrackingMode trackingMode)
        {
            return editBufferPoint.TranslateTo(targetVisualSnapshot, trackingMode);
        }

        public bool IsPointInVisualBuffer(SnapshotPoint editBufferPoint, PositionAffinity affinity)
        {
            return true;
        }
    }
}
