﻿using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// using Community.VisualStudio.Toolkit;

namespace WpfAppToExtractVsMonikers
{
    /// <summary>
    /// Interaction logic for KnownMonikersExplorerControl.xaml
    /// </summary>
    public partial class KnownMonikersExplorerControl : UserControl
    {
        private readonly ServicesDTO _state;

        public KnownMonikersExplorerControl()
        {
            PropertyInfo[] properties = typeof(KnownMonikers).GetProperties(BindingFlags.Static | BindingFlags.Public);

            var state = new ServicesDTO
            {
                Monikers = properties.Select(p => new KnownMonikersViewModel(p.Name, (ImageMoniker)p.GetValue(null, null))).ToList()
            };
            _state = state;
            Loaded += OnLoaded;
            DataContext = this;
            InitializeComponent();
        }

        public IEnumerable<KnownMonikersViewModel> Monikers => _state.Monikers;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            list.KeyUp += List_KeyUp;
            list.MouseDoubleClick += Export_Click;
            txtFilter.Focus();

            var view = (CollectionView)CollectionViewSource.GetDefaultView(list.ItemsSource);
            view.Filter = UserFilter;
        }

        private void List_KeyUp(object sender, KeyEventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (e.Key == Key.Enter)
            {
                Export_Click(this, new RoutedEventArgs());
            }
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CopyName_Click(this, new RoutedEventArgs());
            }
        }


        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }

            return (item as KnownMonikersViewModel).MatchSearchTerm(txtFilter.Text.Trim());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox)sender).Text;
            // RefreshAsync(text).FireAndForget();
        }

        private async Task RefreshAsync(string text)
        {
            await Task.Delay(200);

            if (text == txtFilter.Text)
            {
                CollectionViewSource.GetDefaultView(list.ItemsSource).Refresh();
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            MessageBox.Show("Work in progress...");

            //var model = (KnownMonikersViewModel)list.SelectedItem;
            //var export = new ExportMonikerWindow(model)
            //{
            //    Owner = Application.Current.MainWindow
            //};

            //export.ShowDialog();
        }

        private void CopyName_Click(object sender, RoutedEventArgs e)
        {
            var model = (KnownMonikersViewModel)list.SelectedItem;
            Clipboard.SetText(model.ImageMonikerName);
        }

        private void CopyGuidAndId_Click(object sender, RoutedEventArgs e)
        {
            var model = (KnownMonikersViewModel)list.SelectedItem;
            Clipboard.SetText($"{model.Moniker.Guid}, {model.Moniker.Id}");
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem listViewItem = VisualTreeHelperExtensions.FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (listViewItem is null)
            {
                e.Handled = true;
            }
        }

        internal bool SelectMoniker(ImageMoniker moniker)
        {
            KnownMonikersViewModel match = _state.Monikers.FirstOrDefault((x) => x.Moniker.Equals(moniker));
            if (match != null)
            {
                list.SelectedItem = match;
                list.ScrollIntoView(match);
                return true;
            }
            return false;
        }
    }
}
