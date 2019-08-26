using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Yibi.Samples.Pages;

namespace Yibi.Samples.Droid
{
    [BroadcastReceiver]
    public class OneShotAlarm : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //var name = new Intent(this, typeof(ShowAlarmClock));
            //StartActivity(name);

            //Toast.MakeText(context, "this is yibi alarm message receive", ToastLength.Long).Show();

        }
    }
}