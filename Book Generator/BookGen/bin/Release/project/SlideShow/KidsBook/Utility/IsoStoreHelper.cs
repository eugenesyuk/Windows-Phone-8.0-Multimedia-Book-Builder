using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace KidsBook.Utility
{
    public class IsoStoreHelper
    {
        public static void StoreNewCredentials(string oAuthToken, string oAuthTokenSecret, string userid, string screenName)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings["oauth_token"] = oAuthToken;
            settings["oauth_token_secret"] = oAuthTokenSecret;
            settings["userid"] = userid;
            settings["screen_name"] = screenName;
            settings.Save();
        }

        public static bool CheckIfAuthorized()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.TryGetValue("oauth_token", out StaticData.Token);
            settings.TryGetValue("oauth_token_secret", out StaticData.TokenSecret);
            settings.TryGetValue("userid", out StaticData.UserID);
            return settings.TryGetValue("screen_name", out StaticData.UserName);
        }

        public static void DeleteCredentials()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Clear();
        }
    }
}
