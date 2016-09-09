using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace BookGen.Classes
{
    [Serializable]
    class LoadProjectFile
    {
        public static bool SaveToFile(BookManager book)
        {
            FileStream stream;
            string fileName = "";

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".book";
            saveDialog.Filter = "Book files (.book)|*.book";

            if (saveDialog.ShowDialog() == true)
            {
                fileName = saveDialog.FileName;
            }
            else
                return false;

            try
            {
                using (stream = new FileStream(fileName, FileMode.Create))
                {

                    BinaryFormatter bin = new BinaryFormatter();

                    bin.Serialize(stream, book);

                    stream.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem while saving *.book file.\n "+ e, "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }///TODO:

        /// <summary>
        /// Load book from file
        /// </summary>
        /// <param name="fileName"> File Name</param>
        public static BookManager OpenFromFile()
        {
            string fileName = "";

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".book";
            openDialog.Filter = "Book files (.book)|*.book";


            if (openDialog.ShowDialog() == true)
            {
                fileName = openDialog.FileName;
            }
            else
                return null;

            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    BookManager temp = (BookManager)bin.Deserialize(stream);

                    return temp;
                }
            }
            catch
            {
                MessageBox.Show("Problem while opening *.book file.\n", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
    }
}
