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
using BookGen.Classes;
using Microsoft.Win32;

namespace BookGen.Pages
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class Project : System.Windows.Controls.Page
    {
        public Project()
        {
            InitializeComponent();
        }

        private void rbNewProject_Checked(object sender, RoutedEventArgs e)
        {
            rbNewProject_Copy.IsChecked = false;
            loadButton.IsEnabled = false;
        }

        private void rbNewProject_Copy_Checked(object sender, RoutedEventArgs e)
        {
            rbNewProject.IsChecked = false;
            loadButton.IsEnabled = true;
        }

        private void rbNewProject_Copy_Unchecked(object sender, RoutedEventArgs e)
        {
            rbNewProject.IsChecked = true;
            loadButton.IsEnabled = false;
        }

        private void rbNewProject_Unchecked(object sender, RoutedEventArgs e)
        {
            rbNewProject_Copy.IsChecked = true;
        }

        private void loadButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            BookManager manager = LoadProjectFile.OpenFromFile();


            if (manager != null)
            {
                BookManager.SetState(manager);
                loadButton.Visibility = Visibility.Collapsed;
                doneLabel.Visibility = Visibility.Visible;
                rbNewProject.IsEnabled = false;

            
            }
            else
            {
                loadButton.Content = "Load";
            }
        }

       
    }
}
