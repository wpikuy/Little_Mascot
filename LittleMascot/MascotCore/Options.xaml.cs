using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MascotCore {
    /// <summary>
    /// Options.xaml 的交互逻辑
    /// </summary>
    public partial class Options : Window, InnerComponent {
        public Options() {
            InitializeComponent();
            //TODO: hide
        }

        public void OnDrag(Object sender, RoutedEventArgs e){
            DragMove();
        }

        public void OnInit(){
            throw new NotImplementedException();
        }

        public void OnExit(){
            throw new NotImplementedException();
        }

        public void SetParent(Window window){
            throw new NotImplementedException();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
/*<Grid Visibility="Hidden">
        <Rectangle Fill="#FFECECEC" HorizontalAlignment="Center" VerticalAlignment="Center" Height="360" Width="540"
                   MouseLeftButtonDown="OnDrag"/>
        <Grid HorizontalAlignment="Left" Height="360" VerticalAlignment="Top" Width="134">
            <Label Content="Mini Mascot" HorizontalAlignment="Left" Margin="10,10,0,0" VerticalAlignment="Top" Panel.ZIndex="100" FontFamily="/XTT.ttf#Droid Sans" Height="35" Width="114" FontSize="18"/>
            <Rectangle Fill="#FFE094AC" HorizontalAlignment="Left" Height="360" VerticalAlignment="Top" Width="134"/>
        </Grid>
    </Grid>
 */
