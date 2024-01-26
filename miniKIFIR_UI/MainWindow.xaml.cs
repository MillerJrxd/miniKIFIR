using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        private void Modosit_Button_Click(object sender, RoutedEventArgs e)
        {
            //Ne kérdezd én sem tudom de kell

            var regKeyGeoId = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Control Panel\International\Geo");
            var geoID = (string)regKeyGeoId.GetValue("Nation");
            var allRegions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.ToString()));
            var regionInfo = allRegions.FirstOrDefault(r => r.GeoId == Int32.Parse(geoID));

            Adatok ujdiak = new Adatok();
            MainWindow ujablak = new MainWindow(ujdiak, false);

            ujdiak = (Adatok)dgAdatok.SelectedItem;

            if (felvetelizok.Count == 0 || dgAdatok.SelectedItem == null)
            {
                MessageBox.Show("Nincs kiválasztva tanuló módosításra!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }else
            {
                if (regionInfo?.TwoLetterISORegionName == "HU")
                {
                    List<string> datLista = new List<string>();

                    string TempString = ujdiak.SzuletesiDatum.ToString();

                    foreach (var item in TempString.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    {
                       datLista.Add(item.TrimEnd('.'));
                    }
                    datLista.RemoveAt(datLista.Count - 1);

                    ujablak.txtOmAzon.Text = ujdiak.OM_Azonosito;
                    ujablak.txtNev.Text = ujdiak.Neve;
                    ujablak.txtEmail.Text = ujdiak.Email;
                    ujablak.maskedTextBox1.Text = datLista[1] + '.' + datLista[2] + '.' + datLista[0];
                    ujablak.txtErtesitesi.Text = ujdiak.ErtesitesiCime;
                    ujablak.txtMatekE.Text = ujdiak.Matematika.ToString();
                    ujablak.txtMagyarE.Text = ujdiak.Magyar.ToString();
                    ujablak.ShowDialog();
                }
                else if (regionInfo?.TwoLetterISORegionName == "GB" || regionInfo?.TwoLetterISORegionName == "US" || )
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
