using System;
using System.Numerics;
using System.Windows.Forms;

namespace KAM_Calculator_5._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static void ForOperators2(TextBox textBox, string btnText)
        {
            try
            {
                sbyte koeff1 = 1;
                if ((textBox.Text.IndexOf('+') > 0 || textBox.Text.LastIndexOf('-') > 0 || textBox.Text.IndexOf('*') > 0 || textBox.Text.IndexOf('/') > 0)
                    && (textBox.Text.LastIndexOf('+') != textBox.Text.Length - 1 && textBox.Text.LastIndexOf('-') != textBox.Text.Length - 1 && textBox.Text.LastIndexOf('*') != textBox.Text.Length - 1 && textBox.Text.LastIndexOf('/') != textBox.Text.Length - 1))
                {
                    if (textBox.Text[0] == '-')
                    {
                        koeff1 = -1;
                        textBox.Text = textBox.Text.Substring(1, textBox.Text.Length - 1);
                    }
                    string[] M = textBox.Text.Split(new char[] { '+', '-', '*', '/' });
                    if (koeff1 == -1)
                    {
                        M[0] = "-" + M[0];
                    }

                    if (textBox.Text.IndexOf('+') > 0)
                    {
                        if (M[0].Length <= 300 && M[1].Length <= 300)
                        {
                            textBox.Text = (double.Parse(M[0]) + double.Parse(M[1])).ToString();
                        }
                        else
                        {
                            textBox.Text = (BigInteger.Parse(M[0]) + BigInteger.Parse(M[1])).ToString();
                        }
                    }
                    if (textBox.Text.IndexOf('-') > 0)
                    {
                        if (M[0].Length + M[1].Length <= 300)
                        {
                            textBox.Text = (double.Parse(M[0]) - double.Parse(M[1])).ToString();
                        }
                        else
                        {
                            textBox.Text = (BigInteger.Parse(M[0]) - BigInteger.Parse(M[1])).ToString();
                        }
                    }
                    if (textBox.Text.IndexOf('*') > 0)
                    {
                        if (M[0].Length + M[1].Length <= 300)
                        {
                            textBox.Text = (double.Parse(M[0]) * double.Parse(M[1])).ToString();
                        }
                        else
                        {
                            textBox.Text = (BigInteger.Parse(M[0]) * BigInteger.Parse(M[1])).ToString();
                        }
                    }

                    if (textBox.Text.IndexOf('/') > 0)
                    {
                        if (M[0].Length + M[1].Length <= 300)
                        {
                            textBox.Text = (double.Parse(M[0]) / double.Parse(M[1])).ToString();
                        }
                        else
                        {
                            textBox.Text = (BigInteger.Parse(M[0]) / BigInteger.Parse(M[1])).ToString();
                        }
                    }
                }

                if ((textBox.Text.Length == 0 && btnText == "-") || (textBox.Text[textBox.Text.Length - 1] != '-' || textBox.Text[textBox.Text.Length - 1] != '+' || textBox.Text[textBox.Text.Length - 1] != '*' || textBox.Text[textBox.Text.Length - 1] != '/')
                      && (textBox.Text.LastIndexOf('+') != textBox.Text.Length - 1 && textBox.Text.LastIndexOf('-') != textBox.Text.Length - 1 && textBox.Text.LastIndexOf('*') != textBox.Text.Length - 1 && textBox.Text.LastIndexOf('/') != textBox.Text.Length - 1))
                {
                    textBox.Text += btnText;
                }

            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        #region buttonsClick -> +=Their text
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += button1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += button2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += button3.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += button4.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += button5.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += button6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += button7.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += button8.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += button9.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += button10.Text;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        // "+"
        private void button11_Click(object sender, EventArgs e)
        {
            ForOperators2(textBox1, button11.Text);
        }
        // "-"
        private void button12_Click(object sender, EventArgs e)
        {
            ForOperators2(textBox1, button12.Text);
        }
        // "*"
        private void button13_Click(object sender, EventArgs e)
        {
            ForOperators2(textBox1, button13.Text);
        }
        // "/"
        private void button14_Click(object sender, EventArgs e)
        {
            ForOperators2(textBox1, button14.Text);
        }
        #endregion
        // =
        private void button16_Click(object sender, EventArgs e)
        {
            ForOperators2(textBox1, "");
        }

    }
}
