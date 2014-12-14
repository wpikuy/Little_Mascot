using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MascotCore {
    class TodoEntity{

        public TodoEntity(int year, int month, int day, int hour, int minute, string description){
            _date = new DateTime(year, month, day, hour, minute, 0);
            _description = description;
            _timeUp = false;
            _lastTime = DateTime.Now;
        }

        private readonly DateTime   _date;
        private readonly string     _description;
        private bool                _timeUp;
        private DateTime            _lastTime;

        public bool isTimeNow(){
            if (_timeUp) return false;
            var current = DateTime.Now;
            var interval = _date - current;
            if (interval.Duration().Seconds != interval.Seconds){
                _timeUp = true;
                return true;
            }
            else{
                if (current - _lastTime > TimeSpan.FromMinutes(10)){
                    return true;
                }
                else{
                    return true;
                }
            }
        }
        
        public string Description{
            get { return _timeUp ? "时间到： " + _description : _description; }
        }
    }
}
