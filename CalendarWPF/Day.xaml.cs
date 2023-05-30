using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CalendarWPF
{
    public partial class Day : Window
    {
        private DateTime dateTime;
        public Day(DateTime dateTime)
        {
            InitializeComponent();

            this.dateTime = dateTime;
            restoreFromJson();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow(dateTime);
            window.Show();
            this.Close();
        }

        private void Button_Click_save(object sender, RoutedEventArgs e)
        {
            List<string> selectedCheckBox = new List<string>();

            if (ch1.IsChecked == true) selectedCheckBox.Add(ch1.Name);
            if (ch2.IsChecked == true) selectedCheckBox.Add(ch2.Name);
            if (ch3.IsChecked == true) selectedCheckBox.Add(ch3.Name);
            if (ch4.IsChecked == true) selectedCheckBox.Add(ch4.Name);
            if (ch5.IsChecked == true) selectedCheckBox.Add(ch5.Name);



            JsonHandler jsonHandler = new JsonHandler();
            DayDataObject dayDataObject = new DayDataObject(dateTime, selectedCheckBox);
            jsonHandler.addToFile(dayDataObject);
        }

        private void restoreFromJson()
        {
            JsonHandler jsonHandler = new JsonHandler();
            DayDataObject dayDataObject = jsonHandler.getDayDataObject(dateTime);
            if (dayDataObject == null) return;

            List<string> selectedCheckBox = dayDataObject.selectedCheckBox;
            if (selectedCheckBox == null) return;
            foreach (string checkbox in selectedCheckBox)
            {
                if (FindName(checkbox) is CheckBox)
                {
                    CheckBox ch = FindName(checkbox) as CheckBox;
                    ch.IsChecked = true;
                }
            }
        }
    }
}
