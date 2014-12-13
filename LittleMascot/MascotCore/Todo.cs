using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MascotCore {
    class TodoEntity{
        public int star;
        public DateTime time;
        public string description;
    }
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
                    var entity = new TodoEntity{
                        star = int.Parse(parts[0]),
                        time = new DateTime(int.Parse(parts[1]), int.Parse(parts[2]), 
                            int.Parse(parts[3]),int.Parse(parts[4]), int.Parse(parts[5]), 0),
                        description = parts[6]
                    };
                    _entities.Add(entity);
                }
            }
        }

        public void OnExecute(){
            if (_entities.Count == 0) return;
            var current = DateTime.UtcNow;
            var entity = _entities[_index];
            var bubble = _parent.parent.Components["Bubble"] as Bubble;
            if (current.CompareTo(entity.time) >= 0){
                bubble.showText("时间到：" + entity.description);
            }
            else {
                var interval = entity.time - current;
                switch (entity.star) {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        if (interval.Days > 2){
                            
                        }
                        break;
                }
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
