using GalaSoft.MvvmLight;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Markup;
using KidsBook.Messages;
using GalaSoft.MvvmLight.Messaging;


using System.Text;
using System.Net;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Controls;
using System.IO;
using System.Runtime.Serialization.Json;
using KidsBook.Model;
using System.Collections.ObjectModel;

namespace KidsBook.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region Private fields

        /// <summary>
        /// The images that make up the book's pages (i.e. odd pages)
        /// </summary>
        private BitmapImage[] _images;
        /// <summary>
        /// The images that make up the back of the pages (i.e. even pages)
        /// </summary>
        private BitmapImage[] _pageImages;

        // Book's current loaded images
        private BitmapImage _currImage;
        private BitmapImage _prevImage;
        private BitmapImage _nextImage;
        private BitmapImage _currPageImage;
        private BitmapImage _prevPageImage;
        private BitmapImage _nextPageImage;

        /// <summary>
        /// Number of book covers on the last page's gallery before current cover
        /// </summary>
        private const int _booksBeforeCurrent = 2;
        /// <summary>
        /// Number of book covers on the last page's gallery after current cover
        /// </summary>
        private const int _booksAfterCurrent = 3;

        // Gallery's current books
        private GalleryBook _leftLoadBook;
        private GalleryBook _prevBook;
        private GalleryBook _currBook;
        private GalleryBook _nextBook;
        private GalleryBook _rightLoadBook;
        private GalleryBook _nextBookBuffer;

        // Current page's sounds fields
        private Uri _soundPath = null;
        private bool _soundsAutoPlay = false;

        /// <summary>
        /// Book's text and sounds language
        /// </summary>
        private string _language = "";

        /// <summary>
        /// Z-index for each page: [prev, curr, next, prev back, curr back, next back]
        /// </summary>
        private int[] _zIndexes = new int[6];
        /// <summary>
        /// Z index of the menus that should overlap the rest of the book's layout, such as Twitter or Facebook menus
        /// </summary>
        private int _menusZIndex = 0;
        /// <summary>
        /// Index of current page
        /// </summary>
        private int _currIndex = 1;
        /// <summary>
        /// Total number of spread pages
        /// </summary>
        private int _totalPages;
        /// <summary>
        /// Index of current book if we are in the gallery
        /// </summary>
        private int _galleryCurrBook = 0;
        /// <summary>
        /// Total number of books in the marketplace, to show their covers in the gallery
        /// </summary>
        private int _galleryTotalBooks;
        /// <summary>
        /// Number of times the gallery has been loaded.
        /// </summary>
        private int _galleryLoads = 0;
        /// <summary>
        /// Number of times the galley needs to be loaded for the reflections to render properly
        /// </summary>
        private const int _galleryTotalLoads = 2;
        /// <summary>
        /// Number of new books that have been added to the gallery
        /// </summary>
        private int _newBooks = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Array containing all the pages' main images (i.e. odd pages)
        /// </summary>
        public BitmapImage[] Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                RaisePropertyChanged("Images");
            }
        }

        /// <summary>
        /// Array containing all the pages' back images (i.e. even pages)
        /// </summary>
        public BitmapImage[] PageImages
        {
            get
            {
                return _pageImages;
            }
            set
            {
                _pageImages = value;
                RaisePropertyChanged("PageImages");
            }
        }

        /// <summary>
        /// Current page's image (current odd page's image)
        /// </summary>
        public BitmapImage CurrImage
        {
            get
            {
                return _currImage;
            }
            set
            {
                _currImage = value;
                RaisePropertyChanged("CurrImage");
            }
        }

        /// <summary>
        /// Previous page's image (previous odd page's image)
        /// </summary>
        public BitmapImage PrevImage
        {
            get
            {
                return _prevImage;
            }
            set
            {
                _prevImage = value;
                RaisePropertyChanged("PrevImage");
            }
        }

        /// <summary>
        /// Next page's image (next odd page's image)
        /// </summary>
        public BitmapImage NextImage
        {
            get
            {
                return _nextImage;
            }
            set
            {
                _nextImage = value;
                RaisePropertyChanged("NextImage");
            }
        }

        /// <summary>
        /// Image of the current page's back (current even page's image)
        /// </summary>
        public BitmapImage CurrPageImage
        {
            get
            {
                return _currPageImage;
            }
            set
            {
                _currPageImage = value;
                RaisePropertyChanged("CurrPageImage");
            }
        }

        /// <summary>
        /// Image of the previous page's back (previous even page's image)
        /// </summary>
        public BitmapImage PrevPageImage
        {
            get
            {
                return _prevPageImage;
            }
            set
            {
                _prevPageImage = value;
                RaisePropertyChanged("PrevPageImage");
            }
        }

        /// <summary>
        /// Image of the following page's back (next even page's image)
        /// </summary>
        public BitmapImage NextPageImage
        {
            get
            {
                return _nextPageImage;
            }
            set
            {
                _nextPageImage = value;
                RaisePropertyChanged("NextPageImage");
            }
        }

        /// <summary>
        /// First gallery's book from the left, two places before the current one. Needed for transition effects.
        /// </summary>
        public GalleryBook LeftLoadBook
        {
            get { return _leftLoadBook; }
            set
            {
                RaisePropertyChanged("LeftLoadBook");
                _leftLoadBook = value;
            }
        }

        /// <summary>
        /// The gallery's book right before the current one, placed on its left.
        /// </summary>
        public GalleryBook PrevBook
        {
            get { return _prevBook; }
            set
            {
                RaisePropertyChanged("PrevBook");
                _prevBook = value;
            }
        }

        /// <summary>
        /// The current book the user has navigated to, placed on the middle of the screen.
        /// </summary>
        public GalleryBook CurrBook
        {
            get { return _currBook; }
            set
            {
                RaisePropertyChanged("CurrBook");
                _currBook = value;
            }
        }

        /// <summary>
        /// The gallery's book right after the current one, placed on its right.
        /// </summary>
        public GalleryBook NextBook
        {
            get { return _nextBook; }
            set
            {
                RaisePropertyChanged("NextBook");
                _nextBook = value;
            }
        }

        /// <summary>
        /// Fourth gallery's book from the left, two places after the current one. Needed for transition effects.
        /// </summary>
        public GalleryBook RightLoadBook
        {
            get { return _rightLoadBook; }
            set
            {
                RaisePropertyChanged("RightLoadBook");
                _rightLoadBook = value;
            }
        }

        /// <summary>
        /// Fifth gallery's book from the left, three places after the current one. Never seen by the user, it is used to pre-load the upcoming covers.
        /// </summary>
        public GalleryBook NextBookBuffer
        {
            get { return _nextBookBuffer; }
            set
            {
                RaisePropertyChanged("NextBookBuffer");
                _nextBookBuffer = value;
            }
        }

        /// <summary>
        /// Number of new books added to the last page's gallery. A book is considered to be new if its creation time was after the number of days from today indicated by the StaticData.daysForNewBook parameter
        /// </summary>
        public int NewBooks
        {
            get
            {
                return _newBooks;
            }
            set
            {
                _newBooks = value;
                RaisePropertyChanged("NewBooks");
            }
        }

        /// <summary>
        /// Path to the current page's sound
        /// </summary>
        public Uri SoundPath
        {
            get
            {
                return _soundPath;
            }
            set
            {
                _soundPath = value;
                RaisePropertyChanged("SoundPath");
            }
        }

        /// <summary>
        /// Whether each page's sound should start automatically playing after turning the previous one
        /// </summary>
        public bool SoundsAutoPlay
        {
            get
            {
                return _soundsAutoPlay;
            }
            set
            {
                _soundsAutoPlay = value;
                RaisePropertyChanged("SoundsAutoPlay");
            }
        }

        /// <summary>
        /// Z-Index for each page: [prev, curr, next, prev back, curr back, next back]
        /// </summary>
        public int[] ZIndexes
        {
            get
            {
                return _zIndexes;
            }
            set
            {
                _zIndexes = value;
                RaisePropertyChanged("ZIndexes");
            }
        }

        /// <summary>
        /// Z index of the menus that should overlap the rest of the book's layout, such as Twitter or Facebook menus
        /// </summary>
        public int MenusZIndex
        {
            get
            {
                return _menusZIndex;
            }
            set
            {
                _menusZIndex = value;
                RaisePropertyChanged("MenusZIndex");
            }
        }

        /// <summary>
        /// Total number of spread pages
        /// </summary>
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
        }

        /// <summary>
        /// Book's text and sounds language
        /// </summary>
        public string Language
        {
            get
            {
                return _language;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            StaticData.loadConfig();
            registerToMessages();

            if (IsInDesignMode)
            {
                loadImages();
       
                PrevImage = Images[0];
                CurrImage = Images[1];
                NextImage = Images[2];
                PrevPageImage = PageImages[0];
                CurrPageImage = PageImages[1];
                NextPageImage = PageImages[2];
                LeftLoadBook = StaticData.Gallery[0];
                PrevBook = StaticData.Gallery[1];
                CurrBook = StaticData.Gallery[2];
                NextBook = StaticData.Gallery[3];
                RightLoadBook = StaticData.Gallery[4];
                InitializeZOrder(0);
            }
            else
            {
                loadImages();
            
                loadCover();
            }
        }

        /// <summary>
        /// Loads the images and updates z-indexes for first page (cover)
        /// </summary>
        private void loadCover()
        {
            PrevImage = Images[0];
            CurrImage = Images[1];
            NextImage = Images[2];
            PrevPageImage = PageImages[0];
            CurrPageImage = PageImages[1];
            NextPageImage = PageImages[2];
            InitializeZOrder(0);
            loadNextSound();
        }

        #region Messaging methods

        /// <summary>
        /// Registers the Main ViewModel on creation to all the messages that it may recieve from its View
        /// </summary>
        private void registerToMessages()
        {
            // Changes the z-index of the given canvas when changing page in the view
            Messenger.Default.Register<PageActionMessage>
            (
                this,
                (action) => HandlePageAction(action)
            );

        

            // Sets the AutoPlay attribute of the audio to be played
            Messenger.Default.Register<ChangeAutoPlayMessage>
                (
                    this,
                    (action) => UpdateAutoPlay(action)
                );
        }

        /// <summary>
        /// Handles an upcoming message from the View.
        /// </summary>
        /// <param name="action">The PageAction message sent by the View</param>
        private object HandlePageAction(PageActionMessage action)
        {
            switch (action.action)
            {
                case PageAction.TurningLeft: // Turning page left (i.e. to the next page) animation going on
                    ZIndexes[4] = 4;
                    ZIndexes[2] = 1;
                    ZIndexes[1] = 2;
                    MenusZIndex = 3;
                    RaisePropertyChanged("ZIndexes");
                    break;
                case PageAction.TurningRight: // Turning page right (i.e. to the previous page) animation going on
                    ZIndexes[3] = 4;
                    ZIndexes[1] = 1;
                    ZIndexes[0] = 3;
                    MenusZIndex = 2;
                    RaisePropertyChanged("ZIndexes");
                    break;
                case PageAction.TurningToCover: // Same as turning right, but the previous page must be the first one (cover)
                    PrevImage = Images[1];
                    PrevPageImage = PageImages[1];
                    ZIndexes[3] = 4;
                    ZIndexes[1] = 1;
                    ZIndexes[0] = 3;
                    MenusZIndex = 2;
                    RaisePropertyChanged("ZIndexes");
                    break;
                case PageAction.TurningToGallery: // Same as turning left, but the next page must be the last one (gallery)
                    NextImage = Images[TotalPages - 1]; 
                    NextPageImage = PageImages[TotalPages - 1];
                    ZIndexes[4] = 4;
                    ZIndexes[2] = 1;
                    ZIndexes[1] = 2;
                    MenusZIndex = 3;
                    RaisePropertyChanged("ZIndexes");
                    break;
                case PageAction.TurningLeftFinished: // A turn left has finished, expose the next page
                    InitializeZOrder(-1);
                    break;
                case PageAction.TurningRightFinished: // A turn right has finished, expose the previous page
                    InitializeZOrder(1);
                    break;
                case PageAction.TurningToCoverFinished: // Turn to cover finished, set index accordingly and expose cover
                    _currIndex = 2;
                    InitializeZOrder(1);
                    break;
                case PageAction.TurningToGalleryFinished: // Turn to gallery finished, set index accordingly and expose gallery
                    _currIndex = TotalPages - 1;
                    InitializeZOrder(1);
                    break;
                case PageAction.TurningCanceled: // A turning animation has retracted, set z-indexes as they were previously
                    InitializeZOrder(0);
                    break;
    
               
               
                default:
                    return null;
            }
            return null;
        }


        /// <summary>
        /// Handles an upcoming autoplay message from the View
        /// </summary>
        /// <param name="action">The ChangeAutoPlay message sent by the View</param>
        /// <returns></returns>
        private object UpdateAutoPlay(ChangeAutoPlayMessage action)
        {
            SoundsAutoPlay = action.autoPlay;
            return null;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Loads the images that make the book's pages (front and back sides), including the first and last ones
        /// </summary>
        private void loadImages()
        {
            // Number of pages that make up the book, without counting the first one (cover) and last one (share app page)
            _totalPages = StaticData.totalPages;
            Images = new BitmapImage[_totalPages + 4];
            PageImages = new BitmapImage[_totalPages + 4];

            _language = StaticData.language;

            // Add copy of first image, that will never be shown (used to simplify indexing)
            Images[0] = new BitmapImage(new Uri("img/first_page.png", UriKind.Relative));
            PageImages[0] = new BitmapImage(new Uri("img/first_page.png", UriKind.Relative));

            // Add book cover
            Images[1] = new BitmapImage(new Uri("img/first_page.png", UriKind.Relative));
            PageImages[1] = new BitmapImage(new Uri("img/first_page_white.png", UriKind.Relative));

            // Load all images (book's pages)
            for (int i = 2; i <= _totalPages + 1; i++)
            {
                Images[i] = new BitmapImage(new Uri(String.Format("img/pic{0}_{1}.png", i - 1, Language), UriKind.Relative));
                PageImages[i] = new BitmapImage(new Uri(String.Format("img/pic{0}_{1}_white.png", i - 1, Language), UriKind.Relative));
            }

            // Add last page
            Images[_totalPages + 2] = new BitmapImage(new Uri("img/gallery_background.png", UriKind.Relative));
            PageImages[_totalPages + 2] = new BitmapImage(new Uri("img/gallery_background.png", UriKind.Relative));

            // Add copy of last image, that will never be shown (required since there is always one hidden page)
            Images[_totalPages + 3] = new BitmapImage(new Uri("img/gallery_background.png", UriKind.Relative));
            PageImages[_totalPages + 3] = new BitmapImage(new Uri("img/gallery_background.png", UriKind.Relative));

            // Add the first page, last page and their duplicates to the total page count
            _totalPages += 4;
            StaticData.totalPages = _totalPages;
        }

        /// <summary>
        /// Modifies the z-orders of the pages after some effect has taken place. If a page has been turned, it updates the images references
        /// </summary>
        /// <param name="direction">The direction of the page turn. If 0, it means that no page has been turned</param>
        private void InitializeZOrder(int direction)
        {
            if (direction < 0)
            {
                if (_currIndex < _totalPages - 2)
                {
                    PrevImage = Images[_currIndex];
                    CurrImage = Images[_currIndex + 1];
                    NextImage = Images[_currIndex + 2];
                    PrevPageImage = PageImages[_currIndex];
                    CurrPageImage = PageImages[_currIndex + 1];
                    NextPageImage = PageImages[_currIndex + 2];
                    _currIndex++;
                    loadNextSound();
                }
                else return;
            }
            else if (direction > 0)
            {
                if (_currIndex > 1)
                {
                    PrevImage =  Images[_currIndex - 2];
                    CurrImage = Images[_currIndex - 1];
                    NextImage = Images[_currIndex];
                    PrevPageImage = PageImages[_currIndex - 2];
                    CurrPageImage = PageImages[_currIndex - 1];
                    NextPageImage = PageImages[_currIndex];
                    _currIndex--;
                    loadNextSound();
                }
                else return;
            }

            ZIndexes[0] = 0;
            ZIndexes[1] = 2;
            ZIndexes[2] = 0;
            ZIndexes[3] = 1;
            ZIndexes[4] = 0;
            ZIndexes[5] = 0;
            MenusZIndex = 3;
            RaisePropertyChanged("ZIndexes");
        }

     

       
      

        /// <summary>
        /// Creates the image's reflections after they have been fully loaded
        /// </summary>
        private void MainViewModel_ImageOpened(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (var book in StaticData.Gallery)
            {
                BitmapImage cover = book.Image;
                if (book.Reflection == null)
                    book.Reflection = createReflection(cover);
                i++;
            }
        }

        private void MainViewModel_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //throw e.ErrorException;
            return;
        }

        /// <summary>
        /// Creates the reflection image for a given BitmapImage of the book's gallery.
        /// </summary>
        /// <param name="sourceImage">The image whose reflection will be created</param>
        /// <returns>The reflection image</returns>
        private WriteableBitmap createReflection(BitmapImage sourceImage)
        {
            try
            {
                WriteableBitmap wbDest = null;
                if (sourceImage != null && !String.IsNullOrEmpty(sourceImage.UriSource.ToString()))
                {
                    WriteableBitmap wbSource = null;
                    wbSource = new WriteableBitmap(sourceImage);
                    int height = wbSource.PixelHeight;
                    int width = wbSource.PixelWidth;
                    int shadowHeight = 50;
                    wbDest = new WriteableBitmap(width, shadowHeight);
                    int j = 0;
                    for (int i = height - 1; i >= height - shadowHeight; i--)
                    {
                        Array.Copy(wbSource.Pixels, i * width, wbDest.Pixels, j * width, width);
                        j++;
                    }
                    wbDest.Invalidate();
                }
                return wbDest;
            }
            catch(NullReferenceException)
            {
                // Reflection cannot be created since its image has not been fully loaded yet
                return null;
            }
        }

     

        /// <summary>
        /// Loads the sound's source for a new loaded page
        /// </summary>
        private void loadNextSound()
        {
            if (_currIndex > 1 && _currIndex < _totalPages - 2)
            {
                SoundPath = new Uri(String.Format("/sound/track{0}_{1}.mp3", _currIndex - 1, Language), UriKind.Relative);
            }
            else
            {
                SoundPath = new Uri("", UriKind.RelativeOrAbsolute);
            }
        }

        #endregion

      
    }
}