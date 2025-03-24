// form 1

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace boggleGame
{
    public partial class Form1 : Form
    {
        // This is an array that contains all of the words that can be used in the game
        public string[] wordsList = System.IO.File.ReadAllLines(@"wordsList.txt");

        // List of variables used
        public bool buttonPressed = false;
        public string whatButtonPressed = "20"; // 20 is going to be the value for no buttons pressed because there is no button 20
        public int whatButtonClicked = 20;
        public int noGreenButtons = 0;
        public string listAllPressedButtons = "";

        public string screenContents = "";
        public int points = 0;
        public int timeAllocatedSeconds = 120; //the player has 2 minutes to play the game and find words
        public int timeSpentSeconds = 0;

        public string wordToSearch = "";
        public bool wordMatchFound = false;
        public string[] listWordsFound = new string[150];
        public int noWordsFound = 0;
        public bool duplicateWord = false;
        public bool newWordFound = false;

        public bool gamePaused = false;

        public string gameHighScore = System.IO.File.ReadAllText(@"highScore.txt"); // stores the high score in a variable
        public string gameHighScorer = System.IO.File.ReadAllText(@"highScorer.txt"); // stores the high scorer in a variable
        public bool newHighScore = false;

        string[,] diceLetters = new string[,]
        {
            {"R", "I", "F", "O", "B", "X"}, // dice0 button.disable = false
            {"I", "F", "E", "H", "E", "Y"}, // dice1
            {"D", "E", "N", "O", "W", "S"}, // dice2
            {"U", "T", "O", "K", "N", "D"}, // dice3
            {"H", "M", "S", "R", "A", "O"}, // dice4
            {"L", "U", "P", "E", "T", "S"}, // dice5
            {"A", "C", "I", "T", "O", "A"}, // dice6
            {"Y", "L", "G", "K", "U", "E"}, // dice7
            {"Q", "B", "M", "J", "O", "A"}, // dice8
            {"E", "H", "I", "S", "P", "N"}, // dice9
            {"V", "E", "T", "I", "G", "N"}, // dice10
            {"B", "A", "L", "I", "Y", "T"}, // dice11
            {"E", "Z", "A", "V", "N", "D"}, // dice12
            {"R", "A", "L", "E", "S", "C"}, // dice13
            {"U", "W", "I", "L", "R", "G"}, // dice14
            {"P", "A", "C", "E", "M", "D"}  // dice15
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            labelDice0.Text = diceLetters[0, (rnd.Next(0, 6))];
            labelDice1.Text = diceLetters[1, (rnd.Next(0, 6))];
            labelDice2.Text = diceLetters[2, (rnd.Next(0, 6))];
            labelDice3.Text = diceLetters[3, (rnd.Next(0, 6))];
            labelDice4.Text = diceLetters[4, (rnd.Next(0, 6))];
            labelDice5.Text = diceLetters[5, (rnd.Next(0, 6))];
            labelDice6.Text = diceLetters[6, (rnd.Next(0, 6))];
            labelDice7.Text = diceLetters[7, (rnd.Next(0, 6))];
            labelDice8.Text = diceLetters[8, (rnd.Next(0, 6))];
            labelDice9.Text = diceLetters[9, (rnd.Next(0, 6))];
            labelDice10.Text = diceLetters[10, (rnd.Next(0, 6))];
            labelDice11.Text = diceLetters[11, (rnd.Next(0, 6))];
            labelDice12.Text = diceLetters[12, (rnd.Next(0, 6))];
            labelDice13.Text = diceLetters[13, (rnd.Next(0, 6))];
            labelDice14.Text = diceLetters[14, (rnd.Next(0, 6))];
            labelDice15.Text = diceLetters[15, (rnd.Next(0, 6))];

            timer.Enabled = true;

            labelPause.Visible = false;
            textBoxHighScore.Visible = false;

            listBoxAllFoundWords.Items.Clear();
            listBoxAllFoundWords.Items.Add("");// 3 empty lines in the listbox so that the found words dont go behind the label
            listBoxAllFoundWords.Items.Add("");
            listBoxAllFoundWords.Items.Add("");

            labelDice0.Click += buttonClicked;
            labelDice1.Click += buttonClicked;
            labelDice2.Click += buttonClicked;
            labelDice3.Click += buttonClicked;
            labelDice4.Click += buttonClicked;
            labelDice5.Click += buttonClicked;
            labelDice6.Click += buttonClicked;
            labelDice7.Click += buttonClicked;
            labelDice8.Click += buttonClicked;
            labelDice9.Click += buttonClicked;
            labelDice10.Click += buttonClicked;
            labelDice11.Click += buttonClicked;
            labelDice12.Click += buttonClicked;
            labelDice13.Click += buttonClicked;
            labelDice14.Click += buttonClicked;
            labelDice15.Click += buttonClicked;

            pictureBoxDice0.Click += buttonClicked;
            pictureBoxDice1.Click += buttonClicked;
            pictureBoxDice2.Click += buttonClicked;
            pictureBoxDice3.Click += buttonClicked;
            pictureBoxDice4.Click += buttonClicked;
            pictureBoxDice5.Click += buttonClicked;
            pictureBoxDice6.Click += buttonClicked;
            pictureBoxDice7.Click += buttonClicked;
            pictureBoxDice8.Click += buttonClicked;
            pictureBoxDice9.Click += buttonClicked;
            pictureBoxDice10.Click += buttonClicked;
            pictureBoxDice11.Click += buttonClicked;
            pictureBoxDice12.Click += buttonClicked;
            pictureBoxDice13.Click += buttonClicked;
            pictureBoxDice14.Click += buttonClicked;
            pictureBoxDice15.Click += buttonClicked;
        }
        
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form3 Form3 = new Form3();
            Form3.Close();
            Form2.Show();
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeSpentSeconds++;

            labelTimeLeft.Text = "Time left = " + (timeAllocatedSeconds - timeSpentSeconds).ToString();

            if (timeSpentSeconds == 120)
            {
                endGame();
            }
        }

        private void endGame()
        {
            
            timer.Enabled = false;
            MessageBox.Show("Game Over! Your final score was " + points);

            if (points > int.Parse(gameHighScore))
            {
                labelDice0.Visible = false;
                labelDice1.Visible = false;
                labelDice2.Visible = false;
                labelDice3.Visible = false;
                labelDice4.Visible = false;
                labelDice5.Visible = false;
                labelDice6.Visible = false;
                labelDice7.Visible = false;
                labelDice8.Visible = false;
                labelDice9.Visible = false;
                labelDice10.Visible = false;
                labelDice11.Visible = false;
                labelDice12.Visible = false;
                labelDice13.Visible = false;
                labelDice14.Visible = false;
                labelDice15.Visible = false;
                pictureBoxDice0.Visible = false;
                pictureBoxDice1.Visible = false;
                pictureBoxDice2.Visible = false;
                pictureBoxDice3.Visible = false;
                pictureBoxDice4.Visible = false;
                pictureBoxDice5.Visible = false;
                pictureBoxDice6.Visible = false;
                pictureBoxDice7.Visible = false;
                pictureBoxDice8.Visible = false;
                pictureBoxDice9.Visible = false;
                pictureBoxDice10.Visible = false;
                pictureBoxDice11.Visible = false;
                pictureBoxDice12.Visible = false;
                pictureBoxDice13.Visible = false;
                pictureBoxDice14.Visible = false;
                pictureBoxDice15.Visible = false;
                buttonReset.Visible = false;
                labelScreen.Visible = false;
                timer.Enabled = false;
                labelScreen.Visible = false;
                buttonPause.Visible = false;
                buttonQuit.Visible = false;

                textBoxHighScore.Visible = true;

                MessageBox.Show("!!!YOU GOT A NEW HIGH SCORE!!! please enter your name in the box below the wordle logo and then press enter");

                newHighScore = true;

            }
            else
            {
                Form2 Form2 = new Form2();
                this.Close();
                Form2.Show();
            }           
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (gamePaused == true)
            {
                labelDice0.Visible = true;
                labelDice1.Visible = true;
                labelDice2.Visible = true;
                labelDice3.Visible = true;
                labelDice4.Visible = true;
                labelDice5.Visible = true;
                labelDice6.Visible = true;
                labelDice7.Visible = true;
                labelDice8.Visible = true;
                labelDice9.Visible = true;
                labelDice10.Visible = true;
                labelDice11.Visible = true;
                labelDice12.Visible = true;
                labelDice13.Visible = true;
                labelDice14.Visible = true;
                labelDice15.Visible = true;

                buttonEnter.Visible = true;
                buttonReset.Visible = true;
                listBoxAllFoundWords.Visible = true;
                labelScreen.Visible = true;

                timer.Enabled = true;

                labelPause.Visible = false;

                gamePaused = false;

                buttonPause.Text = "pause";
            }
            else
            {
                labelDice0.Visible = false;
                labelDice1.Visible = false;
                labelDice2.Visible = false;
                labelDice3.Visible = false;
                labelDice4.Visible = false;
                labelDice5.Visible = false;
                labelDice6.Visible = false;
                labelDice7.Visible = false;
                labelDice8.Visible = false;
                labelDice9.Visible = false;
                labelDice10.Visible = false;
                labelDice11.Visible = false;
                labelDice12.Visible = false;
                labelDice13.Visible = false;
                labelDice14.Visible = false;
                labelDice15.Visible = false;

                buttonEnter.Visible = false;
                buttonReset.Visible = false;
                listBoxAllFoundWords.Visible = false;
                labelScreen.Visible = false;

                timer.Enabled = false;

                labelPause.Visible = true;

                gamePaused = true;

                buttonPause.Text = "unpause";
            }
        }

        private void Enter_Click(object sender, EventArgs e) // what happens when the enter button is pressed
        {
            if (newHighScore == true)
            {
                File.WriteAllText("highScore.txt", points.ToString());
                File.WriteAllText("highScorer.txt", textBoxHighScore.Text);
                Form2 Form2 = new Form2();
                this.Close();
                Form2.Show();
            }
            else
            {
                wordToSearch = screenContents;

                for (int i = 0; i < wordsList.Length; i++)
                {
                    if (wordMatchFound == true)
                    {

                    }
                    else if (wordsList[i] == wordToSearch)
                    {
                        wordMatchFound = true;
                    }
                }

                if (wordMatchFound == true)
                {
                    if (noWordsFound == 0)
                    {
                        newWordFound = true;
                        duplicateWord = false;
                    }
                    else
                    {
                        for (int i = 0; i < listWordsFound.Length; i++)
                        {
                            if (duplicateWord == true)
                            {
                                newWordFound = false;
                            }
                            else if (listWordsFound[i] == wordToSearch)
                            {
                                duplicateWord = true;
                                newWordFound = false;
                            }
                            else
                            {
                                duplicateWord = false;
                                newWordFound = true;
                            }

                            
                        }
                    }
                }

                if (newWordFound == true)
                {
                    points = points + wordToSearch.Length;

                    listBoxAllFoundWords.Items.Add(wordToSearch);

                    listWordsFound[noWordsFound] = wordToSearch.ToString();

                    noWordsFound++;
                }

                labelPoints.Text = "Points = " + points.ToString();

                reset();
            }
        }

        private void Reset_Click(object sender, EventArgs e) // what happens when the reset button is pressed
        {
            reset();
        }
        private void reset()
        {
            listAllPressedButtons = "";
            noGreenButtons = 0;
            whatButtonClicked = 20;
            whatButtonPressed = "20";
            buttonPressed = false;
            wordMatchFound = false;
            wordToSearch = "";
            newWordFound = false;
            duplicateWord = false;
            screenUpdate();

            labelDice0.ForeColor = Color.Black;
            labelDice1.ForeColor = Color.Black;
            labelDice2.ForeColor = Color.Black;
            labelDice3.ForeColor = Color.Black;
            labelDice4.ForeColor = Color.Black;
            labelDice5.ForeColor = Color.Black;
            labelDice6.ForeColor = Color.Black;
            labelDice7.ForeColor = Color.Black;
            labelDice8.ForeColor = Color.Black;
            labelDice9.ForeColor = Color.Black;
            labelDice10.ForeColor = Color.Black;
            labelDice11.ForeColor = Color.Black;
            labelDice12.ForeColor = Color.Black;
            labelDice13.ForeColor = Color.Black;
            labelDice14.ForeColor = Color.Black;
            labelDice15.ForeColor = Color.Black;
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            if (sender == labelDice0 || sender == pictureBoxDice0)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 0;
                    listAllPressedButtons = "0";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice1.ForeColor = Color.Green;
                    labelDice4.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    noGreenButtons = 3;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice0.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "0";
                        whatButtonPressed = "0";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice1.ForeColor = Color.Green;
                        labelDice4.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        noGreenButtons = 3;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice1 || sender == pictureBoxDice1)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 1;
                    listAllPressedButtons = "1";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice0.ForeColor = Color.Green;
                    labelDice2.ForeColor = Color.Green;
                    labelDice4.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice1.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "1";
                        whatButtonPressed = "1";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice0.ForeColor = Color.Green;
                        labelDice2.ForeColor = Color.Green;
                        labelDice4.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice2 || sender == pictureBoxDice2)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 2;
                    listAllPressedButtons = "2";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice1.ForeColor = Color.Green;
                    labelDice3.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    labelDice7.ForeColor = Color.Green;
                    noGreenButtons = 5; 
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice2.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "2";
                        whatButtonPressed = "2";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice1.ForeColor = Color.Green;
                        labelDice3.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        labelDice7.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice3 || sender == pictureBoxDice3)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 3;
                    listAllPressedButtons = "3";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice2.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    labelDice7.ForeColor = Color.Green;
                    noGreenButtons = 3;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice3.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "3";
                        whatButtonPressed = "3";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice2.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        labelDice7.ForeColor = Color.Green;
                        noGreenButtons = 3;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice4 || sender == pictureBoxDice4)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 4;
                    listAllPressedButtons = "4";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice0.ForeColor = Color.Green;
                    labelDice1.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    labelDice8.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice4.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "4";
                        whatButtonPressed = "4";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice0.ForeColor = Color.Green;
                        labelDice1.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        labelDice8.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice5 || sender == pictureBoxDice5)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 5;
                    listAllPressedButtons = "5";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice0.ForeColor = Color.Green;
                    labelDice1.ForeColor = Color.Green;
                    labelDice2.ForeColor = Color.Green;
                    labelDice4.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    labelDice8.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    noGreenButtons = 8;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice5.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "5";
                        whatButtonPressed = "5";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice0.ForeColor = Color.Green;
                        labelDice1.ForeColor = Color.Green;
                        labelDice2.ForeColor = Color.Green;
                        labelDice4.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        labelDice8.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        noGreenButtons = 8;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice6 || sender == pictureBoxDice6)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 6;
                    listAllPressedButtons = "6";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice1.ForeColor = Color.Green;
                    labelDice2.ForeColor = Color.Green;
                    labelDice3.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    labelDice7.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    labelDice11.ForeColor = Color.Green;
                    noGreenButtons = 8;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice6.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "6";
                        whatButtonPressed = "6";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice1.ForeColor = Color.Green;
                        labelDice2.ForeColor = Color.Green;
                        labelDice3.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        labelDice7.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        labelDice11.ForeColor = Color.Green;
                        noGreenButtons = 8;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice7 || sender == pictureBoxDice7)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 7;
                    listAllPressedButtons = "7";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice2.ForeColor = Color.Green;
                    labelDice3.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    labelDice11.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice7.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "7";
                        whatButtonPressed = "7";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice2.ForeColor = Color.Green;
                        labelDice3.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        labelDice11.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice8 || sender == pictureBoxDice8)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 8;
                    listAllPressedButtons = "8";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice4.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    labelDice12.ForeColor = Color.Green;
                    labelDice13.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice8.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "8";
                        whatButtonPressed = "8";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice4.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        labelDice12.ForeColor = Color.Green;
                        labelDice13.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice9 || sender == pictureBoxDice9)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 9;
                    listAllPressedButtons = "9";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice4.ForeColor = Color.Green;
                    labelDice5.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    labelDice8.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    labelDice12.ForeColor = Color.Green;
                    labelDice13.ForeColor = Color.Green;
                    labelDice14.ForeColor = Color.Green;
                    noGreenButtons = 8;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice9.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "9";
                        whatButtonPressed = "9";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice4.ForeColor = Color.Green;
                        labelDice5.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        labelDice8.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        labelDice12.ForeColor = Color.Green;
                        labelDice13.ForeColor = Color.Green;
                        labelDice14.ForeColor = Color.Green;
                        noGreenButtons = 8;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice10 || sender == pictureBoxDice10)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 10;
                    listAllPressedButtons = "A";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice5.ForeColor = Color.Green;
                    labelDice6.ForeColor = Color.Green;
                    labelDice7.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    labelDice11.ForeColor = Color.Green;
                    labelDice13.ForeColor = Color.Green;
                    labelDice14.ForeColor = Color.Green;
                    labelDice15.ForeColor = Color.Green;
                    noGreenButtons = 8;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice10.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "A";
                        whatButtonPressed = "A";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice5.ForeColor = Color.Green;
                        labelDice6.ForeColor = Color.Green;
                        labelDice7.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        labelDice11.ForeColor = Color.Green;
                        labelDice13.ForeColor = Color.Green;
                        labelDice14.ForeColor = Color.Green;
                        labelDice15.ForeColor = Color.Green;
                        noGreenButtons = 8;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice11 || sender == pictureBoxDice11)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 11;
                    listAllPressedButtons = "B";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice6.ForeColor = Color.Green;
                    labelDice7.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    labelDice14.ForeColor = Color.Green;
                    labelDice15.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice11.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "B";
                        whatButtonPressed = "B";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice6.ForeColor = Color.Green;
                        labelDice7.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        labelDice14.ForeColor = Color.Green;
                        labelDice15.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice12 || sender == pictureBoxDice12)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 12;
                    listAllPressedButtons = "C";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice8.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    labelDice13.ForeColor = Color.Green;
                    noGreenButtons = 3;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice12.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "C";
                        whatButtonPressed = "C";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice8.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        labelDice13.ForeColor = Color.Green;
                        noGreenButtons = 3;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice13 || sender == pictureBoxDice13)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 13;
                    listAllPressedButtons = "D";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice8.ForeColor = Color.Green;
                    labelDice9.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    labelDice12.ForeColor = Color.Green;
                    labelDice14.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice13.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "D";
                        whatButtonPressed = "D";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice8.ForeColor = Color.Green;
                        labelDice9.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        labelDice12.ForeColor = Color.Green;
                        labelDice14.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice14 || sender == pictureBoxDice14)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 14;
                    listAllPressedButtons = "E";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice9.ForeColor = Color.Green;
                    labelDice10.ForeColor = Color.Green;
                    labelDice11.ForeColor = Color.Green;
                    labelDice13.ForeColor = Color.Green;
                    labelDice15.ForeColor = Color.Green;
                    noGreenButtons = 5;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice14.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "E";
                        whatButtonPressed = "E";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice9.ForeColor = Color.Green;
                        labelDice10.ForeColor = Color.Green;
                        labelDice11.ForeColor = Color.Green;
                        labelDice13.ForeColor = Color.Green;
                        labelDice15.ForeColor = Color.Green;
                        noGreenButtons = 5;
                        allButtonColourPressed();
                    }
                }
            }
            else if (sender == labelDice15 || sender == pictureBoxDice15)
            {
                if (buttonPressed == false)
                {
                    buttonPressed = true;
                    buttonColourUnPressed();
                    whatButtonClicked = 15;
                    listAllPressedButtons = "F";
                    noGreenButtons = 0;
                    //buttonColourPressed();
                    labelDice10.ForeColor = Color.Green;
                    labelDice11.ForeColor = Color.Green;
                    labelDice14.ForeColor = Color.Green;
                    noGreenButtons = 3;
                    allButtonColourPressed();
                    screenUpdate();
                }
                else if (buttonPressed == true)
                {
                    if (labelDice15.ForeColor == Color.Green)
                    {
                        listAllPressedButtons = listAllPressedButtons + "F";
                        whatButtonPressed = "F";
                        buttonColourUnPressed();
                        screenUpdate();
                        noGreenButtons = 0;
                        //buttonColourPressed();
                        labelDice10.ForeColor = Color.Green;
                        labelDice11.ForeColor = Color.Green;
                        labelDice14.ForeColor = Color.Green;
                        noGreenButtons = 3;
                        allButtonColourPressed();
                    }


                    //for (int i = 0; i < listAllPressedButtons.Length; i++)
                    //{
                    //    if (matchFound == true)
                    //    {

                    //    }
                    //    else if (listAllPressedButtons[i].ToString() == "F")
                    //    {
                    //        matchFound = true;
                    //        typeChar = false;
                    //    }
                    //    else
                    //    {
                    //        typeChar = true;
                    //    }
                    //}

                    //if (matchFound == false && typeChar == true)
                    //{
                    //    listAllPressedButtons = listAllPressedButtons + "F";
                    //    whatButtonPressed = "F";
                    //    buttonColourUnPressed();
                    //    screenUpdate();
                    //    matchFound = false;
                    //    noGreenButtons = 0;
                    //    //buttonColourPressed();
                    //    labelDice10.ForeColor = Color.Green;
                    //    labelDice11.ForeColor = Color.Green;
                    //    labelDice14.ForeColor = Color.Green;
                    //    noGreenButtons = 3;
                    //    allButtonColourPressed();
                    //}
                    //matchFound = false;
                    //typeChar = false;
                }
            }

        }


        private void allButtonColourPressed()
        {
            for (int i = 0; i <= (listAllPressedButtons.Length - 1); i++)
            {
                if (listAllPressedButtons[i].ToString() == "0")
                {
                    labelDice0.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "1")
                {
                    labelDice1.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "2")
                {
                    labelDice2.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "3")
                {
                    labelDice3.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "4")
                {
                    labelDice4.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "5")
                {
                    labelDice5.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "6")
                {
                    labelDice6.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "7")
                {
                    labelDice7.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "8")
                {
                    labelDice8.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "9")
                {
                    labelDice9.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "A")
                {
                    labelDice10.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "B")
                {
                    labelDice11.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "C")
                {
                    labelDice12.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "D")
                {
                    labelDice13.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "E")
                {
                    labelDice14.ForeColor = Color.Red;
                }
                else if (listAllPressedButtons[i].ToString() == "F")
                {
                    labelDice15.ForeColor = Color.Red;
                }
            }
        }

        private void buttonColourUnPressed()
        {
            labelDice0.ForeColor = Color.Black;
            labelDice1.ForeColor = Color.Black;
            labelDice2.ForeColor = Color.Black;
            labelDice3.ForeColor = Color.Black;
            labelDice4.ForeColor = Color.Black;
            labelDice5.ForeColor = Color.Black;
            labelDice6.ForeColor = Color.Black;
            labelDice7.ForeColor = Color.Black;
            labelDice8.ForeColor = Color.Black;
            labelDice9.ForeColor = Color.Black;
            labelDice10.ForeColor = Color.Black;
            labelDice11.ForeColor = Color.Black;
            labelDice12.ForeColor = Color.Black;
            labelDice13.ForeColor = Color.Black;
            labelDice14.ForeColor = Color.Black;
            labelDice15.ForeColor = Color.Black;
        }

        private void screenUpdate()
        {
            screenContents = "";

            for (int i = 0; i <= (listAllPressedButtons.Length-1); i++)
            {              
                if (listAllPressedButtons[i].ToString() == "0")
                {
                    screenContents = screenContents + labelDice0.Text;
                } 
                else if (listAllPressedButtons[i].ToString() == "1")
                {
                    screenContents = screenContents + labelDice1.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "2")
                {
                    screenContents = screenContents + labelDice2.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "3")
                {
                    screenContents = screenContents + labelDice3.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "4")
                {
                    screenContents = screenContents + labelDice4.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "5")
                {
                    screenContents = screenContents + labelDice5.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "6")
                {
                    screenContents = screenContents + labelDice6.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "7")
                {
                    screenContents = screenContents + labelDice7.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "8")
                {
                    screenContents = screenContents + labelDice8.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "9")
                {
                    screenContents = screenContents + labelDice9.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "A")
                {
                    screenContents = screenContents + labelDice10.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "B")
                {
                    screenContents = screenContents + labelDice11.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "C")
                {
                    screenContents = screenContents + labelDice12.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "D")
                {
                    screenContents = screenContents + labelDice13.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "E")
                {
                    screenContents = screenContents + labelDice14.Text;
                }
                else if (listAllPressedButtons[i].ToString() == "F")
                {
                    screenContents = screenContents + labelDice15.Text;
                }
            }

            labelScreen.Text = screenContents;

        }

        
    }
}

//form 2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boggleGame
{
    public partial class Form2 : Form
    {
        public string gameHighScore = System.IO.File.ReadAllText(@"highScore.txt");
        public string gameHighScorer = System.IO.File.ReadAllText(@"highScorer.txt");

        public Form2()
        {
            InitializeComponent();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            labelHighScore.Text = "The high score is " + gameHighScore;
            labelHighScorer.Text = gameHighScorer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            this.Hide();
            Form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            this.Close();
            Form1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            this.Hide();
            Form3.Show();
        }

        private void labelHighScorer_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

// form 3

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boggleGame
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonBacktoHome_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            this.Close();
            Form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
