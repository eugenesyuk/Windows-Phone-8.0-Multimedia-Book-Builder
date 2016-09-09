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
using System.Drawing;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Linq;


namespace BookGen.Pages
{
    /// <summary>
    /// Interaction logic for Build.xaml
    /// </summary>
    public partial class Build : Page
    {

        public string ProjectPath = Environment.CurrentDirectory;

        public Build()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Merges all the book's background images with their corresponding language images, and save the resulting images.
        /// </summary>
        /// <param name="path">The path in which to store the resulting images</param>
        public void MergeImage(string path)
        {
            Dispatcher.Invoke((Action) (() => txtStatus.Text = "Merging and copying pictures"));
            Dispatcher.Invoke((Action)(() => progressBar.Value = 16));
            
            // Create and add the book's pages
            int index = 1;
            foreach (var page in BookManager.getSingleton().Pages)
            {
                foreach (var pair in page.Images)
                {
                    MargeBitmap.Source = (Bitmap)Bitmap.FromFile(page.Background);

                    MargeBitmap.ToMarge = (Bitmap)Bitmap.FromFile(pair.Value);

                    MargeBitmap.Merge();

                    // Save image as pic[index]_[language].png
                    MargeBitmap.Save(path + "\\pic" + index + "_" + pair.Key + ".png");

                    MargeBitmap.CreateWhitePage(path + "\\pic" + index + "_" + pair.Key + "_white.png");
                }
                index++;
            }

            MargeBitmap.Source = null;
            MargeBitmap.ToMarge = null;
        }

        /// <summary>
        /// Renames and copies the book's sound files into the project
        /// </summary>
        /// <param name="path">The project's path in which the sound files will be stored</param>
        public void CopySounds(string path)
        {
            Dispatcher.Invoke((Action) (() => txtStatus.Text = "Copying sounds"));
            Dispatcher.Invoke((Action)(() => progressBar.Value = 33));

            int index = 1;
            string fileName = null;
            string fileExtension = null;

            try
            {

                foreach (var page in BookManager.getSingleton().Pages)
                {
                    foreach (var pair in page.Sounds)
                    {
                        fileName = pair.Value;
                        fileExtension = fileName.Substring(fileName.Length - 4);
                        File.Copy(fileName, path + "\\track" + index + "_" + pair.Key + fileExtension, true);
                    }
                    index++;
                }
            }
            catch (Exception e)
            {
                showBuildError(e, "while copying the application sound files");
            }
        }

        /// <summary>
        /// Copy the the rest of the images that make up the book in the given path, renaming them accordingly
        /// </summary>
        /// <param name="path">Root directory of output project</param>
        public void CopyIcons(string path)
        {
            Dispatcher.Invoke((Action) (() => txtStatus.Text = "Copying other files"));
            Dispatcher.Invoke((Action)(() => progressBar.Value = 50));

            try
            {
                File.Copy(BookManager.getSingleton().StartPage.Background, path + @"\img\first_page.png", true);
                MargeBitmap.Source = new Bitmap(BookManager.getSingleton().StartPage.Background);
                MargeBitmap.CreateWhitePage(path + @"\img\first_page_white.png");
                MargeBitmap.Source = null;
                File.Copy(BookManager.getSingleton().StartPage.ReadImage, path + @"\img\read.png", true);
                File.Copy(BookManager.getSingleton().StartPage.ReadImageClicked, path + @"\img\read_selected.png", true);
                File.Copy(BookManager.getSingleton().StartPage.ListenImage, path + @"\img\listen.png", true);
                File.Copy(BookManager.getSingleton().StartPage.ListenImageClicked, path + @"\img\listen_selected.png", true);
                File.Copy(BookManager.getSingleton().PlayIcon, path + @"\img\icon_play_selected.png", true);
                File.Copy(BookManager.getSingleton().PlayIcon, path + @"\img\icon_play_unselected.png", true);
                File.Copy(BookManager.getSingleton().PauseIcon, path + @"\img\icon_pause_selected.png", true);
                File.Copy(BookManager.getSingleton().PauseIcon, path + @"\img\icon_pause_unselected.png", true);
                File.Copy(BookManager.getSingleton().SmallIcon, path + "ApplicationIcon.png", true);
                File.Copy(BookManager.getSingleton().LargeIcon, path + "Background.png", true);
                File.Copy(BookManager.getSingleton().LoadIcon, path + "SplashScreenImage.jpg", true);
            }
            catch (Exception e)
            {
                showBuildError(e, "when trying to copy the icons");
            }
        }

        /// <summary>
        /// Updates the .csproj XML file with the according project build info
        /// </summary>
        /// <param name="projectDirectory">The root directory of the project to be built</param>
        /// <param name="xapFileName">The desired name for the resulting .xap file</param>
        /// <param name="titleName">The desired name for the book</param>
        /// <param name="language">The name of the book's language (only one book can be built at once)</param>
        public void SetBuildInfo(string projectDirectory, string xapFileName, string titleName, string language)
        {
          
            Dispatcher.Invoke((Action) (() => txtStatus.Text = "Updating build info for language: " + language));
            Dispatcher.Invoke((Action)(() => progressBar.Value = 55));

            string projectFileName = projectDirectory + "SlideShow.csproj";
            // clean .csproj XML file copy, without external resources added
            string cleanProjectFileName = projectDirectory + "SlideShow_copy.csproj";

            // changing the title in SlideShow WMAppManifest XML file
            XDocument doc = XDocument.Load(ProjectPath + @"\project\SlideShow\KidsBook\Properties\WMAppManifest.xml");
            //Find a specific Node according to the name
            var specific = (from e in doc.Descendants("App") select e).First();

            specific.Attribute("Title").Value = BookManager.getSingleton().Title.ToString();
            var title = (from e in doc.Descendants("Title")
                         select e).First();
            title.SetValue(BookManager.getSingleton().Title.ToString());
            doc.Save(ProjectPath + @"\project\SlideShow\KidsBook\Properties\WMAppManifest.xml");


            // Add language and extension to xap file name
            string xapName = xapFileName.Trim();
            if (!xapName.EndsWith(".xap"))
                xapName += "_" + language + ".xap";
            else
                xapName = xapName.Substring(0, xapName.Length - 4) + "_" + language + ".xap";

            string nameSpace = "http://schemas.microsoft.com/developer/msbuild/2003";
            XmlDocument xmlDoc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("x", nameSpace);

            // Load "clean" csproj file without resources
            xmlDoc.Load(cleanProjectFileName);

            // Add xap file name
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("XapFilename");

            for (int i = 0; i < elemList.Count; i++)
            {
                elemList[i].InnerXml = xapName;
            }

            //// Add references to resources (images, sounds) to the project

            // Gets project's csproj file's main node. NamespaceManager is needed, otherwise null is returned
            XmlNode rootNode = xmlDoc.SelectSingleNode("x:Project", nsmgr);
            // Here we create a new ItemGroup node in which the resources will be added as its children.
            // Namespace is needed, otherwise the project won't build properly due to namespace mismatching
            XmlNode resourcesNode = xmlDoc.CreateElement("ItemGroup", nameSpace);

            // Add images, in the form <Content Include="img\pic[index]_[language](_white)?.png" />
            string fileName = null;
            int index = 1;
            foreach (var page in BookManager.getSingleton().Pages)
            {
                // Normal pages
                XmlNode resourceNode = xmlDoc.CreateElement("Content", nameSpace);
                XmlAttribute xa = xmlDoc.CreateAttribute("Include");
                xa.Value = "img\\pic" + index + "_" + language + ".png";
                resourceNode.Attributes.Append(xa);
                resourcesNode.AppendChild(resourceNode);
                // White pages
                XmlNode resourceNode2 = xmlDoc.CreateElement("Content", nameSpace);
                XmlAttribute xa2 = xmlDoc.CreateAttribute("Include");
                xa2.Value = "img\\pic" + index + "_" + language + "_white.png";
                resourceNode2.Attributes.Append(xa2);
                resourcesNode.AppendChild(resourceNode2);
                index++;
            }

            //Add sounds, in the form <Content Include="sound\track[index]_[language].[extension]" />
            index = 1;
            string fileExtension = null;
            foreach (var page in BookManager.getSingleton().Pages)
            {
                foreach (var lang in page.Sounds)
                {
                    if (lang.Key.Equals(language))
                    {
                        fileName = lang.Value;
                        fileExtension = fileName.Substring(fileName.Length - 4);
                        XmlNode contentNode = xmlDoc.CreateElement("Content", nameSpace);
                        XmlAttribute xb = xmlDoc.CreateAttribute("Include");
                        xb.Value = "sound\\track" + index + "_" + language + fileExtension;
                        contentNode.Attributes.Append(xb);
                        resourcesNode.AppendChild(contentNode);
                        index++;
                        break;
                    }
                }
            }

            // Save the new .csproj file
            rootNode.AppendChild(resourcesNode);
            xmlDoc.Save(projectFileName);
          
            

        }

        /// <summary>
        /// Call MSBuild to create the xap file of the project
        /// </summary>
        /// <param name="projectFileName">Path to the project's .sln file</param>
        public void CreateXAP(string projectFileName, string language)
        {

            Dispatcher.Invoke((Action) (() => txtStatus.Text = "Building the project for language: " + language));
            Dispatcher.Invoke((Action)(() => progressBar.Value = 66));

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/MSBuild.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            string projectPath = Directory.GetCurrentDirectory();
            startInfo.Arguments = string.Format("\"{0}\" /t:Rebuild /p:Configuration=Release /p:Platform=\"Any CPU\"", projectFileName);

            try
            {
                 Process exeProcess = Process.Start(startInfo);
                 exeProcess.WaitForExit();
                 string xapFileName = BookManager.getSingleton().XapFileName + "_" + language + ".xap";
                 File.Copy(ProjectPath + @"\project\SlideShow\KidsBook\Bin\Release\" + xapFileName, ProjectPath + "\\" + xapFileName, true);
                 Dispatcher.Invoke((Action)(() => txtStatus.Text = "Done!"));
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error while building the project: Microsoft .NET Framevork v4.5 don't exist , please retry after install: http://www.microsoft.com/download/details.aspx?id=30653 ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Dispatcher.Invoke((Action)(() => txtStatus.Text = "An error happened. The book could not be created."));
      
            }
           
        }

       

        private void buildBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
                ThreadPool.QueueUserWorkItem((o) =>
                    {
                        Dispatcher.Invoke((Action)(() => buildBtn.IsEnabled = false));
                        Dispatcher.Invoke((Action)(() => statusLabel.Visibility = Visibility.Visible));
                        Dispatcher.Invoke((Action)(() => progressBar.Visibility = Visibility.Visible));
                        Dispatcher.Invoke((Action)(() => txtStatus.Visibility = Visibility.Visible));

                        copyResources();
                        foreach (string lang in BookManager.getSingleton().Languages)
                        {
                            createConfigFile(lang);
                            buildProject(lang);
                        }

                        Dispatcher.Invoke((Action)(() => progressBar.Value = 100));
                    });
          

        }

        private void createConfigFile(string lang)
        {
            try
            {
                string filePath = ProjectPath + @"\project\SlideShow\KidsBook\config.txt";
                if (File.Exists(filePath))
                    File.Delete(filePath);
                var f = File.Create(filePath);
                f.Close();
                using (StreamWriter file = new System.IO.StreamWriter(filePath))
                {
                    file.WriteLine("// SlideShow configuration file");
                    file.WriteLine(String.Format("totalPages = {0}", BookManager.getSingleton().Pages.Count));
                    file.WriteLine("sensitivity = 1");
                    file.WriteLine(String.Format("language = {0}", lang));
                    file.Write("delays = ");
                    int i = 0;
                    foreach (var page in BookManager.getSingleton().Pages)
                    {
                        i++;
                        if (i < BookManager.getSingleton().Pages.Count)
                            file.Write(String.Format("{0},", page.Delay));
                        else
                            file.WriteLine(page.Delay.ToString());
                    }
                    file.Close();
                }
            }
            catch (Exception e)
            {
                showBuildError(e, "while creating the configuration text file");
            }
        }

        private void copyResources()
        {
           
            MergeImage(ProjectPath + @"\project\SlideShow\KidsBook\img");

            CopySounds(ProjectPath + @"\project\SlideShow\KidsBook\sound");
            CopyIcons(ProjectPath + @"\project\SlideShow\KidsBook\");
        }

        private void buildProject(string language)
        {
            SetBuildInfo(ProjectPath + @"\project\SlideShow\KidsBook\", BookManager.getSingleton().XapFileName, BookManager.getSingleton().Title, language);
        
            CreateXAP(ProjectPath + @"\project\SlideShow\SlideShow.sln", language);
        }

        private void showBuildError(Exception e, string stepOfError)
        {
            MessageBox.Show(String.Format("There was an error {0}. Please fix it and try to build the project again: {1}", stepOfError, e.Message));
            buildBtn.IsEnabled = true;
            progressBar.Value = 0;
            txtStatus.Text = String.Empty;
            statusLabel.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Collapsed;
            txtStatus.Visibility = Visibility.Collapsed;
        }
    }
}
