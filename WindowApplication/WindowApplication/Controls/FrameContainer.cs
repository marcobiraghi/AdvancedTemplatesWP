using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WindowApplication.Controls
{
    public class FrameContainer : ContentControl
    {
        public Control TopBarPanel
        {
            get { return (Control)GetValue(TopBarPanelProperty); }
            set { SetValue(TopBarPanelProperty, value); }
        }

        public static readonly DependencyProperty TopBarPanelProperty =
            DependencyProperty.Register("TopBarPanel", typeof(Control), typeof(FrameContainer), new PropertyMetadata(null));


        public Control MenuPanel
        {
            get { return (Control)GetValue(MenuPanelProperty); }
            set { SetValue(MenuPanelProperty, value); }
        }

        public static readonly DependencyProperty MenuPanelProperty =
            DependencyProperty.Register("MenuPanel", typeof(Control), typeof(FrameContainer), new PropertyMetadata(null));

        public bool UpdateMenuOnPageNavigated { get; set; }
        public bool UpdateTopBarOnPageNavigated { get; set; }

        private ContentControl firstContentPresenter, topBar, menu;
        private Border menuButton, menuShadow;
        private Frame appFrame;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            firstContentPresenter = GetTemplateChild("FirstContentPresenter") as ContentControl;
            if (firstContentPresenter != null)
                appFrame = firstContentPresenter.Content as Frame;

            appFrame.Navigated += (sender, e) => loadMenuAndBar();

            topBar = GetTemplateChild("TopBar") as ContentControl;
            menu = GetTemplateChild("Menu") as ContentControl;
            menuShadow = GetTemplateChild("MenuShadow") as Border;

            menuButton = GetTemplateChild("MenuButton") as Border;
            menuButton.Tapped += (sender, e) => openMenu();

        }

        private object openMenu()
        {
            throw new NotImplementedException();
        }

        private object closeMenu()
        {
            throw new NotImplementedException();
        }

        private void loadMenuAndBar()
        {
            throw new NotImplementedException();
        }

    }
}
