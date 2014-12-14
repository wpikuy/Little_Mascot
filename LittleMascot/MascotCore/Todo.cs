using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MascotCore {
    class Todo : OutterComponent{
        private List<TodoEntity> _entities;
        private int _index;
        private OutterComponentController _parent;
        public void SetParent(OutterComponentController window){
            _parent = window;
        }

        public void OnInit(){
            _index = 0;
            _entities = new List<TodoEntity>();
            using (var stream = File.OpenRead(@".\comps\todo\todo.txt")){
                var reader = new StreamReader(stream);
                while (!reader.EndOfStream){
                    string line = reader.ReadLine();
                    string[] parts = line.Split('|');
                    var entity = new TodoEntity(int.Parse(parts[0]), int.Parse(parts[1]),
                            int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), parts[5]);
                    _entities.Add(entity);
                }
            }
        }

        public void OnExecute(){
            if (_entities.Count == 0) return;
            var entity = _entities[_index];
            if (entity.isTimeNow()){
                ((Bubble)_parent.parent.Components["Bubble"]).showText(entity.Description);
            }
            _index++;
            if (_index >= _entities.Count){
                _index = 0;
            }
        }

        public void OnExit(){
            
        }
    }
}
