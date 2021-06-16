using System;
using System.IO;
using System.Windows.Forms;

namespace KAM_TextAnalisator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("." + ((int)'.').ToString());
            MessageBox.Show("," + ((int)',').ToString());
            MessageBox.Show(":" + ((int)':').ToString());
            MessageBox.Show(";" + ((int)';').ToString());
            MessageBox.Show("\"" + ((int)'\"').ToString());


            //            textBox4.Text = "Прочитать текстовый файл (который может быть больших размеров), посчитать в нем:"+
            //"-количество букв всего; количества каждой буквы" +
            //"- количество цифр; количество чисел" +
            //"- количество слов всего; количества каждого слова" +
            //"- количество строк" +
            //"- количество слов с дефисом" +
            //"- количество знаков препинания"+
            //"- самое длинное слово" +
            //  "Сохранить вычисления в файле с именем: < имя исходного файла>.json в формате JSON.Причем результаты должны быть отсортированы по количеству в убывающем порядке.";
        }


        // ready
        string fileName = "";




        int NumberCount = 0;

        int wordsCount = 0;
        string[] wordsString = new string[26];
        int[] words = new int[26];

        int wordsHyphenCount = 0;

        int longestWord = 0;


        /// <summary>
        /// Open needed file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            //// читаем файл в строку
            //string fileText = System.IO.File.ReadAllText(filename);

            textBox1.Text = openFileDialog1.FileName;
            textBox2.Text = System.IO.File.ReadAllText(filename);


            fileName = openFileDialog1.SafeFileName;

        }




        /// <summary>
        /// Wtite into needed file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, textBox3.Text);

        }




        /// <summary>
        ///  Calculating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // ready
                int linesCount = 0;
                // ready
                string filePath = "";
                // ready
                int fileSize = 0;
                // ready
                int numeralCount = 0;
                // ready
                int lettersCount = 0;
                // ready
                int[] lettersChar = new int[52];
                // ready
                int[] letters = new int[52];
                // ready
                int punctuationCount = 0;


                lettersCount = 0;
                filePath = textBox1.Text;
                linesCount = System.IO.File.ReadAllLines(filePath).Length;
                string[] STRING = new string[linesCount];
                textBox2.Text = System.IO.File.ReadAllText(filePath);


                StreamReader sr = new StreamReader(filePath);
                {
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        STRING[i] += sr.ReadLine();
                        i++;
                    }
                }



                #region  Amount of letter and numeral (English)
                for (int i = 0; i < STRING.Length; i++)
                {
                    fileSize += STRING[i].Length;
                    for (int j = 0; j < STRING[i].Length; j++)
                    {
                        if ((STRING[i][j] >= 97 && STRING[i][j] <= 122) || (STRING[i][j] >= 65 && STRING[i][j] <= 90))
                        {
                            lettersCount++;
                        }
                        if (STRING[i][j] >= 48 && STRING[i][j] <= 57)
                        {
                            numeralCount++;
                        }
                    }
                }
                #endregion


                #region  Amount of every letter
                for (int i = 0; i < letters.Length; i++)
                {
                    if (i < 26)
                    {
                        lettersChar[i] = i + 97;
                    }
                    if (i > 26)
                    {
                        lettersChar[i] = i + 38;
                    }

                }


                for (int i = 0; i < STRING.Length; i++)
                {

                    for (int j = 0; j < STRING[i].Length; j++)
                    {
                        for (int k = 0; k < letters.Length; k++)
                        {
                            //MessageBox.Show($"{STRING[i][j]} and {(char)(lettersChar[k])}");
                            if (STRING[i][j] == (char)(lettersChar[k]))
                            {
                                letters[k]++;
                            }
                        }
                    }
                }

                textBox5.Text = "";
                for (int i = 0; i < letters.Length; i++)
                {
                    textBox5.Text += (char)lettersChar[i] + "=" + letters[i] + "\r\n";

                }

                #endregion


                #region  Amount of PUNCTUATION
                int tt = 0;
                for (int i = 0; i < STRING.Length; i++)
                {
                    for (int j = 0; j < STRING[i].Length; j++)
                    {
                        if (STRING[i][j] == 46 || STRING[i][j] == 44 || STRING[i][j] == 58 || STRING[i][j] == 59 || STRING[i][j] == 34)
                        {
                            if (STRING[i][j] == 34)
                            {
                                tt++;
                            }
                            else
                            {
                                punctuationCount++;
                            }
                        }
                    }
                }
                punctuationCount +=  (tt/2);
                #endregion




                textBox3.Text = "";
                textBox3.Text +=
                     "{\r\n" +
                      $"\"fileName\":\"{fileName}\", \r\n" +
                      $"\"fileSize\":\"{fileSize}\", \r\n" +
                      $"\"lettersCount\":\"{lettersCount}\", \r\n" +
                      $"\"numeralCount\":\"{numeralCount}\", \r\n" +
                      $"\"NumberCount\":\"{NumberCount}\", \r\n" +
                      $"\"wordsCount\":\"{wordsCount}\", \r\n" +
                      $"\"linesCount\":\"{linesCount}\", \r\n" +
                      $"\"wordsHyphenCount\":\"{wordsHyphenCount}\", \r\n" +
                      $"\"punctuationCount\":\"{punctuationCount}\", \r\n" +
                      $"\"longestWord\":\"{longestWord}\", \r\n" +

                            "words {\r\n" +
                                 $"\"a\":\"{23}\" \r\n" +
                                 $"\"b\":\"{25}\" \r\n" +
                            "}, \r\n";
                textBox3.Text += "letters { \r\n";

                for (int i = 0; i < letters.Length; i++)
                {
                    textBox3.Text += $"\"{(char)lettersChar[i]}\":\"{letters[i]}\", \r\n";
                }
                textBox3.Text += "},\r\n";

                //for (int i = 0; i < words.Length; i++)
                //{
                //    textBox3.Text += $"\"a:\"{words[i]}";
                //}
                //textBox3.Text += "}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
