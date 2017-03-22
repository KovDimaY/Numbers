using System;
using System.Drawing;
using System.Windows.Forms;

namespace Numbers
{
    public partial class Player : Form
    {
        int[] key = new int[4];
        int[] guess = new int[4];
        int turns;

        public Player()
        {
            InitializeComponent();
            this.addNumbers(this.comboBox1);
            this.addNumbers(this.comboBox2);
            this.addNumbers(this.comboBox3);
            this.addNumbers(this.comboBox4);
            this.createKey();
            this.turns = 0;
        }

        //function that checks if all comboBoxes are initiated with some value
        private bool allBoxesNotNull()
        {
            bool result = true;
            if (this.comboBox1.SelectedItem == null) return false;
            if (this.comboBox2.SelectedItem == null) return false;
            if (this.comboBox3.SelectedItem == null) return false;
            if (this.comboBox4.SelectedItem == null) return false;
            return result;
        }

        //this function fills the comboBoxes
        private void addNumbers(System.Windows.Forms.ComboBox box)
        {
            for (int i = 0; i < 9; i++)
            {
                box.Items.Add(i + 1);
            }
        }

        //this function prints a number in a needed form
        private String printNumber(int[] number)
        {
            String result = "";
            for (int i = 0; i < number.Length; i++)
            {
                result += number[i];
            }

            return result;
        }

        //this function checks if all digits in a number are distinct
        private bool allDifferent(int[] number)
        {
            bool result = true;
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < number.Length; j++)
                {
                    if (i != j && number[i] == number[j]) return false;
                }
            }
            return result;
        }

        //function that takes a guess of the player
        private void takeGuess()
        {
            this.guess[0] = (int)this.comboBox1.SelectedItem;
            this.guess[1] = (int)this.comboBox2.SelectedItem;
            this.guess[2] = (int)this.comboBox3.SelectedItem;
            this.guess[3] = (int)this.comboBox4.SelectedItem;
        }

        //this function make a secret number of the computer to initialize a game
        private void createKey()
        {
            Random rand = new Random();
            for (int i = 0; i < this.key.Length; i++)
            {
                this.key[i] = 0;
            }
            while (!this.allDifferent(this.key))
            {
                for (int i = 0; i < this.key.Length; i++)
                {
                    this.key[i] = rand.Next(9)+1;
                }
            }
        }

        //function that compare two numbers and return the firs hint
        private int firstComparison()
        {
            int result = 0;
            for (int i = 0; i < this.key.Length; i++)
            {
                for (int j = 0; j < this.guess.Length; j++)
                {
                    if (this.key[i] == this.guess[j]) result++;                    
                }
            }
            return result;
        }

        //function that compare two numbers and return the second hint
        private int secondComparison()
        {
            int result = 0;
            for (int i = 0; i < this.guess.Length; i++)
            {
                if (this.key[i] == this.guess[i]) result++;
            }
            return result;
        }

        //function that do all the routine when game is over
        private void win()
        {
            //disable all buttons
            this.button1.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;
            this.comboBox3.Enabled = false;
            this.comboBox4.Enabled = false;

            //print a congratulations
            this.richTextBox1.ForeColor = Color.Green;
            this.richTextBox1.Text = "          Congratulations!\n";
            this.richTextBox1.Text += "  You have won the game!!!\n";
            this.richTextBox1.Text += "    Correct number is  ";
            this.richTextBox1.Text += this.printNumber(this.guess);
            this.richTextBox1.Text += "\n     You did it with ";
            this.richTextBox1.Text += this.turns;
            if (this.turns==1) this.richTextBox1.Text += " step.";
            else this.richTextBox1.Text += " steps.";
        }

        //function to print the result of the guess of the player
        private void printResults()
        {
            //format: 1234   ->   (2 - 1)
            this.richTextBox1.Text += this.printNumber(this.guess);
            this.richTextBox1.Text += "   ->   (";
            this.richTextBox1.Text += this.firstComparison();
            this.richTextBox1.Text += " - ";
            this.richTextBox1.Text += this.secondComparison();
            this.richTextBox1.Text += ")\n";
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to evoid errores with null exeptions
            if (this.allBoxesNotNull()) this.button1.Enabled = true; 
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to evoid errores with null exeptions
            if (this.allBoxesNotNull()) this.button1.Enabled = true;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to evoid errores with null exeptions
            if (this.allBoxesNotNull()) this.button1.Enabled = true;
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //to evoid errores with null exeptions
            if (this.allBoxesNotNull()) this.button1.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.takeGuess();
            if (this.allDifferent(this.guess)) //avoid incorrect input of the player
            {
                this.turns++;
                if (this.secondComparison() == this.key.Length)
                {
                    this.win();
                } else
                {
                    this.printResults();
                    this.button1.Enabled = false;
                }
            } else
            {
                MessageBox.Show("Sorry, your number is incorrect! All digits have to be distinct! Please, try again with other guess!");
            }
        }
        
    }
}
