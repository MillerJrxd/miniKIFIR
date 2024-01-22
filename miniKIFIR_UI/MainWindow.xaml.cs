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
        internal DateTime convertedDate;
        public MainWindow()
        {
            InitializeComponent();
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
                convertedDate = result;
                MessageBox.Show($"Stored Date: {convertedDate.ToString("yyyy/MM/dd")}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                 ToolTip toolTip = new ToolTip();
                {
                    toolTip.Content = "Nem megfelelő dátum!";
                    toolTip.Background = Brushes.LightYellow;
                    toolTip.Foreground = Brushes.Black;
                    toolTip.IsOpen = true;
                    toolTip.StaysOpen = true;
                    toolTip.PlacementTarget = maskedTextBox1;
                    toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
                };
                maskedTextBox1.Clear();
            }
        }
    }
}