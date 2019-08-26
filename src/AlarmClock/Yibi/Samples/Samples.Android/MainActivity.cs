using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Xamarin.Forms;
using Yibi.Samples.Messages;
using Yibi.Samples.Droid.Services;

namespace Yibi.Samples.Droid
{
    [Activity(Label = "Yibi.Samples", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Toast repeating;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //OneShotAlarm();

            WireUpLongRunningTask();

            LoadApplication(new App());
        }

        void OneShotAlarm()
        {
            var intent = new Intent(this, typeof(OneShotAlarm));
            var source = PendingIntent.GetBroadcast(this, 0, intent, 0);

            // Schedule the alarm for 30 seconds from now!
            var am = (AlarmManager)GetSystemService(AlarmService);
            am.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 30 * 1000, source);

            // Tell the user about what we did.
            if (repeating != null)
                repeating.Cancel();
            repeating = Toast.MakeText(this, "ha ha alarm now", ToastLength.Long);
            repeating.Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void WireUpLongRunningTask()
        {
            MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTaskMessage", message => {
                var intent = new Intent(this, typeof(LongRunningTaskService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTaskMessage", message => {
                var intent = new Intent(this, typeof(LongRunningTaskService));
                StopService(intent);
            });
        }
    }
}