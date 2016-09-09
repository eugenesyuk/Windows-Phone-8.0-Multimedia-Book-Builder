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

using BookGen.Pages;
using System.Drawing;


namespace BookGen
{
    /// <summary>
    /// Interaction logic for WPMainWindow.xaml
    /// </summary>
    public partial class WPMainWindow : Window
    {
        public WPMainWindow()
        {
            InitializeComponent();

            NavigateManager.Pages.Add(new Welcome());
            NavigateManager.Pages.Add(new Project());
            NavigateManager.Pages.Add(new StartPage());
            NavigateManager.Pages.Add(new Pages.Pages());
            NavigateManager.Pages.Add(new Settings());
            NavigateManager.Pages.Add(new Build());
            NavigateManager.Pages.Add(new Finish());
            btnBack.IsEnabled = false;
        }

        void SetLabel(string direction)
        {
            int oldIndex = NavigateManager.CurrPage;

            if (direction.Equals("Next"))
            {
                btnBack.IsEnabled = true;
                btnNext.IsEnabled = (oldIndex-- == myPanel.Children.Count - 1) ? false : true;
            }
            else if (direction.Equals("Back"))
            {
                btnNext.IsEnabled = true;
                btnBack.IsEnabled = (oldIndex++ == 0) ? false : true;
            }

            Visual oldChild = (Visual)VisualTreeHelper.GetChild(myPanel, oldIndex);
            Visual newChild = (Visual)VisualTreeHelper.GetChild(myPanel, NavigateManager.CurrPage);

            ((TextBlock)oldChild).FontWeight = FontWeights.Normal;
            ((TextBlock)newChild).FontWeight = FontWeights.Bold;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            goToPreviousPage();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            goToNextPage();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right && btnNext.IsEnabled)
            {
                goToNextPage();
            }
            else if(e.Key == Key.Left && btnBack.IsEnabled)
            {
                goToPreviousPage();
            }
        }

        private void goToNextPage()
        {
            // Check if we can move to the following page.
            switch (NavigateManager.CurrPage)
            {
                case 2:
                    if (String.IsNullOrEmpty(BookManager.getSingleton().StartPage.Background)
                        || String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ListenImage)
                         || String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ListenImageClicked)
                          || String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ReadImageClicked)
                        || String.IsNullOrEmpty(BookManager.getSingleton().StartPage.ReadImage))
                    {
                        MessageBox.Show("Please set the 5 images of this step before proceeding to the following one", "Incomplete step", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    break;
                case 3:
                    if (!checkBookPages()) return;
                    break;
                case 4:
                    if (Settings.GetFocus) return;
                    if (!checkSettings()) return;
                    break;
            }

            mainFrame.Navigate(NavigateManager.GoNext);
            SetLabel("Next");
        }

        private void goToPreviousPage()
        {
            if (NavigateManager.CurrPage == 4)
            {
                if (Settings.GetFocus) return;
            }

            mainFrame.Navigate(NavigateManager.GoBack);
            SetLabel("Back");
        }

        /// <summary>
        /// Checks whether the book is not empty and all its inner pages are correctly configurated.
        /// </summary>
        /// <returns></returns>
        private bool checkBookPages()
        {
            bool isValid = true;

            if (BookManager.getSingleton().Pages.Count > 0)
            {
                // List with all the languages added by the user
                IList<string> languages = new List<string>();

                foreach (var page in BookManager.getSingleton().Pages)
                {
                    // Check correctness of a single page
                    if (!checkPage(page))
                        return false;

                    // Add languages to list
                    foreach (var lang in page.Images.Keys)
                    {
                        if (!languages.Contains(lang))
                        {
                            languages.Add(lang);
                        }
                    }
                    foreach (var lang in page.Sounds.Keys)
                    {
                        if (!languages.Contains(lang))
                        {
                            languages.Add(lang);
                        }
                    }
                }

                // Check that all the pages have image and audio files for each language added by the user
                isValid = checkLanguages(languages);

                if (isValid)
                    BookManager.getSingleton().Languages = languages;
            }
            else
            {
                MessageBox.Show("You must add at least one page to your book", "Incomplete step", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }

            return isValid;
        }

        private bool checkLanguages(IList<string> languages)
        {
            foreach (var page in BookManager.getSingleton().Pages)
            {
                foreach (var lang in languages)
                {
                    if (!page.Images.ContainsKey(lang))
                    {
                        MessageBox.Show(String.Format("Error in page \"{0}\": there is no image file for language \"{1}\".", page.Title, lang), "Incorrect page configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return false;
                    }
                    if (!page.Sounds.ContainsKey(lang))
                    {
                        MessageBox.Show(String.Format("Error in page \"{0}\": there is no sound file for language \"{1}\".", page.Title, lang), "Incorrect page configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether a page is configured correctly
        /// </summary>
        private bool checkPage(Classes.Page page)
        {
            bool isValid = true;
            var languageImages = page.Images;
            var languageSounds = page.Sounds;

            if (String.IsNullOrEmpty(page.Background))
            {
                MessageBox.Show("You must add a background for every page before continuing", "Incomplete step", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }
            else
            {
                if (languageImages.Count > 0 && languageSounds.Count > 0)
                {
                    if (languageImages.Count != languageSounds.Count)
                    {
                        MessageBox.Show(String.Format("Error in page \"{0}\": the number of sound files must equal the number of language images.", page.Title), "Incorrect page configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        isValid = false;
                    }
                    else
                    {
                        foreach (var language in languageImages)
                        {
                            if (!languageSounds.ContainsKey(language.Key))
                            {
                                MessageBox.Show(String.Format("Error in page \"{0}\": there is no sound file for language \"{1}\".", page.Title, language.Key), "Incorrect page configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                isValid = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must add at least one language (image and sound) to your book", "Incomplete step", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool checkSettings()
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(BookManager.getSingleton().XapFileName)
                || String.IsNullOrEmpty(BookManager.getSingleton().Title)
                || String.IsNullOrEmpty(BookManager.getSingleton().LargeIcon)
                || String.IsNullOrEmpty(BookManager.getSingleton().SmallIcon)
                || String.IsNullOrEmpty(BookManager.getSingleton().PauseIcon)
                  || String.IsNullOrEmpty(BookManager.getSingleton().LoadIcon)
                || String.IsNullOrEmpty(BookManager.getSingleton().PlayIcon))
            {
                MessageBox.Show("All the boxes from this step are compulsory. Please fill them in before continuing", "Incomplete step", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                isValid = false;
            }

            return isValid;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            BookGen.Classes.LoadProjectFile.SaveToFile(BookManager.getSingleton());
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = new MessageBoxResult();

            result = MessageBox.Show("Do you want to save changes? ", "Save Changes?", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
               BookGen.Classes.LoadProjectFile.SaveToFile(BookManager.getSingleton());
            
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            
        }

    }
}
