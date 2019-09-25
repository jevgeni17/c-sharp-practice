using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BARKIN_praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transposition t;
        public MainWindow()
        {
            InitializeComponent();

            t = new Transposition();

        }

        private void startButton_Click(object sender, RoutedEventArgs e) // проверка входных данных
        {

            if (inputTextBox.Text == "" || keyTextBox.Text =="" || encryptRadioButton.IsChecked != true && decryptRadioButton.IsChecked !=true )
            {
                MessageBox.Show("Заполните все поля ввода");
            }
            else
            { 
                t.SetKey(keyTextBox.Text);

                if (encryptRadioButton.IsChecked == true)
                    outputTextBox.Text = t.Encrypt(inputTextBox.Text);
                else
                    outputTextBox.Text = t.Decrypt(inputTextBox.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // кнопка инфо открывает текстовик с информацией об использовании
        {
            string path = (@"swap_info.txt");
            Process.Start(path);
        }

        private void keyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // чтобы в поле ключа можно было вводить только цифры, и чтобы они записывались через пробел
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
            string[] split = keyTextBox.Text.Split(' ');
            if (split[split.Length - 1].Length == 1)
            {
                keyTextBox.Text += " ";
                keyTextBox.Select(keyTextBox.Text.Length, 0);
            }
        }

        private void keyTextBox_TextChanged_1(object sender, TextChangedEventArgs e) // проверка на пустоту
        {
            if (keyTextBox.Text == "")
                MessageBox.Show("Заполните все поля");
        }

        private void keyTextBox_PreviewKeyDown(object sender, KeyEventArgs e) // запрет на ввод пробела
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
