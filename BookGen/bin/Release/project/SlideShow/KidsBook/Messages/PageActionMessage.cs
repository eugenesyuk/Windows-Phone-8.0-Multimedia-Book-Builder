using System;

namespace KidsBook.Messages
{
    /// <summary>
    /// Indicates what kind of action is being performed when changing pages
    /// </summary>
    public class PageActionMessage
    {
        public PageAction action { get; set; }
    }
}
