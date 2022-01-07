//##############################################
//Cᴏᴘʏʀɪɢʜᴛ 2020 DᴏᴜɢʜᴏᴜᴢLɪɢʜᴛ Codecanyon Item 19703216
//Elin Doughouz >> https://www.facebook.com/Elindoughouz
//====================================================

//For the accuracy of the icon and logo, please use this website " https://appicon.co " and add images according to size in folders " mipmap " 

using System.Collections.Generic;
using WoWonder.Activities.NativePost.Extra;
using WoWonder.Helpers.Model;
using WoWonderClient;

namespace WoWonder
{
    internal static class AppSettings
    {
        /// <summary>
        /// Deep Links To App Content
        /// you should add your website without http in the analytic.xml file >> ../values/analytic.xml .. line 5
        /// <string name="ApplicationUrlWeb">demo.wowonder.com</string>
        /// </summary>
        public static string TripleDesAppServiceProvider = "p3JnwHmSZlRi4TTTKUKLcPVs5fiWbjDgdYFsqafmyI3LxdtJsMFWgc9b0HlW4hVY1iQloAuBwwh2htbtK9W3zs4ldDEBt6CfGo5z/oMnL5p1+q2s2qwjy4VHd1IzisXg649Ql3AXHaj0QLnUw+itclmLtqZXAWZcUaXc5VAigcRcg391DKW+V9UeSSjg4hp1iOCtRC0juN4A1cN95PXOH2ZlZLko97SuJMTth0RG46dUtUgXosmh5yxFuHPaj1KO4RJPNj9OhwghSMkT7m/g4f/0oF7X4yad9QtOEqFsz31JQqIrTFKc6i/ZaVqjx6LIv/Ig+FZ5ufU9XKOB9NtO5hv7kS9YNDI4q87Qb17sd3WgYrBZqlQZlr5TDOlhl3Gw9pX6NDUZIonijSLaVOSgHRm4e+iDmACPbPiCZ9pZ/n5z89R1FM+FNui8ODkOdQgQdEiMwmPp9IBB2GZZ+iOxmFKq6W4NB7ILG0JQjdxREfQ8zDaYvY6tTCNr21+VWVB8ah/BvlabWHsGHXlYAI+n/G1+5uHiYrWGpeqpsoea6iDHMWm0sEbdtg0SmTpdswTqyukGB9H1Z/5JpyCK6Jzbr3Jr3XH52+009CZTsn4NmN8a9j1sPqGEA2ZqB4JrhScBBDcS7BqmD5Nf1sDn5ECJFRAfWR5sBHl3uA0JYP4jTBzZJywhkIOEEcbNNvP5QPzriFwf9bTYV/66zQRCykG1W+lsJI6zRUFSnoQFAMA47r5Y49rR9RQ8M50/CW7kquU2fFQuGK0l6tjSTFuGboM8X9nfBDatAZRTxMmHIW1Rq80aCNl0h/KuzXtzdhjwL4pL7sjZneqhDEvE588hfcGa9DI9fgEOqpL8knYfR6ntWoh4laYPNYOf9t3KobCAEWdnJ7Sg/DDavI3wi4xXMrrZk2BwHjLW0bxPdPFEmu2bjuCyoGzbH+c3M9tQPY09YqGew6wMejd0ciwSEUiV6oU3zfJ1lFnPNFd+0YQVGPwNXQnCRp0/OMG9Km5bTfGopBrxke4BmHy/dHbMr60EDzbfeYTwFCFgBFHSRJ1m3fjMkvD3clUUXldBbodhSr1AXJnO4izSUsbm6o3ItdDHqNiyzvePL3tplrpleclW+q2j/DziWjjIO8zxayOIASX5dXyYqanG2vM8K2q7DpkJTSXwd3ZUzKEKNYmaNY1FE1hkNIU7NENZN5ux+nxYyiAQccPavbesSTs1F41ZhgmUgghq2nOJWcEZbcBpiH6QnB92TqiGEkcAdUbcUKBHCjIUf2gw8asSW1JXssohofUCDfyHZY7SCSkq3s1CsmnjcKgFSh2e5I+u261BJKjgOPKtBauPDi7fMzeeYtOi2hK5Jj4ik7KrxzD4knEZKLNk7FfLMFmadajFqMZXZNMIm4weXn3Zlum5Sc6Fyp2CtBQpckLRzZ941x5NwMt7FRy6QSzQmHHELiWDHrc2gYIGzZPcHCvfCbL3luo2QwaGsHyicp+lA6/F0v4lAWXL/HUo9tpjLgIVRoA/JrPhhIbk2+wn8edzgxDTgDuD6ENJvMZXAX0tkqawysHSFzcp8I/l5yrmMsrmELeVaEXqDClUd+Lear7pmxeckDczYwAgwv75Jyt5h1LIc1dg2XRfk2VQTehb8eVkM7OmwbdiHUlbWGHapVjDWs0B5cBiwXBaCFJATULm9HA+p8LKLvZmMVlJFZRFqx+ycRJSqcBLCJ8Q9T99xh6f4aTg0kYfL+nq5JugCjmNKs1mk89BhImzCYFrO6m+AbzU4XYkN10y0OyTr1t6mc07boahEcxA6mzLQuns0Q5jsLgBUqQh1UXN+9BgbzWl4oT8uj4j3xNyaQmwRhz0tVrF3Ubn2pZNZ9RGKFCIv3oM2EdvNhRHSGjMsipexVqBx1oLtXf9ud/BAa/DyXRj0LXN/bqx3T4ysaJYz16mustdFkd7J0iSR+pugI0v470ofOSw2mti2IHbibr/B0TL8eYOAxrF7OSN6zHCq57tlAG4FhZVL5ue/YIMBPlU/+EeSsgVbupjslrKJncDhOenWlkpqiqrMs5+NPfy2H0c2EvzKISUs7WUJl5us6j1b3lms2IsYboZaK+3cbsomNRbF01/9pzlNXgDxs1+LNJy0vNe3aXLWz6Vrm3OvG9FlMgdysX3BLpY0FkMG5fAeu+23HJefVCv0CppKTQlTMdiMRUI0oB6Ze4VfQuS/RrAJVueuQIq55eimnr/ezwXBRHhHPvHHhNpJAg1RCQA/5PhXkE5ySQKt92eMnI0YytskhQoa8KYuO/N7mIDz0kDuhE426ErD1i87rvTdUBdLIbeZRhqW5cqGXboPED6X1jFVbk0q3uQk9A/EtDbHrKa3cfqXXK/B8ARU4emePgg9ShO6PxUogqJZScoZhgGrrZ76McW7afKe9/TDc1TR68rA7gXMJ3ITnDAlPF366/rysuBZUku0DXoPBsQViTfaJ0gTJPeO7/q6yysdXxEVg29ucL3iYPV6kN3/Z+VPQIYQ+90h54JJOS+CyElidgz0aUPcoXyDTL9CoG8H+B8mVIpUbvXCQdcTc4EpNH1PgR1zcmx/tWxz7iyWJeTIUg1460vvTe/Uq60GDCwR4zwfxXi+Ku2etk3";
    
        //Main Settings >>>>>
        //*********************************************************
        public static string Version = "3.4";
        public static string ApplicationName = "My Famous";
        public static string DatabaseName = "MyFamous"; 

        // Friend system = 0 , follow system = 1
        public static int ConnectivitySystem = 1;
         
        //Main Colors >>
        //*********************************************************
        public static string MainColor = "#000000";
         
        //Language Settings >> http://www.lingoes.net/en/translator/langcode.htm
        //*********************************************************
        public static bool FlowDirectionRightToLeft = false;
        public static string Lang = ""; //Default language ar

        //Set Language User on site from phone 
        public static bool SetLangUser = true; 

        //Notification Settings >>
        //*********************************************************
        public static bool ShowNotification = true;
        public static string OneSignalAppId = "64974c58-9993-40ed-b782-0814edc401ea";
        public static string MsgOneSignalAppId = "64974c58-9993-40ed-b782-0814edc401ea";

        // WalkThrough Settings >>
        //*********************************************************
        public static bool ShowWalkTroutPage = true;

        //Main Messenger settings
        //*********************************************************
        public static bool MessengerIntegration = true;
        public static bool ShowDialogAskOpenMessenger = false; 
        public static string MessengerPackageName = "com.wowondermessenger.app"; //APK name on Google Play

        //AdMob >> Please add the code ad in the Here and analytic.xml 
        //*********************************************************
        public static ShowAds ShowAds = ShowAds.AllUsers; 

        public static bool ShowAdMobBanner = false;
        public static bool ShowAdMobInterstitial = false;
        public static bool ShowAdMobRewardVideo = false;
        public static bool ShowAdMobNative = false;
        public static bool ShowAdMobNativePost = false;
        public static bool ShowAdMobAppOpen = false;  
        public static bool ShowAdMobRewardedInterstitial = false;  

        public static string AdInterstitialKey = "ca-app-pub-5135691635931982/3584502890";
        public static string AdRewardVideoKey = "ca-app-pub-5135691635931982/2518408206";
        public static string AdAdMobNativeKey = "ca-app-pub-5135691635931982/2280543246";
        public static string AdAdMobAppOpenKey = "ca-app-pub-5135691635931982/2813560515";  
        public static string AdRewardedInterstitialKey = "ca-app-pub-5135691635931982/7842669101";  

        //Three times after entering the ad is displayed
        public static int ShowAdMobInterstitialCount = 3;
        public static int ShowAdMobRewardedVideoCount = 3;
        public static int ShowAdMobNativeCount = 40;
        public static int ShowAdMobAppOpenCount = 2;  
        public static int ShowAdMobRewardedInterstitialCount = 3;  

        //FaceBook Ads >> Please add the code ad in the Here and analytic.xml 
        //*********************************************************
        public static bool ShowFbBannerAds = false; 
        public static bool ShowFbInterstitialAds = false;  
        public static bool ShowFbRewardVideoAds = false; 
        public static bool ShowFbNativeAds = false; 
         
        //YOUR_PLACEMENT_ID
        public static string AdsFbBannerKey = "250485588986218_554026418632132"; 
        public static string AdsFbInterstitialKey = "250485588986218_554026125298828";  
        public static string AdsFbRewardVideoKey = "250485588986218_554072818627492"; 
        public static string AdsFbNativeKey = "250485588986218_554706301897477"; 

        //Three times after entering the ad is displayed
        public static int ShowFbNativeAdsCount = 40;

        //Colony Ads >> Please add the code ad in the Here 
        //*********************************************************  
        public static bool ShowColonyBannerAds = false; 
        public static bool ShowColonyInterstitialAds = false;  
        public static bool ShowColonyRewardAds = false; 

        public static string AdsColonyAppId = "appff22269a7a0a4be8aa"; 
        public static string AdsColonyBannerId = "vz85ed7ae2d631414fbd";  
        public static string AdsColonyInterstitialId = "vz39712462b8634df4a8"; 
        public static string AdsColonyRewardedId = "vz32ceec7a84aa4d719a"; 
        //********************************************************* 

        public static bool EnableRegisterSystem = true;
         
        /// <summary>
        /// true => Only over 18 years old
        /// false => all 
        /// </summary>
        public static bool IsUserYearsOld = true;

        public static bool AddAllInfoPorfileAfterRegister = true; 
        public static bool ShowSuggestedUsersOnRegister = true;

        //Set Theme Full Screen App
        //*********************************************************
        public static bool EnableFullScreenApp = false;
            
        //Code Time Zone (true => Get from Internet , false => Get From #CodeTimeZone )
        //*********************************************************
        public static bool AutoCodeTimeZone = true;
        public static string CodeTimeZone = "UTC";

        //Error Report Mode
        //*********************************************************
        public static bool SetApisReportMode = false;

        //Social Logins >>
        //If you want login with facebook or google you should change id key in the analytic.xml file 
        //Facebook >> ../values/analytic.xml .. line 10-11 
        //Google >> ../values/analytic.xml .. line 15 
        //*********************************************************
        public static bool EnableSmartLockForPasswords = true; 

        public static bool ShowFacebookLogin = true;
        public static bool ShowGoogleLogin = true;

        public static readonly string ClientId = "212666549038-e6bfab882a8l6uqifht425op7u194rkb.apps.googleusercontent.com";

        //########################### 

        //Main Slider settings
        //*********************************************************
        public static PostButtonSystem PostButton = PostButtonSystem.ReactionDefault;
        public static ToastTheme ToastTheme = ToastTheme.Custom; 

        public static BottomNavigationSystem NavigationBottom = BottomNavigationSystem.Default; 

        public static bool ShowBottomAddOnTab = true; 
         
        public static bool ShowAlbum = true;
        public static bool ShowArticles = true;
        public static bool ShowPokes = true;
        public static bool ShowCommunitiesGroups = true;
        public static bool ShowCommunitiesPages = true;
        public static bool ShowMarket = true;
        public static bool ShowPopularPosts = true;
        public static bool ShowBoostedPosts = true; 
        public static bool ShowBoostedPages = true; 
        public static bool ShowMovies = true;
        public static bool ShowNearBy = true;
        public static bool ShowStory = true;
        public static bool ShowSavedPost = true;
        public static bool ShowUserContacts = true; 
        public static bool ShowJobs = true; 
        public static bool ShowCommonThings = true; 
        public static bool ShowFundings = true;
        public static bool ShowMyPhoto = true; 
        public static bool ShowMyVideo = true; 
        public static bool ShowGames = true;
        public static bool ShowMemories = true;  
        public static bool ShowOffers = true;  
        public static bool ShowNearbyShops = true;   

        public static bool ShowSuggestedPage = true; 
        public static bool ShowSuggestedGroup = true;
        public static bool ShowSuggestedUser = true;
         
        //count times after entering the Suggestion is displayed
        public static int ShowSuggestedPageCount = 90; 
        public static int ShowSuggestedGroupCount = 70; 
        public static int ShowSuggestedUserCount = 50;

        //allow download or not when share
        public static bool AllowDownloadMedia = true; 

        public static bool ShowAdvertise = true; 
         
        /// <summary>
        /// https://rapidapi.com/api-sports/api/covid-193
        /// you can get api key and host from here https://prnt.sc/wngxfc 
        /// </summary>
        public static bool ShowInfoCoronaVirus = true;  
        public static string KeyCoronaVirus = "164300ef98msh0911b69bed3814bp131c76jsneaca9364e61f"; 
        public static string HostCoronaVirus = "covid-193.p.rapidapi.com"; 
         
        public static bool ShowLive = true;  
        public static string AppIdAgoraLive = "619ee4ec26334d2dae20e52d1abbb32e"; 

        //Events settings
        //*********************************************************  
        public static bool ShowEvents = true; 
        public static bool ShowEventGoing = true; 
        public static bool ShowEventInvited = true;  
        public static bool ShowEventInterested = true;  
        public static bool ShowEventPast = true;

        // Story >>
        //*********************************************************
        //Set a story duration >> Sec
        public static long StoryImageDuration = 7;
        public static long StoryVideoDuration = 30; //#New

        /// <summary>
        /// If it is false, it will appear only for the specified time in the value of the StoryVideoDuration
        /// </summary>
        public static bool ShowFullVideo = false; //#New

        public static bool EnableStorySeenList = true;
        public static bool EnableReplyStory = true;

        //*********************************************************

        /// <summary>
        ///  Currency
        /// CurrencyStatic = true : get currency from app not api 
        /// CurrencyStatic = false : get currency from api (default)
        /// </summary>
        public static readonly bool CurrencyStatic = false;
        public static readonly string CurrencyIconStatic = "$";
        public static readonly string CurrencyCodeStatic = "USD";
        public static readonly string CurrencyFundingPriceStatic = "$";

        //Profile settings
        //*********************************************************
        public static bool ShowGift = true;
        public static bool ShowWallet = true; 
        public static bool ShowGoPro = true;  
        public static bool ShowAddToFamily = true;

        public static bool ShowUserGroup = false;  
        public static bool ShowUserPage = false; 
        public static bool ShowUserImage = true;  
        public static bool ShowUserSocialLinks = false;  

        public static CoverImageStyle CoverImageStyle = CoverImageStyle.CenterCrop;  

        /// <summary>
        /// The default value comes from the site .. in case it is not available, it is taken from these values
        /// </summary>
        public static string WeeklyPrice = "3"; 
        public static string MonthlyPrice = "8"; 
        public static string YearlyPrice = "89"; 
        public static string LifetimePrice = "259";

        //Native Post settings
        //********************************************************* 
        public static bool ShowTextWithSpace = true; 

        public static ImagePostStyle ImagePostStyle = ImagePostStyle.FullWidth; 

        public static bool ShowTextShareButton = false;
        public static bool ShowShareButton = true;
         
        public static int AvatarPostSize = 60;
        public static int ImagePostSize = 200;
        public static string PostApiLimitOnScroll = "22";

        //Get post in background >> 1 Min = 30 Sec
        public static long RefreshPostSeconds = 30000;  
        public static string PostApiLimitOnBackground = "12"; 

        public static bool AutoPlayVideo = false;
         
        public static bool EmbedPlayTubePostType = true;
        public static bool EmbedDeepSoundPostType = true;
        public static VideoPostTypeSystem EmbedFacebookVideoPostType = VideoPostTypeSystem.EmbedVideo; 
        public static VideoPostTypeSystem EmbedVimeoVideoPostType = VideoPostTypeSystem.EmbedVideo; 
        public static VideoPostTypeSystem EmbedPlayTubeVideoPostType = VideoPostTypeSystem.Link; 
        public static VideoPostTypeSystem EmbedTikTokVideoPostType = VideoPostTypeSystem.Link; 
        public static VideoPostTypeSystem EmbedTwitterPostType = VideoPostTypeSystem.Link; //New >> The next update it's will support EmbedVideo
        public static bool ShowSearchForPosts = true; 
        public static bool EmbedLivePostType = true;
         
        //new posts users have to scroll back to top
        public static bool ShowNewPostOnNewsFeed = true; 
        public static bool ShowAddPostOnNewsFeed = false; 
        public static bool ShowCountSharePost = true;

        public static WRecyclerView.VolumeState DefaultVolumeVideoPost = WRecyclerView.VolumeState.Off; 

        /// <summary>
        /// Post Privacy
        /// ShowPostPrivacyForAllUser = true : all posts user have icon Privacy 
        /// ShowPostPrivacyForAllUser = false : just my posts have icon Privacy (default)
        /// </summary>
        public static bool ShowPostPrivacyForAllUser = false; 

        public static bool ShowFullScreenVideoPost = true;

        public static bool EnableVideoCompress = false;


        /// <summary>
        /// show title, description and image 
        /// </summary>
        public static bool EnableFitchOgLink = true; //#New

        //Trending page
        //*********************************************************   
        public static bool ShowTrendingPage = true;
         
        public static bool ShowProUsersMembers = true;
        public static bool ShowPromotedPages = true;
        public static bool ShowTrendingHashTags = true;
        public static bool ShowLastActivities = true;
        public static bool ShowShortcuts = true; 
        public static bool ShowFriendsBirthday = true; 
        public static bool ShowAnnouncement = true; 

        /// <summary>
        /// https://www.weatherapi.com
        /// </summary>
        public static WeatherType WeatherType = WeatherType.Celsius; //#New
        public static bool ShowWeather = true;  
        public static string KeyWeatherApi = "e7cffc4d6a9a4a149a1113143201711";

        /// <summary>
        /// https://openexchangerates.org
        /// #Currency >> Your currency
        /// #Currencies >> you can use just 3 from those : USD,EUR,DKK,GBP,SEK,NOK,CAD,JPY,TRY,EGP,SAR,JOD,KWD,IQD,BHD,DZD,LYD,AED,QAR,LBP,OMR,AFN,ALL,ARS,AMD,AUD,BYN,BRL,BGN,CLP,CNY,MYR,MAD,ILS,TND,YER
        /// </summary>
        public static bool ShowExchangeCurrency = false; 
        public static string KeyCurrencyApi = "644761ef2ba94ea5aa84767109d6cf7b"; 
        public static string ExCurrency = "USD";  
        public static string ExCurrencies = "EUR,GBP,TRY"; 
        public static readonly List<string> ExCurrenciesIcons = new List<string> {"€", "£", "₺"}; 

        //********************************************************* 

        public static bool ShowUserPoint = true;

        //Add Post
        public static AddPostSystem AddPostSystem = AddPostSystem.AllUsers; 
        public static bool ShowFilterPost = false;  //#New

        public static bool ShowGalleryImage = true;
        public static bool ShowGalleryVideo = true;
        public static bool ShowMention = true;
        public static bool ShowLocation = true;
        public static bool ShowFeelingActivity = true;
        public static bool ShowFeeling = true;
        public static bool ShowListening = true;
        public static bool ShowPlaying = true;
        public static bool ShowWatching = true;
        public static bool ShowTraveling = true;
        public static bool ShowGif = true;
        public static bool ShowFile = true;
        public static bool ShowMusic = true;
        public static bool ShowPolls = true;
        public static bool ShowColor = true;
        public static bool ShowVoiceRecord = true; 

        public static bool ShowAnonymousPrivacyPost = true;

        //Advertising 
        public static bool ShowAdvertisingPost = true;  

        //Settings Page >> General Account
        public static bool ShowSettingsGeneralAccount = true;
        public static bool ShowSettingsAccount = true;
        public static bool ShowSettingsSocialLinks = true;
        public static bool ShowSettingsPassword = true;
        public static bool ShowSettingsBlockedUsers = true;
        public static bool ShowSettingsDeleteAccount = true;
        public static bool ShowSettingsTwoFactor = true; 
        public static bool ShowSettingsManageSessions = true;  
        public static bool ShowSettingsVerification = true;
         
        public static bool ShowSettingsSocialLinksFacebook = true; 
        public static bool ShowSettingsSocialLinksTwitter = true; 
        public static bool ShowSettingsSocialLinksGoogle = true; 
        public static bool ShowSettingsSocialLinksVkontakte = true;  
        public static bool ShowSettingsSocialLinksLinkedin = true;  
        public static bool ShowSettingsSocialLinksInstagram = true;  
        public static bool ShowSettingsSocialLinksYouTube = true;  

        //Settings Page >> Privacy
        public static bool ShowSettingsPrivacy = true;
        public static bool ShowSettingsNotification = true;

        //Settings Page >> Tell a Friends (Earnings)
        public static bool ShowSettingsInviteFriends = true;

        public static bool ShowSettingsShare = true;
        public static bool ShowSettingsMyAffiliates = true;
        public static bool ShowWithdrawals = true;

        /// <summary>
        /// if you want this feature enabled go to Properties -> AndroidManefist.xml and remove comments from below code
        /// Just replace it with this 5 lines of code
        /// <uses-permission android:name="android.permission.READ_CONTACTS" />
        /// <uses-permission android:name="android.permission.READ_PHONE_NUMBERS" />
        /// </summary>
        public static bool InvitationSystem = true; 

        //Settings Page >> Help && Support
        public static bool ShowSettingsHelpSupport = true;

        public static bool ShowSettingsHelp = true;
        public static bool ShowSettingsReportProblem = true;
        public static bool ShowSettingsAbout = true;
        public static bool ShowSettingsPrivacyPolicy = true;
        public static bool ShowSettingsTermsOfUse = true;

        public static bool ShowSettingsRateApp = true; 
        public static int ShowRateAppCount = 5; 
         
        public static bool ShowSettingsUpdateManagerApp = false; 

        public static bool ShowSettingsInvitationLinks = true; 
        public static bool ShowSettingsMyInformation = true; 


        //Set Theme Tab
        //*********************************************************
        public static bool SetTabDarkTheme = false;
        public static MoreTheme MoreTheme = MoreTheme.Card; 

        //Bypass Web Errors  
        //*********************************************************
        public static bool TurnTrustFailureOnWebException = true;
        public static bool TurnSecurityProtocolType3072On = true;

        //*********************************************************
        public static bool RenderPriorityFastPostLoad = false;

        /// <summary>
        /// if you want this feature enabled go to Properties -> AndroidManefist.xml and remove comments from below code
        /// <uses-permission android:name="com.android.vending.BILLING" />
        /// </summary>
        public static bool ShowInAppBilling = false; 

        public static bool ShowPaypal = true; 
        public static bool ShowBankTransfer = true; 
        public static bool ShowCreditCard = true;

        //********************************************************* 
        public static bool ShowCashFree = false;  

        /// <summary>
        /// Currencies : http://prntscr.com/u600ok
        /// </summary>
        public static string CashFreeCurrency = "INR";  

        //********************************************************* 

        /// <summary>
        /// If you want RazorPay you should change id key in the analytic.xml file
        /// razorpay_api_Key >> .. line 24 
        /// </summary>
        public static bool ShowRazorPay = false; 

        /// <summary>
        /// Currencies : https://razorpay.com/accept-international-payments
        /// </summary>
        public static string RazorPayCurrency = "USD";  
         
        public static bool ShowPayStack = false;  
        public static bool ShowPaySera = false;  //#Next Version   

        //********************************************************* 

        //Chat
        //*********************************************************  
        public static SystemGetLastChat LastChatSystem = SystemGetLastChat.Default;
        public static InitializeWoWonder.ConnectionType ConnectionTypeChat = InitializeWoWonder.ConnectionType.Socket; 
        public static string PortSocketServer = "449"; 
        public static ColorMessageTheme ColorMessageTheme = ColorMessageTheme.Gradient; 

        //Chat Window Activity >>
        //*********************************************************
        //if you want this feature enabled go to Properties -> AndroidManefist.xml and remove comments from below code
        //Just replace it with this 5 lines of code
        /*
         <uses-permission android:name="android.permission.READ_CONTACTS" />
         <uses-permission android:name="android.permission.READ_PHONE_NUMBERS" /> 
         */
        public static bool ShowButtonContact = true;
        /////////////////////////////////////

        public static bool ShowButtonCamera = true;
        public static bool ShowButtonImage = true;
        public static bool ShowButtonVideo = true;
        public static bool ShowButtonAttachFile = true;
        public static bool ShowButtonColor = true;
        public static bool ShowButtonStickers = true;
        public static bool ShowButtonMusic = true;
        public static bool ShowButtonGif = true;
        public static bool ShowButtonLocation = true;

        public static bool ShowMusicBar = false;

        public static bool OpenVideoFromApp = true;
        public static bool OpenImageFromApp = true;

        // Stickers Packs Settings >> 
        public static int StickersOnEachRow = 3;
        public static string StickersBarColor = "#efefef";
        public static string StickersBarColorDark = "#282828";

        public static bool ShowStickerStack0 = true;
        public static bool ShowStickerStack1 = true;
        public static bool ShowStickerStack2 = true;
        public static bool ShowStickerStack3 = true;
        public static bool ShowStickerStack4 = true;
        public static bool ShowStickerStack5 = true;
        public static bool ShowStickerStack6 = true;

        //Record Sound Style & Text 
        public static bool ShowButtonRecordSound = true;

        // Options List Message
        public static bool EnableReplyMessageSystem = true; 
        public static bool EnableForwardMessageSystem = true;  
        public static bool EnableFavoriteMessageSystem = true;  
        public static bool EnableReactionMessageSystem = true;  
        public static bool EnablePinMessageSystem = false; 

        //List Chat >>
        //*********************************************************
        public static bool EnableChatPage = true;
        public static bool EnableChatGroup = true;

        // Options List Chat
        public static bool EnableChatArchive = true; 
        public static bool EnableChatPin = true; 
        public static bool EnableChatMute = true;  
        public static bool EnableChatMakeAsRead = false;  

        public static bool ShowNotificationWithUpload = true;  

        // Video/Audio Call Settings >>
        //*********************************************************
        public static bool EnableAudioVideoCall = true;

        public static bool EnableAudioCall = true;
        public static bool EnableVideoCall = true;

        public static SystemCall UseLibrary = SystemCall.Twilio;

        //Last_Messages Page >>
        ///*********************************************************
        public static bool ShowOnlineOfflineMessage = false;

        public static int RefreshChatActivitiesSeconds = 6000; // 6 Seconds
        public static int MessageRequestSpeed = 3000; // 3 Seconds

        //Options chat heads (Bubbles) 
        //*********************************************************
        //Always , Hide , FullScreen
        public static string DisplayModeSettings = "Always";

        //Default , Left  , Right , Nearest , Fix , Thrown
        public static string MoveDirectionSettings = "Right";

        //Circle , Rectangle
        public static string ShapeSettings = "Circle";

        // Last position
        public static bool IsUseLastPosition = true;


        public static bool ShowSettingsWallpaper = true; 

    }
}