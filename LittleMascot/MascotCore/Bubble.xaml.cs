﻿using System;
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
    /// Bubble.xaml 的交互逻辑
    /// </summary>
    public partial class Bubble : Window, InnerComponent {
        public Bubble() {
            InitializeComponent();
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
    }
}
