// © 2023 Marcello De Bonis. All rights reserved

/*

using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{

    public class NotificationManager : Singleton<NotificationManager>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            
        }
        
        // Start is called before the first frame update
        void Start()
        {
            //Create the Android Notification Channel to send messages through
            var channel = new AndroidNotificationChanne()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.Default,
                Description = "Reminder notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);


            //Create the notification that is going to be sent
            var notification = new AndroidNotification();
            notification.Title = "Sono una notifica, coglione!!!";
            notification.Text = "Dio GESù CRIST";
            notification.FireTime = System.DateTime.Now.AddSeconds(15);

            //Send the notification
            AndroidNotificationCenter.SendNotification(notification, "channel_id");


        }
    }
}

*/
