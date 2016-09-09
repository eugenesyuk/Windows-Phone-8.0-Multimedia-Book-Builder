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

namespace KidsBook.ShareApp
{
    public class UrlConstants
    {
        public const string TwitterRequestToken = @"https://api.twitter.com/oauth/request_token";
        public const string TwitterAccessToken = @"https://api.twitter.com/oauth/authorize";
        public const string FacebookBaseUrl = @"http://www.facebook.com/dialog/oauth";
        public const string FacebookRedirectUri = @"http://www.facebook.com/connect/login_success.html";
        public const string FacebookSendStatusBaseUri = @"https://graph.facebook.com/me/feed";
    }
}
