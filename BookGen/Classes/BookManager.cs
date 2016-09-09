using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookGen.Classes;



namespace BookGen
{
    /// <summary>
    /// Represents a book, containing all its data
    /// </summary>
     [Serializable]
    public class BookManager
    {

        private int pageIndex;
        private List<Page> pages;

        private StartPage startPage;

        private string xapFileName;
        private string title;

        private string largeIcon;
        private string smallIcon;

        private string playIcon;
        private string pauseIcon;

        private string loadIcon;

        private static IList<string> languages;

        private BookManager()
        {
            pages = new List<Page>();
            startPage = new StartPage();
            languages = new List<string>();
            pageIndex = 0;
            this.largeIcon = String.Empty;
            this.smallIcon = String.Empty;
            this.playIcon = String.Empty;
            this.pauseIcon = String.Empty;
            this.loadIcon = String.Empty;
            this.xapFileName = String.Empty;
            this.title = String.Empty;
        }

        static BookManager Book;

        public static BookManager getSingleton()
        {
            if (Book == null)
            {
                Book = new BookManager();
            }

            return Book;

        }

        public void Add(Page page)
        {
            pages.Add(page);

            pageIndex = pages.Count - 1;
        }

        public void Remove(int index)
        {
            pages.RemoveAt(index);

            if (index > pages.Count - 1)
            {
                pageIndex = pages.Count - 1;
            }
            else 
            {
                pageIndex = index;
            }
        }

        public static void SetState(BookManager book)
        {
            BookManager.Book = book;
        }

        public List<Page> Pages
        {
            get { return pages; }
        }

        public Page ActivePage
        {
            get { return pages[pageIndex]; }
        }

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        public StartPage StartPage
        {
            get { return startPage; }
        }

        public string XapFileName
        {
            get { return xapFileName; }
            set { xapFileName = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string LargeIcon
        {
            get { return largeIcon; }
            set { largeIcon = value; }
        }

        public string SmallIcon
        {
            get { return smallIcon; }
            set { smallIcon = value; }
        }

        public string PlayIcon
        {
            get { return playIcon; }
            set { playIcon = value; }
        }

        public string PauseIcon
        {
            get { return pauseIcon; }
            set { pauseIcon = value; }
        }

        public string LoadIcon
        {
            get { return loadIcon; }
            set { loadIcon = value; }
        }

        /// <summary>
        /// List of the book's available languages
        /// </summary>
        public IList<string> Languages
        {
            get { return languages; }
            set { languages = value; }
        }

       

    }
}
