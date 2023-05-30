using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarWPF
{
    internal class DayDataObject
    {
        public DateTime dateTime { get; set; }
        public List<string> selectedCheckBox { get; set; }

        public DayDataObject() { }  
        public DayDataObject(DateTime dateTime, List<string> selectedCheckBox) {
            this.dateTime = dateTime;
            this.selectedCheckBox = selectedCheckBox;
        }  
    }
}
