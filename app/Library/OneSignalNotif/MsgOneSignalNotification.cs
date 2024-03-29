﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using Com.Onesignal;
using Newtonsoft.Json;
using WoWonder.Activities.Tabbes;
using WoWonder.Helpers.Model;
using WoWonder.Helpers.Utils;
using WoWonder.Library.OneSignalNotif.Models;
using WoWonder.SQLite;

namespace WoWonder.Library.OneSignalNotif
{
    public class MsgOneSignalNotification : Java.Lang.Object, OneSignal.IOSNotificationWillShowInForegroundHandler, OneSignal.IOSNotificationOpenedHandler, IOSSubscriptionObserver
    {
        //Force your app to Register Notification directly without loading it from server (For Best Result)
         
        private static string UserId, PostId, PageId, GroupId, EventId, Type;

        private static volatile MsgOneSignalNotification InstanceRenamed;
        public static MsgOneSignalNotification Instance
        {
            get
            {
                MsgOneSignalNotification localInstance = InstanceRenamed;
                if (localInstance == null)
                {
                    lock (typeof(MsgOneSignalNotification))
                    {
                        localInstance = InstanceRenamed;
                        if (localInstance == null)
                        {
                            InstanceRenamed = localInstance = new MsgOneSignalNotification();
                        }
                    }
                }
                return localInstance;

            }
        }

        public void RegisterNotificationDevice()
        {
            try
            {
                if (AppSettings.ShowNotification)
                {
                    if (!string.IsNullOrEmpty(AppSettings.MsgOneSignalAppId) || !string.IsNullOrWhiteSpace(AppSettings.MsgOneSignalAppId))
                    {
                        // OneSignal Initialization
                        OneSignal.InitWithContext(Application.Context);
                        OneSignal.SetAppId(AppSettings.MsgOneSignalAppId);

                        // OneSignal Methods
                        OneSignal.SetNotificationWillShowInForegroundHandler(this);
                        OneSignal.SetNotificationOpenedHandler(this);

                        // OneSignal Register
                        OneSignal.AddSubscriptionObserver(this);
                        OneSignal.DisablePush(false); 
                    }
                }
                else
                {
                    UnRegisterNotificationDevice();
                }
            }
            catch (Exception ex)
            {
                Methods.DisplayReportResultTrack(ex);
            }
        }

        public void UnRegisterNotificationDevice()
        {
            try
            {
                OneSignal.DisablePush(true);
                OneSignal.UnsubscribeWhenNotificationsAreDisabled(false);

                AppSettings.ShowNotification = false;
            }
            catch (Exception ex)
            {
                Methods.DisplayReportResultTrack(ex);
            }
        }

        public void IdsAvailable()
        {
            try
            {
                OSDeviceState device = OneSignal.DeviceState;

                if (device != null)
                {
                    //string email = device.EmailAddress;
                    //string emailId = device.EmailUserId;
                    //string pushToken = device.PushToken;
                    string userId = device.UserId;

                    //bool enabled = device.AreNotificationsEnabled();
                    bool subscribed = device.IsSubscribed;
                    //bool subscribedToOneSignal = device.IsEmailSubscribed;

                    if (subscribed && !string.IsNullOrEmpty(userId))
                        UserDetails.DeviceMsgId = userId;
                }
            }
            catch (Exception ex)
            {
                Methods.DisplayReportResultTrack(ex);
            }
        }

        public void NotificationWillShowInForeground(OSNotificationReceivedEvent result)
        {
            try
            {
                var jsonObject = result.ToJSONObject().ToString();
                Console.WriteLine(jsonObject);
                var notification = JsonConvert.DeserializeObject<OsObject.OsNotificationReceivedObject>(jsonObject);

                string title = notification.Notification.Title;
                string message = notification.Notification.Body;
                Dictionary<string, object> additionalData = notification.Notification.AdditionalData;

                if (additionalData?.Count > 0)
                {
                    string chatType = "", IdChat = "";
                    foreach (var item in additionalData)
                    {
                        switch (item.Key)
                        {
                            case "post_id":
                                PostId = item.Value.ToString();
                                break;
                            case "user_id":
                                UserId = item.Value.ToString();
                                chatType = "user";
                                IdChat = UserId;
                                break;
                            case "page_id":
                                PageId = item.Value.ToString();
                                chatType = "page";
                                IdChat = PageId + UserId;
                                break;
                            case "group_id":
                                GroupId = item.Value.ToString();
                                chatType = "group";
                                IdChat = GroupId;
                                break;
                            case "event_id":
                                EventId = item.Value.ToString();
                                break;
                            case "type":
                                Type = item.Value.ToString();
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(IdChat))
                    {
                        if (ListUtils.MuteList.Count == 0)
                        {
                            var sqLiteDatabase = new SqLiteDatabase();
                            ListUtils.MuteList = sqLiteDatabase.Get_MuteList();
                        }

                        var check = ListUtils.MuteList.FirstOrDefault(a => a.ChatId == IdChat && a.ChatType == chatType);
                        if (check != null)
                        {
                            OneSignal.ClearOneSignalNotifications();
                        }
                    }
                }
                 
                if (message.Contains("call") || message.Contains("Calling"))
                {
                    OneSignal.ClearOneSignalNotifications();
                }  
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long)?.Show(); //Allen
                Methods.DisplayReportResultTrack(ex);
            }
        }

        public void NotificationOpened(OSNotificationOpenedResult result)
        {
            try
            {
                //string actionId = result.Action.ActionId;
                //OSNotificationAction.ActionType type = result.Action.Type; // "ActionTaken" | "Opened"

                var jsonObject = result.ToJSONObject().ToString();
                Console.WriteLine(jsonObject);
                var notification = JsonConvert.DeserializeObject<OsObject.OsNotificationReceivedObject>(jsonObject);

                string title = notification.Notification.Title;
                string message = notification.Notification.Body;
                Dictionary<string, object> additionalData = notification.Notification.AdditionalData;
                 
                if (additionalData?.Count > 0)
                {
                    foreach (var item in additionalData)
                    {
                        switch (item.Key)
                        {
                            case "post_id":
                                PostId = item.Value.ToString();
                                break;
                            case "user_id":
                                UserId = item.Value.ToString();
                                break;
                            case "page_id":
                                PageId = item.Value.ToString();
                                break;
                            case "group_id":
                                GroupId = item.Value.ToString();
                                break;
                            case "event_id":
                                EventId = item.Value.ToString();
                                break;
                            case "type":
                            case "chat_type":
                                Type = item.Value.ToString();
                                break; 
                        }
                    }
                }

                //to : do
                //go to activity or fragment depending on data 
                Intent intent = new Intent(Application.Context, typeof(TabbedMainActivity));
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                intent.AddFlags(ActivityFlags.SingleTop);
                intent.SetAction(Intent.ActionView);
                intent.PutExtra("userId", UserId);
                intent.PutExtra("PostId", PostId);
                intent.PutExtra("PageId", PageId);
                intent.PutExtra("GroupId", GroupId);
                intent.PutExtra("EventId", EventId);
                intent.PutExtra("type", Type);
                intent.PutExtra("Notifier", "Chat");
                Application.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Methods.DisplayReportResultTrack(ex);
            }
        }

        public void OnOSSubscriptionChanged(OSSubscriptionStateChanges p0)
        {
            try
            {
                var jsonObject = p0.ToJSONObject().ToString();
                var notification = JsonConvert.DeserializeObject<OsObject.OsNotificationObject>(jsonObject);
                Console.WriteLine(notification);

                if (notification?.To.IsSubscribed != null && notification.To.IsSubscribed.Value && !string.IsNullOrEmpty(notification.To.UserId))
                    UserDetails.DeviceMsgId = notification.To.UserId;

                IdsAvailable();
            }
            catch (Exception ex)
            {
                Methods.DisplayReportResultTrack(ex);
            }
        } 
    }
}