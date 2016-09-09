using System;
using System.Drawing;
using System.IO;
//using System.Windows.Media.Imaging;
using System.Windows;
using System.Diagnostics;

namespace BookGen
{
     [Serializable]
    static class MargeBitmap
    {

        #region Fields

        private static Bitmap source = null;
        private static Bitmap toMarge = null;

        #endregion

        #region Properties

        /// <summary>
        /// Image containing the page's background. It must be a 480x800 pixels image.
        /// </summary>
        public static Bitmap Source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>
        /// Image containing the text to be placed over the page's background. This image must be a 480x800 pixels image containing text of any color over white background.
        /// </summary>
        public static Bitmap ToMarge
        {
            get { return toMarge; }
            set { toMarge = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Merges the two images placed at the source and toMarge static fields.
        /// </summary>
        public static void Merge()
        {
            if (source == null || toMarge == null)
                return;

            Color clr;

            // merge background and text
            for (int py = 0; py < toMarge.Height; py++)
            {
                for (int px = 0; px < toMarge.Width; px++)
                {
                    clr = toMarge.GetPixel(px, py);

                    // If it finds a non-white pixel (text), write it over the source image (background)
                    if (clr.ToArgb() != Color.White.ToArgb() && clr.A > 10)
                        source.SetPixel(px, py, clr);
                }
            }
        }

        /// <summary>
        /// Creates a copy of the given image, flipped and with a 70% opacity white layer on top. This image is used for the back side of the book's pages.
        /// </summary>
        /// <param name="fileName">The path in which to store the resulting page</param>
        public static void CreateWhitePage(string fileName)
        {
            if (source == null)
                return;

            Bitmap bmp = Flip(RotateFlipType.Rotate180FlipY);

            Color clr;

            for (int py = 0; py < bmp.Height; py++)
            {
                for (int px = 0; px < bmp.Width; px++)
                {
                    clr = bmp.GetPixel(px, py);
                    clr = Color.FromArgb(70, clr);
                    bmp.SetPixel(px, py, clr);
                }
            }

            bmp.Save(fileName);
        }


        public static void Save(string fileName)
        {
            if (source != null)
                source.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
        }


        public static Bitmap Flip(RotateFlipType flipType)
        {
            Bitmap tmp = new Bitmap(source);
            tmp.RotateFlip(flipType);
            return tmp;
        }

        #endregion

    }
}
