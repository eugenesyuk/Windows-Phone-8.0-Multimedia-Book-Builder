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

using BookGen.Windows;
using Microsoft.Win32;

namespace BookGen.Pages
{
    /// <summary>
    /// Interaction logic for Pages.xaml
    /// </summary>
    public partial class Pages : Page
    {
        public Pages()
        {
            InitializeComponent();
        }

        #region Pages

        private void btnPagesAdd_Click(object sender, RoutedEventArgs e)
        {
            PagesForm addForm = new PagesForm();

            addForm.ShowDialog();

            if (addForm.DialogResult.HasValue && addForm.DialogResult.Value)
            {
                RefreshPagesList();
                label1.Visibility = Visibility.Visible;
            }
        }

        private void btnPagesRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbPages.SelectedIndex > -1)
            {
                BookManager.getSingleton().Remove(this.cbPages.SelectedIndex);
                RefreshPagesList();
            }
        }

        private void btnPagesSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbPages.SelectedIndex > -1)
            {
                PagesForm setting = new PagesForm(true);

                setting.ShowDialog();

                if (setting.DialogResult.HasValue && setting.DialogResult.Value)
                {
                    RefreshPagesList();
                }
            }
        }

        private void cbPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbPages.SelectedIndex > -1)
            {
                BookManager.getSingleton().PageIndex = this.cbPages.SelectedIndex;

                this.RefreshLanguageList();
                this.RefreshSoundsList();
                this.RefreshBackground();
            }
        }

        private void RefreshPagesList()
        {
            this.cbPages.Items.Clear();

            foreach (Classes.Page page in BookManager.getSingleton().Pages)
            {
                this.cbPages.Items.Add(page.Title);
            }

            this.cbPages.SelectedIndex = BookManager.getSingleton().PageIndex;

            this.RefreshLanguageList();
            this.RefreshSoundsList();
            this.RefreshBackground();
        }

        #endregion

        #region Background

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.cbPages.SelectedIndex > -1)
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
                            BookManager.getSingleton().ActivePage.Background = open.FileName;
                            this.label1.Visibility = System.Windows.Visibility.Hidden;
                            RefreshBackground();
                        }
                        else
                        {
                            MessageBox.Show("The image must have a size of 480x800 pixels", "Incorrect size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please choose a .png or .jpg image file", "Incorrect format", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must select an existing page before adding anything to it!", "Empty page", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void RefreshBackground()
        {
            if (this.cbPages.SelectedIndex > -1)
            {
                if (BookManager.getSingleton().ActivePage.Background != "")
                {
                    this.imgBackground.Source = new BitmapImage(new Uri(BookManager.getSingleton().ActivePage.Background));
                }
                else
                {
                    this.imgBackground.Source = null;
                    this.label1.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        #endregion

        #region Languages

        private void btnLanguagesAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbPages.SelectedIndex > -1)
            {
                LanguagesForm add = new LanguagesForm();

                add.ShowDialog();

                if (add.DialogResult.HasValue && add.DialogResult.Value)
                {
                    RefreshLanguageList();
                }

                this.cbLanguages.SelectedIndex = this.cbLanguages.Items.Count - 1;
            }
            else
            {
                showPageNotSelectedError();
            }
        }

        private void btnLanguagesRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbLanguages.SelectedIndex > -1)
            {
                int start = cbLanguages.SelectedItem.ToString().IndexOf('(') + 1;
                int length = cbLanguages.SelectedItem.ToString().IndexOf(')') - start;

                BookManager.getSingleton().ActivePage.RemoveImage(cbLanguages.SelectedItem.ToString().Substring(start, length));

                this.RefreshLanguageList();
            }
        }

        private void btnLanguagesSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbLanguages.SelectedIndex > -1)
            {
                int index = cbLanguages.SelectedIndex;
                LanguagesForm setting = new LanguagesForm(true, index);

                setting.ShowDialog();

                if (setting.DialogResult.HasValue && setting.DialogResult.Value)
                {
                    this.RefreshLanguageList();

                    this.cbLanguages.SelectedIndex = index;
                }
            }
        }

        private void RefreshLanguageList()
        {
            this.cbLanguages.Items.Clear();

            if (this.cbPages.SelectedIndex > -1)
            {
                foreach (KeyValuePair<string, string> pair in BookManager.getSingleton().ActivePage.Images)
                {
                    this.cbLanguages.Items.Add(String.Format("{0} ({1})", System.IO.Path.GetFileName(pair.Value), pair.Key));
                    this.cbLanguages.SelectedItem = cbLanguages.Items[0];
                }
            }
        }

        #endregion

        #region Sounds

        private void btnSoundsAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbPages.SelectedIndex > -1)
            {
                SoundsForm add = new SoundsForm();

                add.ShowDialog();

                if (add.DialogResult.HasValue && add.DialogResult.Value)
                {
                    RefreshSoundsList();
                }

                this.cbSounds.SelectedIndex = this.cbSounds.Items.Count - 1;
            }
            else
            {
                showPageNotSelectedError();
            }
        }

        private void btnSoundsRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbSounds.SelectedIndex > -1)
            {
                int start = cbSounds.SelectedItem.ToString().IndexOf('(') + 1;
                int length = cbSounds.SelectedItem.ToString().IndexOf(')') - start;

                BookManager.getSingleton().ActivePage.RemoveSound(cbSounds.SelectedItem.ToString().Substring(start, length));

                this.RefreshSoundsList();
            }
        }

        private void btnSoundsSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.cbSounds.SelectedIndex > -1)
            {
                int index = cbSounds.SelectedIndex;
                SoundsForm setting = new SoundsForm(true, index);

                setting.ShowDialog();

                if (setting.DialogResult.HasValue && setting.DialogResult.Value)
                {
                    this.RefreshSoundsList();

                    this.cbSounds.SelectedIndex = index;
                }
            }
        }

        private void RefreshSoundsList()
        {
            this.cbSounds.Items.Clear();

            if (this.cbPages.SelectedIndex > -1)
            {
                foreach (KeyValuePair<string, string> pair in BookManager.getSingleton().ActivePage.Sounds)
                {
                    this.cbSounds.Items.Add(String.Format("{0} ({1})", System.IO.Path.GetFileName(pair.Value), pair.Key));
                    this.cbSounds.SelectedItem = this.cbSounds.Items[0];

                }
            }
        }

        #endregion

        private void showPageNotSelectedError()
        {
            MessageBox.Show("You must add a page before adding anything to it!", "Empty page", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            RefreshPagesList();
            

        }

    }
}
