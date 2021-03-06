﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MascotCore {
    class RandomMsg : OutterComponent{
        private OutterComponentController _parent;
        private Random _rand;
        private List<string> _entities; 
        public void SetParent(OutterComponentController window){
            _parent = window;
        }

        public void OnInit() {
            _rand = new Random();
            _entities = new List<string>();
            using (var stream = File.OpenRead(@".\comps\rand\rand.txt")) {
                var reader = new StreamReader(stream);
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    _entities.Add(line);
                }
            }
        }

        public void OnExecute(){
            var index = _rand.Next(_entities.Count);
            ((Bubble) _parent.parent.Components["Bubble"]).showText(_entities[index]);
        }

        public void OnExit(){
            
        }
    }
}
