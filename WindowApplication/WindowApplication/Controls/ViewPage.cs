using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowApplication.Common;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WindowApplication.Controls
{
    public class ViewPage : Page
    {
        public UIElement MenuContent
        {
            get { return (UIElement)GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(UIElement), typeof(ViewPage), new PropertyMetadata(null));

        public UIElement TopBarContent
        {
            get { return (UIElement)GetValue(TopBarContentProperty); }
            set { SetValue(TopBarContentProperty, value); }
        }

        public static readonly DependencyProperty TopBarContentProperty =
            DependencyProperty.Register("TopBarContent", typeof(UIElement), typeof(ViewPage), new PropertyMetadata(null));

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ViewPage()
            : base()
        {
            navigationHelper = new NavigationHelper(this);

            var view = ApplicationView.GetForCurrentView();
            if (view.DesiredBoundsMode != ApplicationViewBoundsMode.UseCoreWindow)
                view.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

            StatusBar.GetForCurrentView().BackgroundColor = new Color() { R = 0, G = 0, B = 0 };
            StatusBar.GetForCurrentView().ForegroundColor = new Color() { R = 255, G = 255, B = 255 };
            StatusBar.GetForCurrentView().BackgroundOpacity = 0;
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
            base.OnNavigatedFrom(e);
        }

        void ViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            var statusBar = StatusBar.GetForCurrentView();
            if (statusBar != null)
                statusBar.ProgressIndicator.Text = "loading";
        }

        protected async Task ShowTrayLoader(bool show)
        {
            var statusBar = StatusBar.GetForCurrentView();
            if (statusBar == null)
                return;

            if (show)
                await statusBar.ProgressIndicator.ShowAsync();
            else
                await statusBar.ProgressIndicator.HideAsync();
        }
    }
}
