﻿using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows;

namespace WpfReadOptionsValues
{
    [ComVisible(true)]
    [Guid(Constants.LinqAdvancedOptionPageGuid)]
    public class AdvancedOptionPage : UIElementDialogPage
    {
        private string optionValue = "alpha123567";

        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }
        protected override UIElement Child
        {
            get
            {
                AdvancedOptions page = new AdvancedOptions
                {
                    AdvancedOptionsPage = this
                };
                page.Initialize();
                return page;
            }
        }
    }
}
