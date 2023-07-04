// © 2023 Marcello De Bonis. All rights reserved



using System;
//using Unity.Notifications.Android;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{

    public class NotificationManager : Singleton<NotificationManager>
    {

        #region Variables And Properties

        //REMBER OF ATTACHING THIS SCRIPT TO EVENTMANAGER

        #endregion
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
        }
        
        // Start is called before the first frame update
        void Start()
        {
            /*
            //Create the Android Notification Channel to send messages through
            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.Default,
                Description = "Reminder notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);


            //Create the notification that is going to be sent
            var notification = new AndroidNotification();
            notification.Title = "Sono un titolo!!!";
            notification.Text = "Sono una descrizione";
            notification.FireTime = System.DateTime.Now.AddSeconds(15);

            //Send the notification
            AndroidNotificationCenter.SendNotification(notification, "channel_id");

            */
        }
    }
}


