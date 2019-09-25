using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace BARKIN_praktika
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // кнопка, которая ведет на форму шифра перестановки
        {
            MainWindow swap = new MainWindow();
            swap.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // кнопка, которая ведет на форму шифра Виженера
        {
            Vizhener vizhener = new Vizhener();
            vizhener.Show();
            MessageBox.Show("Введите слово, которое хотите зашифровать в текстовой документ и сохраните его");
            string path = (@"type_word.txt");
            Process.Start(path);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // кнопка, которая ведет на форму RSA шифра
        {
            RSA rsa = new RSA();
            rsa.Show();
            MessageBox.Show("Введите слово, которое хотите зашифровать в текстовой документ и сохраните его");
            string path = (@"in.txt");
            Process.Start(path);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //событие для перемещения начальной формы "Menu" без Title Bar
        {
            this.DragMove();
        }

        private void MyFlatImageButton_Click(object sender, RoutedEventArgs e) // Кнопка выхода из программы
        {
            this.Close();
        }
    }
}
