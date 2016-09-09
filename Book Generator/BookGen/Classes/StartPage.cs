using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookGen.Classes
{
    [Serializable]
    public class StartPage
    {
        public StartPage()
        {
            this.background = String.Empty;
            this.readImage = String.Empty;
            this.listenImage = String.Empty;
            this.listenImageClicked = String.Empty;
            this.readImageClicked = String.Empty;
        }

        public string Background
        {
            get { return this.background; }
            set { this.background = value; }
        }

        public string ReadImage
        {
            get { return this.readImage; }
            set { this.readImage = value; }
        }

        public string ReadImageClicked
        {
            get { return this.readImageClicked; }
            set { this.readImageClicked = value; }
        }

        public string ListenImage
        {
            get { return this.listenImage; }
            set { this.listenImage = value; }
        }

        public string ListenImageClicked
        {
            get { return this.listenImageClicked; }
            set { this.listenImageClicked = value; }
        }

        string background;
        string readImage;
        string readImageClicked;
        string listenImage;
        string listenImageClicked;
    }
}
