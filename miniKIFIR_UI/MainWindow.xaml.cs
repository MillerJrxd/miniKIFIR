using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        internal readonly string connectionString = "Server=localhost;Database=minikifir;User ID=root;Password=;";

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
                if (felvetelizok.Count == 0)
                {
                    foreach (string sor in File.ReadAllLines(adatokBetoltese.FileName))
                    {
                        felvetelizok.Add(new Adatok(sor));
                    }
                    dgAdatok.ItemsSource = felvetelizok;
                }
                else
                {
                    if (MessageBox.Show("Lecserél(I) vagy Hozzáad(N)", "Vizsgálat", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        felvetelizok.Clear();
                        foreach (string sor in File.ReadAllLines(adatokBetoltese.FileName))
                        {
                            felvetelizok.Add(new Adatok(sor));
                        }
                        dgAdatok.ItemsSource = felvetelizok;
                    }
                    else
                    {
                        foreach (string sor in File.ReadAllLines(adatokBetoltese.FileName))
                        {
                            felvetelizok.Add(new Adatok(sor));
                        }
                        dgAdatok.ItemsSource = felvetelizok;
                    }
                }
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
            else
            {
                for (int i = 0; i < felvetelizok.Count; i++)
                {
                    for (int j = 0; j < dgAdatok.SelectedItems.Count; j++)
                    {
                        if (felvetelizok[i].OM_Azonosito == ((IFelvetelizok)dgAdatok.SelectedItems[j]).OM_Azonosito)
                        {
                            felvetelizok.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
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
            mentes.Filter = "CSV fájl (*.csv) | *.csv | JSON fájl (*.json) | *.json";
            var melyikMentes = System.IO.Path.GetExtension(mentes.FileName);
            if (mentes.ShowDialog() == true)
            {
                if (melyikMentes == ".json")
                {
                    string jso = JsonSerializer.Serialize(mentes, typeof(string));
                }
                else
                {
                    StreamWriter sw = new StreamWriter(mentes.FileName);
                    foreach (var item in felvetelizok)
                    {
                        sw.WriteLine(item.CSVSortAdVissza());
                    }
                    sw.Close();
                    MessageBox.Show("Elmentve.");
                }
            }
        }

        private void Modosit_Button_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;

            Adatok ujdiak = new Adatok();
            MainWindow ujablak = new MainWindow(ujdiak, false);

            ujdiak = (Adatok)dgAdatok.SelectedItem;

            if (felvetelizok.Count == 0 || dgAdatok.SelectedItem == null)
            {
                MessageBox.Show("Nincs kiválasztva tanuló módosításra!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }else
            {
                if (ci?.TwoLetterISOLanguageName == "hu")
                {
                    List<string> datLista = new List<string>();

                    string TempString = ujdiak.SzuletesiDatum.ToString();

                    foreach (var x in TempString.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    {
                       datLista.Add(x.TrimEnd('.'));
                    }
                    datLista.RemoveAt(datLista.Count - 1);

                    ujablak.txtOmAzon.Text = ujdiak.OM_Azonosito;
                    ujablak.txtNev.Text = ujdiak.Neve;
                    ujablak.txtEmail.Text = ujdiak.Email;
                    ujablak.maskedTextBox1.Text = datLista[1] + '.' + datLista[2] + '.' + datLista[0];
                    ujablak.txtErtesitesi.Text = ujdiak.ErtesitesiCime;


                    if (ujdiak.Matematika != -1)
                    {
                        ujablak.txtMatekE.Text = ujdiak.Matematika.ToString();
                    }
                    
                    if (ujdiak.Magyar != -1)
                    {
                        ujablak.txtMagyarE.Text = ujdiak.Magyar.ToString();
                    }
                    else ujablak.txtMagyarE.Text = "";

                    if (ujablak.ShowDialog() == true)
                    {
                        if (ujablak.cbPontRögzit.IsChecked == false)
                        {
                            ujdiak.Neve = ujablak.txtNev.Text;
                            ujdiak.Email = ujablak.txtEmail.Text;
                            ujdiak.SzuletesiDatum = ujablak.convertedDate;
                            ujdiak.ErtesitesiCime = ujablak.txtErtesitesi.Text;
                        }
                        else
                        {
                            ujdiak.Neve = ujablak.txtNev.Text;
                            ujdiak.Email = ujablak.txtEmail.Text;
                            ujdiak.SzuletesiDatum = ujablak.convertedDate;
                            ujdiak.ErtesitesiCime = ujablak.txtErtesitesi.Text;
                            ujdiak.Matematika = int.Parse(ujablak.txtMatekE.Text);
                            ujdiak.Magyar = int.Parse(ujablak.txtMagyarE.Text);
                        }
                        dgAdatok.Items.Refresh();
                    }
                    else MessageBox.Show("Nem került sor az adatok módosítására!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (ci?.TwoLetterISOLanguageName == "en")
                {
                    //TempString	"25/10/2005 00:00:00"	string

                    List<string> datLista = new List<string>();

                    string TempString = ujdiak.SzuletesiDatum.ToString();


                    foreach (string y in TempString.Split('/', StringSplitOptions.RemoveEmptyEntries)) 
                    {
                        datLista.Add(y);
                    }
                    var kiszed = datLista[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                    datLista.RemoveAt(2);
                    kiszed.Remove("00:00:00");
                    datLista.Add(kiszed[0]);

                    ujablak.txtOmAzon.Text = ujdiak.OM_Azonosito;
                    ujablak.txtNev.Text = ujdiak.Neve;
                    ujablak.txtEmail.Text = ujdiak.Email;
                    ujablak.maskedTextBox1.Text = datLista[1] + '/' + datLista[0] + '/' + datLista[2];
                    ujablak.txtErtesitesi.Text = ujdiak.ErtesitesiCime;

                    if (ujdiak.Matematika != -1)
                    {
                        ujablak.txtMatekE.Text = ujdiak.Matematika.ToString();
                    }

                    if (ujdiak.Magyar != -1)
                    {
                        ujablak.txtMagyarE.Text = ujdiak.Magyar.ToString();
                    }
                    else ujablak.txtMagyarE.Text = "";

                    if (ujablak.ShowDialog() == true)
                    {
                        if (ujablak.cbPontRögzit.IsChecked == false)
                        {
                            ujdiak.Neve = ujablak.txtNev.Text;
                            ujdiak.Email = ujablak.txtEmail.Text;
                            ujdiak.SzuletesiDatum = ujablak.convertedDate;
                            ujdiak.ErtesitesiCime = ujablak.txtErtesitesi.Text;
                        }
                        else
                        {
                            ujdiak.Neve = ujablak.txtNev.Text;
                            ujdiak.Email = ujablak.txtEmail.Text;
                            ujdiak.SzuletesiDatum = ujablak.convertedDate;
                            ujdiak.ErtesitesiCime = ujablak.txtErtesitesi.Text;
                            ujdiak.Matematika = int.Parse(ujablak.txtMatekE.Text);
                            ujdiak.Magyar = int.Parse(ujablak.txtMagyarE.Text);
                        }
                        dgAdatok.Items.Refresh();
                    }
                    else MessageBox.Show("Nem került sor az adatok módosítására!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void btnDataImport_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Hiba történt az adatbázishoz való kapcsolódás közben.\nHiba kód: {ex.ErrorCode}\nÜzenet: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
            }
            
            if (connection.State == System.Data.ConnectionState.Open)
            {
                if (felvetelizok.Count > 0)
                {
                    string clearDataQuery = "DELETE FROM felvetelizok";
                    MySqlCommand clearData = new MySqlCommand(clearDataQuery, connection);
                    clearData.ExecuteNonQuery();


                    string insertDataQuery = "INSERT INTO felvetelizok (om_azon, nev, email, szuletesi_datum, ertesitesi_cim, matek_eredmeny, magyar_eredmeny) VALUES (@OM_Azonosito, @Neve, @Email, @SzuletesiDatum, @ErtesitesiCime, @Matematika, @Magyar)";

                    foreach (IFelvetelizok item in felvetelizok)
                    {
                        if (item is Adatok adatok)
                        {
                            MySqlCommand insertDataCmd = new MySqlCommand(insertDataQuery, connection);
                            {
                                insertDataCmd.Parameters.AddWithValue("@OM_Azonosito", adatok.OM_Azonosito);
                                insertDataCmd.Parameters.AddWithValue("@Neve", adatok.Neve);
                                insertDataCmd.Parameters.AddWithValue("@Email", adatok.Email);
                                insertDataCmd.Parameters.AddWithValue("@SzuletesiDatum", adatok.SzuletesiDatum);
                                insertDataCmd.Parameters.AddWithValue("@ErtesitesiCime", adatok.ErtesitesiCime);

                                if (adatok.Matematika == -1)
                                {
                                    insertDataCmd.Parameters.AddWithValue("@Matematika", null);
                                }
                                else insertDataCmd.Parameters.AddWithValue("@Matematika", adatok.Matematika);

                                if (adatok.Magyar == -1)
                                {
                                    insertDataCmd.Parameters.AddWithValue("@Magyar", null);
                                }
                                else insertDataCmd.Parameters.AddWithValue("@Magyar", adatok.Magyar);

                                try
                                {
                                    insertDataCmd.ExecuteNonQuery();
                                }
                                catch (MySqlException ex)
                                {
                                    MessageBox.Show($"Sikertelen művelet!\nHiba kód: {ex.ErrorCode}\nÜzenet: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    MessageBox.Show("Sikeres művelet!", "Feltöltés", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Nincs adat, amit fel lehetne tölteni adatbázisba!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    connection.Close();
                }
            }
        }
    }
}
