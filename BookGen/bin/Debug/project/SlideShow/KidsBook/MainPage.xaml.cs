using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using KidsBook.Messages;
using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Media.Animation;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace KidsBook
{
    public partial class MainPage : PhoneApplicationPage
    {

        #region private helper attributes

        /// <summary>
        /// True if the automatic animation of a page is in progress
        /// </summary>
        private bool _animating;
        /// <summary>
        /// True if a page is being turned
        /// </summary>
        private bool _turning;
        /// <summary>
        /// True if the page the user has clicked must turn on its own
        /// </summary>
        private bool _autoTurn;
        /// <summary>
        /// Signals the direction of the turning: -1 if left, 1 if right, 0 if no turn
        /// </summary>
        private int _direction = 0;
        /// <summary>
        /// Position on the X axis in where the used clicked
        /// </summary>
        private double _mouseClickHorizontalPosition;
        /// <summary>
        /// Total number of spread pages that make up the book
        /// </summary>
        private int _totalPages = StaticData.totalPages;
        /// <summary>
        /// Index of current spread page
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// Starting position of the mouse when turning a page
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
        private double _step = 0.025;

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

        #region events

        public event EventHandler PageTurned;

        #endregion

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _animating = false;
            _pageIndex = 1;

            _height = this.CurrCanvas.Height;
            _width = this.CurrCanvas.Width;

            // Create a Storyboard for timing animations
            _timer = (Storyboard)XamlReader.Load(_sb);
            _timer.Completed += new EventHandler(OnTimerTick);

            InitializeClipRegions();
        }

        private void OnBeginTurn(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_animating)
                return;

            _mouseClickHorizontalPosition = e.GetPosition(null).Y;

            try
            {
                // Turning page to left
                if ((_mouseClickHorizontalPosition >= 440 && Orientation == PageOrientation.LandscapeLeft)
                    || (_mouseClickHorizontalPosition <= 430 && Orientation == PageOrientation.LandscapeRight))
                {
                    // Check we're not on the last page
                    if (_pageIndex < _totalPages - 2)
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
                        return;
                    }
                }
                // Turning page to right
                else if ((_mouseClickHorizontalPosition <= 430 && Orientation == PageOrientation.LandscapeLeft)
                    || (_mouseClickHorizontalPosition >= 440 && Orientation == PageOrientation.LandscapeRight))
                {
                    // Check we're not on the first page
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
                        return;
                    }
                }

            }
            // In very odd situations there are problems with the clipping regions, try again
            catch(ArgumentException)
            {
                _turning = false;
                InitializeClipRegions();
                return;
            }
        }

        private void OnContinueTurn(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_animating)
                return;

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

        private void OnEndTurn(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_animating)
                return;

            if (_turning)
            {
                double mousePos = e.GetPosition((FrameworkElement)sender).X;
                // If the mouse has not moved much and is close to one of the side's border, the page should turn on its own
                if ((mousePos <= 70 || mousePos >= 700) && System.Math.Abs(mousePos - _startPos) < 10)
                    _autoTurn = true;
                CompleteTurn();
            }
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
                    Reset(PageAction.TurningCanceled);
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
                    Reset(PageAction.TurningCanceled);
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

            double p3x = p2y * tan;

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

        private void FinishTurnLeft()
        {
            _pageIndex++;
            if (PageTurned != null)
                PageTurned(this, EventArgs.Empty);
            Reset(PageAction.TurningLeftFinished);
            // Clip back to original shape
            TurnTo(0.0);
            InitializeClipRegions();
        }

        private void FinishTurnRight()
        {
            _pageIndex--;
            if (PageTurned != null)
                PageTurned(this, EventArgs.Empty);
            Reset(PageAction.TurningRightFinished);
            // Clip back to original shape
            TurnTo(1.0);
            InitializeClipRegions();
        }

        private void Reset(PageAction pAction)
        {
            _turning = false;
            _animating = false;
            _direction = 0;

            if (_owner != null)
                _owner.ReleaseMouseCapture();
            _owner = null;

            _shadow.Visibility = Visibility.Collapsed;

            InitializeZOrder(pAction);
        }

        /// <summary>
        /// Sets action message for a completed turn
        /// </summary>
        private void InitializeZOrder(PageAction pAction)
        {
            var ActionMessage = new PageActionMessage() { action = pAction };
            Messenger.Default.Send<PageActionMessage>(ActionMessage);
        }

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
                    Reset(PageAction.TurningCanceled);
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
                    Reset(PageAction.TurningCanceled);
                }
            }
            else
                _timer.Begin();
        }

    }
}
