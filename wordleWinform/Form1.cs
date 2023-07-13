using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wordleWinform
{
    public partial class Form1 : Form
    {


        public class Word
        {
            public string word { get; set; }
        }

        public string GetRandomFiveLetterWord()
        {
            string url = "https://api.datamuse.com/words?sp=?????";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(url).Result;
            JArray jsonArray = JArray.Parse(response);
            string randomWord = jsonArray[new Random().Next(0, jsonArray.Count)]["word"].ToString().ToLower();
            return randomWord;
        }


        int attemptIndex = 0;
        bool allowSubmit = false;
        List<RichTextBox> richTextBoxList;
        string answer;

        public Form1()
        {
            InitializeComponent();

            richTextBoxList = new List<RichTextBox> { richTextBox1, richTextBox2, richTextBox3, richTextBox4, richTextBox5, richTextBox6 };

            foreach (RichTextBox richTextBox in richTextBoxList)
            {
                richTextBox.TextChanged += RichTextBox_TextChanged;
                richTextBox.MaxLength = 5;
                richTextBox.Multiline = false;
            }
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            var currentRichTextBox = (RichTextBox)sender;

            if (currentRichTextBox.Text.Length < 5)
            {
                allowSubmit = false;
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            answer = GetRandomFiveLetterWord();
            //MessageBox.Show(answer);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string attempt = richTextBoxList[attemptIndex].Text.ToLower();
            RichTextBox currRichTextBox = richTextBoxList[attemptIndex];

            if (attempt.Length != 5)
            {
                MessageBox.Show("Please enter a 5-letter word.");
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                if (attempt[i] == answer[i])
                {
                    currRichTextBox.Select(i, 1);
                    currRichTextBox.SelectionColor = Color.Green;
                }
                else if (answer.Contains(attempt[i]))
                {
                    currRichTextBox.Select(i, 1);
                    currRichTextBox.SelectionColor = Color.Orange;
                }
                else
                {
                    currRichTextBox.Select(i, 1);
                    currRichTextBox.SelectionColor = Color.Red;
                }
            }

            currRichTextBox.DeselectAll();

            attemptIndex++;
            if (attemptIndex >= richTextBoxList.Count)
            {
                button1.Enabled = false;
                MessageBox.Show($"You have completed all attempts.\nThe correct answer is {answer}");
            }
            else
            {
                richTextBoxList[attemptIndex].Enabled = true;
                richTextBoxList[attemptIndex -  1].Enabled = false;
                richTextBoxList[attemptIndex].Focus();
            }
        }
    }
}
