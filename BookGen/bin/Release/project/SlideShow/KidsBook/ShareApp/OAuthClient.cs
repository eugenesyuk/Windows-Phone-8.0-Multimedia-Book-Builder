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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Phone.Tasks;
using KidsBook.Utility;
using GalaSoft.MvvmLight.Messaging;
using KidsBook.Messages;
using System.Runtime.Serialization.Json;

namespace KidsBook.ShareApp
{
    public class OAuthClient
    {
        public enum RequestType
        {
            InitialToken,
            AccessToken,
            StatusUpdate
        }

        public static void PerformRequest(Dictionary<string, string> parameters, string url, string consumerSecret, string token, RequestType type, string status = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            string OAuthHeader = OAuthClient.GetOAuthHeader(parameters, "POST", url, consumerSecret, token);
            request.Headers["Authorization"] = OAuthHeader;


            if (type == RequestType.StatusUpdate)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.BeginGetRequestStream(new AsyncCallback(SetStatusUpdate), new object[] { request, type, status });
            }
            else
            {
                request.BeginGetResponse(new AsyncCallback(GetResponse), new object[] { request, type });
            }
        }

        static void SetStatusUpdate(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)((object[])result.AsyncState)[0];
            string status = "status=" + StringHelper.SanitizeStatus(StringHelper.EncodeToUpper(((object[])result.AsyncState)[2].ToString()));
            byte[] data = Encoding.UTF8.GetBytes(status);

            using (Stream s = request.EndGetRequestStream(result))
            {
                s.Write(data, 0, data.Length);
            }

            request.BeginGetResponse(new AsyncCallback(GetResponse), new object[] { request, (RequestType)(((object[])result.AsyncState)[1]) });
        }

        static void GetResponse(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)(((object[])result.AsyncState)[0]);
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    RequestType currentType = (RequestType)(((object[])result.AsyncState)[1]);
                    string completeString = reader.ReadToEnd();

                    switch (currentType)
                    {
                        case RequestType.InitialToken:
                            {
                                string[] data = completeString.Split(new char[] { '&' });
                                int index = data[0].IndexOf("=");
                                StaticData.Token = data[0].Substring(index + 1, data[0].Length - index - 1);
                                index = data[1].IndexOf("=");
                                StaticData.TokenSecret = data[1].Substring(index + 1, data[1].Length - index - 1);

                                WebBrowserTask task = new WebBrowserTask();
                                task.Uri = new Uri("http://api.twitter.com/oauth/authorize?oauth_token=" + StaticData.Token);
                                task.Show();
                                break;
                            }
                        case RequestType.AccessToken:
                            {
                                string[] data = completeString.Split(new char[] { '&' });
                                int index = data[0].IndexOf("=");
                                StaticData.Token = data[0].Substring(index + 1, data[0].Length - index - 1);
                                index = data[1].IndexOf("=");
                                StaticData.TokenSecret = data[1].Substring(index + 1, data[1].Length - index - 1);
                                index = data[2].IndexOf("=");
                                StaticData.UserID = data[2].Substring(index + 1, data[2].Length - index - 1);
                                index = data[3].IndexOf("=");
                                StaticData.UserName = data[3].Substring(index + 1, data[3].Length - index - 1);

                                IsoStoreHelper.StoreNewCredentials(StaticData.Token, StaticData.TokenSecret, StaticData.UserID, StaticData.UserName);

                                // Send a message to notify to the MainPage that the authentication request has finished
                                var PageMsg = new PageActionMessage()
                                {
                                    action = PageAction.TwitterAuthFinished
                                };
                                Messenger.Default.Send<PageActionMessage>(PageMsg);

                                break;
                            }
                        default:
                            {
                                Debug.WriteLine(completeString);
                                break;
                            }
                    }
                }

            }
            catch (WebException)
            {
                // Delete the credentials to try again later
                IsoStoreHelper.DeleteCredentials();

                // Send a message to notify that there has been something wrong with the request
                var PageMsg = new PageActionMessage()
                {
                    action = PageAction.TwitterAuthFailed
                };
                Messenger.Default.Send<PageActionMessage>(PageMsg);
            }
        }

        public static string GetOAuthHeader(Dictionary<string, string> parameters, string httpMethod, string url, string consumerSecret, string tokenSecret)
        {
            parameters = parameters.OrderBy(x => x.Key).ToDictionary(v => v.Key, v => v.Value);

            string concat = string.Empty;

            string OAuthHeader = "OAuth ";

            foreach (string k in parameters.Keys)
            {
                if (k == "status")
                    concat += k + "=" + StringHelper.SanitizeStatus(StringHelper.EncodeToUpper(parameters[k])) + "&";
                else
                    concat += k + "=" + parameters[k] + "&";

                if (!BannedParameters.Parameters.Contains(k))
                    OAuthHeader += k + "=" + "\"" + parameters[k] + "\", ";
            }

            concat = concat.Remove(concat.Length - 1, 1);
            concat = StringHelper.EncodeToUpper(concat);

            concat = httpMethod + "&" + StringHelper.EncodeToUpper(url) + "&" + concat;

            byte[] content = Encoding.UTF8.GetBytes(concat);

            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(consumerSecret + "&" + tokenSecret));
            hmac.ComputeHash(content);

            string hash = Convert.ToBase64String(hmac.Hash);
            hash = hash.Replace("-", "");

            OAuthHeader += "oauth_signature=\"" + Uri.EscapeDataString(hash) + "\"";

            return OAuthHeader;
        }


        /// <summary>
        /// Gets the url the web browser will navigate to so the user can allow this app to access its facebook account
        /// </summary>
        /// <returns>A string with the url the user will be directed to</returns>
        public static string getFacebookLoginUrl()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("client_id", AuthConstants.FacebookAppId);
            parameters.Add("response_type", "token");
            parameters.Add("scope", "user_about_me, offline_access, publish_stream");
            parameters.Add("redirect_uri", UrlConstants.FacebookRedirectUri);
            parameters.Add("display", "touch");

            // Build the login page URL
            StringBuilder urlBuilder = new StringBuilder();
            foreach (var param in parameters)
            {
                if (urlBuilder.Length > 0)
                    urlBuilder.Append("&");
                var encoded = HttpUtility.UrlEncode(param.Value);
                urlBuilder.AppendFormat("{0}={1}", param.Key, encoded);
            }
            var loginUrl = UrlConstants.FacebookBaseUrl + "?" + urlBuilder.ToString();

            return loginUrl;
        }

        /// <summary>
        /// Posts a new status by calling the Facebook API
        /// </summary>
        /// <param name="status"> The status to be sent </param>
        /// <param name="callback"> A callback method to be called once the request has completed </param>
        public static void PostStatusUpdate(string status, Action<bool, Exception> callback)
        {
            var request = HttpWebRequest.Create(UrlConstants.FacebookSendStatusBaseUri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.BeginGetRequestStream((reqResult) =>
            {
                using (var strm = request.EndGetRequestStream(reqResult))
                using (var writer = new StreamWriter(strm))
                {
                    writer.Write("access_token=" + StaticData.AccessToken);
                    writer.Write("&message=" + HttpUtility.UrlEncode(status));
                }
                request.BeginGetResponse((result) =>
                {
                    try
                    {
                        var response = request.EndGetResponse(result);
                        using (var rstrm = response.GetResponseStream())
                        {
                            var serializer = new DataContractJsonSerializer(typeof(FacebookPostResponse));
                            var postResponse = serializer.ReadObject(rstrm) as FacebookPostResponse;
                            callback(true, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        callback(false, ex);
                    }
                }, null);
            }, null);
        }

    }
}
