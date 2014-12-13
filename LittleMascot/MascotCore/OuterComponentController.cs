using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MascotCore {
    public interface OutterComponent {
        void SetParent(OutterComponentController window);
        void OnInit();
        void OnExecute();
        void OnExit();
    }
    public class OutterComponentController : InnerComponent{
        public AppearenceWindow parent;
        public Dictionary<string, OutterComponent> Components;
        private List<OutterComponent> _listComponents; 
        private int _index;
        private DispatcherTimer _timer;

        public void OnInit(){
            Components = new Dictionary<string, OutterComponent>();
            _index = 0;
            Type[] comps ={
                typeof (RandomMsg),
                //typeof (TrayIcon),
                //typeof (OuterComponentController)
            };

            foreach (Type compType in comps) {
                var com = compType.Assembly.CreateInstance(compType.FullName) as OutterComponent; // 新建一个实例
                com.SetParent(this);
                com.OnInit(); // 初始化实例
                string[] typeNames = compType.FullName.Split('.'); // 从类全名中获取类名
                string typeName = typeNames[typeNames.Count() - 1]; // 得到类名
                Components.Add(typeName, com); // 统一保管实例
            }
            _listComponents = new List<OutterComponent>(Components.Values);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(30);
            _timer.Tick += (sender, args) =>{
                _listComponents[_index].OnExecute();
                _index++;
                if (_index >= _listComponents.Count){
                    _index = 0;
                }
            };
            _timer.Start();
        }

        public void OnExit() {
            _timer.Stop();
            foreach (OutterComponent outterComponent in Components.Values){
                outterComponent.OnExit();
            }
        }

        public void SetParent(Window controller) {
            parent = controller as AppearenceWindow;
        }
    }
}
