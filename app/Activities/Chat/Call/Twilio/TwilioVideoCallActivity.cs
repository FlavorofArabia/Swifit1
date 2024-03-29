﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AT.Markushi.UI;
using Newtonsoft.Json;
using TwilioVideo;
using WoWonder.Activities.Chat.MsgTabbes;
using WoWonder.Helpers.CacheLoaders;
using WoWonder.Helpers.Controller;
using WoWonder.Helpers.Model;
using WoWonder.Helpers.Utils;
using WoWonder.SQLite;
using WoWonderClient;
using WoWonderClient.Classes.Call;
using WoWonderClient.Classes.Message;
using WoWonderClient.Requests;
using Manifest = Android.Manifest;
using VideoView = TwilioVideo.VideoView;

namespace WoWonder.Activities.Chat.Call.Twilio
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Locale | ConfigChanges.UiMode | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.ScreenLayout | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ResizeableActivity = true, SupportsPictureInPicture = true)]
    public class TwilioVideoCallActivity : AppCompatActivity, ISensorEventListener, TwilioVideoHelper.IListener
    {

        #region Variables Basic

        private TwilioVideoHelper TwilioVideo { get; set; }
        private string CallType = "0";
        private CallUserObject CallUserObject;


        private RelativeLayout MainUserViewProfile;
        private Button SwitchCamButton;
        private CircleButton EndCallButton, MuteAudioButton, MuteVideoButton;
        private ImageView UserImageView, PictureInToPictureButton;
        private TextView UserNameTextView, NoteTextView;
        private Timer TimerRequestWaiter = new Timer();
        private VideoView UserPrimaryVideo, ThumbnailVideo;
        private LocalVideoTrack LocalVideoTrack;
        private VideoTrack UserVideoTrack;
        private bool DataUpdated;
        private int CountSecondsOfOutGoingCall;
        private string LocalVideoTrackId, RemoteVideoTrackId;
        private MsgTabbedMainActivity GlobalContext;
        private SensorManager SensorManager;
        private Sensor Proximity;
        private readonly int SensorSensitivity = 4;

        #endregion

        #region General

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                Methods.App.FullScreenApp(this);

                Window.AddFlags(WindowManagerFlags.KeepScreenOn);

                // Create your application here
                SetContentView(Resource.Layout.TwilioVideoCallActivityLayout);

                SensorManager = (SensorManager)GetSystemService(SensorService);
                Proximity = SensorManager.GetDefaultSensor(SensorType.Proximity);

                GlobalContext = MsgTabbedMainActivity.GetInstance();
                //Get Value And Set Toolbar
                InitComponent();
                InitTwilioCall();
                MsgTabbedMainActivity.RunCall = true;
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnResume()
        {
            try
            {
                base.OnResume();
                SensorManager.RegisterListener(this, Proximity, SensorDelay.Normal);
                AddOrRemoveEvent(true);
                UpdateState();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnStart()
        {
            try
            {
                base.OnStart();
                UpdateState();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnPause()
        {
            try
            {
                DataUpdated = false;
                base.OnPause();
                AddOrRemoveEvent(false);
                SensorManager.UnregisterListener(this);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnRestart()
        {
            try
            {
                base.OnRestart();
                TwilioVideo = TwilioVideoHelper.GetOrCreate(this, TypeCall.Video);
                UpdateState();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override void OnTrimMemory(TrimMemory level)
        {
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                base.OnTrimMemory(level);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override void OnLowMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration);
                base.OnLowMemory();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnDestroy()
        {
            try
            {
                MsgTabbedMainActivity.RunCall = false;
                base.OnDestroy();
            }
            catch (Exception exception)
            {
                MsgTabbedMainActivity.RunCall = false;
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion

        #region Functions

        private void InitComponent()
        {
            try
            {
                MainUserViewProfile = FindViewById<RelativeLayout>(Resource.Id.userInfoview_container);
                UserPrimaryVideo = FindViewById<VideoView>(Resource.Id.userthumbnailVideo); // userthumbnailVideo 
                ThumbnailVideo = FindViewById<VideoView>(Resource.Id.local_video_view_container); //local_video_view_container
                SwitchCamButton = FindViewById<Button>(Resource.Id.switch_cam_button);
                MuteVideoButton = FindViewById<CircleButton>(Resource.Id.mute_video_button);
                EndCallButton = FindViewById<CircleButton>(Resource.Id.end_call_button);
                MuteAudioButton = FindViewById<CircleButton>(Resource.Id.mute_audio_button);
                UserImageView = FindViewById<ImageView>(Resource.Id.userImageView);
                UserNameTextView = FindViewById<TextView>(Resource.Id.userNameTextView);
                NoteTextView = FindViewById<TextView>(Resource.Id.noteTextView);

                PictureInToPictureButton = FindViewById<ImageView>(Resource.Id.pictureintopictureButton);
                if (!PackageManager.HasSystemFeature(PackageManager.FeaturePictureInPicture))
                    PictureInToPictureButton.Visibility = ViewStates.Gone;

                MuteVideoButton.Selected = true;
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private void AddOrRemoveEvent(bool addEvent)
        {
            try
            {
                // true +=  // false -=
                if (addEvent)
                {
                    SwitchCamButton.Click += Switch_cam_button_Click;
                    MuteVideoButton.Click += Mute_video_button_Click;
                    EndCallButton.Click += End_call_button_Click;
                    MuteAudioButton.Click += Mute_audio_button_Click;
                    PictureInToPictureButton.Click += PictureInToPictureButton_Click;
                }
                else
                {
                    SwitchCamButton.Click -= Switch_cam_button_Click;
                    MuteVideoButton.Click -= Mute_video_button_Click;
                    EndCallButton.Click -= End_call_button_Click;
                    MuteAudioButton.Click -= Mute_audio_button_Click;
                    PictureInToPictureButton.Click -= PictureInToPictureButton_Click;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        #endregion

        #region Events

        private void PictureInToPictureButton_Click(object sender, EventArgs e)
        {
            try
            {
                var actions = new List<RemoteAction>();
                //.SetActions(new List<RemoteAction>().Add(new RemoteAction().Title = "")
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    var param = new PictureInPictureParams.Builder().SetAspectRatio(new Rational(9, 16)).Build();
                    EnterPictureInPictureMode(param);
                }
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void Switch_cam_button_Click(object sender, EventArgs e)
        {
            try
            {
                TwilioVideo.FlipCamera();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void Mute_video_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (MuteVideoButton.Selected)
                {
                    MuteVideoButton.SetImageResource(Resource.Drawable.ic_camera_video_mute);

                    MuteVideoButton.Selected = false;
                }
                else
                {
                    MuteVideoButton.SetImageResource(Resource.Drawable.ic_camera_video_open);
                    MuteVideoButton.Selected = true;
                }

                var isVideoEnabled = MuteVideoButton.Selected;
                FindViewById(Resource.Id.local_video_container).Visibility = isVideoEnabled ? ViewStates.Visible : ViewStates.Gone;
                LocalVideoTrack.Enable(isVideoEnabled);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void Mute_audio_button_Click(object sender, EventArgs e)
        {

            try
            {
                if (MuteAudioButton.Selected)
                {
                    MuteAudioButton.Selected = false;
                    MuteAudioButton.SetImageResource(Resource.Drawable.ic_camera_mic_open);
                }
                else
                {
                    MuteAudioButton.Selected = true;
                    MuteAudioButton.SetImageResource(Resource.Drawable.ic_camera_mic_mute);
                }

                TwilioVideo.Mute(MuteAudioButton.Selected);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void End_call_button_Click(object sender, EventArgs e)
        {
            try
            {
                PollyController.RunRetryPolicyFunction(new List<Func<Task>> { () => RequestsAsync.Call.DeclineCallTwilioAsync(CallUserObject.Data.Id, TypeCall.Video) });
                FinishCall();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion

        #region Sensor System

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            try
            {
                // Do something here if sensor accuracy changes.
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void OnSensorChanged(SensorEvent e)
        {
            try
            {
                if (e.Sensor.Type == SensorType.Proximity)
                {
                    if (e.Values[0] >= -SensorSensitivity && e.Values[0] <= SensorSensitivity)
                    {
                        //near 
                        GlobalContext?.SetOffWakeLock();
                    }
                    else
                    {
                        //far 
                        GlobalContext?.SetOnWakeLock();
                    }
                }
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion

        #region Twilio  

        private async void InitTwilioCall()
        {
            try
            {
                bool granted =
                    ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) ==
                    Permission.Granted &&
                    ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.RecordAudio) ==
                    Permission.Granted;

                CheckVideoCallPermissions(granted);

                CallType = Intent?.GetStringExtra("type") ?? ""; // Twilio_video_call , Twilio_audio_call,Agora_video_call_recieve,Agora_audio_call_recieve

                if (!string.IsNullOrEmpty(Intent?.GetStringExtra("callUserObject")))
                    CallUserObject = JsonConvert.DeserializeObject<CallUserObject>(Intent?.GetStringExtra("callUserObject") ?? "");

                switch (CallType)
                {
                    case "Twilio_video_call":
                        {
                            if (!string.IsNullOrEmpty(CallUserObject.Data.AccessToken))
                            {
                                if (!string.IsNullOrEmpty(CallUserObject.UserId))
                                    Load_userWhenCall();

                                TwilioVideo = TwilioVideoHelper.GetOrCreate(this, TypeCall.Video);
                                UpdateState();
                                NoteTextView.Text = GetText(Resource.String.Lbl_Waiting_for_answer);

                                var (apiStatus, respond) = await RequestsAsync.Call.AnswerCallTwilioAsync(CallUserObject.Data.Id, TypeCall.Video);
                                if (apiStatus == 200)
                                {
                                    ConnectToRoom();

                                    var ckd = GlobalContext?.LastCallsTab?.MAdapter?.MCallUser?.FirstOrDefault(a => a.Id == CallUserObject.Data.Id); // id >> Call_Id
                                    if (ckd == null)
                                    {
                                        Classes.CallUser cv = new Classes.CallUser
                                        {
                                            Id = CallUserObject.Data.Id,
                                            UserId = CallUserObject.UserId,
                                            Avatar = CallUserObject.Avatar,
                                            Name = CallUserObject.Name,
                                            FromId = CallUserObject.Data.FromId,
                                            Active = CallUserObject.Data.Active,
                                            Time = "Answered call",
                                            Status = CallUserObject.Data.Status,
                                            RoomName = CallUserObject.Data.RoomName,
                                            Type = CallType,
                                            TypeIcon = "Accept",
                                            TypeColor = "#008000"
                                        };

                                        GlobalContext?.LastCallsTab?.MAdapter?.Insert(cv);

                                        SqLiteDatabase dbDatabase = new SqLiteDatabase();
                                        dbDatabase.Insert_CallUser(cv);

                                    }
                                }
                                //else Methods.DisplayReportResult(this, respond);
                            }

                            break;
                        }
                    case "Twilio_video_calling_start":
                        NoteTextView.Text = GetText(Resource.String.Lbl_Calling_video);
                        TwilioVideo = TwilioVideoHelper.GetOrCreate(this, TypeCall.Video);

                        Methods.AudioRecorderAndPlayer.PlayAudioFromAsset("outgoin_call.mp3", "Looping");

                        if (AppSettings.ConnectionTypeChat == InitializeWoWonder.ConnectionType.Socket)
                            UserDetails.Socket?.EmitAsync_Create_callEvent(CallUserObject.UserId);

                        StartApiService();

                        UpdateState();
                        break;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private void Load_userWhenCall()
        {
            try
            {
                MainUserViewProfile.Visibility = ViewStates.Visible;
                UserNameTextView.Text = CallUserObject.Name;

                //profile_picture
                GlideImageLoader.LoadImage(this, CallUserObject.Avatar, UserImageView, ImageStyle.CircleCrop, ImagePlaceholders.Drawable);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private void StartApiService()
        {
            if (!Methods.CheckConnectivity())
                ToastUtils.ShowToast(this, GetString(Resource.String.Lbl_CheckYourInternetConnection), ToastLength.Short);
            else
                PollyController.RunRetryPolicyFunction(new List<Func<Task>> { LoadProfileFromUserId });
        }

        private async Task LoadProfileFromUserId()
        {
            Load_userWhenCall();
            var (apiStatus, respond) = await RequestsAsync.Call.CreateNewCallTwilioAsync(CallUserObject.UserId, TypeCall.Video);
            if (apiStatus == 200)
            {
                if (respond is CallUserObject.DataCallUser result)
                {
                    CallUserObject.Data.Id = result.Id.ToString();
                    CallUserObject.Data.AccessToken = result.AccessToken;
                    CallUserObject.Data.AccessToken2 = result.AccessToken2;
                    CallUserObject.Data.RoomName = result.RoomName;

                    TimerRequestWaiter = new Timer { Interval = 5000 };
                    TimerRequestWaiter.Elapsed += TimerCallRequestAnswer_Waiter_Elapsed;
                    TimerRequestWaiter.Start();
                }
            }
            else
            {
                FinishCall();

                //Methods.DisplayReportResult(this, respond);
            }
        }

        private async void TimerCallRequestAnswer_Waiter_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var (apiStatus, respond) = await RequestsAsync.Call.CheckForAnswerTwilioAsync(CallUserObject.Data.Id, TypeCall.Video);
                switch (apiStatus)
                {
                    case 200:
                        {
                            Methods.AudioRecorderAndPlayer.StopAudioFromAsset();

                            if (!string.IsNullOrEmpty(CallUserObject.Data.AccessToken))
                            {
                                TimerRequestWaiter.Enabled = false;
                                TimerRequestWaiter.Stop();
                                TimerRequestWaiter.Close();

                                RunOnUiThread(async () =>
                                {
                                    await Task.Delay(1000);

                                    TwilioVideo?.UpdateToken(CallUserObject.Data.AccessToken2);
                                    TwilioVideo?.JoinRoom(ApplicationContext, CallUserObject.Data.RoomName);

                                    Classes.CallUser cv = new Classes.CallUser
                                    {
                                        Id = CallUserObject.Data.Id,
                                        UserId = CallUserObject.UserId,
                                        Avatar = CallUserObject.Avatar,
                                        Name = CallUserObject.Name,
                                        FromId = CallUserObject.Data.FromId,
                                        Active = CallUserObject.Data.Active,
                                        Time = "Answered call",
                                        Status = CallUserObject.Data.Status,
                                        RoomName = CallUserObject.Data.RoomName,
                                        Type = CallType,
                                        TypeIcon = "Accept",
                                        TypeColor = "#008000"
                                    };

                                    GlobalContext?.LastCallsTab?.MAdapter?.Insert(cv);

                                    SqLiteDatabase dbDatabase = new SqLiteDatabase();
                                    dbDatabase.Insert_CallUser(cv);

                                });
                            }

                            break;
                        }
                    case 300:
                        RunOnUiThread(() =>
                        {
                            try
                            {
                                if (CountSecondsOfOutGoingCall < 70)
                                {
                                    CountSecondsOfOutGoingCall += 10;
                                }
                                else
                                {
                                    //Call Is inactive 
                                    TimerRequestWaiter.Enabled = false;
                                    TimerRequestWaiter.Stop();
                                    TimerRequestWaiter.Close();

                                    var ckd = GlobalContext?.LastCallsTab?.MAdapter?.MCallUser?.FirstOrDefault(a => a.Id == CallUserObject.Data.Id); // id >> Call_Id
                                    if (ckd == null)
                                    {
                                        Classes.CallUser cv = new Classes.CallUser
                                        {
                                            Id = CallUserObject.Data.Id,
                                            UserId = CallUserObject.UserId,
                                            Avatar = CallUserObject.Avatar,
                                            Name = CallUserObject.Name,
                                            FromId = CallUserObject.Data.FromId,
                                            Active = CallUserObject.Data.Active,
                                            Time = "Missed call",
                                            Status = CallUserObject.Data.Status,
                                            RoomName = CallUserObject.Data.RoomName,
                                            Type = CallType,
                                            TypeIcon = "Cancel",
                                            TypeColor = "#FF0000"
                                        };

                                        GlobalContext?.LastCallsTab?.MAdapter?.Insert(cv);

                                        SqLiteDatabase dbDatabase = new SqLiteDatabase();
                                        dbDatabase.Insert_CallUser(cv);

                                    }

                                    FinishCall();
                                }
                            }
                            catch (Exception exception)
                            {
                                Methods.DisplayReportResultTrack(exception);
                            }
                        });
                        break;
                    default:
                        RunOnUiThread(() =>
                        {
                            try
                            {
                                //Call Is inactive 
                                TimerRequestWaiter.Enabled = false;
                                TimerRequestWaiter.Stop();
                                TimerRequestWaiter.Close();

                                var ckd = GlobalContext?.LastCallsTab?.MAdapter?.MCallUser?.FirstOrDefault(a => a.Id == CallUserObject.Data.Id); // id >> Call_Id
                                if (ckd == null)
                                {
                                    Classes.CallUser cv = new Classes.CallUser
                                    {
                                        Id = CallUserObject.Data.Id,
                                        UserId = CallUserObject.UserId,
                                        Avatar = CallUserObject.Avatar,
                                        Name = CallUserObject.Name,
                                        FromId = CallUserObject.Data.FromId,
                                        Active = CallUserObject.Data.Active,
                                        Time = "Declined call",
                                        Status = CallUserObject.Data.Status,
                                        RoomName = CallUserObject.Data.RoomName,
                                        Type = CallType,
                                        TypeIcon = "Declined",
                                        TypeColor = "#FF8000"
                                    };

                                    GlobalContext?.LastCallsTab?.MAdapter?.Insert(cv);

                                    SqLiteDatabase dbDatabase = new SqLiteDatabase();
                                    dbDatabase.Insert_CallUser(cv);

                                }

                                FinishCall();

                                //Methods.DisplayReportResult(this, respond);
                            }
                            catch (Exception exception)
                            {
                                Methods.DisplayReportResultTrack(exception);
                            }
                        });
                        break;
                }
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion

        #region TwilioVideoHelper.IListener

        public void SetLocalVideoTrack(LocalVideoTrack track)
        {
            try
            {
                if (LocalVideoTrack == null)
                {
                    LocalVideoTrack = track;
                    var trackId = track?.Name;
                    if (LocalVideoTrackId == trackId)
                    {
                    }
                    else
                    {
                        LocalVideoTrackId = trackId;
                        LocalVideoTrack.AddSink(ThumbnailVideo);
                        ThumbnailVideo.Visibility = LocalVideoTrack == null ? ViewStates.Invisible : ViewStates.Visible;
                    }
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void SetRemoteVideoTrack(VideoTrack track)
        {
            try
            {
                var trackId = track?.Name;

                if (RemoteVideoTrackId == trackId)
                    return;

                RemoteVideoTrackId = trackId;
                if (UserVideoTrack == null)
                {
                    UserVideoTrack = track;
                    UserVideoTrack?.AddSink(UserPrimaryVideo);
                    ThumbnailVideo.Visibility = LocalVideoTrack == null ? ViewStates.Invisible : ViewStates.Visible;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void RemoveLocalVideoTrack(LocalVideoTrack track)
        {
            try
            {
                SetLocalVideoTrack(null);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void RemoveRemoteVideoTrack(VideoTrack track)
        {
            try
            {
                MainUserViewProfile.Visibility = ViewStates.Visible;
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void OnRoomConnected(string roomId)
        {

        }

        public void OnRoomDisconnected(TwilioVideoHelper.StopReason reason)
        {
            try
            {
                ToastUtils.ShowToast(this, GetText(Resource.String.Lbl_Room_Disconnected), ToastLength.Short);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void OnParticipantConnected(string participantId)
        {
            try
            {
                MainUserViewProfile.Visibility = ViewStates.Gone;
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void OnParticipantDisconnected(string participantId)
        {
            RunOnUiThread(FinishCall);
        }

        public void SetCallTime(int seconds)
        {

        }

        #endregion

        #region Permissions

        private void RequestCameraAndMicrophonePermissions()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera) ||
                ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.RecordAudio))
                ToastUtils.ShowToast(this, GetText(Resource.String.Lbl_Need_Camera), ToastLength.Long);
            else
                ActivityCompat.RequestPermissions(this,
                    new[] { Manifest.Permission.Camera, Manifest.Permission.RecordAudio, Manifest.Permission.ModifyAudioSettings }, 1);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == 1)
                CheckVideoCallPermissions(grantResults.Any(p => p == Permission.Denied));
        }

        private void CheckVideoCallPermissions(bool granted)
        {
            if (!granted)
                RequestCameraAndMicrophonePermissions();
        }


        #endregion

        private void ConnectToRoom()
        {
            TwilioVideo?.UpdateToken(CallUserObject.Data.AccessToken);
            TwilioVideo?.JoinRoom(this, CallUserObject.Data.RoomName);
        }

        private void UpdateState()
        {
            try
            {
                if (DataUpdated)
                    return;
                DataUpdated = true;
                TwilioVideo?.Bind(this);
                UpdatingState();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override bool OnSupportNavigateUp()
        {
            TryCancelCall();
            return true;
        }

        protected virtual void UpdatingState()
        {
        }

        private void TryCancelCall()
        {
            CloseScreen();
        }

        private void CloseScreen()
        {
            Finish();
        }

        public override void OnBackPressed()
        {
            FinishCall();
        }

        protected virtual void FinishCall()
        {
            try
            {
                //Close Api Starts here >> 
                if (!Methods.CheckConnectivity())
                    ToastUtils.ShowToast(this, GetString(Resource.String.Lbl_CheckYourInternetConnection), ToastLength.Short);
                else
                    PollyController.RunRetryPolicyFunction(new List<Func<Task>> { () => RequestsAsync.Call.CloseCallTwilioAsync(CallUserObject.Data.Id, TypeCall.Video) });

                if (TwilioVideo != null && TwilioVideo.ClientIsReady)
                {
                    TwilioVideo.Unbind(this);
                    TwilioVideo.FinishCall();
                }
                Methods.AudioRecorderAndPlayer.StopAudioFromAsset();
                Finish();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }
    }
}