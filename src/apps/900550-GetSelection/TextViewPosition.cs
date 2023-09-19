namespace GetSelection
{
    public struct TextViewPosition
    {
        private readonly int _column;
        private readonly int _line;

        public TextViewPosition(int line, int column)
        {
            _line = line;
            _column = column;
        }

        public int Line { get { return _line; } }
        public int Column { get { return _column; } }


        public static bool operator <(TextViewPosition a, TextViewPosition b)
        {
            if (a.Line < b.Line)
            {
                return true;
            }
            else if (a.Line == b.Line)
            {
                return a.Column < b.Column;
            }
            else
            {
                return false;
            }
        }

        public static bool operator >(TextViewPosition a, TextViewPosition b)
        {
            if (a.Line > b.Line)
            {
                return true;
            }
            else if (a.Line == b.Line)
            {
                return a.Column > b.Column;
            }
            else
            {
                return false;
            }
        }

        public static TextViewPosition Min(TextViewPosition a, TextViewPosition b)
        {
            return a > b ? b : a;
        }

        public static TextViewPosition Max(TextViewPosition a, TextViewPosition b)
        {
            return a > b ? a : b;
        }

        public override string ToString()
        {
            return $"Line: {Line}, Column: {Column}";
        }
    }
}
