using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Windows.Controls;

namespace TypingSpeedMeter
{
    /// <summary>
    /// Adornment class that draws a box containing a typing speed meter in the top right hand corner of the viewport
    /// </summary>
    class TypingSpeedMeter
    {
        private TypingSpeedControl _root;
        private IWpfTextView _view;
        private IAdornmentLayer _adornmentLayer;
        private int _curMax;
        private DateTime _start;


        public TypingSpeedMeter(IWpfTextView view)
        {
            _view = view;
            _root = new TypingSpeedControl();
            _curMax = 0;
            _start = DateTime.UtcNow;

            // Grab a reference to the adornment layer that this adornment should be added to
            _adornmentLayer = view.GetAdornmentLayer("TypingSpeed");

            // Reposition the adornment whenever the editor window is resized
            _view.ViewportHeightChanged += delegate { OnSizeChange(); };
            _view.ViewportWidthChanged += delegate { OnSizeChange(); };

        }

        /// <summary>
        /// Updates the size of the green speed bar based on a simple (KeysPressed/TimeElapsed) calculation
        /// </summary>
        /// <param name="typedChars">Number of keys pressed since start of session </param>
        public void UpdateBar(int typedChars)
        {
            int max = 1000;
            double curLevel = 0;

            DateTime now = DateTime.UtcNow;
            var interval = now.Subtract(_start).TotalMinutes;

            int speed = (int)(typedChars / interval);

            //speed
            _root.val.Content = speed;
            if (speed > _curMax)
            {
                _curMax = speed;
                _root.MaxVal.Content = "Max: " + _curMax.ToString();
            }

            if (speed >= max)
            {
                curLevel = 1;
            }
            else
            {
                curLevel = (double)speed / max;
            }

            _root.fill.Height = _root.bar.Height * curLevel;
        }

        /// <summary>
        /// Reposition the Speed Meter adornment whenever the Editor is resized
        /// </summary>
        public void OnSizeChange()
        {
            //clear the adornment layer of previous adornments
            _adornmentLayer.RemoveAdornment(_root);

            //Place the image in the top right hand corner of the Viewport
            Canvas.SetLeft(_root, _view.ViewportRight - 80);
            Canvas.SetTop(_root, _view.ViewportTop + 15);

            //add the image to the adornment layer and make it relative to the viewports
            _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _root, null);
        }
    }
}
