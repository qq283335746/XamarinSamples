using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Yibi.Samples.Core.Models;
using Yibi.Samples.Messages;

namespace Yibi.Samples
{
    public class TaskCounter
    {
        public async Task RunCounter(CancellationToken token)
        {
            AlarmClockInfo CurrentAlarmClockInfo = null;

            await Task.Run(async () => {

                while (true)
                {
                    var alarmClockInfo = await App.DbContext.GetEnableAlarmClockAsync();
                    if (alarmClockInfo != null)
                    {
                        if (CurrentAlarmClockInfo != null && CurrentAlarmClockInfo.ID == alarmClockInfo.ID)
                        {
                            await Task.Delay(1000);
                            continue;
                        }

                        CurrentAlarmClockInfo = alarmClockInfo;

                        Device.BeginInvokeOnMainThread(() => {
                            MessagingCenter.Send<AlarmClockInfo>(alarmClockInfo, "AlarmClockInfoMessage");
                        });
                    }

                    await Task.Delay(1000);
                }

            }, token);
        }
    }
}
