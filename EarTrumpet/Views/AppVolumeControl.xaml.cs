﻿using EarTrumpet.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EarTrumpet.Views
{
    public partial class AppVolumeControl : UserControl
    {
        public event EventHandler<AppVolumeControlExpandedEventArgs> AppExpanded;

        public AppItemViewModel App { get { return (AppItemViewModel)GetValue(StreamProperty); } set { SetValue(StreamProperty, value); } }
        public static readonly DependencyProperty StreamProperty = DependencyProperty.Register(
          "App", typeof(AppItemViewModel), typeof(AppVolumeControl), new PropertyMetadata(new PropertyChangedCallback(AppChanged)));

        public AppVolumeControl()
        {
            InitializeComponent();

            PreviewMouseRightButtonUp += AppVolumeControl_PreviewMouseRightButtonUp;
        }

        private static void AppChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (AppVolumeControl)d;
            self.GridRoot.DataContext = self.App;
        }

        private void AppVolumeControl_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ExpandApp();
        }

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            App.IsMuted = !App.IsMuted;
            e.Handled = true;
        }

        public void ExpandApp()
        {
            AppExpanded?.Invoke(this, new AppVolumeControlExpandedEventArgs
            {
                ViewModel = App,
                Container = (UIElement)this,
            });
        }

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            App.RefreshDisplayName();
        }
    }

    public class AppVolumeControlExpandedEventArgs
    {
        public AppItemViewModel ViewModel;
        public UIElement Container;
    }
}
