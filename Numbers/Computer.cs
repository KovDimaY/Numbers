using System;
using System.Drawing;
using System.Windows.Forms;

namespace Numbers
{
    public partial class Computer : Form
    {
        
        private int[,] allGuesses = new int[15,6];
        private int[] currentGuess = new int[4];
        private int iteration;
        private bool isPossible; 
        Random rand = new Random();

        public Computer()
        {
            InitializeComponent();
            this.isPossible = true;
            this.addNumbers(this.comboBox1);
            this.addNumbers(this.comboBox2);
            this.iteration = 0;
            this.firstGuess();
            this.richTextBox1.Text += "\nIs ";
            this.richTextBox1.Text += this.printNumber(this.currentGuess);
            this.richTextBox1.Text += " your number?";

        }

        //this function fills the comboBoxes
        private void addNumbers(ComboBox box)
        {
            for (int i = 0; i < 5; i++)
            {
                box.Items.Add(i);
            }
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

        //this function makes a first guess of the computer to initialize a game
        private void firstGuess()
        {
            //initialize a current guess with zeroes
            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                this.currentGuess[i] = 0;
            }
            //to be sure that all digits are distinct
            while (!this.allDifferent(this.currentGuess))
            {
                for (int i = 0; i < this.currentGuess.Length; i++)
                {
                    this.currentGuess[i] = rand.Next(9) + 1;
                }
            }
            //save the guess in the base of all guesses
            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                this.allGuesses[this.iteration,i] = this.currentGuess[i];
            }
        }

        //function that compares two numbers and return the firs hint
        private int firstComparison(int index)
        {
            int result = 0;
            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                for (int j = 0; j < this.currentGuess.Length; j++)
                {
                    if (this.currentGuess[i] == this.allGuesses[index,j]) result++;
                }
            }
            return result;
        }

        //function that compare two numbers and return the second hint
        private int secondComparison(int index)
        {
            int result = 0;
            for (int i = 0; i < this.currentGuess.Length; i++)
            {
                if (this.currentGuess[i] == this.allGuesses[index, i]) result++;
            }
            return result;
        }

        //function that checks if all comboBoxes are initiated with some value
        private bool allBoxesNotNull()
        {
            bool result = true;
            if (this.comboBox1.SelectedItem == null) return false;
            if (this.comboBox2.SelectedItem == null) return false;
            return result;
        }

        //function that do all the routine when game is over
        private void win()
        {
            //disable all buttons
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;

            //print a congratulations
            this.richTextBox1.ForeColor = Color.Green;
            this.richTextBox1.Text = "               It was easy... :)\n";
            this.richTextBox1.Text += "          Your number is ";
            this.richTextBox1.Text += this.printNumber(this.currentGuess);
            this.richTextBox1.Text += "!\nComputer spent ";
            this.richTextBox1.Text += this.iteration + 1;
            if (this.iteration == 0) this.richTextBox1.Text += " step to get it...";
            else this.richTextBox1.Text += " steps to get it...";
        }

        //function that tell the player about his cheating
        private void cheating()
        {
            //disable all buttons
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.comboBox1.Enabled = false;
            this.comboBox2.Enabled = false;

            //print a message about cheating
            this.richTextBox1.ForeColor = Color.Red;
            this.richTextBox1.Text += "\nSorry, but it is impossible!\n";
            this.richTextBox1.Text += "You have made a mistake or tried to cheat me...";
        }

        //function to print the answer of the player
        private void printAnswer()
        {
            this.richTextBox1.Text += " No (";
            this.richTextBox1.Text += this.allGuesses[this.iteration, 4];
            this.richTextBox1.Text += " - ";
            this.richTextBox1.Text += this.allGuesses[this.iteration, 5];
            this.richTextBox1.Text += ")\n";
        }

        //guess of the computer from the smallest to the greatest 
        private void newGuessDown()
        {
            this.isPossible = false;

            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    for (int k = 1; k < 10; k++)
                    {
                        for (int m = 1; m < 10; m++)
                        {
                            this.currentGuess[0] = i;
                            this.currentGuess[1] = j;
                            this.currentGuess[2] = k;
                            this.currentGuess[3] = m;

                            if (this.allDifferent(this.currentGuess))
                            {
                                bool isCorrect = true;
                                //check all answers of the player
                                for (int q = 0; q < this.iteration+1; q++)
                                {
                                    if (this.firstComparison(q) != this.allGuesses[q,4] || 
                                        this.secondComparison(q) != this.allGuesses[q, 5])
                                    {
                                        isCorrect = false;
                                    }
                                }
                                if (isCorrect)
                                {
                                    this.isPossible = true;
                                    this.iteration++;
                                    for (int p = 0; p < this.currentGuess.Length; p++)
                                    {
                                        this.allGuesses[this.iteration, p] = this.currentGuess[p];
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        //guess of the computer from the greatest to the smallest (for diversity)
        private void newGuessUp()
        {
            this.isPossible = false;

            for (int i = 9; i > 0; i--)
            {
                for (int j = 9; j > 0; j--)
                {
                    for (int k = 9; k > 0; k--)
                    {
                        for (int m = 9; m > 0; m--)
                        {
                            this.currentGuess[0] = i;
                            this.currentGuess[1] = j;
                            this.currentGuess[2] = k;
                            this.currentGuess[3] = m;

                            if (this.allDifferent(this.currentGuess))
                            {
                                bool isCorrect = true;

                                //check all answers of the player
                                for (int q = 0; q < this.iteration + 1; q++)
                                {
                                    if (this.firstComparison(q) != this.allGuesses[q, 4] ||
                                        this.secondComparison(q) != this.allGuesses[q, 5])
                                    {
                                        isCorrect = false;
                                    }
                                }
                                if (isCorrect)
                                {
                                    this.isPossible = true;
                                    this.iteration++;
                                    for (int p = 0; p < this.currentGuess.Length; p++)
                                    {
                                        this.allGuesses[this.iteration, p] = this.currentGuess[p];
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            //read the answer of the player
            this.allGuesses[this.iteration, 4] = (int)this.comboBox1.SelectedItem;
            this.allGuesses[this.iteration, 5] = (int)this.comboBox2.SelectedItem;

            if ((int)this.comboBox2.SelectedItem == 4) // all digits are correct so computer wins
            {
                this.win();
            } else
            {
                this.printAnswer();
                
                //to make guesses a little bit different from each other
                if (this.rand.Next(2) == 0)
                {
                    this.newGuessDown();
                }
                else
                {
                    this.newGuessUp();
                }

                if (this.isPossible) //to check if the player is cheating
                {
                    this.richTextBox1.Text += "Is ";
                    this.richTextBox1.Text += this.printNumber(this.currentGuess);
                    this.richTextBox1.Text += " your number?";
                    
                    //to obligate player select new values
                    this.comboBox1.SelectedItem = null;
                    this.comboBox2.SelectedItem = null;
                    this.button1.Enabled = false;
                }
                else
                {
                    this.cheating();
                }
            }       
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.win();
        }
        
    }
}
