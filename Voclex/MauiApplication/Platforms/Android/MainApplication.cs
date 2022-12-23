using Android.App;
using Android.Runtime;

namespace MauiApplication
{
    [Application]
    public class MainApplication : Microsoft.Maui.MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}