using System;
using System.Collections.Generic;
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
using QuranCS;


namespace QuranCSTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Run SelectedAyahRun { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            SurahComboBox.ItemsSource = Quran.GetSurahNames();
        }

        private void SurahBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            var index = SurahComboBox.SelectedIndex +1;
            Surah surah = new Surah(index);

            //refresh ayah combo box
            AyahComboBox.Items.Clear();
            for (int i = 1; i <= surah.AyahCount; i++)
            {
                AyahComboBox.Items.Add(i);
            }

            var lines = DisplayParagraph.Inlines;
            lines.Clear();
            foreach (var ayah in surah)
            {
                var ayahRun = new Run(ayah.ToString() + " ۝ ");
                ayahRun.PreviewMouseLeftButtonDown += AyahPressed;
                ayahRun.AddHandler(UIElement.MouseLeftButtonUpEvent, 
                    new MouseButtonEventHandler(AyahReleased));

                lines.Add(ayahRun);
            }

        }

        private void AyahReleased(object sender, MouseButtonEventArgs e)
        {
            if (sender == SelectedAyahRun)
            {
                MessageBox.Show(SelectedAyahRun.Text);
            }
        }

        private void AyahPressed(object sender, MouseButtonEventArgs e)
        {
            SelectedAyahRun = (Run)sender;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchResults = Quran.BasicSearch(SearchBox.Text);
            var lines = DisplayParagraph.Inlines;
            lines.Clear();
            foreach (var result in searchResults)
            {
                lines.Add(result.ToString() + " ۝ \n");
            }
        }


    }
}
