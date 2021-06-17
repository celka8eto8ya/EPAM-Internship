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
        }

        // ready
        string fileName = "";

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
                    if (i >= 26)
                    {
                        lettersChar[i] = i + 39;
                        MessageBox.Show(lettersChar[i]+"="+(int)lettersChar[i]+" "+ (char)lettersChar[i]);
                    }
                }

                for (int i = 0; i < STRING.Length; i++)
                {
                    for (int j = 0; j < STRING[i].Length; j++)
                    {
                        for (int k = 0; k < letters.Length; k++)
                        {
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
                }

                string[] wordsMASS = new string[wordsString.Count];

                for (int i = 0; i < wordsString.Count; i++)
                {
                    wordsMASS[i] = wordsString[i];
                }

                wordsMASS = wordsMASS.Distinct().ToArray();
                int[] wordsFrequency = new int[wordsMASS.Length];

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


                #region OUTPUT

                // UNcomment 
                textBox3.Text = "";
                textBox3.Text +=
                     "{\r\n";
                if (fileName.Length > longestWord.Length)
                {
                    textBox3.Text += $"\"fileName\":\"{fileName}\", \r\n" +
                    $"\"longestWord\":\"{longestWord}\", \r\n";
                }
                else
                {
                    textBox3.Text += $"\"longestWord\":\"{longestWord}\", \r\n" +
                       $"\"fileName\":\"{fileName}\", \r\n";
                }


                // sort VARIABLES
                int[] massSORT = { fileSize, lettersCount, numeralCount, numberCount, wordsCount, linesCount, wordsHyphenCount, punctuationCount };

                string[] massSORTstr = { $"fileSize={fileSize}", $"lettersCount={lettersCount}", $"numeralCount={numeralCount}", $"numberCount={numberCount}", $"wordsCount={wordsCount}", $"linesCount={linesCount}", $"wordsHyphenCount={wordsHyphenCount}", $"punctuationCount={punctuationCount}" };
                for (int i = 0; i < massSORT.Length; i++)
                {
                    textBox5.Text += $"{massSORT[i]} >> {massSORTstr[i]} \r\n";
                }
                Array.Sort(massSORT);
                Array.Reverse(massSORT);

                for (int i = 0; i < massSORT.Length; i++)
                {
                    for (int j = 0; j < massSORTstr.Length; j++)
                    {
                        if (massSORTstr[j].Length > 0)
                        {
                            string subString = massSORTstr[j].Substring(massSORTstr[j].IndexOf("=") + 1, massSORTstr[j].Length - massSORTstr[j].IndexOf("=") - 1);
                            if (massSORT[i] == int.Parse(subString))
                            {
                                string subString1 = massSORTstr[j].Substring(0, massSORTstr[j].IndexOf("="));
                                textBox3.Text += $"\"{subString1}\":\"{massSORT[i]}\", \r\n";
                                massSORTstr[j] = "";
                            }
                        }
                    }
                }


                // sort LETTERS
                textBox3.Text += "\"letters\":{ \r\n";

                string[] NEWlettersChar = new string[lettersChar.Length];
                for (int i = 0; i < lettersChar.Length; i++)
                {
                    NEWlettersChar[i] = $"{lettersChar[i]}={letters[i]}";
                }

                int kk = 0;
                Array.Sort(letters);
                Array.Reverse(letters);
                for (int i = 0; i < letters.Length; i++)
                {
                    for (int j = 0; j < letters.Length; j++)
                    {
                        if (NEWlettersChar[j].Length > 0)
                        {
                            string subString = NEWlettersChar[j].Substring(NEWlettersChar[j].IndexOf("=") + 1, NEWlettersChar[j].Length - NEWlettersChar[j].IndexOf("=") - 1);
                            if (letters[i] == int.Parse(subString))
                            {
                               
                                string subString1 = NEWlettersChar[j].Substring(0, NEWlettersChar[j].IndexOf("="));
                                //MessageBox.Show(i.ToString()+""+ (letters.Length - 1).ToString());
                                if (kk == letters.Length - 1)
                                {
                                    textBox3.Text += $"\"{(char)(int.Parse(subString1))}\":\"{letters[i]}\" \r\n";
                                    //MessageBox.Show(i.ToString());
                                }
                                else
                                {
                                    textBox3.Text += $"\"{(char)(int.Parse(subString1))}\":\"{letters[i]}\", \r\n";
                                }
                                NEWlettersChar[j] = "";
                                kk++;

                            }
                        }
                    }
                }
                textBox3.Text += "},\r\n";



                // sort WORDS
                textBox3.Text += "\"words\":{ \r\n";

                string[] NEWwordsMASS = new string[wordsMASS.Length];
                for (int i = 0; i < wordsMASS.Length; i++)
                {
                    NEWwordsMASS[i] = $"{wordsMASS[i]}={wordsFrequency[i]}";
                }

                int ll = 0;
                Array.Sort(wordsFrequency);
                Array.Reverse(wordsFrequency);
               
                for (int i = 0; i < wordsMASS.Length; i++)
                {
                    for (int j = 0; j < wordsMASS.Length; j++)
                    {
                        if (NEWwordsMASS[j].Length > 0)
                        {
                            string subString = NEWwordsMASS[j].Substring(NEWwordsMASS[j].IndexOf("=") + 1, NEWwordsMASS[j].Length - NEWwordsMASS[j].IndexOf("=") - 1);
                            if (wordsFrequency[i] == int.Parse(subString))
                            {
                                string subString1 = NEWwordsMASS[j].Substring(0, NEWwordsMASS[j].IndexOf("="));
                                if (ll == wordsMASS.Length - 1)
                                {
                                    textBox3.Text += $"\"{subString1}\":\"{wordsFrequency[i]}\" \r\n";
                                }
                                else
                                {
                                    textBox3.Text += $"\"{subString1}\":\"{wordsFrequency[i]}\", \r\n";
                                }
                                NEWwordsMASS[j] = "";
                                ll++;
                            }
                        }
                    }
                }
                textBox3.Text += "} \r\n}";

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
