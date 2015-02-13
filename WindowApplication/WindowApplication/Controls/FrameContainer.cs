using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace WindowApplication.Controls
{
    public class FrameContainer : ContentControl
    {
        private ContentControl FirstContentPresenter;
        private Frame AppFrame;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FirstContentPresenter = GetTemplateChild("FirstContentPresenter") as ContentControl;
            if (FirstContentPresenter != null)
                AppFrame = FirstContentPresenter.Content as Frame;
        }
    }
}
