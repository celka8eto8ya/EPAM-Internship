using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KAM_TextAnalisator_2._0
{

    public class Output
    {
        public string longestWord { get; set; }   //
        public string fileName { get; set; }      //
        public Dictionary<string, int> counts { get; set; } //
        public Dictionary<char, int> letters { get; set; } //
        public Dictionary<string, int> words { get; set; } //
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Input path to file, please:");
                string pathOpen = Console.ReadLine();

                var OUTPUT = new Output();
                OUTPUT.letters = new Dictionary<char, int>();
                OUTPUT.words = new Dictionary<string, int>();
                OUTPUT.counts = new Dictionary<string, int>();

                OUTPUT.counts.Add("linesCount", System.IO.File.ReadAllLines(pathOpen).Length);
                OUTPUT.fileName = System.IO.Path.GetFileName(pathOpen);

                for (int i = 0; i < 52; i++)
                {
                    if (i < 26)
                    {
                        OUTPUT.letters.Add(Convert.ToChar(i + 97), 0);
                    }
                    if (i >= 26)
                    {
                        OUTPUT.letters.Add(Convert.ToChar(i + 39), 0);
                    }
                }

                List<string> words = new List<string>();
                OUTPUT.counts.Add("fileSize", 0);
                OUTPUT.counts.Add("lettersCount", 0);
                OUTPUT.counts.Add("numeralCount", 0);
                OUTPUT.counts.Add("wordsHyphenCount", 0);
                OUTPUT.counts.Add("numberCount", 0);
                OUTPUT.counts.Add("wordsCount", 0);
                OUTPUT.counts.Add("punctuationCount", 0);
                
                using (StreamReader sr = new StreamReader(pathOpen))
                {
                    OUTPUT.longestWord = "";
                    while (!sr.EndOfStream)
                    {
                        string S = sr.ReadLine();
                        OUTPUT.counts["fileSize"] += S.Length;

                        string[] Mass = S.Split(' ');
                        for (int i = 0; i < Mass.Length; i++)
                        {
                            int countWord = 0;
                            int countNumber = 0;
                            for (int j = 0; j < Mass[i].Length; j++)
                            {
                                if ((Mass[i][j] >= 97 && Mass[i][j] <= 122) || (Mass[i][j] >= 65 && Mass[i][j] <= 90) || Mass[i][j] == 96 || Mass[i][j] == 39 || Mass[i][j] == 45)
                                {
                                    countWord++;
                                }
                                if ((Mass[i][j] >= 97 && Mass[i][j] <= 122) || (Mass[i][j] >= 65 && Mass[i][j] <= 90))
                                {
                                    OUTPUT.counts["lettersCount"] += 1;
                                    if (OUTPUT.letters[Mass[i][j]] >= 0)
                                    {
                                        OUTPUT.letters[Mass[i][j]] += 1;
                                    }
                                }
                                if (Mass[i][j] >= 48 && Mass[i][j] <= 57)
                                {
                                    OUTPUT.counts["numeralCount"] += 1;
                                    countNumber++;
                                }
                                if (Mass[i][j] == 46 || Mass[i][j] == 44 || Mass[i][j] == 58 || Mass[i][j] == 59 || Mass[i][j] == 34)
                                {
                                    OUTPUT.counts["punctuationCount"] += 1;
                                }
                            }
                            if ((countWord == Mass[i].Length) && countWord != 0)
                            {
                                OUTPUT.counts["wordsCount"] += 1;
                                if (Mass[i].IndexOf('-') > 0)
                                {
                                    OUTPUT.counts["wordsHyphenCount"] += 1;
                                }
                                if (Mass[i].Length > OUTPUT.longestWord.Length)
                                {
                                    OUTPUT.longestWord = Mass[i];
                                }
                                words.Add(Mass[i]);
                            }
                            if ((countNumber == Mass[i].Length) && countNumber != 0)
                            {
                                OUTPUT.counts["numberCount"] += 1;
                            }
                        }
                    }
                }

                string[] wordsM = new string[words.Count];
                for (int i = 0; i < words.Count; i++)
                {
                    wordsM[i] = words[i];
                }
                wordsM = wordsM.Distinct().ToArray();
                for (int i = 0; i < words.Count; i++)
                {
                    if (i < wordsM.Length)
                    { OUTPUT.words.Add(wordsM[i],0);}
                    if (OUTPUT.words[words[i]] >= 0)
                    {
                        OUTPUT.words[words[i]]+= 1;
                    }
                }

                // SORTING OUTPUT
                OUTPUT.letters = OUTPUT.letters.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                OUTPUT.words = OUTPUT.words.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                OUTPUT.counts = OUTPUT.counts.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

                // Output at the console value "OUTPUT" // DELETE AFTER
                var json = JsonConvert.SerializeObject(OUTPUT);
                Console.WriteLine(json);
                Console.WriteLine();
                //

                Console.Write("Input path for saving results in file (.json):");
                File.WriteAllText(Console.ReadLine(), JsonConvert.SerializeObject(OUTPUT));
                Console.WriteLine($"Data has saved in chose file.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }
    }
}
