using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using KidsBook.Messages;
using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Media.Animation;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Collections.Generic;


using System.Net;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Diagnostics;

namespace KidsBook
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Possible visual states the application can be in
        /// </summary>
   
        private enum States
        {
            MenuDown,
            PopMenuUp,
            FirstPageMenuUp,
            ClearAll,
            ClearMenus,
        };

        #region private helper attributes

        /// <summary>
        /// True if a page's automatic animation is in progress
        /// </summary>
        private bool _animating;
        /// <summary>
        /// True if a page is being turned
        /// </summary>
        private bool _turning;
        /// <summary>
        /// True if the current page must turn on its own, without the user sliding it
        /// </summary>
        private bool _autoTurn;
        /// <summary>
        /// Signals the direction of the turning: -1 for left, 1 for right, 0 for no turn
        /// </summary>
        private int _direction = 0;
        /// <summary>
        /// Number of duplicated pages at both ends of the book (needed to create page turning effect for all pages)
        /// </summary>
        private const int _duplicatedPages = 2;
        /// <summary>
        /// Position on the X axis in where the used clicked
        /// </summary>
        private double _mouseClickHorizontalPosition;
        /// <summary>
        /// Position on the Y axis in where the used clicked
        /// </summary>
        private double _mouseClickVerticalPosition;
        /// <summary>
        /// Total number of spread pages that make up the book
        /// </summary>
        private int _totalPages = StaticData.totalPages;
        /// <summary>
        /// Index of current spread page (n.b. the first and last pages are duplicated)
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// Index of current book in last page's gallery
        /// </summary>

        private double _startPos = -1;
        /// <summary>
        /// Percent of page turned (between 0 and 1)
        /// </summary>
        private double _percent = 0.0;
        /// <summary>
        /// Page width
        /// </summary>
        double _width = 0.0;
        /// <summary>
        /// Page height
        /// </summary>
        double _height = 0.0;
        /// <summary>
        /// Step size for animations
        /// </summary>
        private double _step = 0.2;
        /// <summary>
        /// True if the pop-up menu is out of sight; false otherwise
        /// </summary>
        private bool _isMenuDown = true;
        /// <summary>
        /// True if there is a sub-menu (e.g. twitter menu) on the screen at the moment; false otherwise. Used to exit from them when clicking outside.
        /// </summary>
        private bool _subMenuOn = false;
        /// <summary>
        /// Name of the current visual state the application is in.
        /// </summary>
        private States _currState;
        /// <summary>
        /// True if the pages' sounds should start automatically
        /// </summary>
        private bool _soundsAutoPlay = false;
        /// <summary>
        /// Owner of mouse capture
        /// </summary>
        private FrameworkElement _owner = null;
        /// <summary>
        /// Timer for the turning page storyboard
        /// </summary>
        private Storyboard _timer = null;
        /// <summary>
        /// Polygon used to draw a shadow
        /// </summary>
        private Polygon _shadow = null;
        /// <summary>
        /// Maximum shadow width
        /// </summary>
        private double _shadowWidth = 16;
        /// <summary>
        /// Number of degrees required for shadow to attain maximum width
        /// </summary>
        private double _shadowBreak = 5;
        private PathGeometry _oddClipRegion = null;
        private LineSegment _oddClipRegionLineSegment1 = null;
        private LineSegment _oddClipRegionLineSegment2 = null;
        private PathGeometry _evenClipRegion = null;
        private LineSegment _evenClipRegionLineSegment1 = null;
        private LineSegment _evenClipRegionLineSegment2 = null;
        private LineSegment _evenClipRegionLineSegment3 = null;
        private TransformGroup _transformGroup = null;
        private RotateTransform _rotateTransform = null;
        private TranslateTransform _translateTransform = null;

        #endregion

        #region XAML definitions

        /// <summary>
        /// XAML definition for clip region used on right-hand pages
        /// </summary>
        private const string _opg =
            "<PathGeometry xmlns=\"http://schemas.microsoft.com/client/2007\">" +
            "<PathFigure StartPoint=\"0,0\">" +
            "<LineSegment />" +
            "<LineSegment />" +
            "</PathFigure>" +
            "</PathGeometry>";

        /// <summary>
        /// XAML definition for clip region used on left-hand pages
        /// </summary>
        private const string _epg =
            "<PathGeometry xmlns=\"http://schemas.microsoft.com/client/2007\">" +
            "<PathFigure StartPoint=\"0,0\">" +
            "<LineSegment Point=\"0,0\" />" +
            "<LineSegment Point=\"0,0\" />" +
            "<LineSegment Point=\"0,0\" />" +
            "</PathFigure>" +
            "</PathGeometry>";

        /// <summary>
        /// XAML definition for transforms used on left-hand pages
        /// </summary>
        private const string _tg =
            "<TransformGroup xmlns=\"http://schemas.microsoft.com/client/2007\">" +
            "<RotateTransform />" +
            "<TranslateTransform />" +
            "</TransformGroup>";

        /// <summary>
        /// XAML definition for Storyboard timer
        /// </summary>
        private const string _sb = "<Storyboard xmlns=\"http://schemas.microsoft.com/client/2007\" Duration=\"0:0:0.01\" />";

        /// <summary>
        /// XAML definition for shadow polygon
        /// </summary>
        private const string _poly =
            "<Polygon xmlns=\"http://schemas.microsoft.com/client/2007\" Canvas.ZIndex=\"4\" Fill=\"Black\" Opacity=\"0.2\" Points=\"0,0 0,0 0,0 0,0\" Visibility=\"Collapsed\">" +
            "<Polygon.Clip>" +
            "<RectangleGeometry Rect=\"0,0,{0},{1}\" />" +
            "</Polygon.Clip>" +
            "</Polygon>";

        /// <summary>
        /// XAML definition for dots ellipse
        /// </summary>
        //private const string _dot = "<Ellipse xmlns=\"http://schemas.microsoft.com/client/2007\" Fill=\"#FFCACACA\" Width=\"15\" Height=\"15\" Margin=\"5,0\"/>";
        private const string _dot = "<Image xmlns=\"http://schemas.microsoft.com/client/2007\" Source=\"img/dot_uselected.png\" Width=\"30\" Height=\"30\" Stretch=\"Fill\"/>";

        #endregion

        #region events

        public event EventHandler PageTurned;

        #endregion

        // Constructor
        public MainPage()
        {
            InitializeComponent();


          

         


            // Find out if the user is playing any other sounds in a different app
            if (!Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
            {
                System.Windows.MessageBox.Show("If you want to play sounds, you should turn off any sounds from other applications first");
                StaticData.canPlaySounds = false;
            }

            _animating = false;
            _pageIndex = 1;

            _height = this.CurrCanvas.Height;
            _width = this.CurrCanvas.Width;

            // Create a Storyboard for timing animations
            _timer = (Storyboard)XamlReader.Load(_sb);
            _timer.Completed += new EventHandler(OnTimerTick);




            // Load gallery
            //var msg = new PageActionMessage() { action = PageAction.LoadGallery };
            //Messenger.Default.Send<PageActionMessage>(msg);

            UpdateMenusVisibility();
            InitializeClipRegions();
            InitializeButtons();
        
        }

       

        /// <summary>
        /// Handler for the Pages' mouseLeftButtonDown event. Handles the beginning of a page turn.
        /// </summary>
        private void OnBeginTurn(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_animating || _currState == States.FirstPageMenuUp)
                return;

            // Hide the pop-up menu when clicking somewhere else on the page
            if (!_isMenuDown)
            {
                if (_pageIndex == 1) // On the first page, show its menu
                {
                    VisualStateManager.GoToState(this, "FirstPageMenuUp", true);
                    _currState = States.FirstPageMenuUp;
                }
                else
                {
                    VisualStateManager.GoToState(this, "MenuDown", true);
                    _currState = States.MenuDown;
                }
                _isMenuDown = true;
                return;
            }

            _mouseClickHorizontalPosition = e.GetPosition(null).Y;
            _mouseClickVerticalPosition = e.GetPosition(null).X;

            try
            {
                // Turning page to left
                if ((_mouseClickHorizontalPosition >= 440 && Orientation == PageOrientation.LandscapeLeft)
                    || (_mouseClickHorizontalPosition <= 430 && Orientation == PageOrientation.LandscapeRight))
                {
                    // Check we're not on the last page (i.e. any of the last two pages, as they are duplicated)
                    if (_pageIndex < _totalPages - _duplicatedPages)
                    {
                        _turning = true;
                        _direction = -1;
                        _percent = 0.0;
                        _startPos = e.GetPosition((FrameworkElement)sender).X;
                        ((FrameworkElement)sender).CaptureMouse();
                        _owner = (FrameworkElement)sender;

                        TurnTo(_percent);

                        // Assign clipping regions and transforms to relevant canvases
                        CurrCanvas.Clip = _oddClipRegion;
                        CurrPageCanvas.Clip = _evenClipRegion;
                        CurrPageCanvas.RenderTransform = _transformGroup;

                        // Sets action message for a left turn
                        var ActionMessage = new PageActionMessage() { action = PageAction.TurningLeft };
                        Messenger.Default.Send<PageActionMessage>(ActionMessage);
                    }
                    else
                    {
                        _turning = false;
                        return;
                    }
                }
                // Turning page to right
                else if ((_mouseClickHorizontalPosition <= 430 && Orientation == PageOrientation.LandscapeLeft)
                    || (_mouseClickHorizontalPosition >= 440 && Orientation == PageOrientation.LandscapeRight))
                {
                    // Check we're not on the first page (i.e. the first or the second, as the first one is duplicated)
                    if (_pageIndex > 1)
                    {
                        _turning = true;
                        _direction = 1;
                        _percent = 1.0;
                        _startPos = e.GetPosition((FrameworkElement)sender).X;
                        ((FrameworkElement)sender).CaptureMouse();
                        _owner = (FrameworkElement)sender;

                        // Turn page to specified angle
                        TurnTo(_percent);

                        // Sets action  message for a right turn
                        var ActionMessage = new PageActionMessage() { action = PageAction.TurningRight };
                        Messenger.Default.Send<PageActionMessage>(ActionMessage);

                        // Assign clipping regions and transforms to relevant canvases
                        PrevCanvas.Clip = _oddClipRegion;
                        PrevPageCanvas.Clip = _evenClipRegion;
                        PrevPageCanvas.RenderTransform = _transformGroup;
                    }
                    else
                    {
                        _turning = false;
                        return;
                    }
                }
                else
                {
                    _turning = false;
                    return;
                }

            }
            // In very odd situations there are problems with the clipping regions, try again
            catch (ArgumentException)
            {
                resetPagesConfiguration();
                return;
            }
        }

        /// <summary>
        /// Handler for the Pages' mouseMove event. Updates the pages' shapes when turning.
        /// </summary>
        private void OnContinueTurn(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_animating || _currState == States.FirstPageMenuUp)
                return;

            if (_pageIndex < _totalPages - _duplicatedPages) // Not in last page (gallery)
            {
                VisualStateManager.GoToState(this, "ClearAll", true);
                _currState = States.ClearAll;
            }

            // Turning left
            if (_turning && _direction == -1)
            {
                // Compute change in X
                double dx = _startPos - e.GetPosition((FrameworkElement)sender).X;

                // If mouse moved right, update _startPos so page
                // begins turning with first move left
                if (dx < 0)
                {
                    _startPos = e.GetPosition((FrameworkElement)sender).X;
                    return;
                }

                // Compute turn percentage based on change in X
                double percent = dx / (_width * StaticData.sensitivity);

                if (percent > 1.0)
                    percent = 1.0;
                else if (percent < 0.0)
                    percent = 0.0;

                // Exit now if no change
                if (percent == _percent)
                    return;

                // Update percent turned
                _percent = percent;

                // Turn page to specified angle
                TurnTo(_percent);
            }
            // Turning right
            else if (_turning && _direction == 1)
            {
                // Compute change in X
                double dx = e.GetPosition((FrameworkElement)sender).X - _startPos;

                // If mouse moved left, update _startPos so page
                // begins turning with first move right
                if (dx < 0)
                {
                    _startPos = e.GetPosition((FrameworkElement)(((FrameworkElement)sender).Parent)).X;
                    return;
                }

                // Compute turn percentage based on change in X
                double percent = 1.0 - (dx / (_width * StaticData.sensitivity));

                if (percent > 1.0)
                    percent = 1.0;
                else if (percent < 0.0)
                    percent = 0.0;

                // Exit now if no change
                if (percent == _percent)
                    return;

                // Update percent turned
                _percent = percent;

                // Turn page to specified angle
                TurnTo(_percent);
            }
        }

        /// <summary>
        /// Handler for the Pages' mouseLeftButtonUp event. Decides whether a page has been turned or not, and other click-related events.
        /// </summary>
        private void OnEndTurn(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_animating || _currState == States.FirstPageMenuUp)
                return;

            // If there is an active sub-menu on the screen and the user clicks outside, clear it
            if (_subMenuOn)
            {
                VisualStateManager.GoToState(this, "ClearMenus", true);
                _currState = States.ClearMenus;
                _subMenuOn = false;
            }
            else
            {
                double mouseHorPos = e.GetPosition((FrameworkElement)sender).X;

                if (_turning)
                {
                    // If the mouse has not moved much and is close to one of the sides' border, the page should turn on its own
                    if ((mouseHorPos <= 70 || mouseHorPos >= 700) && System.Math.Abs(mouseHorPos - _startPos) < 10)
                    {
                        _autoTurn = true;
                        VisualStateManager.GoToState(this, "ClearAll", true);
                        _currState = States.ClearAll;
                    }
                    // Finish the page turn or cancel it using the appropiate effect
                    CompleteTurn();
                }
                else
                {
                    if (_pageIndex < _totalPages - _duplicatedPages) // Not in last page (gallery)
                    {
                        VisualStateManager.GoToState(this, "MenuDown", true);
                        _currState = States.MenuDown;
                    }
                }

            }

        }

        /// <summary>
        /// Reset the clip regions and visual states for the current spread pages to their original values
        /// </summary>
        private void resetPagesConfiguration()
        {
            if (PageTurned != null)
                PageTurned(this, EventArgs.Empty);
            CompleteTurn(PageAction.TurningCanceled);
            // Clip back to original shape
            TurnTo(0.0);
            InitializeClipRegions();
            _turning = false;
            VisualStateManager.GoToState(this, "MenuDown", true);
            _currState = States.MenuDown;
            UpdateMenusVisibility();
        }

        /// <summary>
        /// Brings the pop-up menu forward.
        /// </summary>
        private void ShowPopUpMenu()
        {
            VisualStateManager.GoToState(this, "PopUpMenuUp", true);
            _currState = States.PopMenuUp;
            _isMenuDown = false;
        }

        /// <summary>
        /// Brings the first page menu forward
        /// </summary>
        private void ShowFirstPageMenu()
        {
            VisualStateManager.GoToState(this, "FirstPageMenuUp", true);
            _currState = States.FirstPageMenuUp;
            _isMenuDown = false;
        }

        private void CompleteTurn()
        {

            if (_percent == 0.0 && !_autoTurn)
            {
                if (_direction == 1)
                {
                    FinishTurnRight();
                }
                else
                {
                    CompleteTurn(PageAction.TurningCanceled);
                }
                return;
            }

            if (_percent == 1.0 && !_autoTurn)
            {
                if (_direction == -1)
                {
                    FinishTurnLeft();
                }
                else
                {
                    CompleteTurn(PageAction.TurningCanceled);
                }
                return;
            }

            if (_direction == -1)
            {
                if (_percent < 0.3 && !_autoTurn)
                    _step = -Math.Abs(_step);
                else
                    _step = Math.Abs(_step);
            }
            else
            {
                if (_percent > 0.7 && !_autoTurn)
                    _step = Math.Abs(_step);
                else
                    _step = -Math.Abs(_step);
            }

            if (_autoTurn) _autoTurn = false;

            _animating = true;
            _timer.Begin();

            return;
        }

        /// <summary>
        /// Chooses the menus to be shown according to the current page, and choses the visibility of other elements
        /// </summary>
        private void UpdateMenusVisibility()
        {
            if (_pageIndex == 1) // First page (cover)
            {
                ShowFirstPageMenu();
                AudioButtonsGrid.Visibility = Visibility.Collapsed;
                CoverButtonsGrid.Visibility = Visibility.Visible;
                HomeButton2.Visibility = Visibility.Collapsed;


            }
            else if (_pageIndex >= _totalPages - _duplicatedPages) // Last page 
            {

                AudioMenuCanvas.Visibility = Visibility.Collapsed;
                ShowPopUpMenuBorder.Visibility = Visibility.Collapsed;
                AudioButtonsGrid.Visibility = Visibility.Collapsed;
                CoverButtonsGrid.Visibility = Visibility.Visible;
                HomeButton2.Visibility = Visibility.Visible;


                return;
            }
            else // Inner pages
            {

                AudioButtonsGrid.Visibility = Visibility.Visible;
                CoverButtonsGrid.Visibility = Visibility.Collapsed;
            }
            AudioMenuCanvas.Visibility = Visibility.Visible;
            ShowPopUpMenuBorder.Visibility = Visibility.Visible;

        }


        /// <summary>
        /// Computes the clipping regions and shadow when turning a page
        /// </summary>
        /// <param name="percent">Percent of page turned, between 0 and 1</param>
        private void TurnTo(double percent)
        {

            // Compute angle of rotation
            double degrees = 45 - (percent * 45);
            double radians = degrees * Math.PI / 180;

            // Compute x coordinates along bottom of canvas
            double dx1 = _width - (percent * _width);
            double dx2 = _width - dx1;

            // Compute tangent of rotation angle
            double tan = Math.Tan(radians);

            // Configure clipping region for right-hand page
            double p2y;

            if (tan == 0)
                p2y = _height;
            else
                p2y = _height + (dx1 / tan);

            double p3x = p2y * tan * 1.05; // TODO: figure out the reason for 1.05

            _oddClipRegionLineSegment1.Point = new Point(0, p2y);
            _oddClipRegionLineSegment2.Point = new Point(p3x, 0);

            // Configure clipping region for left-hand page
            double p7x = dx2 - (_height * tan);

            if (p7x >= 0.0) // 4-corner clipping region
            {
                _evenClipRegion.Figures[0].StartPoint = new Point(0, 0);
                _evenClipRegionLineSegment1.Point = new Point(0, _height);
                _evenClipRegionLineSegment2.Point = new Point(dx2, _height);
                _evenClipRegionLineSegment3.Point = new Point(p7x, 0);
            }
            else // 3-corner clipping region
            {
                double y = _height - (dx2 / tan);
                _evenClipRegion.Figures[0].StartPoint = _evenClipRegionLineSegment3.Point = new Point(0, y);
                _evenClipRegionLineSegment1.Point = new Point(0, _height);
                _evenClipRegionLineSegment2.Point = new Point(dx2, _height);
            }

            // Apply clipping regions and transforms
            _rotateTransform.CenterX = dx2;
            _rotateTransform.CenterY = _height;
            _rotateTransform.Angle = 2 * degrees;

            _translateTransform.X = 2 * (_width - dx2);

            // Configure shadow
            if (percent == 0.0 || percent == 1.0)
            {
                _shadow.Visibility = Visibility.Collapsed;
                return;
            }

            _shadow.Visibility = Visibility.Visible;

            double min = this._shadowBreak;
            double max = 45 - this._shadowBreak;
            double width;

            if (degrees > min && degrees < max)
                width = _shadowWidth;
            else
            {
                if (degrees <= min)
                    width = (degrees / _shadowBreak) * _shadowWidth;
                else // degrees >= max
                    width = ((45 - degrees) / _shadowBreak) * _shadowWidth;
            }

            double x1 = _width + dx1 + (_height * tan);
            double x2 = _width + dx1;
            double y2 = _height;
            double x3 = x2 + width;
            double y3 = _height;
            double x4 = x1 + width;

            _shadow.Points[0] = new Point(x1, 0);
            _shadow.Points[1] = new Point(x2, y2);
            _shadow.Points[2] = new Point(x3, y3);
            _shadow.Points[3] = new Point(x4, 0);
        }

        /// <summary>
        /// Updates the pages' images and other settings when turning a page left has completed (to navigate to the next page)
        /// </summary>
        private void FinishTurnLeft()
        {
            _pageIndex++;
            if (PageTurned != null)
                PageTurned(this, EventArgs.Empty);
            Player.Stop();
            // Clip back to original shape
            TurnTo(0.0);
            InitializeClipRegions();
            InitializeButtons();

            UpdateMenusVisibility();
  

            if (_pageIndex < _totalPages - _duplicatedPages)
            {
                VisualStateManager.GoToState(this, "MenuDown", true);
                _currState = States.MenuDown;
                CompleteTurn(PageAction.TurningLeftFinished);
            }
            else // Last page
            {
                TurnToFirstPageAuto();
            }
        }

        /// <summary>
        /// Updates the pages' images and other settings when turning a page right has completed (to navigate to the previous page)
        /// </summary>
        private void FinishTurnRight()
        {
            _pageIndex--;
            if (PageTurned != null)
                PageTurned(this, EventArgs.Empty);
            Player.Stop();
            if (_pageIndex == 1)
            {
                CompleteTurn(PageAction.TurningToCoverFinished);
            }
            else
            {
                CompleteTurn(PageAction.TurningRightFinished);
            }
            // Clip back to original shape
            TurnTo(1.0);
            InitializeClipRegions();
            InitializeButtons();

            UpdateMenusVisibility();
            

            if (_pageIndex > 1)
            {
                VisualStateManager.GoToState(this, "MenuDown", true);
                _currState = States.MenuDown;
            }

        }

        /// <summary>
        /// Finishes the page turn indicated by the PageAction passed as its parameter
        /// </summary>
        private void CompleteTurn(PageAction pAction)
        {
            _turning = false;
            _animating = false;
            _direction = 0;

            if (_owner != null)
                _owner.ReleaseMouseCapture();
            _owner = null;

            _shadow.Visibility = Visibility.Collapsed;

            InitializeZOrder(pAction);

            if (_pageIndex < _totalPages - _duplicatedPages) // Not in last page (gallery)
            {
                VisualStateManager.GoToState(this, "MenuDown", true);
                _currState = States.MenuDown;
            }
        }

        /// <summary>
        /// Sets action message for a completed turn
        /// </summary>
        private void InitializeZOrder(PageAction pAction)
        {
            var ActionMessage = new PageActionMessage() { action = pAction };
            Messenger.Default.Send<PageActionMessage>(ActionMessage);
        }

        /// <summary>
        /// Creates the clip regions used for the page turning effects
        /// </summary>
        private void InitializeClipRegions()
        {
            // Create a PathGeometry for clipping right-hand pages
            _oddClipRegion = (PathGeometry)XamlReader.Load(_opg);
            _oddClipRegionLineSegment1 = (LineSegment)_oddClipRegion.Figures[0].Segments[0];
            _oddClipRegionLineSegment2 = (LineSegment)_oddClipRegion.Figures[0].Segments[1];

            // Create a PathGeometry for clipping left-hand pages
            string xaml = String.Format(_epg, this.PrevCanvas.Height);
            _evenClipRegion = (PathGeometry)XamlReader.Load(xaml);
            _evenClipRegionLineSegment1 = (LineSegment)_evenClipRegion.Figures[0].Segments[0];
            _evenClipRegionLineSegment2 = (LineSegment)_evenClipRegion.Figures[0].Segments[1];
            _evenClipRegionLineSegment3 = (LineSegment)_evenClipRegion.Figures[0].Segments[2];

            // Create a TransformGroup for transforming left-hand pages
            _transformGroup = (TransformGroup)XamlReader.Load(_tg);
            _rotateTransform = (RotateTransform)_transformGroup.Children[0];
            _translateTransform = (TranslateTransform)_transformGroup.Children[1];

            // Add the shadow to the main canvas
            _shadow = (Polygon)XamlReader.Load(String.Format(_poly, _width * 2, _height));
            PageTurnCanvas.Children.Add(_shadow);
        }

        /// <summary>
        /// Initializes the audio menu buttons
        /// </summary>
        private void InitializeButtons()
        {
            if (Player.AutoPlay)
            {
                PauseButton.Visibility = System.Windows.Visibility.Visible;
                PlayButton.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                PauseButton.Visibility = System.Windows.Visibility.Collapsed;
                PlayButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// Updates the position and shape of the canvases for each tick of the turning animation
        /// </summary>
        private void OnTimerTick(Object sender, EventArgs e)
        {
            _percent += _step;

            if (_percent < 0.0)
                _percent = 0.0;
            else if (_percent > 1.0)
                _percent = 1.0;

            TurnTo(_percent);

            if (_percent == 0.0)
            {
                if (_direction == 1)
                {
                    FinishTurnRight();
                }
                else
                {
                    CompleteTurn(PageAction.TurningCanceled);
                }
            }
            else if (_percent == 1.0)
            {
                if (_direction == -1)
                {
                    FinishTurnLeft();
                }
                else
                {
                    CompleteTurn(PageAction.TurningCanceled);
                }
            }
            else
                _timer.Begin();
        }

        private void DoneOKButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            VisualStateManager.GoToState(this, "ClearMenus", true);
            _currState = States.ClearMenus;
            _subMenuOn = false;
        }

        /// <summary>
        /// Event handler for the phone's back key press


        private void PlayButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StaticData.canPlaySounds)
            {
                PlayButton.Source = new BitmapImage(new Uri("img/icon_play_selected.png", UriKind.Relative));
            }
        }

        private void PlayButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StaticData.canPlaySounds)
            {
                PlayButton.Source = new BitmapImage(new Uri("img/icon_play_unselected.png", UriKind.Relative));
                Player.Play();
                PauseButton.Visibility = System.Windows.Visibility.Visible;
                PlayButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void PauseButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StaticData.canPlaySounds)
            {
                PauseButton.Source = new BitmapImage(new Uri("img/icon_pause_selected.png", UriKind.Relative));
            }
        }

        private void PauseButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (StaticData.canPlaySounds)
            {
                PauseButton.Source = new BitmapImage(new Uri("img/icon_pause_unselected.png", UriKind.Relative));
                Player.Pause();
                PlayButton.Visibility = System.Windows.Visibility.Visible;
                PauseButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void Player_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Sleep for given number of seconds, then turn page
            if (_soundsAutoPlay && !_turning && StaticData.delays[_pageIndex - _duplicatedPages] > 0)
            {
                System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(StaticData.delays[_pageIndex - _duplicatedPages]));
                AutoPageTurn();
            }
        }

        private void ShowPopUpMenuBorder_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // If the audio menu can appear, slide it up
            if (StaticData.canPlaySounds)
            {
                ShowPopUpMenu();
            }
        }

        private void ListenButtonCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AudioButton.Source = new BitmapImage(new Uri("img/listen_selected.png", UriKind.Relative));
        }

        private void ListenButtonCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(() => AudioButton.Source = new BitmapImage(new Uri("img/listen.png", UriKind.Relative)));

            _soundsAutoPlay = true;

            var autoPlayMsg = new ChangeAutoPlayMessage()
            {
                autoPlay = true
            };

            Messenger.Default.Send<ChangeAutoPlayMessage>(autoPlayMsg);
            AutoPageTurn();
        }

        private void ReadButtonCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ReadButton.Source = new BitmapImage(new Uri("img/read_selected.png", UriKind.Relative));
        }

        private void ReadButtonCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(() => ReadButton.Source = new BitmapImage(new Uri("img/read.png", UriKind.Relative)));

            _soundsAutoPlay = false;

            var autoPlayMsg = new ChangeAutoPlayMessage()
            {
                autoPlay = false
            };

            Messenger.Default.Send<ChangeAutoPlayMessage>(autoPlayMsg);
            AutoPageTurn();
        }

        /// <summary>
        /// Turns a page automatically to the left without any action from the user
        /// </summary>
        private void AutoPageTurn()
        {
            // Set canvases to be modified in the turning effect
            CurrCanvas.Clip = _oddClipRegion;
            CurrPageCanvas.Clip = _evenClipRegion;
            CurrPageCanvas.RenderTransform = _transformGroup;
            // Send a message to the ViewModel so it will update the ZIndexes properly
            var ActionMessage = new PageActionMessage() { action = PageAction.TurningLeft };
            Messenger.Default.Send<PageActionMessage>(ActionMessage);
            // Set turning variables accordingly
            _turning = true;
            _direction = -1;
            _percent = 0.0;
            TurnTo(_percent);
            _autoTurn = true;
            // Clear menus
            VisualStateManager.GoToState(this, "ClearAll", true);
            _currState = States.ClearAll;
            _isMenuDown = true;
            // Finish turn
            CompleteTurn();
        }

        /// <summary>
        /// Performs an automatic turn to the first page (cover)
        /// </summary>
        private void TurnToFirstPageAuto()
        {
            // Assign clipping regions and transforms to relevant canvases
            PrevCanvas.Clip = _oddClipRegion;
            PrevPageCanvas.Clip = _evenClipRegion;
            PrevPageCanvas.RenderTransform = _transformGroup;
            // Send a message to the ViewModel so it will update the ZIndexes properly
            var ActionMessage = new PageActionMessage() { action = PageAction.TurningToCover };
            Messenger.Default.Send<PageActionMessage>(ActionMessage);
            // Set turning variables accordingly
            _turning = true;
            _direction = 1;
            _percent = 1.0;
            TurnTo(_percent);
            _autoTurn = true;
            // Clear menus
            VisualStateManager.GoToState(this, "ClearAll", true);
            _currState = States.ClearAll;
            _isMenuDown = true;
            // Finish turn, as if we were in the 2nd page (so the previous one will be the 1st)
            _pageIndex = 2;
            CompleteTurn();
        }

        /// <summary>
        /// Performs an automatic turn to the last page (gallery)
        /// </summary>
        private void TurnToLastPageAuto()
        {
            // Assign clipping regions and transforms to relevant canvases
            CurrCanvas.Clip = _oddClipRegion;
            CurrPageCanvas.Clip = _evenClipRegion;
            CurrPageCanvas.RenderTransform = _transformGroup;
            // Send a message to the ViewModel so it will update the ZIndexes properly
            var ActionMessage = new PageActionMessage() { action = PageAction.TurningToGallery };
            Messenger.Default.Send<PageActionMessage>(ActionMessage);
            // Set turning variables accordingly
            _turning = true;
            _direction = -1;
            _percent = 0.0;
            TurnTo(_percent);
            _autoTurn = true;
            // Clear menus
            VisualStateManager.GoToState(this, "ClearAll", true);
            _currState = States.ClearAll;
            _isMenuDown = true;
            // Finish turn, as if we were in the previous to last page (so the next one will be the gallery)
            _pageIndex = _totalPages - _duplicatedPages - 1;
            CompleteTurn();
        }

        private void HomeButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HomeButton.Source = new BitmapImage(new Uri("img/icon_home_selected.png", UriKind.Relative));
        }

        private void HomeButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HomeButton.Source = new BitmapImage(new Uri("img/icon_home_unselected.png", UriKind.Relative));
            TurnToFirstPageAuto();
        }

    








    }
}
