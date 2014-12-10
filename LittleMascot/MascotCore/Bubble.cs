using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace MascotCore {
    public partial class Bubble : InnerComponent {
        public Bubble() {
            
        }

        private AppearenceWindow _parent;
        private Image _image;
        private TextBlock _text;
        private Grid _frame;
        private const string _imgLocation = @".\imgs\bubble.png";
        private bool _isShowing;
        private DispatcherTimer _timer;

        public void showText(string text) {
            _text.Text = text;
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Start();

            var showAnimation = new ThicknessAnimation{
                From = new Thickness(200, 200, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new CircleEase()
            };

            _frame.BeginAnimation(Grid.MarginProperty, showAnimation);

            var textAnimation = new DoubleAnimation{
                From = 0,
                To = 22,
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new CircleEase()
            };

            _text.BeginAnimation(TextBlock.FontSizeProperty, textAnimation);
        }

        private void hideText(object sender, EventArgs e) {
            _timer.Stop();

            var hideAnimation = new ThicknessAnimation{
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(200, 200, 0, 0),
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new CircleEase()
            };

            _frame.BeginAnimation(Grid.MarginProperty, hideAnimation);

            var textAnimation = new DoubleAnimation {
                From = 22,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new CircleEase()
            };

            _text.BeginAnimation(TextBlock.FontSizeProperty, textAnimation);
        }
        
        public void OnInit(){
            _image = _parent.Bubble;
            _text = _parent.BubbleText;
            _frame = _parent.BubbleFrame;
            _isShowing = false;
            _timer = new DispatcherTimer(DispatcherPriority.Normal);
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += hideText;

            PngBitmapDecoder decoder = new PngBitmapDecoder(
                new Uri(_imgLocation, UriKind.Relative), 
                BitmapCreateOptions.PreservePixelFormat, 
                BitmapCacheOption.Default);
            _image.Source = decoder.Frames[0];

            _frame.Margin = new Thickness(200, 200, 0, 0);
        }

        public void OnExit() {
            
        }

        public void SetParent(Window window) {
            _parent = window as AppearenceWindow;
        }
    }
}
