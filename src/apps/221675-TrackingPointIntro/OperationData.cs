using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingPointIntro
{
    public class OperationData
    {
        public int Position { get; set; }
        public int Length { get; set; }
        public string OperationText { get; set; }
        public TextOperation Operation { get; set; }
    }
    public enum TextOperation
    {
        Insert,
        Delete,
        Replace
    }
}
