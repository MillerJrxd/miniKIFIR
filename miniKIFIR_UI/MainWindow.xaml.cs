using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace miniKIFIR_UI
{
    /// <summary>
    /// Interaction logic for miniKIFIR_Main.xaml
    /// </summary>
    public partial class miniKIFIR_Main : Window
    {
        ObservableCollection<IFelvetelizok> felvetelizok = new ObservableCollection<IFelvetelizok>();
        public miniKIFIR_Main()
        {
            InitializeComponent();
            dgAdatok.ItemsSource = felvetelizok;
        }
        public void Import_Click_Button(object sender, RoutedEventArgs e)
        {
           OpenFileDialog adatokBetoltese = new OpenFileDialog();
            if (adatokBetoltese.ShowDialog() == true)
            {
                foreach (string sor in File.ReadAllLines(adatokBetoltese.FileName))
                {
                    felvetelizok.Add(new Adatok(sor));
                }
                dgAdatok.ItemsSource = felvetelizok;
            }
        }
        private void btnAppExit_Click(object s, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void DragWithHeader(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        private void Torles_Click_Button(object sender, RoutedEventArgs e)
        {
            if (dgAdatok.SelectedIndex < 0)
            {
                MessageBox.Show("Nincs kijelölt elem!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else felvetelizok.RemoveAt(dgAdatok.SelectedIndex);
        }

        private void Felvesz_Click_Button(object sender, RoutedEventArgs e)
        {
            Adatok ujdiak = new Adatok();

            MainWindow ujablak = new MainWindow(ujdiak, true);

            if (ujablak.ShowDialog() == true)
            {
               felvetelizok.Add(ujdiak);
            }
            else MessageBox.Show("Nem került sor adatfelvételre!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Export_Click_Button(object sender, RoutedEventArgs e)
        {
            SaveFileDialog mentes = new SaveFileDialog();
            mentes.Title = "File neve: ";
            mentes.DefaultExt = ".csv";
            if (mentes.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(mentes.FileName);
                foreach (var item in felvetelizok)
                {
                    sw.WriteLine(item.CSVSortAdVissza());
                }
                sw.Close();
            }
            MessageBox.Show("Elmentve.");
        }
    }
}
