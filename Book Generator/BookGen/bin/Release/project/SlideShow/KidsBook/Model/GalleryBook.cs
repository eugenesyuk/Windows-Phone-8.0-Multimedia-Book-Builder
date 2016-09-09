using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using System;

namespace KidsBook.Model
{
    /// <summary>
    /// Represents a book from the last page's gallery
    /// </summary>
    [DataContract]
    public class GalleryBook
    {

        public GalleryBook()
        {
            Id = 0;
            Title = string.Empty;
            Author = string.Empty;
            Url = string.Empty;
            Package = string.Empty;
            Image = new BitmapImage();
            Reflection = null;
            MarketPlace = false;
            AndroidMarket = false;
            iTunes = false;
            DateAdded = DateTime.Now;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Package { get; set; }

        [DataMember]
        public bool MarketPlace { get; set; }

        [DataMember]
        public bool AndroidMarket { get; set; }

        [DataMember]
        public bool iTunes { get; set; }

        [DataMember]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Cover's image
        /// </summary>
        public BitmapImage Image { get; set; }

        /// <summary>
        /// Cover's image reflection (shadow)
        /// </summary>
        public WriteableBitmap Reflection { get; set; }
    }
}

