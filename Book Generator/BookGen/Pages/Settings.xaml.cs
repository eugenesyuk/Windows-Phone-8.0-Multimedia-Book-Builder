using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace BookGen.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        private static bool gotFocus = new bool();

        public static bool GetFocus
        {
            get { return gotFocus; }
        }

        public Settings()
        {
            InitializeComponent();
        }

        private void lblLargeImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (checkImageSize(open.FileName, 173))
                {
                    BookManager.getSingleton().LargeIcon = open.FileName;
                    imgLargeIcon.Source = new BitmapImage(new Uri(open.FileName));
                    lblLargeImage.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void lblSmallImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (checkImageSize(open.FileName, 99))
                {
                    BookManager.getSingleton().SmallIcon = open.FileName;
                    imgSmallIcon.Source = new BitmapImage(new Uri(open.FileName));
                    lblSmallImage.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void txtXapName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtXapName.Text))
                MessageBox.Show("Please add a name for the XAP file", "Empty field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                BookManager.getSingleton().XapFileName = this.txtXapName.Text;
        }

        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtTitle.Text))
                MessageBox.Show("Please add a title for the book", "Empty field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                BookManager.getSingleton().Title = this.txtTitle.Text;
        }

        private void lblPlayIcon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (isImageCorrect(open.FileName))
                {
                    BookManager.getSingleton().PlayIcon = open.FileName;
                    playIcon.Source = new BitmapImage(new Uri(open.FileName));
                    lblPlayIcon.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void lblPauseIcon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (isImageCorrect(open.FileName))
                {
                    BookManager.getSingleton().PauseIcon = open.FileName;
                    pauseIcon.Source = new BitmapImage(new Uri(open.FileName));
                    lblPauseIcon.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private bool isImageCorrect(string path)
        {
            bool isValid = true;
            if (path.EndsWith(".png", true, null) || path.EndsWith(".jpg", true, null))
            {
                var selectedImg = new BitmapImage(new Uri(path));
            }
            else
            {
                MessageBox.Show("Please choose a .png or .jpg image file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }
            return isValid;
        }

        private bool checkImageSize(string path, int sidelength)
        {
            bool isValid = true;
            if (path.EndsWith(".png", true, null) || path.EndsWith(".jpg", true, null))
            {
                var selectedImg = new BitmapImage(new Uri(path));
                if (selectedImg.PixelHeight != sidelength || selectedImg.PixelWidth != sidelength)
                {
                    MessageBox.Show(String.Format("The image must have a size of {0}x{0} pixels. Please choose a different one", sidelength), "Incorrect size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    isValid = false;
                }
            }
            else
            {
                MessageBox.Show("Please choose a .png or .jpg image file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }
            return isValid;
        }

        private bool checkLoadImageSize(string path, int sidelength , int sideheight)
        {
            bool isValid = true;
            if (path.EndsWith(".png", true, null) || path.EndsWith(".jpg", true, null))
            {
                var selectedImg = new BitmapImage(new Uri(path));
                if (selectedImg.PixelHeight != sideheight || selectedImg.PixelWidth != sidelength)
                {
                    MessageBox.Show(String.Format("The image must have a size of {0}x{1} pixels. Please choose a different one", sidelength, sideheight), "Incorrect size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    isValid = false;
                }
            }
            else
            {
                MessageBox.Show("Please choose a .png or .jpg image file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }
            return isValid;
        }

        private void txtXapName_GotFocus(object sender, RoutedEventArgs e)
        {
            gotFocus = true;
        }

        private void txtXapName_LostFocus(object sender, RoutedEventArgs e)
        {
            gotFocus = false;
        }

        private void txtTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            gotFocus = true;
        }

        private void txtTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            gotFocus = false;
        }

        private void lblLoadIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (checkLoadImageSize(open.FileName, 480, 800))
                {
                    BookManager.getSingleton().LoadIcon = open.FileName;
                    loadicon.Source = new BitmapImage(new Uri(open.FileName));
                    lblLoadIcon.Visibility = System.Windows.Visibility.Hidden;
                   
                }
            }
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(BookManager.getSingleton().XapFileName.ToString()))
            {
                txtXapName.Text = BookManager.getSingleton().XapFileName.ToString();
                
            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().Title.ToString()))
            {
                txtTitle.Text = BookManager.getSingleton().Title.ToString();

            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().LargeIcon.ToString()))
            {
                imgLargeIcon.Source = new BitmapImage(new Uri(BookManager.getSingleton().LargeIcon.ToString()));
                lblLargeImage.Visibility = Visibility.Hidden;

            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().SmallIcon.ToString()))
            {
                imgSmallIcon.Source = new BitmapImage(new Uri(BookManager.getSingleton().SmallIcon.ToString()));
                lblSmallImage.Visibility = Visibility.Hidden;

            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().PlayIcon.ToString()))
            {
                playIcon.Source = new BitmapImage(new Uri(BookManager.getSingleton().PlayIcon.ToString())); 
                lblPlayIcon.Visibility = Visibility.Hidden;

            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().PauseIcon.ToString()))
            {
                pauseIcon.Source = new BitmapImage(new Uri(BookManager.getSingleton().PauseIcon.ToString()));
                lblPauseIcon.Visibility = Visibility.Hidden;

            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().LoadIcon.ToString()))
            {
                loadicon.Source = new BitmapImage(new Uri(BookManager.getSingleton().LoadIcon.ToString())); 
                lblLoadIcon.Visibility = Visibility.Hidden; 

            }
            //if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ListenImage.ToString()))
            //{
            //    imgSoundImage.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ListenImage.ToString()));
            //    lblSoundImage.Visibility = Visibility.Hidden;
            //}
            //if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ListenImageClicked.ToString()))
            //{
            //    imgSoundImage_Clicked.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ListenImageClicked.ToString()));
            //    lblSoundImage_CLicked.Visibility = Visibility.Hidden;
            //}
            //if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ReadImage.ToString()))
            //{
            //    imgReadImage.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ReadImage.ToString()));
            //    lblReadImage.Visibility = Visibility.Hidden;
            //}
            //if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ReadImageClicked.ToString()))
            //{
            //    imgReadImag_Clicked.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ReadImageClicked.ToString()));
            //    lblReadImage_Clicked.Visibility = Visibility.Hidden;
            //}
        }

        private void playIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (isImageCorrect(open.FileName))
                {
                    BookManager.getSingleton().PlayIcon = open.FileName;
                    playIcon.Source = new BitmapImage(new Uri(open.FileName));
                    lblPlayIcon.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void pauseIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (isImageCorrect(open.FileName))
                {
                    BookManager.getSingleton().PauseIcon = open.FileName;
                    pauseIcon.Source = new BitmapImage(new Uri(open.FileName));
                    lblPauseIcon.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void loadicon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                if (checkLoadImageSize(open.FileName, 480, 800))
                {
                    BookManager.getSingleton().LoadIcon = open.FileName;
                    loadicon.Source = new BitmapImage(new Uri(open.FileName));
                    lblLoadIcon.Visibility = System.Windows.Visibility.Hidden;

                }
            }
        }

    }
}
