using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KAM_TextAnalisator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("" + ((int)'0').ToString());
            //MessageBox.Show(" " + ((int)'1').ToString());
            //MessageBox.Show(" " + ((int)'9').ToString());



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




        List<string> wordsName = new List<string>();
        List<int> words = new List<int>();



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
                #region Variables
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
                // ready
                int wordsCount = 0;
                // ready
                List<string> wordsString = new List<string>();
                // ready
                string longestWord = "";
                // ready
                int wordsHyphenCount = 0;
                // ready
                int numberCount = 0;
                #endregion


                #region  Calculating STRING (Array of strings)
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
                #endregion 


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
                punctuationCount += (tt / 2);
                #endregion


                #region  Amount of WORDS + largestWord + wordsHyphenCount + numberCount
                for (int i = 0; i < STRING.Length; i++)
                {

                    STRING[i] = STRING[i].Replace("  ", " ");
                    STRING[i] = STRING[i].Replace("   ", " ");
                    STRING[i] = STRING[i].Replace("    ", " ");
                    STRING[i] = STRING[i].Replace("     ", " ");
                    STRING[i] = STRING[i].Replace("      ", " ");
                    STRING[i] = STRING[i].Replace("       ", " ");
                    STRING[i] = STRING[i].Replace("        ", " ");
                    STRING[i] = STRING[i].Replace("         ", " ");
                    STRING[i] = STRING[i].Replace("          ", " ");
                    STRING[i] = STRING[i].Replace("           ", " ");
                    string[] MASS = STRING[i].Split(' ');

                    for (int l = 0; l < MASS.Length; l++)
                    {
                        int count = 0;
                        int countNum = 0;
                        for (int m = 0; m < MASS[l].Length; m++)
                        {
                            //MessageBox.Show(MASS[l]);

                            if ((MASS[l][m] >= 97 && MASS[l][m] <= 122) || (MASS[l][m] >= 65 && MASS[l][m] <= 90) || MASS[l][m] == 96 || MASS[l][m] == 39 || MASS[l][m] == 45)
                            {
                                count++;
                            }
                            if (MASS[l][m] >= 48 && MASS[l][m] <= 57)
                            {
                                countNum++;
                            }

                        }
                        if (count == MASS[l].Length && count != 0)
                        {
                            wordsString.Add(MASS[l]);
                            wordsCount++;
                        }
                        if (countNum == MASS[l].Length && countNum != 0)
                        {
                            numberCount++;
                        }

                    }
                }
                //MessageBox.Show(wordsString.Count.ToString());
                //MessageBox.Show("wordsCount"+wordsCount.ToString());
                textBox5.Text = "";
                string maxWord = wordsString[0];
                int maxWordLength = wordsString[0].Length;
                for (int l = 0; l < wordsString.Count; l++)
                {
                    if (wordsString[l].IndexOf("-") > 0)
                    {
                        wordsHyphenCount++;
                    }
                    if (wordsString[l].Length > maxWordLength)
                    {
                        maxWordLength = wordsString[l].Length;
                        longestWord = wordsString[l];

                    }
                    textBox5.Text += $"\"{wordsString[l]}\"\r\n";


                    //// WORDS
                    //words.Add(1);
                    //wordsName.Add(wordsString[l]);
                    ////MessageBox.Show(words.Count.ToString());
                    ////MessageBox.Show(wordsName.Count.ToString());
                    //int COUNT = words.Count;
                    ////for (int i = 0; i < words.Count; i++)
                    //while (COUNT>0)
                    //{
                    //    int i = 0;
                    //    if (wordsString[l] == wordsName[i] && l != 0)
                    //    {
                    //        words[i]++;
                    //    }
                    //    else
                    //    {
                    //        words.Add(1);
                    //        wordsName.Add(wordsString[l]);
                    //    }
                    //    i++;
                    //}

                    //textBox5.Text += $"\r\n";
                    //textBox5.Text += $"\r\n";
                    //for (int i = 0; i < words.Count; i++)
                    //{
                    //    textBox5.Text += $"\"{wordsName[i]}\":\"{words[i]}\", \r\n";
                    //}
                }

                //MessageBox.Show(longestWord);
                //List<string> wordsSTRING = wordsString;
                string[] wordsMASS = new string[wordsString.Count];

                //MessageBox.Show("Count of new List>" + wordsSTRING.Count);

                for (int i = 0; i < wordsString.Count; i++)
                {
                    wordsMASS[i] = wordsString[i];

                }

                MessageBox.Show(wordsMASS.Length.ToString());

                wordsMASS = wordsMASS.Distinct().ToArray();
                int[] wordsFrequency = new int[wordsMASS.Length];

                MessageBox.Show(wordsMASS.Length.ToString());
                for (int i = 0; i < wordsMASS.Length; i++)
                {
                    for (int j = 0; j < wordsString.Count; j++)
                    {
                        if (wordsMASS[i] == wordsString[j])
                        {
                            wordsFrequency[i]++;
                        }
                    }

                    textBox5.Text += $"\"{wordsMASS[i]}\":\"{wordsFrequency[i]}\", \r\n";
                }

                #endregion







                textBox3.Text = "";
                textBox3.Text +=
                     "{\r\n" +
                      $"\"fileName\":\"{fileName}\", \r\n" +
                      $"\"fileSize\":\"{fileSize}\", \r\n" +
                      $"\"lettersCount\":\"{lettersCount}\", \r\n" +
                      $"\"numeralCount\":\"{numeralCount}\", \r\n" +
                      $"\"numberCount\":\"{numberCount}\", \r\n" +
                      $"\"wordsCount\":\"{wordsCount}\", \r\n" +
                      $"\"linesCount\":\"{linesCount}\", \r\n" +
                      $"\"wordsHyphenCount\":\"{wordsHyphenCount}\", \r\n" +
                      $"\"punctuationCount\":\"{punctuationCount}\", \r\n" +
                      $"\"longestWord\":\"{longestWord}\", \r\n";

                textBox3.Text += "\"letters\":{ \r\n";

                for (int i = 0; i < letters.Length; i++)
                {
                    if (i == letters.Length - 1)
                    {
                        textBox3.Text += $"\"{(char)lettersChar[i]}\":\"{letters[i]}\" \r\n";
                        textBox3.Text += "},\r\n";
                    }
                    else
                    {
                        if ((char)lettersChar[i] == 'A')
                        {
                            textBox3.Text += $"{(char)lettersChar[i]}\":\"{letters[i]}\", \r\n";
                        }
                        else
                        {
                            textBox3.Text += $"\"{(char)lettersChar[i]}\":\"{letters[i]}\", \r\n";
                        }
                    }

                }

                textBox3.Text += "\"words\":{ \r\n";
                for (int i = 0; i < wordsMASS.Length; i++)
                {
                    if (i == wordsMASS.Length - 1)
                    {
                        textBox3.Text += $"\"{wordsMASS[i]}\":\"{wordsFrequency[i]}\" \r\n";
                        textBox3.Text += "} \r\n}";
                    }
                    else
                    {
                        textBox3.Text += $"\"{wordsMASS[i]}\":\"{wordsFrequency[i]}\", \r\n";
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
