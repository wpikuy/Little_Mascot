using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Rectangle = System.Drawing.Rectangle;

namespace MascotCore {
    /// <summary>
    /// AppearenceWindow.xaml 的交互逻辑
    /// AppearenceWindow 作为Mascot的核心逻辑部分
    /// </summary>
    public partial class AppearenceWindow : Window{

        private bool _isRunning;

        public AppearenceWindow() {
            InitializeComponent();
            initInnerComponents();
            fixPositionOnMultiscreens();
            initAppearence();
            this.Closed += exitApplication;
        }

        #region Exit Part

        private void exitApplication(object sender, EventArgs e){
            exitApplication();
        }

        public void exit_click(Object sender, RoutedEventArgs e){
            exitApplication();
        }

        public void exitApplication(){
            exitInnerComponents();
            _isRunning = false;
            _playThread.Abort();
            Close();
        }

        public void text_click(Object sender, RoutedEventArgs e) {
            ((Bubble)Components["Bubble"]).showText("ニャンパスー");
        }

        #endregion

        #region Inner Components

        // inner components
        public Dictionary<string, InnerComponent> Components { get; private set; }

        private void initInnerComponents(){
            /*
             * 初始化内部组件
             */

            Components = new Dictionary<string, InnerComponent>();

            // 需要被使用的实例
            Type[] comps ={
                typeof (Bubble),
                //typeof (TrayIcon),
                //typeof (Options),
                //typeof (OuterComponentController)
            };

            foreach (Type compType in comps){
                var com = compType.Assembly.CreateInstance(compType.FullName) as InnerComponent; // 新建一个实例
                com.SetParent(this);
                com.OnInit(); // 初始化实例
                string[] typeNames = compType.FullName.Split('.'); // 从类全名中获取类名
                string typeName = typeNames[typeNames.Count() - 1]; // 得到类名
                Components.Add(typeName, com); // 统一保管实例
            }
        }

        private void exitInnerComponents(){
            foreach (InnerComponent comp in Components.Values) {
                comp.OnExit();
            }
        }

        #endregion

        #region Fix Position On Multiscreens

        // 修正多屏显示时候的位置
        private void fixPositionOnMultiscreens(){
            // 获取位置
            var areaRect = new Rectangle((int) Left, (int) Top, (int) Width, (int) Height);

            // 是否在内部
            bool inside = false;
            foreach (Screen screen in Screen.AllScreens){
                if (!screen.Bounds.Contains(areaRect)){
                    inside = true;
                    break;
                }
            }

            // 对位置进行调整
            if (!inside){
                // 调整到主显示器中心
                Screen primaryScreen = Screen.AllScreens[0];
                Left = primaryScreen.Bounds.X + primaryScreen.Bounds.Width / 2 - Width / 2;
                Top = primaryScreen.Bounds.Y + primaryScreen.Bounds.Height / 2 - Height / 2;
            }
        }

        #endregion

        #region Animation Part

        /* 
         * 人物图形显示部分
         */

        private Image _image;
        private Dictionary<string, List<BitmapSource>> _animations;
        private float _animInterval = 0.4f;
        private string _playingAnim;
        private Thread _playThread;

        private void initAppearence(){

            this.Topmost = true;
            this.ShowInTaskbar = false;
            _image = PicImage;
            _playingAnim = "idle";
            _animations = new Dictionary<string, List<BitmapSource>>();
            string animRootPath = @".\anims";

            // 读取动画
            if (!Directory.Exists(animRootPath)){
                Directory.CreateDirectory(animRootPath);
            }

            string[] animDirs = Directory.GetDirectories(animRootPath);
            foreach (string animDir in animDirs){
                string[] picNames = Directory.GetFiles(animDir);
                var anim = new List<BitmapSource>();
                foreach (string pic in picNames) {
                    if (!pic.EndsWith("png", StringComparison.OrdinalIgnoreCase)){
                        continue;
                    }
                    Console.WriteLine(pic);
                    PngBitmapDecoder decoder = new PngBitmapDecoder(
                        new Uri(pic, UriKind.Relative),
                        BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default);
                    anim.Add(decoder.Frames[0]);
                }
                if (anim.Count > 0){
                    string[] names = animDir.Split('\\');
                    string name = names[names.Count() - 1];
                    _animations.Add(name, anim);
                }
            }

            // 开始播放动画
            _isRunning = true;
            _playThread = new Thread(PlayAnim);
            _playThread.Start();
        }

        public void changeAnim(string name){
            if (_animations.ContainsKey(name)){
                _playingAnim = name;
            }
        }

        private void PlayAnim(){
            string playingAnim = string.Empty;
            int curFrame = 0;

            while (_isRunning){

                Thread.Sleep((int) (1000 * _animInterval));
                if (!_animations.ContainsKey(_playingAnim)) {
                    continue;
                }

                if (playingAnim != _playingAnim){
                    playingAnim = _playingAnim;
                    curFrame = 0;
                }

                this.Dispatcher.Invoke(() => { _image.Source = _animations[playingAnim][curFrame]; });

                curFrame++;
                if (curFrame >= _animations[playingAnim].Count){
                    curFrame = 0;
                }

            }
        }

        private void OnLeftButton(Object sender, RoutedEventArgs e){
            this.DragMove();
        }

        #endregion

    }
}
