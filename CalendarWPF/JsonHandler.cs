using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CalendarWPF
{
    internal class JsonHandler
    {
        private const string pathToJson = "History.json";

        public void addToFile(DayDataObject dayDataObject) {

            string jsonString = File.ReadAllText(pathToJson);
            List<DayDataObject> listJsonObjects = JsonConvert.DeserializeObject<List<DayDataObject>>(jsonString);
            if (listJsonObjects == null) listJsonObjects = new List<DayDataObject>();

            DayDataObject removingDayDataObject = null;
            foreach (DayDataObject ddo in listJsonObjects)
            {
                if (ddo.dateTime.Equals(dayDataObject.dateTime))
                {
                    removingDayDataObject= ddo;
                    break;
                } 
            }
            listJsonObjects.Remove(removingDayDataObject);
            listJsonObjects.Add(dayDataObject);

            jsonString = JsonConvert.SerializeObject(listJsonObjects);
            File.WriteAllText("History.json", jsonString);
        }

        public DayDataObject getDayDataObject(DateTime dateTime)
        {
            string jsonString = File.ReadAllText(pathToJson);
            List<DayDataObject> listJsonObjects = JsonConvert.DeserializeObject<List<DayDataObject>>(jsonString);
            if (listJsonObjects == null) return null;

            foreach (DayDataObject dayDataObject in listJsonObjects)
            {
                if (dayDataObject.dateTime.Equals(dateTime)) return dayDataObject;
            }
            return null;
        }

        public BitmapImage getImageFirstSelected(DateTime dateTime)
        {
            DayDataObject day = getDayDataObject(dateTime);
            if (day == null) return null;                       

            List<string> selectedCheckBox = day.selectedCheckBox;
            if (selectedCheckBox == null || selectedCheckBox.Count == 0) return null;   

            string first = selectedCheckBox[0];
            
            if (first.Equals("ch1")) return new BitmapImage(new Uri("pack://application:,,,/sources/Football.png"));
            if (first.Equals("ch2")) return new BitmapImage(new Uri("pack://application:,,,/sources/Running.png"));
            if (first.Equals("ch3")) return new BitmapImage(new Uri("pack://application:,,,/sources/Swimming.png"));
            if (first.Equals("ch4")) return new BitmapImage(new Uri("pack://application:,,,/sources/Weightlifting.png"));
            if (first.Equals("ch5")) return new BitmapImage(new Uri("pack://application:,,,/sources/Yoga.png"));
            return null;
        }
    }
}
