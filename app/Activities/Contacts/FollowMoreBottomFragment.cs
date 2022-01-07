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
using WoWonder.Activities.Base;
using WoWonder.Activities.Contacts;
using WoWonderClient.Requests;
using WoWonder.SQLite;

namespace WoWonder.Activities.NativePost.Share
{
    public class FollowMoreBottomFragment : BottomSheetDialogFragment
    {
        #region  Variables Basic


        MyContactsActivity GlobalContext;
        BottomSheetListView lv;
        string[] items;
        #endregion
        UserDataObject userdata;
        #region General
        public FollowMoreBottomFragment( MyContactsActivity GlobalContext,UserDataObject userdata)
        {
            this.GlobalContext = GlobalContext;
            this.userdata = userdata;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            try
            {
                Context contextThemeWrapper = AppSettings.SetTabDarkTheme ? new ContextThemeWrapper(Activity, Resource.Style.MyTheme_Dark_Base) : new ContextThemeWrapper(Activity, Resource.Style.MyTheme_Base);
                LayoutInflater localInflater = inflater.CloneInContext(contextThemeWrapper);
                View view = localInflater?.Inflate(Resource.Layout.SimpleRecyclerView, container, false);
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
                title.Text = GetText(Resource.String.Lbl_MoreOptions);
                lv = view.FindViewById<BottomSheetListView>(Resource.Id.SimplelistView);
                string []   items = new string[7];
                items[0] = GetText(Resource.String.Lbl_addcheckin);
                items[1] = GetText(Resource.String.Lbl_addfindmefriends);
                items[2] = GetText(Resource.String.Lbl_addfindmefamily);
                items[3] = GetText(Resource.String.Lbl_addblackbox);
                items[4] = GetText(Resource.String.Lbl_mute);
                items[5] = GetText(Resource.String.Lbl_Remove);
                items[6] = GetText(Resource.String.Lbl_Block);
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
            //GlobalContext.WithDrawalsFragment.TxtCountry.Text = items[e.Position];
            if (e.Position == 6)
                BlockUserButtonClick();
            Dismiss();
        }
        public async void BlockUserButtonClick()
        {
            try
            {
                if (!Methods.CheckConnectivity())
                {
                    ToastUtils.ShowToast(GlobalContext, GetString(Resource.String.Lbl_CheckYourInternetConnection), ToastLength.Short);
                }
                else
                {
                    var (apiStatus, respond) = await RequestsAsync.Global.BlockUserAsync(userdata.UserId, true); //true >> "block"
                    switch (apiStatus)
                    {
                        case 200:
                            {
                                var dbDatabase = new SqLiteDatabase();
                                dbDatabase.Insert_Or_Replace_OR_Delete_UsersContact(userdata, "Delete");


                                ToastUtils.ShowToast(GlobalContext, GetString(Resource.String.Lbl_Blocked_successfully), ToastLength.Short);

                                //OverridePendingTransition(Resource.Animation.slide_out_left, Resource.Animation.slide_out_left);
                                //Finish();
                                break;
                            }
                        default:
                            Methods.DisplayReportResult(GlobalContext, respond);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        #endregion

        #region Events

        //ShareToPage


        #endregion



    }
}