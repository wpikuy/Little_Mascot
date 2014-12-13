using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    public partial class Options : Window {
        public Options() {
            InitializeComponent();
            OnInit();
            Closed += (sender, args) => OnExit();
        }

        public void OnDrag(Object sender, RoutedEventArgs e){
            DragMove();
        }

        public void OnSaveAndExit(Object sender, RoutedEventArgs e) {
            Close();
        }

        private Dictionary<string, string> _options; 

        public void OnInit(){
            _options = getOptions();
            AutoExecuteAfterBooting.IsChecked = bool.Parse(_options["AutoExecuteAfterBooting"]);
        }

        private string _optionsName = @".\mconf.bin";
        private Dictionary<string, string> getOptions(){
            BinaryFormatter formatter = new BinaryFormatter();
            Dictionary<string, string> result = null;
            if (!File.Exists(_optionsName)){
                result = new Dictionary<string, string>();
                result.Add("AutoExecuteAfterBooting", false.ToString());
                using (var stream = File.Create(_optionsName)){
                    formatter.Serialize(stream, result);
                }
            }
            using(var stream = File.OpenRead(_optionsName)){
                result = formatter.Deserialize(stream) as Dictionary<string, string>;
            }
            return result;
        }

        private void saveOptions(Dictionary<string, string> obj){
            BinaryFormatter formatter = new BinaryFormatter();
            using (var stream = File.Create(_optionsName)) {
                formatter.Serialize(stream, obj);
            }
        }

        public void OnExit(){
            saveOptions(_options);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
