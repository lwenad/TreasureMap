using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace TreasureMap
{
    public partial class Form1 : Form
    {

        List<Image> ListErrorImage = new List<Image>();
        Image TreasureMapImage = Image.FromFile("C:\\Users\\Lei\\Pictures\\treasure_map.png");
        private static readonly string Password = "1983creamer";
        private static int _errorCounter = 0;
        private static DateTime? LastClickTime = null;
        private static int WaitInSecond = 0;
        private static readonly string FailedAttmepsText = "Failed attempts {0}";
        private static SoundPlayer SoundPlayer = new SoundPlayer();
        public Form1()
        {
            InitializeComponent();

            ListErrorImage.Add(Image.FromFile("C:\\Users\\Lei\\Pictures\\ErrorImage1.PNG"));
            ListErrorImage.Add(Image.FromFile("C:\\Users\\Lei\\Pictures\\ErrorImage2.PNG"));
            ListErrorImage.Add(Image.FromFile("C:\\Users\\Lei\\Pictures\\ErrorImage3.PNG"));
            ListErrorImage.Add(Image.FromFile("C:\\Users\\Lei\\Pictures\\ErrorImage4.PNG"));
            ListErrorImage.Add(Image.FromFile("C:\\Users\\Lei\\Pictures\\ErrorImage5.jpg"));


            label2.Text = FailedAttmepsText.Replace("{0}", "0");
            maskedTextBox1.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if(LastClickTime == null)
            {
                LastClickTime = DateTime.Now;
            }
            else
            {
                if(DateTime.Now.AddSeconds(-1* WaitInSecond) < LastClickTime)
                {
                    MessageBox.Show($"too fast, have to wait at least {WaitInSecond} seconds to enter password again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    LastClickTime = DateTime.Now;

                }
            }

            try
            {
                if (string.IsNullOrEmpty(maskedTextBox1.Text) || maskedTextBox1.Text.ToLower() != Password)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

                    Random random = new Random();
                    int randomNumber = random.Next(0, 100);
                    int index = randomNumber % ListErrorImage.Count;
                    pictureBox1.Image = ListErrorImage[index];
                    _errorCounter++;

                    label2.Text = FailedAttmepsText.Replace("{0}", _errorCounter.ToString());

                    if (_errorCounter == 10 )
                    {
                        HintButton.Visible = true;
                    }
                }
                else
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.Image = TreasureMapImage;
                }
            }
             catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void HintButton_Click(object sender, EventArgs e)
        {
            string hint1 = "hint1: the last name is very interesting ...";
            string hint2 = "hint2: English is the second language, how should i spelling the last name ? ";
            string hint3 = "hint3  I need to add more creamer for the coffee.";

            string result = null;
            if (_errorCounter >= 10)
            {
                result = hint1;
            }

            if (_errorCounter > 15)
            {
                result = result+ "\n" + hint2;
            }
            if (_errorCounter > 20)
            {
                result = result + "\n" + hint3;
            }

            MessageBox.Show(result);
        }

        private void CheckBoxPassword_Click(object sender, EventArgs e)
        {

            if(CheckBoxPassword.Checked)
            {
                maskedTextBox1.PasswordChar = '\0';
            }
            else
            {
                maskedTextBox1.PasswordChar = '*';
            }
        }
    }
}
