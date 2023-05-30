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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalendarWPF
{
   
    public partial class MainWindow : Window
    {
        private DateTime currentDateTime;
        private List<string> days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private string pathToJson = "History.json";
        public MainWindow()
        {
            InitializeComponent();
            this.currentDateTime = DateTime.Now;
            displayDayOfMonth(currentDateTime);
        }

        public MainWindow(DateTime currentDateTime)
        {
            InitializeComponent();
            this.currentDateTime = currentDateTime;
            displayDayOfMonth(currentDateTime);
        }

        private void Day_Selection(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button)) return;
            Button currentButton = (Button)sender;
            if (currentButton.Content is StackPanel)
            {
                StackPanel currentButtonStackPanel = (StackPanel)currentButton.Content;

                TextBlock currentButtonTextBlock = (TextBlock)currentButtonStackPanel.Children[0];
                int dayNumber = int.Parse(currentButtonTextBlock.Text);

                DateTime dateSelectedButton = new DateTime(currentDateTime.Year, currentDateTime.Month, dayNumber);
                Window window = new Day(dateSelectedButton);
                window.Show();
                this.Close();
            }



        }

       
        private int numberDay(DateTime dateTime)
        {
            string nameDay = dateTime.DayOfWeek.ToString();
            int indexDay = days.IndexOf(nameDay);
            return indexDay + 1;
        }

      
        private void displayDayOfMonth(DateTime dateTime)
        {
            DateTime dateFirstDay = new DateTime(dateTime.Year, dateTime.Month, 1);     
            int numberFirstDay = numberDay(dateFirstDay);
            MonthTextBlock.Text = dateFirstDay.ToString("MMMM");

            int idButton = 1;

            foreach (var element in CalendarGrid.Children)
            {
                if (element is Button)
                {
                    Button btn = (Button)element;
                    int rowIndex = Grid.GetRow(btn);
                    int columnIndex = Grid.GetColumn(btn);
                    if (columnIndex < numberFirstDay && rowIndex == 1)
                    {
                        btn.Visibility = Visibility.Hidden;
                    }
                    else if (idButton <= DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
                    {
                        if (btn.Content is StackPanel)
                        {
                            StackPanel currentButtonStackPanel = (StackPanel)btn.Content;

                            TextBlock currentButtonTextBlock = (TextBlock)currentButtonStackPanel.Children[0];
                            currentButtonTextBlock.Text = idButton.ToString();

                            Image currentButtonImage = currentButtonStackPanel.Children[1] as Image;
                            JsonHandler jsh = new JsonHandler();
                            currentButtonImage.Source = jsh.getImageFirstSelected(new DateTime(dateTime.Year, dateTime.Month, idButton));
                        }

                        btn.Visibility = Visibility.Visible;
                        btn.Name = "day_" + idButton++;
                    }
                    else
                    {
                        btn.Visibility = Visibility.Hidden;
                    }

                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currentDateTime = currentDateTime.AddMonths(1);
            displayDayOfMonth(currentDateTime);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            currentDateTime = currentDateTime.AddMonths(-1);
            displayDayOfMonth(currentDateTime);
        }
    }
}
