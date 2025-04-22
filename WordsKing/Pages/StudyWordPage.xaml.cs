﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WordsKing.ViewModels;

namespace WordsKing.Pages
{
    /// <summary>
    /// StudyWordPage.xaml 的交互逻辑
    /// </summary>
    public partial class StudyWordPage : Page
    {
        public StudyWordPage(StudyWordPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void ScrollViewer_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}
