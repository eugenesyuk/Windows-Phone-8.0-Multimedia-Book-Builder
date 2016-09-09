public enum PageAction
{
    /// <summary>
    /// Turning left in progress 
    /// </summary>
    TurningLeft,
    /// <summary>
    /// Turning right in progress
    /// </summary>
    TurningRight,
    /// <summary>
    /// Turning right to first page in progress
    /// </summary>
    TurningToCover,
    /// <summary>
    /// Turning left to last page in progress
    /// </summary>
    TurningToGallery,
    /// <summary>
    /// Turning left has finished, go to following page
    /// </summary>
    TurningLeftFinished,
    /// <summary>
    /// Turning right has finished, got to previous page
    /// </summary>
    TurningRightFinished,
    /// <summary>
    /// Page animation must go backwards, current page will stay
    /// </summary>
    TurningCanceled,
    /// <summary>
    /// Turning right to first page finished
    /// </summary>
    TurningToCoverFinished,
    /// <summary>
    /// Turning left to last page finished
    /// </summary>
    TurningToGalleryFinished,
    /// <summary>
    /// User succesfully logged on in Twitter
    /// </summary>
    TwitterAuthFinished,
    /// <summary>
    /// User did not log on succesfully in Twitter
    /// </summary>
    TwitterAuthFailed,
    /// <summary>
    /// Gallery just got navigated to, intialize its books' positions
    /// </summary>
    InitializeGallery,
    /// <summary>
    /// Download the contents of the gallery
    /// </summary>
    LoadGallery,
    /// <summary>
    /// Move to the following book on the gallery
    /// </summary>
    GoToNextBook,
    /// <summary>
    /// /// Move to the previous book on the gallery
    /// </summary>
    GoToPrevBook
}