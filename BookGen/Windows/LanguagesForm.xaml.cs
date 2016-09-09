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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace BookGen.Windows
{
    /// <summary>
    /// Interaction logic for LanguagesForm.xaml
    /// </summary>
    public partial class LanguagesForm : Window
    {
        bool edit = false;

        public LanguagesForm(bool edit=false, int index=0)
        {
            InitializeComponent();

            if (edit)
            {

                int i = 0;

                foreach (KeyValuePair<string, string> pair in BookManager.getSingleton().ActivePage.Images)
                {
                    if (i == index)
                    {
                        this.txtImage.Text = pair.Value;
                        this.txtLanguage.Text = pair.Key;
                    }

                    i++;
                }

            }

            this.edit = edit;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            string imageText = txtImage.Text.Trim();
            string languageText = txtLanguage.Text.Trim();

            if (String.IsNullOrEmpty(imageText) || String.IsNullOrEmpty(languageText)) 
            {
                MessageBox.Show("You must fill both fields in to continue", "Empty field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (!isValidBackgroundImage(imageText))
                return;


            if (!edit)
            {
                BookManager.getSingleton().ActivePage.AddImage(languageText, imageText);
            }
            else
            {
                if (BookManager.getSingleton().ActivePage.Images.ContainsKey(txtLanguage.Text))
                    BookManager.getSingleton().ActivePage.Images[languageText] = imageText;
                else if (BookManager.getSingleton().ActivePage.Images.ContainsValue(imageText))
                {
                    foreach (var pair in BookManager.getSingleton().ActivePage.Images)
                    {
                        if (pair.Value.Equals(imageText))
                        {
                            BookManager.getSingleton().ActivePage.Images.Remove(pair.Key);
                            BookManager.getSingleton().ActivePage.AddImage(languageText, imageText);
                            break;
                        }
                    }
                }
                else
                {
                    BookManager.getSingleton().ActivePage.AddImage(languageText, imageText);
                }
            }

            DialogResult = true;

            Close();
        }

        private void btnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            browseForImage();
        }

        private void browseForImage()
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string itemName = open.FileName;
                if (isValidBackgroundImage(itemName))
                {
                    this.txtImage.Text = itemName;
                }
            }
        }

        /// <summary>
        /// Tells if a file meets all the requirements for being a background image, and shows error messages otherwise
        /// </summary>
        /// <param name="fileName">The file's full path</param>
        /// <returns>True if the file is a .png or .jpg image with a resolution of 480x800 pixels</returns>
        private bool isValidBackgroundImage(string fileName)
        {
            bool isValid = true;

            if (System.IO.File.Exists(fileName))
            {
                if (fileName.EndsWith(".png", true, null) || fileName.EndsWith(".jpg", true, null))
                {
                    var selectedImg = new BitmapImage(new Uri(fileName));
                    if (selectedImg.PixelHeight != 480 || selectedImg.PixelWidth != 800)
                    {
                        MessageBox.Show("The image must have a size of 480x800 pixels", "Bad size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        isValid = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please choose a .png or .jpg image file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    isValid = false;
                }
            }
            else
            {
                MessageBox.Show("The image you chose does not exist. Try again.", "The image does not exist", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }

            return isValid;
        }

        private void txtImage_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            browseForImage();
        }
    }
}
