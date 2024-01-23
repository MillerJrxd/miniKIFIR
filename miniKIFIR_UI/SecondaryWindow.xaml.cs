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

namespace miniKIFIR_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Adatok felvetelizoAdatai;
        internal DateTime convertedDate;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Adatok ujdiak) : this()
        {
            this.felvetelizoAdatai = ujdiak;
        }

        private void btnAppExit_Click(object s, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnAppMinimize_Click(object s, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void NumberValidation(object s, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextValidation(object s, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z\\P{IsBasicLatin}]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

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
            else
            {
                MessageBox.Show("Nem létező dátum!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                maskedTextBox1.Clear();
            }
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            string tempString = txtOmAzon.Text;

            this.felvetelizoAdatai.Matematika = int.Parse(txtMatekE.Text);
            this.felvetelizoAdatai.Magyar = int.Parse(txtMagyarE.Text);
            this.felvetelizoAdatai.Neve = txtNev.Text;
            this.felvetelizoAdatai.Email = txtEmail.Text;
            this.felvetelizoAdatai.ErtesitesiCime = txtErtesitesi.Text;
            this.felvetelizoAdatai.SzuletesiDatum = convertedDate;

            if (felvetelizoAdatai.Magyar > 50 || felvetelizoAdatai.Magyar < 0)
            {
                MessageBox.Show("Nem lehet ennyi pontja magyarból a felvételizőnek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMagyarE.Clear();
            }
            if (felvetelizoAdatai.Matematika > 50 || felvetelizoAdatai.Matematika < 0)
            {
                MessageBox.Show("Nem lehet ennyi pontja matematikából a felvételizőnek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                txtMatekE.Clear();
            }
            if (tempString.Length == 11 && tempString.StartsWith("72"))
            {
                this.felvetelizoAdatai.OM_Azonosito = tempString;
            }
            else MessageBox.Show("Nem megfelelő OM Azonosító!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            MessageBox.Show("Az adatok sikeresen rögzítve lettek!", "Sikeres rögzítés", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}