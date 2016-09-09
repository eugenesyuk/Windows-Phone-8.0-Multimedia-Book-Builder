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
        /// The images that will make the book (odd pages)
        /// </summary>
        private BitmapImage[] _images;
        /// <summary>
        /// The images that will make the back of the pages (even pages)
        /// </summary>
        private BitmapImage[] _pageImages;

        private BitmapImage _currImage;
        private BitmapImage _prevImage;
        private BitmapImage _nextImage;
        private BitmapImage _currPageImage;
        private BitmapImage _prevPageImage;
        private BitmapImage _nextPageImage;

        /// <summary>
        /// Z-index for each page: [prev, curr, next, prev back, curr back, next back]
        /// </summary>
        private int[] _zIndexes = new int[6];

        /// <summary>
        /// Index of current page
        /// </summary>
        private int _currIndex = 1;
        /// <summary>
        /// Total number of spread pages
        /// </summary>
        private int _totalPages;

        #endregion

        #region Properties

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
        /// Total number of spread pages
        /// </summary>
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

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
                InitializeZOrder(0);
            }
            else
            {
                loadImages();
                PrevImage = Images[0];
                CurrImage = Images[1];
                NextImage = Images[2];
                PrevPageImage = PageImages[0];
                CurrPageImage = PageImages[1];
                NextPageImage = PageImages[2];
                InitializeZOrder(0);
            }
        }

        #region Messaging methods

        private void registerToMessages()
        {
            // Changes the z-index of the given canvas when changing page in the view
            Messenger.Default.Register<PageActionMessage>
            (
                this,
                (action) => ChangeZIndexes(action)
            );
        }

        private object ChangeZIndexes(PageActionMessage action)
        {
            switch (action.action)
            {
                case PageAction.TurningLeft:
                    ZIndexes[4] = 3;
                    ZIndexes[2] = 1;
                    ZIndexes[1] = 2;
                    RaisePropertyChanged("ZIndexes");
                    break;
                case PageAction.TurningRight:
                    ZIndexes[3] = 3;
                    ZIndexes[1] = 1;
                    ZIndexes[0] = 2;
                    RaisePropertyChanged("ZIndexes");
                    break;
                case PageAction.TurningLeftFinished:
                    InitializeZOrder(-1);
                    break;
                case PageAction.TurningRightFinished:
                    InitializeZOrder(1);
                    break;
                case PageAction.TurningCanceled:
                    InitializeZOrder(0);
                    break;
                default:
                    return null;
            }
            return null;
        }

        #endregion

        #region Helper methods

        private void loadImages()
        {
            _totalPages = 8;
            Images = new BitmapImage[_totalPages + 2];
            PageImages = new BitmapImage[_totalPages + 2];

            // Add copy of first image, that will never be shown
            Images[0] = new BitmapImage(new Uri("img/pic1.png", UriKind.Relative));
            PageImages[0] = new BitmapImage(new Uri("img/pic1_white.png", UriKind.Relative));

            // Load all images
            for (int i = 1; i <= _totalPages; i++)
            {
                Images[i] = new BitmapImage(new Uri(String.Format("img/pic{0}.png", i), UriKind.Relative));
                PageImages[i] = new BitmapImage(new Uri(String.Format("img/pic{0}_white.png", i), UriKind.Relative));
            }

            // Add copy of last image, that will never be shown
            Images[_totalPages + 1] = new BitmapImage(new Uri(String.Format("img/pic{0}.png", _totalPages), UriKind.Relative));
            PageImages[_totalPages + 1] = new BitmapImage(new Uri(String.Format("img/pic{0}.png", _totalPages), UriKind.Relative));

            _totalPages += 2;
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
                }
                else return;
            }
            else if (direction > 0)
            {
                if (_currIndex > 1)
                {
                    PrevImage = Images[_currIndex - 2];
                    CurrImage = Images[_currIndex - 1];
                    NextImage = Images[_currIndex];
                    PrevPageImage = PageImages[_currIndex - 2];
                    CurrPageImage = PageImages[_currIndex - 1];
                    NextPageImage = PageImages[_currIndex];
                    _currIndex--;
                }
                else return;
            }

            ZIndexes[0] = 0;
            ZIndexes[1] = 2;
            ZIndexes[2] = 0;
            ZIndexes[3] = 1;
            ZIndexes[4] = 0;
            ZIndexes[5] = 0;
            RaisePropertyChanged("ZIndexes");
        }

        #endregion

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}