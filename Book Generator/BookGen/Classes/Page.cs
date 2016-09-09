using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BookGen.Classes
{
    [Serializable]
    public class Page
    {
        private Dictionary<string, string> images;
        private Dictionary<string, string> sounds;

        private string background;

        private string title;
        private float delay;

        /// <summary>
        /// Page constructor
        /// </summary>
        /// <param name="title">Page's title</param>
        /// <param name="delay">Delay in seconds</param>
        public Page(string title, float delay)
        {
            this.title = title;
            this.delay = delay;

            this.background = String.Empty;

            this.images = new Dictionary<string, string>();
            this.sounds = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds a language image to this page
        /// </summary>
        /// <param name="language">The name of the language whose image is to be added</param>
        /// <param name="file">The image file to be added</param>
        public void AddImage(string language, string file)
        {
            if (this.images.ContainsKey(language))
            {
                string message = String.Format("You have already added a language image in \"{0}\". Do you want to replace it?", language);

                MessageBoxResult result = MessageBox.Show(message, "Override", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    this.images[language] = file;
                }
            }
            else
            {
                this.images.Add(language, file);
            }
        }

        /// <summary>
        /// Removes a language image from this page
        /// </summary>
        /// <param name="language">The name of the language whose image will be removed</param>
        public void RemoveImage(string language)
        {
            this.images.Remove(language);
        }

        /// <summary>
        /// Adds a sound file to this page
        /// </summary>
        /// <param name="language">The name of the language whose sound is to be added</param>
        /// <param name="file">The sound file to be added</param>
        public void AddSound(string language, string file)
        {
            if (this.sounds.ContainsKey(language))
            {
                string message = String.Format("The language \"{0}\" already has an audio file. Do you want to replace it?", language);

                MessageBoxResult result = MessageBox.Show(message, "Override", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    this.sounds[language] = file;
                }
            }
            else
            {
                this.sounds.Add(language, file);
            }
        }

        /// <summary>
        /// Removes a sound file from this page
        /// </summary>
        /// <param name="language">The name of the language whose sound file will be removed</param>
        public void RemoveSound(string language)
        {
            this.sounds.Remove(language);
        }

        /// <summary>
        /// Page's background image, to be shared among all its languages
        /// </summary>
        public string Background
        {
            get { return this.background; }
            set { this.background = value; }
        }

        /// <summary>
        /// Page's name
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Delay in seconds after the page will turn on its own after its sound has stopped playing
        /// </summary>
        public float Delay 
        {
            get { return this.delay; }
            set { this.delay = value; }
        }

        /// <summary>
        /// Contains the language images for this page, along with their language's name as (name, image) pairs
        /// </summary>
        public Dictionary<string, string> Images
        {
            get { return this.images; }
        }

        /// <summary>
        /// Contains the sound files for the page, along with their language's name as (name, sound) pairs
        /// </summary>
        public Dictionary<string, string> Sounds
        {
            get { return this.sounds; }
        }
    }
}
