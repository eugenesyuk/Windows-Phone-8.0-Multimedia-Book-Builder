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
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();

            
             
        }

        private void imgBackground_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string itemName = open.FileName;
                if (itemName.EndsWith(".png", true, null) || itemName.EndsWith(".jpg", true, null))
                {
                    var selectedImg = new BitmapImage(new Uri(itemName));
                    if (selectedImg.PixelHeight == 480 && selectedImg.PixelWidth == 800)
                    {
                        BookManager.getSingleton().StartPage.Background = itemName;
                        imgBackground.Source = selectedImg;
                        lblBackground.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        incorrectSize(480, 800);
                    }
                }
                else
                {
                    incorrectImageFormat();
                }
            }
        }

        private void imgSoundImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string itemName = open.FileName;
                if (itemName.EndsWith(".png", true, null) || itemName.EndsWith(".jpg", true, null))
                {
                    var selectedImg = new BitmapImage(new Uri(itemName));
                    if (selectedImg.PixelHeight <= 150 && selectedImg.PixelWidth <= 150)
                    {
                        BookManager.getSingleton().StartPage.ListenImage = itemName;
                        imgSoundImage.Source = selectedImg;
                        lblSoundImage.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        incorrectButtonSize(150, 150);
                    }              
                }
                else
                {
                    incorrectImageFormat();
                }
            }
        }


        private void imgReadImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string itemName = open.FileName;
                if (itemName.EndsWith(".png", true, null) || itemName.EndsWith(".jpg", true, null))
                {

                    var selectedImg = new BitmapImage(new Uri(itemName));
                    if (selectedImg.PixelHeight <=150 && selectedImg.PixelWidth <= 150)
                    {
                        BookManager.getSingleton().StartPage.ReadImage = itemName;
                        imgReadImage.Source = selectedImg;
                        lblReadImage.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        incorrectButtonSize(150, 150);
                    }
                }
                else
                {
                    incorrectImageFormat();
                }
            }
        }

        private void incorrectButtonSize(int p1, int p2)
        {
            MessageBox.Show(String.Format("The image must have a width and height not larger than of {0} pixels", p1), "Bad size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void incorrectImageFormat()
        {
            MessageBox.Show("Please choose a .png or .jpg image file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void incorrectSize(int height, int width)
        {
            MessageBox.Show(String.Format("The image must have a size of {0}x{1} pixels", height, width), "Bad size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void lblReadImage_Clicked_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgReadImag_Clicked_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string itemName = open.FileName;
                if (itemName.EndsWith(".png", true, null) || itemName.EndsWith(".jpg", true, null))
                {

                    var selectedImg = new BitmapImage(new Uri(itemName));
                    if (selectedImg.PixelHeight <= 150 && selectedImg.PixelWidth <= 150)
                    {
                        BookManager.getSingleton().StartPage.ReadImageClicked = itemName;
                        imgReadImag_Clicked.Source = selectedImg;
                        lblReadImage_Clicked.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        incorrectButtonSize(150, 150);
                    }
                }
                else
                {
                    incorrectImageFormat();
                }
            }
        }

        private void imgSoundImage_Clicked_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string itemName = open.FileName;
                if (itemName.EndsWith(".png", true, null) || itemName.EndsWith(".jpg", true, null))
                {

                    var selectedImg = new BitmapImage(new Uri(itemName));
                    if (selectedImg.PixelHeight <= 150 && selectedImg.PixelWidth <= 150)
                    {
                        BookManager.getSingleton().StartPage.ListenImageClicked = itemName;
                        imgSoundImage_Clicked.Source = selectedImg;
                        lblSoundImage_CLicked.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        incorrectButtonSize(150, 150);
                    }
                }
                else
                {
                    incorrectImageFormat();
                }
            }
        }

        private void lblSoundImage_CLicked_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.Background.ToString()))
            {
                imgBackground.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.Background.ToString()));
            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ListenImage.ToString()))
            {
                imgSoundImage.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ListenImage.ToString()));
                lblSoundImage.Visibility = Visibility.Hidden;
            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ListenImageClicked.ToString()))
            {
                imgSoundImage_Clicked.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ListenImageClicked.ToString()));
                lblSoundImage_CLicked.Visibility = Visibility.Hidden;
            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ReadImage.ToString()))
            {
                imgReadImage.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ReadImage.ToString()));
                lblReadImage.Visibility = Visibility.Hidden;
            }
            if (!String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ReadImageClicked.ToString()))
            {
                imgReadImag_Clicked.Source = new BitmapImage(new Uri(BookManager.getSingleton().StartPage.ReadImageClicked.ToString()));
                lblReadImage_Clicked.Visibility = Visibility.Hidden;
            }


        }

       
    }
}
