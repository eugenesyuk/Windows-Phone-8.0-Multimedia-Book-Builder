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
using System.IO;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using KidsBook.Model;

namespace KidsBook
{
    public class StaticData
    {
        /// <summary>
        /// Url of the service for the gallery's books
        /// </summary>
        public static Uri galleryServiceUrl = new Uri("http://dev225-bajki.pgs-soft.com/BooksService.svc/BooksFor/1");
        //public static Uri galleryServiceUrl = new Uri("http://localhost:51083/BooksService.svc/BooksFor/1");

        /// <summary>
        /// Total number of spread pages, without counting the first one (cover) and last (gallery)
        /// </summary>
        public static int totalPages = 0;

        /// <summary>
        /// Sensitivity of page turning
        /// </summary>
        public static double sensitivity = 1;

        /// <summary>
        /// Language of the book's text and sounds. Used in the files' (pics, sounds) names.
        /// </summary>
        public static string language = String.Empty;

        /// <summary>
        /// Delays, in seconds, after which each page will turn on its own after its sound has finished playing. If zero, the page will never turn on its own
        /// </summary>
        public static IList<double> delays = null;

        /// <summary>
        /// Number of books of the last page's gallery
        /// </summary>
        public static int galleryTotalBooks = 0;

        /// <summary>
        /// Last page's gallery's books
        /// </summary>
        public static List<GalleryBook> Gallery = new List<GalleryBook>();

        /// <summary>
        /// Number of days that a new book added to the MarketPlace is considered to be "new", so the star on the first page will be updated accordingly
        /// </summary>
        public static int daysForNewBook = 7;

        /// <summary>
        /// Tells if the app can play sounds or there are other apps on the phone playing sounds currently
        /// </summary>
        public static bool canPlaySounds = true;

        // Twitter fields
        public static string Token;
        public static string TokenSecret;
        public static string UserName;
        public static string UserID;

        // Facebook fields
        public static string AccessToken;

        /// <summary>
        /// Reads the config.txt configuration file and loads the required fields. It does not check for errors
        /// </summary>
        public static void loadConfig()
        {
            var resource = System.Windows.Application.GetResourceStream(new Uri("config.txt", UriKind.Relative));
            using (StreamReader stream = new StreamReader(resource.Stream))
            {
                string line = null;
                string[] elements = null;
                while ((line = stream.ReadLine()) != null)
                {
                    if(!(line = line.Trim()).StartsWith("//"))
                    {
                        elements = line.Split('=');
                        string field = elements[0].ToLower().Trim();
                        if (field.Equals("totalpages"))
                        {
                            int value;
                            if (Int32.TryParse(elements[1], out value))
                                totalPages = value;
                        }
                        else if (field.Equals("sensitivity"))
                        {
                            double value;
                            if (Double.TryParse(elements[1], out value))
                                sensitivity = value;
                        }
                        else if (field.Equals("language"))
                        {
                            language = elements[1].Trim();
                        }
                        else if (field.Equals("delays"))
                        {
                            delays = new List<double>();
                            double value;
                            string[] values = elements[1].Split(',');
                            foreach (string dly in values)
                            {
                                if (Double.TryParse(dly.Trim(), out value))
                                    delays.Add(value);
                            }
                        }
                    }
                }
            }
        }
    }
}
