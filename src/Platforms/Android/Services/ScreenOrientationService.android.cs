using nasa_maui.Interfaces;

namespace nasa_maui.Services
{
    public class ScreenOrientationService : IScreenOrientationService
    {
        public void SetScreenOrientation(ScreenOrientation screenOrientation)
        {
            Platform.CurrentActivity.RequestedOrientation = screenOrientation switch {
                ScreenOrientation.Landscape => Android.Content.PM.ScreenOrientation.Landscape,
                ScreenOrientation.Portrait => Android.Content.PM.ScreenOrientation.Portrait,
                _ => Android.Content.PM.ScreenOrientation.Portrait
            };

            AndroidX.Core.View.WindowCompat.SetDecorFitsSystemWindows(Platform.CurrentActivity.Window, false);
            var windowInsetsControllerCompat = AndroidX.Core.View.WindowCompat.GetInsetsController(Platform.CurrentActivity.Window, Platform.CurrentActivity.Window.DecorView);
            var types = AndroidX.Core.View.WindowInsetsCompat.Type.StatusBars() |
                        AndroidX.Core.View.WindowInsetsCompat.Type.NavigationBars();

            windowInsetsControllerCompat.SystemBarsBehavior = AndroidX.Core.View.WindowInsetsControllerCompat.BehaviorShowBarsBySwipe;
            
            if(screenOrientation == ScreenOrientation.Landscape)
            {
                windowInsetsControllerCompat.Hide(types);
            }
            else
            {
                windowInsetsControllerCompat.Show(types);
            }
        }
    }
}
