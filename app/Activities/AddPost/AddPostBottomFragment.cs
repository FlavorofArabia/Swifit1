using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDialogsCore;
using Android.Content;
using Android.OS; 
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomSheet;
using Newtonsoft.Json;
using WoWonder.Library.Anjo.Share;
using WoWonder.Library.Anjo.Share.Abstractions;
using WoWonder.Activities.Communities.Groups;
using WoWonder.Activities.Communities.Pages;
using WoWonder.Activities.NativePost.Post;
using WoWonder.Helpers.Utils;
using WoWonderClient.Classes.Global;
using WoWonderClient.Classes.Posts;
using Exception = System.Exception;
using WoWonder.Library.Anjo;
using WoWonder.Helpers.Controller;
using WoWonder.Activities.Chat.Editor;
using Android;
using Android.Content.PM;

namespace WoWonder.Activities.NativePost.Share
{
    public class AddPostBottomFragment : BottomSheetDialogFragment
    {
        #region  Variables Basic


        Tabbes.TabbedMainActivity GlobalContext;
        BottomSheetListView lv;
        string[] items;
        #endregion

        #region General

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            try
            {
                Context contextThemeWrapper = AppSettings.SetTabDarkTheme ? new ContextThemeWrapper(Activity, Resource.Style.MyTheme_Dark_Base) : new ContextThemeWrapper(Activity, Resource.Style.MyTheme_Base);
                LayoutInflater localInflater = inflater.CloneInContext(contextThemeWrapper);
                View view = localInflater?.Inflate(Resource.Layout.SimpleRecyclerView, container, false);
                GlobalContext = Tabbes.TabbedMainActivity.GetInstance();
                return view;

            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
                return null!;
            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            try
            {
                base.OnViewCreated(view, savedInstanceState);

                
                InitComponent(view);
                AddOrRemoveEvent(true);
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            AddOrRemoveEvent(false);
        }
        public override void OnLowMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration);
                base.OnLowMemory();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion

        #region Functions

        private void InitComponent(View view)
        {
            try
            {
                TextView title = view.FindViewById<TextView>(Resource.Id.txttitle);
                title.Text = GetText(Resource.String.Lbl_Addnewstory);
               lv = view.FindViewById<BottomSheetListView>(Resource.Id.SimplelistView);
                var arrayAdapter = new List<string>();
               
                arrayAdapter.Add(GetText(Resource.String.image));
                if (WoWonderTools.CheckAllowedFileSharingInServer("Video"))
                    arrayAdapter.Add(GetText(Resource.String.video));

                arrayAdapter.Add(GetText(Resource.String.text));
                items = arrayAdapter.ToArray();
                var ListAdapter = new ArrayAdapter<String>(GlobalContext, Android.Resource.Layout.SimpleListItem1, items);
                lv.Adapter= (ListAdapter);
               
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
                switch (addEvent)
                {
                    // true +=  // false -=
                    case true:
                        lv.ItemClick += Lv_ItemClick;
                        break;
                    default:
                        lv.ItemClick -= Lv_ItemClick;
                        break;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                var itemString = items[e.Position];
                if (itemString == GetText(Resource.String.image))
                {
                    GlobalContext.OpenDialogGallery("Image"); //requestCode >> 500 => Image Gallery
                }
                else if (itemString == GetText(Resource.String.video))
                {
                    GlobalContext.OpenDialogVideo();
                }
                else if (itemString == GetText(Resource.String.Lbl_VideoGallery))
                {
                    GlobalContext.ImageType = "Video";

                    switch ((int)Build.VERSION.SdkInt)
                    {
                        // Check if we're running on Android 5.0 or higher
                        case < 23:
                            //requestCode >> 501 => video Gallery
                            new IntentController(GlobalContext).OpenIntentVideoGallery();
                            break;
                        default:
                            {
                                if (GlobalContext.CheckSelfPermission(Manifest.Permission.Camera) == Permission.Granted && PermissionsController.CheckPermissionStorage())
                                {
                                    //requestCode >> 501 => video Gallery
                                    new IntentController(GlobalContext).OpenIntentVideoGallery();
                                }
                                else
                                {
                                    new PermissionsController(GlobalContext).RequestPermission(108);
                                }

                                break;
                            }
                    }
                }
                else if (itemString == GetText(Resource.String.Lbl_RecordVideoFromCamera))
                {
                    GlobalContext.ImageType = "VideoCamera";

                    switch ((int)Build.VERSION.SdkInt)
                    {
                        // Check if we're running on Android 5.0 or higher
                        case < 23:
                            //requestCode >> 513 => video Camera
                            new IntentController(GlobalContext).OpenIntentVideoCamera();
                            break;
                        default:
                            {
                                if (GlobalContext.CheckSelfPermission(Manifest.Permission.Camera) == Permission.Granted && PermissionsController.CheckPermissionStorage())
                                {
                                    //requestCode >> 513 => video Camera
                                    new IntentController(GlobalContext).OpenIntentVideoCamera();
                                }
                                else
                                {
                                    new PermissionsController(GlobalContext).RequestPermission(108);
                                }

                                break;
                            }
                    }
                }
               
                else if (itemString == GetText(Resource.String.text))
                {
                    Intent intent = new Intent(GlobalContext, typeof(EditColorActivity));
                    GlobalContext.StartActivityForResult(intent, 2200);
                }
            }
            catch (Exception ee)
            {
                Methods.DisplayReportResultTrack(ee);
            }
            Dismiss();
        }

        #endregion

        #region Events

        //ShareToPage
        
        
        #endregion
    }
}