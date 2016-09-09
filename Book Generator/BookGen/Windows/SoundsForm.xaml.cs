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
using System.IO;

namespace BookGen.Windows
{
    /// <summary>
    /// Interaction logic for SoundsForm.xaml
    /// </summary>
    public partial class SoundsForm : Window
    {
        bool edit = false;

        public SoundsForm(bool edit=false, int index=0)
        {
            InitializeComponent();

            if (edit)
            {
                int i = 0;

                foreach (KeyValuePair<string, string> pair in BookManager.getSingleton().ActivePage.Sounds)
                {
                    if (i == index)
                    {
                        this.txtSound.Text = pair.Value;
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
            if (this.txtSound.Text.Trim() == "" || this.txtLanguage.Text.Trim() == "")
            {
                MessageBox.Show("You must fill both fields in to continue", "Empty field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (isValidSound(txtSound.Text))
            {
                if (!edit)
                {
                    BookManager.getSingleton().ActivePage.AddSound(this.txtLanguage.Text, txtSound.Text);
                }
                else
                {
                    if (BookManager.getSingleton().ActivePage.Sounds.ContainsKey(txtLanguage.Text))
                        BookManager.getSingleton().ActivePage.Sounds[this.txtLanguage.Text] = this.txtSound.Text;
                    else if (BookManager.getSingleton().ActivePage.Sounds.ContainsValue(txtSound.Text))
                    {
                        foreach (var pair in BookManager.getSingleton().ActivePage.Sounds)
                        {
                            if (pair.Value.Equals(txtSound.Text))
                            {
                                BookManager.getSingleton().ActivePage.Sounds.Remove(pair.Key);
                                BookManager.getSingleton().ActivePage.Sounds.Add(txtLanguage.Text, txtSound.Text);
                                break;
                            }
                        }
                    }
                    else
                    {
                        BookManager.getSingleton().ActivePage.AddSound(this.txtLanguage.Text, txtSound.Text);
                    }
                }

                DialogResult = true;

                Close();
            }
        }

        private void btnBrowseSound_Click(object sender, RoutedEventArgs e)
        {
            browseForSound();
        }

        private void browseForSound()
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == true)
            {
                string fileName = open.FileName;

                if (isValidSound(fileName))
                    this.txtSound.Text = open.FileName;
            }
        }

        /// <summary>
        /// Tells if a file meets all the requirements for being a page's sound, and shows error messages otherwise
        /// </summary>
        /// <param name="fileName">The file's full path</param>
        /// <returns>True if the file is a .mp3 or .wav sound file of 1MB maximum size</returns>
        private bool isValidSound(string fileName)
        {
            bool isValid = true;

            if (System.IO.File.Exists(fileName))
            {
                if (fileName.EndsWith(".mp3", true, null) || fileName.EndsWith(".wav", true, null))
                {
                    long maxSize = 1024 * 1024 * 5; // 5 MB max. size
                    long fileSize = new FileInfo(fileName).Length;
                    if (fileSize > maxSize)
                    {
                        MessageBox.Show("The sound file is too big (5 MB max.)", "Bad size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        isValid = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please choose a .wav or .mp3 audio file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    isValid = false;
                }
            }
            else
            {
                MessageBox.Show("The file you chose does not exist. Try again.", "The file does not exist", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }

            return isValid;
        }

        private void txtSound_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            browseForSound();
        }
    }
}
