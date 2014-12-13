using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MascotCore {
    class TrayIcon : InnerComponent{

        private NotifyIcon _notify;
        private string _iconPaht = @".\imgs\icon.ico";
        private AppearenceWindow _parent;
        private Options _options;

        public void OnInit(){
            _notify = new NotifyIcon();
            _notify.Icon = new Icon(_iconPaht);
            _notify.Visible = true;
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add("Settings", delegate{
                if (_options == null){
                    _options = new Options();
                    _options.Closed += (sender, args) => {
                        _options = null;
                    };
                }
                _options.Show();
            });
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Exit", delegate { _parent.exitApplication(); });
            _notify.ContextMenu = contextMenu;
        }

        public void OnExit(){
            _notify.Visible = false;
            if (_options != null){
                _options.Close();
            }
        }

        public void SetParent(Window window){
            _parent = window as AppearenceWindow;
        }
    }
}
