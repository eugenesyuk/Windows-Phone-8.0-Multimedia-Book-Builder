using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BookGen
{
    [Serializable]
    static class NavigateManager
    {
        public static List<Page> Pages = new List<Page>();

        public static int CurrPage = 0;

        public static Page GoNext
        {
            get
            {
                if (CurrPage == Pages.Count - 1)
                    return Pages[Pages.Count - 1];

                return Pages[++CurrPage];
            }
        }

        public static Page GoBack
        {
            get
            {
                if (CurrPage == 0)
                    return Pages[0];

                return Pages[--CurrPage];
            }
        }
    }
}
