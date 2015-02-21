using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TobBarApplication.Controls;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace TobBarApplication
{
    public sealed partial class App : Application
    {
        public static FrameContainer AppContainer { get; set; }
        public static Frame RootFrame { get; set; }

        private TransitionCollection transitions;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            FrameContainer appContainer = Window.Current.Content as FrameContainer;
            Frame rootFrame = null;

            if (appContainer != null)
            {
                AppContainer = appContainer;
                rootFrame = appContainer.Content as Frame;
                RootFrame = rootFrame;
            }


            // Initialize application if not initialized
            if (appContainer == null)
            {
                // Create application container and a root frame
                // Set up a new Frame to be placed inside container
                appContainer = new FrameContainer();
                rootFrame = new Frame() { Background = null };
                RootFrame = rootFrame;
                appContainer.Content = rootFrame;
                AppContainer = appContainer;

                // TODO: customize this value to obtain the best caching value for your app
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load previous application state
                }

                // Set application container as current frame
                Window.Current.Content = appContainer;
            }

            if (rootFrame.Content == null)
            {
                // Remove first animation
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // Navigate to first page, when stack is not recovered
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            //Workaround for global font setting
            RootFrame.FontFamily = new FontFamily("Segoe WP");

            // Activate current Window
            Window.Current.Activate();
        }

        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;

        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state
            deferral.Complete();
        }
    }
}