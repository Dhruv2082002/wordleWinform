using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace wordleWinform
{
    public partial class Form1 : Form
    {
        List<string> fiveLetterWords = new List<string>
         {
    "apple", "brave", "cloud", "dough", "early", "flame", "grape", "happy", "igloo", "jelly",
    "karma", "lemon", "mango", "nerve", "olive", "pizza", "queen", "raise", "sweep", "table",
    "unzip", "vital", "worry", "xerox", "youth", "zebra", "abuse", "baker", "cabin", "daisy",
    "eager", "fairy", "globe", "habit", "icing", "jumbo", "kayak", "leash", "magic", "noble",
    "ocean", "panda", "quilt", "radio", "silly", "tiger", "uncle", "vivid", "waltz", "xenon",
    "yacht", "zealous","after", "black", "charm", "dance", "evoke", "fable", "glass", "haste",
    "ideal", "jolly", "knots", "lunar", "march", "nymph", "oasis", "prize", "quest", "rider",
    "sauce", "tango", "under", "viper", "wheat", "xenon", "youth", "zebra", "adapt", "blaze",
    "chaos", "drown", "eagle", "flair", "grace", "heart", "image", "joker", "kings", "lucky",
    "merry", "noble", "opera", "proud", "queen", "risky", "swift", "trick", "unity", "value",
    "witty", "xerox", "yummy", "zebra", "angry", "basic", "crazy", "dream", "early", "fancy",
    "great", "happy", "ideal", "jolly", "known", "loved", "major", "noble", "opera", "proud",
    "quiet", "ready", "sunny", "truly", "upset", "vivid", "witty", "xenon", "youth", "zebra"
        };

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
            Random random = new Random();
            int randomIndex = random.Next(0, fiveLetterWords.Count());
            answer = fiveLetterWords[randomIndex];
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
