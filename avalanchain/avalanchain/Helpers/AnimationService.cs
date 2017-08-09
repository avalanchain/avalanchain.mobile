using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace avalanchain
{
    public class AnimationService
    {
        public static async Task AnimateItem(View uiElement, uint duration)
        {
            await Task.WhenAll(new Task[] {
                uiElement.ScaleTo(1.5, duration, Easing.CubicIn),
                uiElement.FadeTo(1, duration/2, Easing.CubicInOut).ContinueWith(
                    _ =>
                    {
                        // Queing on UI to workaround an issue with Forms 2.1
                        Device.BeginInvokeOnMainThread(() => {
                            uiElement.ScaleTo(1, duration, Easing.CubicOut);
                        });
                    })
            });
        }
    }
}
