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
using WoWonder.Activities.MyProfile;
using WoWonder.Activities.SettingsPreferences.General;

namespace WoWonder.Activities.NativePost.Share
{
    public class MoreSettingsBottomFragment : BottomSheetDialogFragment
    {
        #region  Variables Basic


        MyProfile.MyProfileActivity GlobalContext;
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
                GlobalContext = MyProfile.MyProfileActivity.GetInstance();
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
                title.Text = GetText(Resource.String.Lbl_More);
                title.Visibility = ViewStates.Gone;
               lv = view.FindViewById<BottomSheetListView>(Resource.Id.SimplelistView);
                var arrayAdapter = new List<string>();
                arrayAdapter.Add(GetText(Resource.String.Lbl_EditAvatar));
                //  arrayAdapter.Add(GetText(Resource.String.Lbl_EditCover));

                arrayAdapter.Add(GetText(Resource.String.Lbl_Edit_Profile_and_avatar));
                arrayAdapter.Add(GetText(Resource.String.Lbl_personalinfosettings));
                arrayAdapter.Add(GetText(Resource.String.Lbl_bio));
                //arrayAdapter.Add(GetText(Resource.String.Lbl_ViewPrivacy));
                //arrayAdapter.Add(GetText(Resource.String.Lbl_SettingsAccount));

                switch (ListUtils.SettingsSiteList?.Pro)
                {
                    case "1" when AppSettings.ShowGoPro && GlobalContext.UserData.ProType != "4":
                        arrayAdapter.Add(GetText(Resource.String.Lbl_upgrade_now));
                        break;
                }
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
                string text = items[e.Position];
                if (text == GetText(Resource.String.Lbl_EditAvatar))
                {
                    GlobalContext.OpenDialogGallery("Avatar");
                }
                else if (text == GetText(Resource.String.Lbl_Edit_Profile_and_avatar))
                {
                    try
                    {
                        var intent = new Intent(GlobalContext, typeof(EditMyProfileActivity));
                        GlobalContext.StartActivity(intent);
                    }
                    catch (Exception ee)
                    {
                        Methods.DisplayReportResultTrack(ee);
                    }
                }
                else if (text == GetText(Resource.String.Lbl_personalinfosettings))
                {
                    try
                    {
                        var intent = new Intent(GlobalContext, typeof(MyAccountActivity));
                        GlobalContext.StartActivity(intent);
                    }
                    catch (Exception ee)
                    {
                        Methods.DisplayReportResultTrack(ee);
                    }
                }
                else if (text == GetText(Resource.String.Lbl_bio))
                {
                    var intent = new Intent(GlobalContext, typeof(UpdateAboutActivity));
                    //intent.PutExtra("about", preference.Summary);
                    GlobalContext.StartActivity(intent);
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