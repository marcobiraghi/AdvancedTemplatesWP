using System;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TobBarApplication.Controls
{
    public class FrameContainer : ContentControl
    {
        public UIElement TopBarPanel
        {
            get { return (UIElement)GetValue(TopBarPanelProperty); }
            set { SetValue(TopBarPanelProperty, value); }
        }

        public static readonly DependencyProperty TopBarPanelProperty =
            DependencyProperty.Register("TopBarPanel", typeof(UIElement), typeof(FrameContainer), new PropertyMetadata(null));

       public bool UpdateTopBarOnPageNavigated
        {
            get { return (bool)GetValue(UpdateTopBarOnPageNavigatedProperty); }
            set { SetValue(UpdateTopBarOnPageNavigatedProperty, value); }
        }

        public static readonly DependencyProperty UpdateTopBarOnPageNavigatedProperty =
            DependencyProperty.Register("UpdateTopBarOnPageNavigated", typeof(bool), typeof(FrameContainer), new PropertyMetadata(false));


        private ContentControl firstContentPresenter, topBar;
        private Frame appFrame;
        private Grid mainGrid, topBarGrid;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            firstContentPresenter = GetTemplateChild("FirstContentPresenter") as ContentControl;
            if (firstContentPresenter != null)
                appFrame = firstContentPresenter.Content as Frame;

            loadBar(true);
            appFrame.Navigated += (sender, e) => loadBar();

            topBar = GetTemplateChild("TopBar") as ContentControl;
            topBarGrid = GetTemplateChild("TopBarGrid") as Grid;

            mainGrid = GetTemplateChild("MainGrid") as Grid;

            var view = ApplicationView.GetForCurrentView();
            if (view != null && view.DesiredBoundsMode != ApplicationViewBoundsMode.UseCoreWindow)
                view.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

            var statusBar = StatusBar.GetForCurrentView();
            if (statusBar != null)
            {
                statusBar.BackgroundColor = new Color() { R = 0, G = 0, B = 0 };
                statusBar.ForegroundColor = new Color() { R = 255, G = 255, B = 255 };
                statusBar.BackgroundOpacity = 0;
            }

            DisplayInformation.GetForCurrentView().OrientationChanged += FrameContainer_OrientationChanged;
        }

        async void FrameContainer_OrientationChanged(DisplayInformation sender, object args)
        {
            if (sender.CurrentOrientation == DisplayOrientations.Portrait)
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    await statusBar.ShowAsync();
                    topBarGrid.Height = topBarGrid.ActualHeight + 25;
                }
            }
            else
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    await statusBar.HideAsync();
                    topBarGrid.Height = topBarGrid.ActualHeight - 25;
                }
            }
        }

        private void loadBar(bool force = false)
        {
            if (UpdateTopBarOnPageNavigated || force)
                TopBarPanel = (((Frame)firstContentPresenter.Content).Content as ViewPage).TopBarContent;
        }
    }
}
