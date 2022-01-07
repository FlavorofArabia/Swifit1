using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using MaterialDialogsCore;
using WoWonder.Activities.Contacts;
using WoWonder.Activities.NativePost.Share;
using WoWonder.Helpers.CacheLoaders;
using WoWonder.Helpers.Utils;
using WoWonderClient.Classes.Global;
using WoWonderClient.Requests;
using Exception = System.Exception;

namespace WoWonder.Activities.Wallet.Fragment
{
    public class WithdrawalsFragment : AndroidX.Fragment.App.Fragment, MaterialDialog.IListCallback, MaterialDialog.ISingleButtonCallback
    {
        #region  Variables Basic
        private ImageView Avatar;
        private TextView TxtProfileName, TxtUsername;
        private TextView TextMiniRequest;

        private TextView TxtMyBalance;
        public EditText TxtWithdrawMethod, TxtAmount, TxtPayPalEmail, TxtAccountNumber, TxtCountry, TxtAccountName, TxtSwiftCode, TxtAddress;
        public LinearLayout LayoutPayPalEmail, LayoutBank;
        private Button BtnRequestWithdrawal;
        private double CountBalance;
        public string TypeDialog, TypeWithdrawMethod = "paypal";
        private TabbedWalletActivity GlobalContext;

        #endregion

        #region General

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
            GlobalContext = (TabbedWalletActivity)Activity;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            try
            {
                View view = inflater.Inflate(Resource.Layout.WithdrawalsFragment, container, false);
                
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
                
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }
        public override void OnPause()
        {
            AddOrRemoveEvent(false) ;
            base.OnPause();
        }
        public override void OnResume()
        {
            AddOrRemoveEvent(true);
            base.OnResume();
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
                Avatar = view.FindViewById<ImageView>(Resource.Id.avatar);
                TxtProfileName = view.FindViewById<TextView>(Resource.Id.name);
                TxtUsername = view.FindViewById<TextView>(Resource.Id.tv_subname);

                TextMiniRequest = view.FindViewById<TextView>(Resource.Id.description);

                TxtMyBalance = view.FindViewById<TextView>(Resource.Id.myBalance);

                TxtWithdrawMethod = view.FindViewById<EditText>(Resource.Id.WithdrawMethodEditText);

                TxtAmount = view.FindViewById<EditText>(Resource.Id.AmountEditText);

                LayoutPayPalEmail = view.FindViewById<LinearLayout>(Resource.Id.LayoutPayPalEmail);
                TxtPayPalEmail = view.FindViewById<EditText>(Resource.Id.PayPalEmailEditText);

                LayoutBank = view.FindViewById<LinearLayout>(Resource.Id.LayoutBank);
                TxtAccountNumber = view.FindViewById<EditText>(Resource.Id.AccountNumberEditText);

                TxtCountry = view.FindViewById<EditText>(Resource.Id.CountryEditText);

                TxtAccountName = view.FindViewById<EditText>(Resource.Id.AccountNameEditText);

                TxtSwiftCode = view.FindViewById<EditText>(Resource.Id.SwiftCodeEditText);

                TxtAddress = view.FindViewById<EditText>(Resource.Id.AddressEditText);

                BtnRequestWithdrawal = view.FindViewById<Button>(Resource.Id.RequestWithdrawalButton);

                var userData = ListUtils.MyProfileList?.FirstOrDefault();
                if (userData != null)
                {
                    GlideImageLoader.LoadImage(GlobalContext, userData.Avatar, Avatar, ImageStyle.CircleCrop, ImagePlaceholders.Drawable);
                    TxtProfileName.Text = WoWonderTools.GetNameFinal(userData);
                    TxtUsername.Text = "@" + userData.Username;

                    TxtMyBalance.Text = userData.Wallet;
                }

                Methods.SetColorEditText(TxtWithdrawMethod, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtAmount, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtPayPalEmail, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtAccountNumber, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtCountry, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtAccountName, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtSwiftCode, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);
                Methods.SetColorEditText(TxtAddress, AppSettings.SetTabDarkTheme ? Color.White : Color.Black);

                Methods.SetFocusable(TxtWithdrawMethod);
                Methods.SetFocusable(TxtCountry);

              //  AdsGoogle.Ad_AdMobNative(this);

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
                        TxtWithdrawMethod.Click += TxtWithdrawMethod_Click; ;

                        TxtCountry.Click += TxtCountry_Click; ;
                        BtnRequestWithdrawal.Click += BtnRequestWithdrawalOnClick;
                        break;
                    default:
                        TxtWithdrawMethod.Click -= TxtWithdrawMethod_Click;
                        TxtCountry.Click -= TxtCountry_Click;
                        BtnRequestWithdrawal.Click -= BtnRequestWithdrawalOnClick;
                        break;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private void TxtWithdrawMethod_Click(object sender, EventArgs e)
        {
            try
            {
                //if (e?.Event?.Action != MotionEventActions.Down) return;

                //TypeDialog = "WithdrawMethod";

                //var dialogList = new MaterialDialog.Builder(GlobalContext).Theme(AppSettings.SetTabDarkTheme ? MaterialDialogsCore.Theme.Dark : MaterialDialogsCore.Theme.Light);

                //var arrayAdapter = new List<string>
                //{
                //    GetText(Resource.String.Btn_Paypal), GetText(Resource.String.Lbl_Bank)
                //};

                //dialogList.Title(GetText(Resource.String.Lbl_WithdrawMethod)).TitleColorRes(Resource.Color.primary);
                //dialogList.Items(arrayAdapter);
                //dialogList.NegativeText(GetText(Resource.String.Lbl_Close)).OnNegative(this);
                //dialogList.AlwaysCallSingleChoiceCallback();
                //dialogList.ItemsCallback(this).Build().Show();
                var searchFilter = new DrawalMethodBottomFragment();
                searchFilter.Show(GlobalContext.SupportFragmentManager, "CountryFilter");
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        private void TxtCountry_Click(object sender, EventArgs e)
        {
            try
            {
                //TypeDialog = "Country";

                //var countriesArray = WoWonderTools.GetCountryList(GlobalContext);

                //var dialogList = new MaterialDialog.Builder(GlobalContext).Theme(AppSettings.SetTabDarkTheme ? MaterialDialogsCore.Theme.Dark : MaterialDialogsCore.Theme.Light);

                //var arrayAdapter = countriesArray.Select(item => item.Value).ToList();

                //dialogList.Title(GetText(Resource.String.Lbl_Country)).TitleColorRes(Resource.Color.primary);
                //dialogList.Items(arrayAdapter);
                //dialogList.NegativeText(GetText(Resource.String.Lbl_Close)).OnNegative(this);
                //dialogList.AlwaysCallSingleChoiceCallback();
                //dialogList.ItemsCallback(this);
                //var md = dialogList.Build();
                //var windowatr = md.Window.Attributes;
                //windowatr.Gravity = GravityFlags.Bottom;
                //md.Window.Attributes = windowatr;
                //md.Show();

                var searchFilter = new CountriesBottomFragment();
                searchFilter.Show(GlobalContext.SupportFragmentManager, "CountryFilter");


            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion

        #region Events

        
        public void OnSelection(MaterialDialog dialog, View itemView, int position, string itemString)
        {
            try
            {
                switch (TypeDialog)
                {
                    case "WithdrawMethod":
                        {
                            if (itemString == GetText(Resource.String.Btn_Paypal))
                            {
                                TypeWithdrawMethod = "paypal";
                                LayoutPayPalEmail.Visibility = ViewStates.Visible;
                                LayoutBank.Visibility = ViewStates.Gone;
                            }
                            else
                            {
                                TypeWithdrawMethod = "bank";

                                LayoutPayPalEmail.Visibility = ViewStates.Gone;
                                LayoutBank.Visibility = ViewStates.Visible;
                            }

                            TxtWithdrawMethod.Text = itemString;
                            break;
                        }
                    case "Country":
                        TxtCountry.Text = itemString;
                        break;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void OnClick(MaterialDialog p0, DialogAction p1)
        {
            try
            {
                if (p1 == DialogAction.Positive)
                {

                }
                else if (p1 == DialogAction.Negative)
                {
                    p0.Dismiss();
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }
  

        private async void BtnRequestWithdrawalOnClick(object sender, EventArgs e)
        {
            try
            {
                if (CountBalance < Convert.ToDouble(TxtAmount.Text))
                {
                    ToastUtils.ShowToast(GlobalContext, GetText(Resource.String.Lbl_ThereIsNoBalance), ToastLength.Long);
                    return;
                }

                if (Convert.ToDouble(TxtAmount.Text) < Convert.ToDouble(ListUtils.SettingsSiteList?.MWithdrawal))
                {
                    ToastUtils.ShowToast(GlobalContext, GetText(Resource.String.Lbl_CantRequestWithdrawals), ToastLength.Long);
                    return;
                }

                switch (TypeWithdrawMethod)
                {
                    case "paypal" when string.IsNullOrEmpty(TxtPayPalEmail.Text.Replace(" ", "")) || string.IsNullOrEmpty(TxtAmount.Text.Replace(" ", "")):
                        ToastUtils.ShowToast(GlobalContext, GetText(Resource.String.Lbl_Please_check_your_details), ToastLength.Long);
                        return;
                    case "bank" when string.IsNullOrEmpty(TxtAmount.Text.Replace(" ", "")) || string.IsNullOrEmpty(TxtAccountNumber.Text.Replace(" ", "")) || string.IsNullOrEmpty(TxtCountry.Text.Replace(" ", ""))
                                     || string.IsNullOrEmpty(TxtAccountName.Text.Replace(" ", "")) || string.IsNullOrEmpty(TxtSwiftCode.Text.Replace(" ", "")) || string.IsNullOrEmpty(TxtAddress.Text.Replace(" ", "")):
                        ToastUtils.ShowToast(GlobalContext, GetText(Resource.String.Lbl_Please_check_your_details), ToastLength.Long);
                        return;
                }

                if (Methods.CheckConnectivity())
                {
                    //Show a progress
                    AndHUD.Shared.Show(GlobalContext, GetText(Resource.String.Lbl_Loading));

                    var dictionary = new Dictionary<string, string>
                    {
                        {"type", TypeWithdrawMethod},
                        {"amount", TxtAmount.Text},
                    };

                    switch (TypeWithdrawMethod)
                    {
                        case "paypal":
                            dictionary.Add("paypal_email", TxtPayPalEmail.Text);
                            break;
                        case "bank":
                            dictionary.Add("iban", TxtAccountNumber.Text);
                            dictionary.Add("country", TxtCountry.Text);
                            dictionary.Add("full_name", TxtAccountName.Text);
                            dictionary.Add("swift_code", TxtSwiftCode.Text);
                            dictionary.Add("address", TxtAddress.Text);
                            break;
                    }

                    var (apiStatus, respond) = await RequestsAsync.Global.WithdrawAsync(dictionary);
                    switch (apiStatus)
                    {
                        case 200:
                            {
                                switch (respond)
                                {
                                    case MessageObject result:
                                        Console.WriteLine(result.Message);
                                        AndHUD.Shared.Dismiss(GlobalContext);
                                        ToastUtils.ShowToast(GlobalContext, GetText(Resource.String.Lbl_RequestSentWithdrawals), ToastLength.Long);

                                        GlobalContext.Finish();
                                        break;
                                }

                                break;
                            }
                        default:
                            Methods.DisplayAndHudErrorResult(GlobalContext, respond);
                            break;
                    }
                }
                else
                {
                    ToastUtils.ShowToast(GlobalContext, GetText(Resource.String.Lbl_CheckYourInternetConnection), ToastLength.Long);
                }
            }
            catch (Exception exception)
            {
                AndHUD.Shared.Dismiss(GlobalContext);
                Methods.DisplayReportResultTrack(exception);
            }
        }

        #endregion
    }
}