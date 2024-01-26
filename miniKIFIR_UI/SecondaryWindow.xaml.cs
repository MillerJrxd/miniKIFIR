using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Threading;
using System.Linq;

namespace miniKIFIR_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Adatok felvetelizoAdatai;
        bool uzemMod;
        private DateTime convertedDate;

        public MainWindow(Adatok ujdiak, bool mod) : this()
        {
            this.felvetelizoAdatai = ujdiak;
            this.uzemMod = mod;
            if (this.uzemMod)
            {
                txtOmAzon.IsEnabled = true;
            }
            else { txtOmAzon.IsEnabled = false; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        //Ablak bezárás, erőforrások felszabadítása
        private void WindowManager()
        {
            Window UI_Window = Window.GetWindow(this);
            UI_Window.Resources.Clear();
            UI_Window.Close();
        }

        //Ablak mozgatása
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //Ablakbezárás megerősítéssel
        private void btnAppExit_Click(object s, RoutedEventArgs e)
        {
            if (MessageBox.Show("Biztosan ki szeretne lépni?\nFigyelem ez a művelet megszakítja az adatfelvételt!", "Figyelem", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                WindowManager();
            }
            else
            {
                return;
            }
        }

        //Abalak minimalizálása
        private void btnAppMinimize_Click(object s, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //Szám ellenörzés, ezt használja az OM Azonosító, Magyar, Matek pont mezők.
        private void NumberValidation(object s, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Szöveg ellenörzés, ezt használja a Név mező
        private void TextValidation(object s, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z\\P{IsBasicLatin}]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Checkbox ellenörzés, magyar illetve matek mezők be/kikapcsolása.
        private void cbPontRögzit_Click(object sender, RoutedEventArgs e)
        {
            if (cbPontRögzit.IsChecked == true)
            {
                txtMatekE.IsEnabled = true;
                txtMagyarE.IsEnabled = true;
            }
            else
            {
                txtMatekE.IsEnabled = false;
                txtMagyarE.IsEnabled = false;
            }
        }

        //Név ellenörzés, 2 szó, felesleges space-ek kiszűrése
        private void txtNev_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] nev = { String.Empty };

            if (txtNev.Text.Contains(' '))
            {
                nev = txtNev.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (nev.Length >= 2)
                {
                    for (int i = 0; i < nev.Length; i++)
                    {
                        if (!char.IsUpper(nev[i][0]))
                        {
                            MessageBox.Show("A névben lévő szavaknak nagy betűvel kell kezdődnie!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtNev.Clear();
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("A névnek legalább 2 szóból kell állnia (Vezetéknév, Keresztnév).", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("A névnek muszáj legalább egy 'SPACE'-t tartalmaznia", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Dátum ellenörzés a maszkolt textboxból.
        private void maskedTextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            string dateString = maskedTextBox1.Text;

            if (DateTime.TryParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                if (result > DateTime.Now)
                {
                    MessageBox.Show("Nem lehet jövőbeli dátum.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    maskedTextBox1.Clear();
                }
                else if (DateTime.Now.Year - result.Year > 19)
                {
                    MessageBox.Show("A megadott tanuló nem lehet 18 évesnél idősebb!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    maskedTextBox1.Clear();
                }
                else
                {
                    convertedDate = result;
                }
            }
            else if (DateTime.TryParseExact(dateString, "MM. dd. yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result2))
            {
                if (result2 > DateTime.Now)
                {
                    MessageBox.Show("Nem lehet jövőbeli dátum.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    maskedTextBox1.Clear();
                }
                else if (DateTime.Now.Year - result2.Year > 19)
                {
                    MessageBox.Show("A megadott tanuló nem lehet 18 évesnél idősebb!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    maskedTextBox1.Clear();
                }
                else
                {
                    convertedDate = result2;
                }
            }
            else
            {
                MessageBox.Show("Nem létező dátum!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                maskedTextBox1.Clear();
            }
        }

        //Rögzít gomb, adat átadás, illetve adatok ellenörzése.
        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            if (cbPontRögzit.IsChecked == true)
            {
                if (txtMatekE.Text == "" || txtMagyarE.Text == "" || txtNev.Text == "" || txtErtesitesi.Text == "" || txtEmail.Text == "")
                {
                    MessageBox.Show("Nem lehet üres mező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (int.Parse(txtMagyarE.Text) > 50 || 0 > int.Parse(txtMagyarE.Text))
                {
                    MessageBox.Show("Nem lehet ennyi pontja magyarból a felvételizőnek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtMagyarE.Clear();
                    txtMagyarE.Focus();
                }
                else if (txtEmail.Text.Count(x => x == '@') != 1)
                {
                    MessageBox.Show($"Az email cím mező több mint egy '@' jelet tartalmaz, nem megfelelő email!\n '@' Jelek  száma:{txtEmail.Text.Count(x => x == '@')}",
                        "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (int.Parse(txtMatekE.Text) > 50 || 0 > int.Parse(txtMatekE.Text))
                {
                    MessageBox.Show("Nem lehet ennyi pontja matematikából a felvételizőnek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtMatekE.Clear();
                    txtMatekE.Focus();
                }
                else
                {
                    this.felvetelizoAdatai.OM_Azonosito = txtOmAzon.Text;
                    this.felvetelizoAdatai.Neve = txtNev.Text;
                    this.felvetelizoAdatai.Email = txtEmail.Text;
                    this.felvetelizoAdatai.SzuletesiDatum = convertedDate;
                    this.felvetelizoAdatai.ErtesitesiCime = txtErtesitesi.Text;
                    this.felvetelizoAdatai.Matematika = int.Parse(txtMatekE.Text);
                    this.felvetelizoAdatai.Magyar = int.Parse(txtMagyarE.Text);
                    MessageBox.Show("Az adatok sikeresen rögzítve lettek!", "Sikeres rögzítés", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;

                    WindowManager();
                }
            }
            else if (txtNev.Text == "" || txtOmAzon.Text == "" || txtErtesitesi.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Nem lehet üres mező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtEmail.Text.Count(x => x == '@') != 1)
            {
                MessageBox.Show($"Az email cím mező több mint egy '@' jelet tartalmaz, nem megfelelő email!\n '@' Jelek  száma:{txtEmail.Text.Count(x => x == '@')}",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.felvetelizoAdatai.OM_Azonosito = txtOmAzon.Text;
                this.felvetelizoAdatai.Neve = txtNev.Text;
                this.felvetelizoAdatai.Email = txtEmail.Text;
                this.felvetelizoAdatai.SzuletesiDatum = convertedDate;
                this.felvetelizoAdatai.ErtesitesiCime = txtErtesitesi.Text;
                this.felvetelizoAdatai.Matematika = -1;
                this.felvetelizoAdatai.Magyar = -1;
                MessageBox.Show("Az adatok sikeresen rögzítve lettek!", "Sikeres rögzítés", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;

                WindowManager();
            }
        }
    }
}

