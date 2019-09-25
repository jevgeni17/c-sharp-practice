using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BARKIN_praktika //// Реализация шифра Виженера
{
    /// <summary>
    /// Interaction logic for Vizhener.xaml
    /// </summary>
    public partial class Vizhener : Window
    {
        public Vizhener()
        {
            InitializeComponent();
            N = characters.Length;
        }

        char[] characters = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                        'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' }; // алфавит

        private int N; //длина алфавита

        private void buttonEncrypt_Click_1(object sender, RoutedEventArgs e) // Шифровка
        {
            string pathq;
            if (radioButtonGamma.IsChecked == true) // шифровка гаммированием
            {
                string s;

                StreamReader sr = new StreamReader("type_word.txt");
                StreamWriter sw = new StreamWriter("out.txt");

                while (!sr.EndOfStream) // пока текущая позиция потока не находится в конце потока выполняется цикл
                {
                    s = sr.ReadLine(); // переменной присваиваются значения присвоенные пользователем
                    sw.WriteLine(Encode(s, Generate_Pseudorandom_KeyWord(s.Length, 100))); 
                }

                sr.Close();
                sw.Close();

                pathq = (@"out.txt");
                Process.Start(pathq);
                MessageBox.Show("Ваше зашифрованное слово появилось в текстовом документе");

            }
            else 
            {
                if (textBoxKeyWord.Text.Length > 0) // шифровка Виженера
                {
                    string s;

                    StreamReader sr = new StreamReader("type_word.txt");
                    StreamWriter sw = new StreamWriter("out.txt");

                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        sw.WriteLine(Encode(s, textBoxKeyWord.Text));
                    }

                    sr.Close();
                    sw.Close();

                    pathq = (@"out.txt");
                    Process.Start(pathq);
                    MessageBox.Show("Ваше зашифрованное слово появилось в текстовом документе");
                }
                else
                    MessageBox.Show("Введите ключевое слово!");
            } 
        }

        private void buttonDecipher_Click_1(object sender, RoutedEventArgs e) //Расшифровка
        {
            string pathq;

            if (radioButtonGamma.IsChecked == true) // расшифровка гаммированием
            {
                string s;

                StreamReader sr = new StreamReader("out.txt");
                StreamWriter sw = new StreamWriter("type_word.txt");

                while (!sr.EndOfStream) 
                {
                    s = sr.ReadLine();
                    sw.WriteLine(Decode(s, Generate_Pseudorandom_KeyWord(s.Length, 100)));
                }

                sr.Close();
                sw.Close();

                pathq = (@"type_word.txt");
                Process.Start(pathq);
                MessageBox.Show("Ваше расшифрованное слово появилось в текстовом документе");
            }
            else
            { 
                if (textBoxKeyWord.Text.Length > 0) // расшифровка Виженера
                {
                    string s;


                    StreamReader sr = new StreamReader("out.txt");
                    StreamWriter sw = new StreamWriter("type_word.txt");

                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        sw.WriteLine(Decode(s, textBoxKeyWord.Text));
                    }

                    sr.Close();
                    sw.Close();
                    pathq = (@"type_word.txt");
                    Process.Start(pathq);
                    MessageBox.Show("Ваше расшифрованное слово появилось в текстовом документе");
                }
                else
                    MessageBox.Show("Введите ключевое слово!");
            }
        }

        private string Encode(string input, string keyword) // Метод шифрования
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;

            try
            {
                foreach (char symbol in input)
                {
                    int c = (Array.IndexOf(characters, symbol) +
                        Array.IndexOf(characters, keyword[keyword_index])) % N;

                    result += characters[c];

                    keyword_index++;

                    if ((keyword_index + 1) == keyword.Length)
                        keyword_index = 0;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Входные данные некорректны {ex.Message}", "Проверьте корректность введенных данных");
            }

            return result;
        }

        private string Decode(string input, string keyword) // Метод расшифровки
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();

            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                int p = (Array.IndexOf(characters, symbol) + N -
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                result += characters[p];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }

            return result;
        }

        private string Generate_Pseudorandom_KeyWord(int lenght, int startSeed) // для гаммирования понадобится генерирование псевдослучайного ключа
        {
            Random rand = new Random(startSeed);

            string result = "";

            for (int i = 0; i < lenght; i++)
                result += characters[rand.Next(0, characters.Length)];

            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = (@"vizhener_info.txt");
            Process.Start(path);
        }

        private void radioButtonGamma_Checked(object sender, RoutedEventArgs e)
        {
            textBoxKeyWord.IsEnabled = false;
        }

        private void radioButtonVizhener_Checked(object sender, RoutedEventArgs e)
        {
            textBoxKeyWord.IsEnabled = true;
        }

    }
}
