using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MascotCore {
    public interface InnerComponent{
        void OnInit();
        void OnExit();
        void SetParent(Window window);
    }
}
