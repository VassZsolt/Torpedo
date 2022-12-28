using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using NationalInstruments.Torpedo.Model;
using Newtonsoft.Json;

namespace NationalInstruments.Torpedo.View
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        private const string Path = @"./Matches.json";

        public Results()
        {
            InitializeComponent();
            List<Result> results = new List<Result>();
            try
            {
                using (StreamReader r = new StreamReader(Path))
                {
                    string json = r.ReadToEnd();
                    List<Result> items = JsonConvert.DeserializeObject<List<Result>>(json);
                    foreach (var item in items)
                    {
                        results.Add(new Result(item.FirstPlayer, item.SecondPlayer, item.Winner, item.NumberOfRounds));
                    }
                    lvMatches.ItemsSource = results;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
                title.Content = "Nincsenek korábbi eredmények";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


    }
}
