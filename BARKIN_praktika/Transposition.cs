using System;
using System.Windows;

namespace BARKIN_praktika
{
    public class Transposition // Реализация шифра перестановки происходит в этом классе
    {
        private int[] key = null;  // Массив, содержащий ключ

        //Дальше следуют три версии перегруженного метода SetKey, который сохраняет в экземпляр класса значение ключа
        public void SetKey(int[] _key)   //входным аргументом является массив целых чисел:
        {
            key = new int[_key.Length];

            for (int i = 0; i < _key.Length; i++)
                key[i] = _key[i];
        }

        public void SetKey(string[] _key) //  принимает на вход массив ключа, элементы которого представлены строками
        {
            key = new int[_key.Length];
            for (int i = 0; i < _key.Length; i++)
                key[i] = Convert.ToInt32(_key[i]);
        }

        public void SetKey(string _key) //метод принимает на вход строку, в которой содержится ключ
        {
            SetKey(_key.Split(' '));
        }

        public string Encrypt(string input)//шифровка
        {

            for (int i = 0; i < input.Length % key.Length; i++) // доводим длину входной строки до числа без остатка делящегося на длину ключа
                input += input[i];

            string result = ""; // здесь будет храниться результат, т.е зашифрованный текст

            try
            {
                for (int i = 0; i < input.Length; i += key.Length) // здесь происходит непосредственно шифрование с помощью шифра перестановки
                {
                    char[] transposition = new char[key.Length];

                    for (int j = 0; j < key.Length; j++)   // могут возникнуть ошибки
                        transposition[key[j] - 1] = input[i + j];

                    for (int j = 0; j < key.Length; j++)
                        result += transposition[j];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Входные данные некорректны {ex.Message}","Проверьте корректность введенных данных");
            }
            
            if(key.Length != input.Length) // чтобы не возникало лишних ошибок связанных с ключом и исходным текстом
                MessageBox.Show("кол-во символов ключа и исходного текста должны совпадать !");

            return result;
        }

        // расшифровка
        public string Decrypt(string input) // метод расшифрования по структуре идентичен методу шифрования, за исключением того, что в нем отсутствует код, доводящий входную строку до нужной длины
        {
            string result = "";

            try
            {
                for (int i = 0; i < input.Length; i += key.Length)
                {
                    char[] transposition = new char[key.Length];

                    for (int j = 0; j < key.Length; j++)
                        transposition[j] = input[i + key[j] - 1]; // расшифровка

                    for (int j = 0; j < key.Length; j++)
                        result += transposition[j];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Проверьте корректность введенных данных");
            }

            return result;
        }
    }
}