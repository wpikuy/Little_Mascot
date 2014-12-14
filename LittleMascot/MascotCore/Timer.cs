using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MascotCore {
    class TimerEntity{
        public DateTime Date;
        public string Description;
        public bool Shown;
    }
    class Timer : OutterComponent {
        private OutterComponentController _parent;
        private int _index;
        private List<TimerEntity> _entities;
        public void SetParent(OutterComponentController window){
            _parent = window;
        }

        public void OnInit() {
            _index = 0;
            _entities = new List<TimerEntity>();
            using (var stream = File.OpenRead(@".\comps\timer\timer.txt")) {
                var reader = new StreamReader(stream);
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] parts = line.Split('|');
                    var entity = new TimerEntity{
                        Date = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]),
                            int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), 0),
                        Description = parts[5],
                        Shown = false
                    };
                    _entities.Add(entity);
                }
            }
        }

        public void OnExecute(){
            var bubble = ((Bubble) _parent.parent.Components["Bubble"]);
            foreach (TimerEntity entity in _entities){
                if (!entity.Shown && entity.Date - DateTime.Now < TimeSpan.FromSeconds(0)){
                    bubble.showText(entity.Description);
                    entity.Shown = true;
                }
            }
        }

        public void OnExit(){

        }
    }
}
