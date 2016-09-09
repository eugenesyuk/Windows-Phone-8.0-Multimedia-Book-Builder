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

namespace BookGen.Windows
{
    /// <summary>
    /// Interaction logic for PagesForm.xaml
    /// </summary>
    public partial class PagesForm : Window
    {
        /// <summary>
        /// Represents whether we are editing an existing page or creating a new one
        /// </summary>
        bool edit = false;

        public PagesForm(bool edit = false)
        {
            InitializeComponent();
            if (edit)
            {
                this.txtPageName.Text = BookManager.getSingleton().ActivePage.Title;
                this.txtDelay.Text = BookManager.getSingleton().ActivePage.Delay.ToString();
                this.edit = edit;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string pageName = txtPageName.Text.Trim();
            string pageDelayStr = txtDelay.Text.Trim();
            float pageDelay = 0;

            if (pageName == "" || pageDelayStr == "")
            {
                MessageBox.Show("You need to fill both fields in to continue", "Empty field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (!float.TryParse(pageDelayStr, out pageDelay))
            {
                MessageBox.Show("You need to introduce a numeric value (in seconds) for the delay field", "Invalid number", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (pageDelay < 0)
            {
                MessageBox.Show("The delay has to be a value greater than zero!", "Invalid number", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (!edit)
            {
                if (pageDoesNotExist(pageName))
                {
                    Classes.Page page = new Classes.Page(pageName, pageDelay);
                    BookManager.getSingleton().Add(page);
                }
                else
                {
                    MessageBox.Show("A page with that name already exists. Choose a different name or edit that page.", "The page exists", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
            else
            {
                BookManager.getSingleton().ActivePage.Title = pageName;
                BookManager.getSingleton().ActivePage.Delay = pageDelay;
            }

            DialogResult = true;

            Close();
        }

        private bool pageDoesNotExist(string name)
        {
            foreach (var page in BookManager.getSingleton().Pages)
            {
                if(page.Title.Equals(name))
                    return false;
            }
            return true;
        }
    }
}
