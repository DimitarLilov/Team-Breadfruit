﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Poker
{

    public partial class Form1 : Form
    {
        #region Variables
        //ProgressBar asd = new ProgressBar();
        //public int Nm;
        Panel playerPanel = new Panel();
        Panel botOnePanel = new Panel();
        Panel botTwoPanel = new Panel();
        Panel botThreePanel = new Panel();
        Panel botFourPanel = new Panel();
        Panel botFivePanel = new Panel();

        //constants
        private int callChipsValue = 500;
        public int foldedPlayers = 5;
        private int TotalTableCards = 17;

        public const int DefaultStartingChips = 10000;
        public const int LastRound = 4;

        public int playerChips = DefaultStartingChips;
        public int botOnehips = DefaultStartingChips;
        public int botTwoChips = DefaultStartingChips;
        public int botThreeChips = DefaultStartingChips;
        public int botFourChips = DefaultStartingChips;
        public int botFiveChips = DefaultStartingChips;


        public double type;
        public double totalRounds;
        public double botOnePower;
        public double botTwoPower;
        public double botThreePower;
        public double botFourPower;
        public double botFivePower;
        public double playerPower;
        public double playerType = -1;
        double Raise;

       public double botOneType = -1;
       public double botTwoType = -1;
       public double botThreeType = -1;
       public double botFourType = -1;
       public double botFiveType = -1; // bots

        public bool botOneTurn;
        public bool botTwoTurn;
        public bool botThreeTurn;
        public bool botFourTurn;
        public bool botFiveTurn;

        //If the bots folded
        public bool hasBotOneBankrupted;
        public bool hasBotTwoBankrupted;
        public bool hasBotThreeBankrupted;
        public bool hasBotFourBankrupted;
        public bool hasBotFiveBankrupted;

       public bool hasPlayerFolded;
       public bool botOneFolded;
       public bool botTwoFolded;
       public bool botThreeFolded;
       public bool botFourFolded;
       public bool botFiveFolded;

       public bool hasAddedChips;
       public bool changed;

        public int playerCall;
        public int botOneCall;
        public int botTwoCall;
        public int botThreeCall;
        public int botFourCall;
        public int botFiveCall;

        public int playerRaise;
        public int botOneRaise;
        public int botTwoRaise;
        public int botThreeRaise;
        public int botFourRaise;
        public int botFiveRaise;

        private int height;
        private int width;
        public int winners;

        private const int Flop = 1;
        private const int Turn = 2;
        private const int River = 3;
        private const int End = 4;
        private int maxLeft = 6;

        public int last;
        public int raisedTurn = 1;

        //Lists
        public List<bool?> bankruptPlayers = new List<bool?>();

        public List<Type> winningingHands = new List<Type>();
        public List<string> CheckWinners = new List<string>();
        List<int> totalAllInChips = new List<int>();

        public bool hasPlayerBankrupted;
        public bool playerTurn = true;
        public bool shouldRestart;
        public bool isRaising;

        public Poker.Type sorted;

        public string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);


        // Nothing has changed down here
        public int[] cardsAsNumbers = new int[17];
        public Image[] Deck = new Image[52];
        public PictureBox[] cardsImages = new PictureBox[52];

        Timer timer = new Timer();
        Timer Updates = new Timer();

        public int t = 60;
        public int dealtCards;
        public int i = 0;
        public int bigBlind = 500;
        public int smallBlind = 250;
        public int DefaultMaximumMoney = 10000000;
        public int turnCount;
        private readonly Rules currentRules;
        private readonly HandRules handRules;
        private readonly Winner winner;
        // public readonly Dealer dealer;

        #endregion

        #region Form1 main
        public Form1()
        {
            callChipsValue = bigBlind;
            MaximizeBox = false;
            MinimizeBox = false;

            Updates.Start();
            InitializeComponent();

            width = this.Width;
            height = this.Height;

            Shuffle();

            EnableAllTextBoxes();

            InitializeChipsTextBoxes();

            InitializeTimer();

            InitializeBlindsBoxes();

            playerRaiseTextBox.Text = (bigBlind * 2).ToString();
            currentRules = new Rules(this);
            handRules = new HandRules(this);
            winner = new Winner(this);
            //dealer = new Dealer(this);
        }

        public Rules CurrentRules
        {
            get { return currentRules; }
        }

        public HandRules HandRules
        {
            get { return handRules; }
        }

        public Winner Winner1
        {
            get { return winner; }
        }

        //public Dealer Dealer
        //{
        //    get { return dealer; }
        //}

        private void InitializeBlindsBoxes()
        {
            bigBlindTextBox.Visible = false;
            smallBlindTextBox.Visible = false;
            bigBlindButton.Visible = false;
            smallBlindButton.Visible = false;
        }

        private void InitializeTimer()
        {
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            Updates.Interval = 100;
            Updates.Tick += Update_Tick;
        }

        private void InitializeChipsTextBoxes()
        {
            this.playerChipsTextBox.Text = "Chips : " + playerChips;
            this.botOneChipsTextBox.Text = "Chips : " + botOnehips;
            this.botTwoChipsTextBox.Text = "Chips : " + botTwoChips;
            this.botThreeChipsTextBox.Text = "Chips : " + botThreeChips;
            this.botFourChipsTextBox.Text = "Chips : " + botFourChips;
            this.botFiveChipsTextBox.Text = "Chips : " + botFiveChips;
        }

        private void EnableAllTextBoxes()
        {
            potTextBox.Enabled = false;
            playerChipsTextBox.Enabled = false;
            botOneChipsTextBox.Enabled = false;
            botTwoChipsTextBox.Enabled = false;
            botThreeChipsTextBox.Enabled = false;
            botFourChipsTextBox.Enabled = false;
            botFiveChipsTextBox.Enabled = false;
        }

        #endregion

        #region Shuffle
        async Task Shuffle()
        {
            Random randomNumber = new Random();

            bankruptPlayers.Add(hasPlayerBankrupted);
            bankruptPlayers.Add(hasBotOneBankrupted);
            bankruptPlayers.Add(hasBotTwoBankrupted);
            bankruptPlayers.Add(hasBotThreeBankrupted);
            bankruptPlayers.Add(hasBotFourBankrupted);
            bankruptPlayers.Add(hasBotFiveBankrupted);

            playerCallButton.Enabled = false;
            playerRaiseButton.Enabled = false;
            playerFoldButton.Enabled = false;
            playerCheckButton.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;

            bool isCardOnTheTable = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");

            int horizontal = 580;
            int vertical = 480;

            
            //shuffles cards
            ShuffleCards(randomNumber);
            
            await DealOutCards(horizontal, vertical, isCardOnTheTable, backImage);

            CheckIfGameShouldBeRestarted();

            CheckIfBotsActionsShouldBeEnabled();
        }

        #region Shuffle methods
        public void ShuffleCards(Random random)
        {
            for (int i = ImgLocation.Length; i > 0; i--)
            {
                int nextRandomNumber = random.Next(i);
                var k = ImgLocation[nextRandomNumber];
               ImgLocation[nextRandomNumber] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }
        }

        public void CheckIfBotsActionsShouldBeEnabled()
        {
            if (dealtCards == 17)
            {
                playerRaiseButton.Enabled = true;
                playerCallButton.Enabled = true;
                playerRaiseButton.Enabled = true;
                playerRaiseButton.Enabled = true;
                playerFoldButton.Enabled = true;

            }
        }

        public void CheckIfGameShouldBeRestarted()
        {
            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
               foldedPlayers = 5;
            }
        }

        private async Task DealOutCards(int horizontal, int vertical, bool isCardOnTheTable, Bitmap backImage)
        {

            for (dealtCards = 0; dealtCards < TotalTableCards; dealtCards++)
            {
                Deck[dealtCards] = Image.FromFile(ImgLocation[dealtCards]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[dealtCards] = ImgLocation[dealtCards].Replace(c, string.Empty);
                }

                cardsAsNumbers[dealtCards] = int.Parse(ImgLocation[dealtCards]) - 1;
                cardsImages[dealtCards] = new PictureBox();
                cardsImages[dealtCards].SizeMode = PictureBoxSizeMode.StretchImage;
                cardsImages[dealtCards].Height = 130;
                cardsImages[dealtCards].Width = 80;
                this.Controls.Add(cardsImages[dealtCards]);
                cardsImages[dealtCards].Name = "pb" + dealtCards;
                await Task.Delay(200);

                #region Throwing Cards
                if (dealtCards < 2)
                {
                    if (cardsImages[0].Tag != null)
                    {
                        cardsImages[1].Tag = cardsAsNumbers[1];
                    }
                    cardsImages[0].Tag = cardsAsNumbers[0];
                    cardsImages[dealtCards].Image = Deck[dealtCards];
                    cardsImages[dealtCards].Anchor = (AnchorStyles.Bottom);
                    cardsImages[dealtCards].Location = new Point(horizontal, vertical);

                    horizontal += cardsImages[dealtCards].Width;
                    this.Controls.Add(playerPanel);
                    playerPanel.Location = new Point(cardsImages[0].Left - 10, cardsImages[0].Top - 10);
                    playerPanel.BackColor = Color.DarkBlue;
                    playerPanel.Height = 150;
                    playerPanel.Width = 180;
                    playerPanel.Visible = false;
                }

                if (botOnehips > 0)
                {
                    foldedPlayers--;
                    //checks which player to give cards to
                    if (dealtCards >= 2 && dealtCards < 4)
                    {
                        if (cardsImages[2].Tag != null)
                        {
                            cardsImages[3].Tag = cardsAsNumbers[3];
                        }

                        cardsImages[2].Tag = cardsAsNumbers[2];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        isCardOnTheTable = true;
                        cardsImages[dealtCards].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        cardsImages[dealtCards].Image = backImage;
                        cardsImages[dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += cardsImages[dealtCards].Width;
                        cardsImages[dealtCards].Visible = true;
                        this.Controls.Add(botOnePanel);
                        botOnePanel.Location = new Point(cardsImages[2].Left - 10, cardsImages[2].Top - 10);
                        botOnePanel.BackColor = Color.DarkBlue;
                        botOnePanel.Height = 150;
                        botOnePanel.Width = 180;
                        botOnePanel.Visible = false;
                        if (dealtCards == 3)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                if (botTwoChips > 0)
                {
                    foldedPlayers--;
                    //checks which player to give cards to
                    if (dealtCards >= 4 && dealtCards < 6)
                    {
                        if (cardsImages[4].Tag != null)
                        {
                            cardsImages[5].Tag = cardsAsNumbers[5];
                        }

                        cardsImages[4].Tag = cardsAsNumbers[4];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        isCardOnTheTable = true;
                        cardsImages[dealtCards].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        cardsImages[dealtCards].Image = backImage;
                        //cardsImages[i].Image = Deck[i];
                        cardsImages[dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += cardsImages[dealtCards].Width;
                        cardsImages[dealtCards].Visible = true;
                        this.Controls.Add(botTwoPanel);
                        botTwoPanel.Location = new Point(cardsImages[4].Left - 10, cardsImages[4].Top - 10);
                        botTwoPanel.BackColor = Color.DarkBlue;
                        botTwoPanel.Height = 150;
                        botTwoPanel.Width = 180;
                        botTwoPanel.Visible = false;
                        if (dealtCards == 5)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }
                if (botThreeChips > 0)
                {
                    foldedPlayers--;
                    if (dealtCards >= 6 && dealtCards < 8)
                    {
                        if (cardsImages[6].Tag != null)
                        {
                            cardsImages[7].Tag = cardsAsNumbers[7];
                        }

                        cardsImages[6].Tag = cardsAsNumbers[6];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        isCardOnTheTable = true;
                        cardsImages[dealtCards].Anchor = (AnchorStyles.Top);
                        cardsImages[dealtCards].Image = backImage;
                        cardsImages[dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += cardsImages[dealtCards].Width;
                        cardsImages[dealtCards].Visible = true;
                        this.Controls.Add(botThreePanel);
                        botThreePanel.Location = new Point(cardsImages[6].Left - 10, cardsImages[6].Top - 10);
                        botThreePanel.BackColor = Color.DarkBlue;
                        botThreePanel.Height = 150;
                        botThreePanel.Width = 180;
                        botThreePanel.Visible = false;

                        if (dealtCards == 7)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                if (botFourChips > 0)
                {
                    foldedPlayers--;

                    if (dealtCards >= 8 && dealtCards < 10)
                    {
                        if (cardsImages[8].Tag != null)
                        {
                            cardsImages[9].Tag = cardsAsNumbers[9];
                        }

                        cardsImages[8].Tag = cardsAsNumbers[8];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        isCardOnTheTable = true;
                        cardsImages[dealtCards].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        cardsImages[dealtCards].Image = backImage;
                        cardsImages[dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += cardsImages[dealtCards].Width;
                        cardsImages[dealtCards].Visible = true;

                        this.Controls.Add(botFourPanel);
                        botFourPanel.Location = new Point(cardsImages[8].Left - 10, cardsImages[8].Top - 10);
                        botFourPanel.BackColor = Color.DarkBlue;
                        botFourPanel.Height = 150;
                        botFourPanel.Width = 180;
                        botFourPanel.Visible = false;

                        if (dealtCards == 9)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                if (botFiveChips > 0)
                {
                    foldedPlayers--;
                    //checks which player to give cards to
                    if (dealtCards >= 10 && dealtCards < 12)
                    {
                        if (cardsImages[10].Tag != null)
                        {
                            cardsImages[11].Tag = cardsAsNumbers[11];
                        }

                        cardsImages[10].Tag = cardsAsNumbers[10];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        isCardOnTheTable = true;
                        cardsImages[dealtCards].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        cardsImages[dealtCards].Image = backImage;
                        //cardsImages[i].Image = Deck[i];
                        cardsImages[dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += cardsImages[dealtCards].Width;
                        cardsImages[dealtCards].Visible = true;
                        this.Controls.Add(botFivePanel);
                        botFivePanel.Location = new Point(cardsImages[10].Left - 10, cardsImages[10].Top - 10);
                        botFivePanel.BackColor = Color.DarkBlue;
                        botFivePanel.Height = 150;
                        botFivePanel.Width = 180;
                        botFivePanel.Visible = false;

                        if (dealtCards == 11)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }
                //sets the cards on the table face down
                if (dealtCards >= 12)
                {
                    cardsImages[12].Tag = cardsAsNumbers[12];
                    if (dealtCards > 12)
                    {
                        cardsImages[13].Tag = cardsAsNumbers[13];
                    }

                    if (dealtCards > 13)
                    {
                        cardsImages[14].Tag = cardsAsNumbers[14];
                    }

                    if (dealtCards > 14)
                    {
                        cardsImages[15].Tag = cardsAsNumbers[15];
                    }

                    if (dealtCards > 15)
                    {
                        cardsImages[16].Tag = cardsAsNumbers[16];
                    }

                    if (!isCardOnTheTable)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    isCardOnTheTable = true;
                    if (cardsImages[dealtCards] != null)
                    {
                        cardsImages[dealtCards].Anchor = AnchorStyles.None;
                        cardsImages[dealtCards].Image = backImage;
                        //cardsImages[i].Image = Deck[i];
                        cardsImages[dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                #endregion

                if (botOnehips <= 0)
                {
                    hasBotOneBankrupted = true;
                    cardsImages[2].Visible = false;
                    cardsImages[3].Visible = false;
                }
                else
                {
                    hasBotOneBankrupted = false;
                    if (dealtCards == 3)
                    {
                        if (cardsImages[3] != null)
                        {
                            cardsImages[2].Visible = true;
                            cardsImages[3].Visible = true;
                        }
                    }
                }

                if (botTwoChips <= 0)
                {
                    hasBotTwoBankrupted = true;
                    cardsImages[4].Visible = false;
                    cardsImages[5].Visible = false;
                }
                else
                {
                    hasBotTwoBankrupted = false;
                    if (dealtCards == 5)
                    {
                        if (cardsImages[5] != null)
                        {
                            cardsImages[4].Visible = true;
                            cardsImages[5].Visible = true;
                        }
                    }
                }

                if (botThreeChips <= 0)
                {
                    hasBotThreeBankrupted = true;
                    cardsImages[6].Visible = false;
                    cardsImages[7].Visible = false;
                }
                else
                {
                    hasBotThreeBankrupted = false;
                    if (dealtCards == 7)
                    {
                        if (cardsImages[7] != null)
                        {
                            cardsImages[6].Visible = true;
                            cardsImages[7].Visible = true;
                        }
                    }
                }

                if (botFourChips <= 0)
                {
                    hasBotFourBankrupted = true;
                    cardsImages[8].Visible = false;
                    cardsImages[9].Visible = false;
                }
                else
                {
                    hasBotFourBankrupted = false;
                    if (dealtCards == 9)
                    {
                        if (cardsImages[9] != null)
                        {
                            cardsImages[8].Visible = true;
                            cardsImages[9].Visible = true;
                        }
                    }
                }

                if (botFiveChips <= 0)
                {
                    hasBotFiveBankrupted = true;
                    cardsImages[10].Visible = false;
                    cardsImages[11].Visible = false;
                }
                else
                {
                    hasBotFiveBankrupted = false;
                    if (dealtCards == 11)
                    {
                        if (cardsImages[11] != null)
                        {
                            cardsImages[10].Visible = true;
                            cardsImages[11].Visible = true;
                        }
                    }
                }
                if (dealtCards == 16)
                {
                    if (!shouldRestart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }
        }
        #endregion

        #endregion

        async Task Turns()
        {
            #region Rotating
            if (!hasPlayerBankrupted && playerTurn)
            {
                CurrentTurnPlayerAction();
            }

            if (hasPlayerBankrupted || !playerTurn)
            {
                await WhoIsAllIn();

                if (hasPlayerBankrupted && !hasPlayerFolded)
                {
                    if (playerCallButton.Text.Contains("All in") == false || playerRaiseButton.Text.Contains("All in") == false)
                    {
                        bankruptPlayers.RemoveAt(0);
                        bankruptPlayers.Insert(0, null);
                        maxLeft--;
                        hasPlayerFolded = true;
                    }
                }

                await CheckRaise(0);

                timerBar.Visible = false;
                playerRaiseButton.Enabled = false;
                playerCallButton.Enabled = false;
                playerRaiseButton.Enabled = false;
                playerRaiseButton.Enabled = false;
                playerFoldButton.Enabled = false;

                timer.Stop();

                botOneTurn = true;

                if (!hasBotOneBankrupted)
                {
                    if (botOneTurn)
                    {
                        CheckCurrentBid(botOneStatus, ref botOneCall, ref botOneRaise, 1);
                        CheckCurrentBid(botOneStatus, ref botOneCall, ref botOneRaise, 2);
                        CurrentRules.GameRules(2, 3, ref botOneType, ref botOnePower, hasBotOneBankrupted);
                        MessageBox.Show("Bot 1's Turn");
                        this.CheckBotsHand(2, 3, ref botOnehips, ref botOneTurn, ref hasBotOneBankrupted, botOneStatus, botOnePower, botOneType);
                        turnCount++;
                        last = 1;
                        botOneTurn = false;
                        botTwoTurn = true;
                    }
                }

                if (hasBotOneBankrupted && !botOneFolded)
                {
                    bankruptPlayers.RemoveAt(1);
                    bankruptPlayers.Insert(1, null);
                    maxLeft--;
                    botOneFolded = true;
                }

                if (hasBotOneBankrupted || !botOneTurn)
                {
                    await CheckRaise(1);
                    botTwoTurn = true;
                }

                if (!hasBotTwoBankrupted)
                {
                    if (botTwoTurn)
                    {
                        CheckCurrentBid(botTwoStatus, ref botTwoCall, ref botTwoRaise, 1);
                        CheckCurrentBid(botTwoStatus, ref botTwoCall, ref botTwoRaise, 2);
                        CurrentRules.GameRules(4, 5, ref botTwoType, ref botTwoPower, hasBotTwoBankrupted);

                        MessageBox.Show("Bot 2's Turn");
                        this.CheckBotsHand(4, 5, ref botTwoChips, ref botTwoTurn, ref hasBotTwoBankrupted, botTwoStatus, botTwoPower, botTwoType);

                        turnCount++;
                        last = 2;
                        botTwoTurn = false;
                        botThreeTurn = true;
                    }
                }

                if (hasBotTwoBankrupted && !botTwoFolded)
                {
                    bankruptPlayers.RemoveAt(2);
                    bankruptPlayers.Insert(2, null);

                    maxLeft--;
                    botTwoFolded = true;
                }

                if (hasBotTwoBankrupted || !botTwoTurn)
                {
                    await CheckRaise(2);
                    botThreeTurn = true;
                }

                if (!hasBotThreeBankrupted)
                {
                    if (botThreeTurn)
                    {
                        CheckCurrentBid(botThreeStatus, ref botThreeCall, ref botThreeRaise, 1);
                        CheckCurrentBid(botThreeStatus, ref botThreeCall, ref botThreeRaise, 2);
                        CurrentRules.GameRules(6, 7, ref botThreeType, ref botThreePower, hasBotThreeBankrupted);
                        MessageBox.Show("Bot 3's Turn");
                        this.CheckBotsHand(6, 7, ref botThreeChips, ref botThreeTurn, ref hasBotThreeBankrupted, botThreeStatus, botThreePower, botThreeType);

                        turnCount++;
                        last = 3;
                        botThreeTurn = false;
                        botFourTurn = true;
                    }
                }

                if (hasBotThreeBankrupted && !botThreeFolded)
                {
                    bankruptPlayers.RemoveAt(3);
                    bankruptPlayers.Insert(3, null);
                    maxLeft--;
                    botThreeFolded = true;
                }

                if (hasBotThreeBankrupted || !botThreeTurn)
                {
                    await CheckRaise(3);
                    botFourTurn = true;
                }

                if (!hasBotFourBankrupted)
                {
                    if (botFourTurn)
                    {
                        CheckCurrentBid(botFourStatus, ref botFourCall, ref botFourRaise, 1);
                        CheckCurrentBid(botFourStatus, ref botFourCall, ref botFourRaise, 2);
                        CurrentRules.GameRules(8, 9, ref botFourType, ref botFourPower, hasBotFourBankrupted);
                        MessageBox.Show("Bot 4's Turn");
                        this.CheckBotsHand(8, 9, ref botFourChips, ref botFourTurn, ref hasBotFourBankrupted, botFourStatus, botFourPower, botFourType);
                        turnCount++;
                        last = 4;
                        botFourTurn = false;
                        botFiveTurn = true;
                    }
                }

                if (hasBotFourBankrupted && !botFourFolded)
                {
                    bankruptPlayers.RemoveAt(4);
                    bankruptPlayers.Insert(4, null);
                    maxLeft--;
                    botFourFolded = true;
                }


                if (hasBotFourBankrupted || !botFourTurn)
                {
                    await CheckRaise(4);
                    botFiveTurn = true;
                }

                if (!hasBotFiveBankrupted)
                {
                    if (botFiveTurn)
                    {
                        CheckCurrentBid(botFiveStatus, ref botFiveCall, ref botFiveRaise, 1);
                        CheckCurrentBid(botFiveStatus, ref botFiveCall, ref botFiveRaise, 2);
                        CurrentRules.GameRules(10, 11, ref botFiveType, ref botFivePower, hasBotFiveBankrupted);
                        MessageBox.Show("Bot 5's Turn");
                        this.CheckBotsHand(10, 11, ref botFiveChips, ref botFiveTurn, ref hasBotFiveBankrupted, botFiveStatus, botFivePower, botFiveType);
                        turnCount++;
                        last = 5;
                        botFiveTurn = false;
                    }
                }

                if (hasBotFiveBankrupted && !botFiveFolded)
                {
                    bankruptPlayers.RemoveAt(5);
                    bankruptPlayers.Insert(5, null);
                    maxLeft--;
                    botFiveFolded = true;
                }

                if (hasBotFiveBankrupted || !botFiveTurn)
                {
                    await CheckRaise(5);
                    playerTurn = true;
                }

                if (hasPlayerBankrupted && !hasPlayerFolded)
                {
                    if (playerCallButton.Text.Contains("All in") == false || playerRaiseButton.Text.Contains("All in") == false)
                    {
                        bankruptPlayers.RemoveAt(0);
                        bankruptPlayers.Insert(0, null);
                        maxLeft--;
                        hasPlayerFolded = true;
                    }
                }
            #endregion
                // Check who is all in
                await WhoIsAllIn();

                if (!shouldRestart)
                {
                    await Turns();
                }

                shouldRestart = false;
            }
        }

        private void CurrentTurnPlayerAction()
        {
            CheckCurrentBid(playerStatus, ref playerCall, ref playerRaise, 1);

            timerBar.Visible = true;
            timerBar.Value = 1000;
            t = 60;

            timer.Start();
            playerRaiseButton.Enabled = true;
            playerCallButton.Enabled = true;
            playerRaiseButton.Enabled = true;
            playerRaiseButton.Enabled = true;
            playerFoldButton.Enabled = true;
            turnCount++;
            CheckCurrentBid(playerStatus, ref playerCall, ref playerRaise, 2);
        }

        #region possible hands - Most likely for the player
        //HandRules class
        #endregion

        async Task CheckRaise(int currentTurn)
        {
            if (isRaising)
            {
                turnCount = 0;
                isRaising = false;
                raisedTurn = currentTurn;
                changed = true;
            }

            else
            {
                if (turnCount >= maxLeft - 1 ||
                    !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 ||
                        !changed && turnCount == maxLeft ||
                        raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        callChipsValue = 0;
                        raisedTurn = 123;
                        totalRounds++;

                        if (!hasPlayerBankrupted)
                        {
                            playerStatus.Text = "";
                        }

                        if (!hasBotOneBankrupted)
                        {
                            botOneStatus.Text = "";
                        }

                        if (!hasBotTwoBankrupted)
                        {
                            botTwoStatus.Text = "";
                        }

                        if (!hasBotThreeBankrupted)
                        {
                            botThreeStatus.Text = "";
                        }

                        if (!hasBotFourBankrupted)
                        {
                            botFourStatus.Text = "";
                        }

                        if (!hasBotFiveBankrupted)
                        {
                            botFiveStatus.Text = "";
                        }
                    }
                }
            }

            if (totalRounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (cardsImages[j].Image != Deck[j])
                    {
                        cardsImages[j].Image = Deck[j];
                        playerCall = 0;
                        playerRaise = 0;
                        botOneCall = 0;
                        botOneRaise = 0;
                        botTwoCall = 0;
                        botTwoRaise = 0;
                        botThreeCall = 0;
                        botThreeRaise = 0;
                        botFourCall = 0;
                        botFourRaise = 0;
                        botFiveCall = 0;
                        botFiveRaise = 0;
                    }
                }
            }

            if (totalRounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (cardsImages[j].Image != Deck[j])
                    {
                        cardsImages[j].Image = Deck[j];
                        playerCall = 0; playerRaise = 0;
                        botOneCall = 0; botOneRaise = 0;
                        botTwoCall = 0; botTwoRaise = 0;
                        botThreeCall = 0; botThreeRaise = 0;
                        botFourCall = 0; botFourRaise = 0;
                        botFiveCall = 0; botFiveRaise = 0;
                    }
                }
            }

            if (totalRounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (cardsImages[j].Image != Deck[j])
                    {
                        cardsImages[j].Image = Deck[j];
                        playerCall = 0; playerRaise = 0;
                        botOneCall = 0; botOneRaise = 0;
                        botTwoCall = 0; botTwoRaise = 0;
                        botThreeCall = 0; botThreeRaise = 0;
                        botFourCall = 0; botFourRaise = 0;
                        botFiveCall = 0; botFiveRaise = 0;
                    }
                }
            }

            if (totalRounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    CurrentRules.GameRules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
                }

                if (!botOneStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    CurrentRules.GameRules(2, 3, ref botOneType, ref botOnePower, hasBotOneBankrupted);
                }

                if (!botTwoStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    CurrentRules.GameRules(4, 5, ref botTwoType, ref botTwoPower, hasBotTwoBankrupted);
                }

                if (!botThreeStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    CurrentRules.GameRules(6, 7, ref botThreeType, ref botThreePower, hasBotThreeBankrupted);
                }

                if (!botFourStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    CurrentRules.GameRules(8, 9, ref botFourType, ref botFourPower, hasBotFourBankrupted);
                }

                if (!botFiveStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    CurrentRules.GameRules(10, 11, ref botFiveType, ref botFivePower, hasBotFiveBankrupted);
                }

                Winner1.WinnerRules(playerType, playerPower, "Player", fixedLast);
                Winner1.WinnerRules(botOneType, botOnePower, "Bot 1", fixedLast);
                Winner1.WinnerRules(botTwoType, botTwoPower, "Bot 2", fixedLast);
                Winner1.WinnerRules(botThreeType, botThreePower, "Bot 3", fixedLast);
                Winner1.WinnerRules(botFourType, botFourPower, "Bot 4", fixedLast);
                Winner1.WinnerRules(botFiveType, botFivePower, "Bot 5", fixedLast);

                shouldRestart = true;
                playerTurn = true;
                hasPlayerBankrupted = false;
                hasBotOneBankrupted = false;
                hasBotTwoBankrupted = false;
                hasBotThreeBankrupted = false;
                hasBotFourBankrupted = false;
                hasBotFiveBankrupted = false;

                if (playerChips <= 0)
                {
                    AddChipsWhenLost f2 = new AddChipsWhenLost();
                    f2.ShowDialog();

                    if (f2.AddChipsValue != 0)
                    {
                        playerChips = f2.AddChipsValue;
                        botOnehips += f2.AddChipsValue;
                        botTwoChips += f2.AddChipsValue;
                        botThreeChips += f2.AddChipsValue;
                        botFourChips += f2.AddChipsValue;
                        botFiveChips += f2.AddChipsValue;
                        hasPlayerBankrupted = false;
                        playerTurn = true;
                        playerRaiseButton.Enabled = true;
                        playerFoldButton.Enabled = true;
                        playerCheckButton.Enabled = true;
                        playerRaiseButton.Text = "Raise";
                    }
                }
                playerPanel.Visible = false;
                botOnePanel.Visible = false;
                botTwoPanel.Visible = false;
                botThreePanel.Visible = false;
                botFourPanel.Visible = false;
                botFivePanel.Visible = false;

                playerCall = 0;
                playerRaise = 0;
                botOneCall = 0;
                botOneRaise = 0;
                botTwoCall = 0;
                botTwoRaise = 0;
                botThreeCall = 0;
                botThreeRaise = 0;
                botFourCall = 0;
                botFourRaise = 0;
                botFiveCall = 0;
                botFiveRaise = 0;

                last = 0;
                callChipsValue = bigBlind;
                Raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bankruptPlayers.Clear();
                totalRounds = 0;
                playerPower = 0;
                playerType = -1;
                type = 0;

                botOnePower = 0;
                botTwoPower = 0;
                botThreePower = 0;
                botFourPower = 0;
                botFivePower = 0;

                botOneType = -1;
                botTwoType = -1;
                botThreeType = -1;
                botFourType = -1;
                botFiveType = -1;
                totalAllInChips.Clear();
                CheckWinners.Clear();
                winners = 0;
                winningingHands.Clear();
                sorted.Current = 0;
                sorted.Power = 0;

                for (int os = 0; os < 17; os++)
                {
                    cardsImages[os].Image = null;
                    cardsImages[os].Invalidate();
                    cardsImages[os].Visible = false;
                }

                potTextBox.Text = "0";
                playerStatus.Text = "";

                await Shuffle();
                await Turns();
            }
        }

        void CheckCurrentBid(Label status, ref int chipCall, ref int chipRaise, int options)
        {
            if (totalRounds != LastRound)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        chipRaise = int.Parse(changeRaise);
                    }

                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        chipCall = int.Parse(changeCall);
                    }

                    if (status.Text.Contains("Check"))
                    {
                        chipRaise = 0;
                        chipCall = 0;
                    }

                }

                if (options == 2)
                {
                    //
                    if (chipRaise != Raise && chipRaise <= Raise)
                    {
                        callChipsValue = Convert.ToInt32(Raise) - chipRaise;
                    }

                    if (chipCall != callChipsValue || chipCall <= callChipsValue)
                    {
                        callChipsValue = callChipsValue - chipCall;
                    }

                    if (chipRaise == Raise && Raise > 0)
                    {
                        callChipsValue = 0;
                        playerCallButton.Enabled = false;
                        playerCallButton.Text = "Call";
                    }
                }
            }
        }

        async Task WhoIsAllIn()
        {
            #region All in
            if (playerChips <= 0 && !hasAddedChips)
            {
                totalAllInChips.Add(playerChips);
                hasAddedChips = true;
            }

            hasAddedChips = false;

            if (botOnehips <= 0 && !hasBotOneBankrupted)
            {
                if (!hasAddedChips)
                {
                    totalAllInChips.Add(botOnehips);
                    hasAddedChips = true;
                }

                hasAddedChips = false;
            }

            if (botTwoChips <= 0 && !hasBotTwoBankrupted)
            {
                if (!hasAddedChips)
                {
                    totalAllInChips.Add(botTwoChips);
                    hasAddedChips = true;
                }

                hasAddedChips = false;
            }

            if (botThreeChips <= 0 && !hasBotThreeBankrupted)
            {
                if (!hasAddedChips)
                {
                    totalAllInChips.Add(botThreeChips);
                    hasAddedChips = true;
                }

                hasAddedChips = false;
            }

            if (botFourChips <= 0 && !hasBotFourBankrupted)
            {
                if (!hasAddedChips)
                {
                    totalAllInChips.Add(botFourChips);
                    hasAddedChips = true;
                }

                hasAddedChips = false;
            }

            if (botFiveChips <= 0 && !hasBotFiveBankrupted)
            {
                if (!hasAddedChips)
                {
                    totalAllInChips.Add(botFiveChips);
                    hasAddedChips = true;
                }
            }

            if (totalAllInChips.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                totalAllInChips.Clear();
            }
            #endregion

            int currentWinnerNumber = bankruptPlayers.Count(x => x == false);

            #region LastManStanding
            if (currentWinnerNumber == 1)
            {
                int index = bankruptPlayers.IndexOf(false);
                if (index == 0)
                {
                    playerChips += int.Parse(potTextBox.Text);
                    playerChipsTextBox.Text = playerChips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    botOnehips += int.Parse(potTextBox.Text);
                    botOneChipsTextBox.Text = botOnehips.ToString();
                    botOnePanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    botTwoChips += int.Parse(potTextBox.Text);
                    botTwoChipsTextBox.Text = botTwoChips.ToString();
                    botTwoPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    botThreeChips += int.Parse(potTextBox.Text);
                    botThreeChipsTextBox.Text = botThreeChips.ToString();
                    botThreePanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    botFourChips += int.Parse(potTextBox.Text);
                    botFourChipsTextBox.Text = botFourChips.ToString();
                    botFourPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    botFiveChips += int.Parse(potTextBox.Text);
                    botFiveChipsTextBox.Text = botFiveChips.ToString();
                    botFivePanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int cardNumber = 0; cardNumber < TotalTableCards; cardNumber++)
                {
                    cardsImages[cardNumber].Visible = false;
                }

                await Finish(1);
            }

            hasAddedChips = false;
            #endregion

            #region FiveOrLessLeft
            if (currentWinnerNumber < 6 &&
                currentWinnerNumber > 1 &&
                totalRounds >= End)
            {
                await Finish(2);
            }
            #endregion
        }

        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }

            playerPanel.Visible = false;
            botOnePanel.Visible = false;
            botTwoPanel.Visible = false;
            botThreePanel.Visible = false;
            botFourPanel.Visible = false;
            botFivePanel.Visible = false;

            callChipsValue = bigBlind;
            Raise = 0;
            foldedPlayers = 5;
            type = 0;
            totalRounds = 0;
            botOnePower = 0;
            botTwoPower = 0;
            botThreePower = 0;
            botFourPower = 0;
            botFivePower = 0;
            playerPower = 0;
            playerType = -1;
            Raise = 0;

            botOneType = -1;
            botTwoType = -1;
            botThreeType = -1;
            botFourType = -1;
            botFiveType = -1;

            botOneTurn = false;
            botTwoTurn = false;
            botThreeTurn = false;
            botFourTurn = false;
            botFiveTurn = false;

            hasBotOneBankrupted = false;
            hasBotTwoBankrupted = false;
            hasBotThreeBankrupted = false;
            hasBotFourBankrupted = false;
            hasBotFiveBankrupted = false;

            hasPlayerFolded = false;
            botOneFolded = false;
            botTwoFolded = false;
            botThreeFolded = false;
            botFourFolded = false;
            botFiveFolded = false;

            hasPlayerBankrupted = false;
            playerTurn = true;
            shouldRestart = false;
            isRaising = false;

            playerCall = 0;
            botOneCall = 0;
            botTwoCall = 0;
            botThreeCall = 0;
            botFourCall = 0;
            botFiveCall = 0;

            playerRaise = 0;
            botOneRaise = 0;
            botTwoRaise = 0;
            botThreeRaise = 0;
            botFourRaise = 0;
            botFiveRaise = 0;

            height = 0;
            width = 0;
            winners = 0;

            maxLeft = 6;
            last = 123;
            raisedTurn = 1;

            bankruptPlayers.Clear();
            CheckWinners.Clear();
            totalAllInChips.Clear();
            winningingHands.Clear();

            sorted.Current = 0;
            sorted.Power = 0;
            potTextBox.Text = "0";
            t = 60;
            turnCount = 0;

            playerStatus.Text = "";
            botOneStatus.Text = "";
            botTwoStatus.Text = "";
            botThreeStatus.Text = "";
            botFourStatus.Text = "";
            botFiveStatus.Text = "";

            if (playerChips <= 0)
            {
                AddChipsWhenLost addChipsWhenLost = new AddChipsWhenLost();
                addChipsWhenLost.ShowDialog();

                if (addChipsWhenLost.AddChipsValue != 0)
                {
                    playerChips = addChipsWhenLost.AddChipsValue;
                    botOnehips += addChipsWhenLost.AddChipsValue;
                    botTwoChips += addChipsWhenLost.AddChipsValue;
                    botThreeChips += addChipsWhenLost.AddChipsValue;
                    botFourChips += addChipsWhenLost.AddChipsValue;
                    botFiveChips += addChipsWhenLost.AddChipsValue;

                    hasPlayerBankrupted = false;
                    playerTurn = true;
                    playerRaiseButton.Enabled = true;
                    playerFoldButton.Enabled = true;
                    playerCheckButton.Enabled = true;
                    playerRaiseButton.Text = "Raise";
                }
            }

            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

            for (int cardNumber = 0; cardNumber < 17; cardNumber++)
            {
                cardsImages[cardNumber].Image = null;
                cardsImages[cardNumber].Invalidate();
                cardsImages[cardNumber].Visible = false;
            }

            await Shuffle();
        }

        void FixWinners()
        {
            winningingHands.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = String.Empty;

            if (!playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                CurrentRules.GameRules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
            }

            if (!botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                CurrentRules.GameRules(2, 3, ref botOneType, ref botOnePower, hasBotOneBankrupted);
            }

            if (!botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                CurrentRules.GameRules(4, 5, ref botTwoType, ref botTwoPower, hasBotTwoBankrupted);
            }

            if (!botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                CurrentRules.GameRules(6, 7, ref botThreeType, ref botThreePower, hasBotThreeBankrupted);
            }

            if (!botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                CurrentRules.GameRules(8, 9, ref botFourType, ref botFourPower, hasBotFourBankrupted);
            }

            if (!botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                CurrentRules.GameRules(10, 11, ref botFiveType, ref botFivePower, hasBotFiveBankrupted);
            }

            Winner1.WinnerRules(playerType, playerPower, "Player", fixedLast);

            Winner1.WinnerRules(botOneType, botOnePower, "Bot 1", fixedLast);

            Winner1.WinnerRules(botTwoType, botTwoPower, "Bot 2", fixedLast);

            Winner1.WinnerRules(botThreeType, botThreePower, "Bot 3", fixedLast);

            Winner1.WinnerRules(botFourType, botFourPower, "Bot 4", fixedLast);

            Winner1.WinnerRules(botFiveType, botFivePower, "Bot 5", fixedLast);
        }

        #region second possible hands - bots
        void CheckBotsHand(int botFirstCard, int botSecondCard, ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower, double botCurrent)
        {
            if (!hasBotFolded)
            {
                if (botCurrent == -1)
                {
                    this.CheckBotsHighCard(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 0)
                {
                    PairTable(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 1)
                {
                    this.CheckBotsPairHand(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 2)
                {
                    this.CheckBotsTwoPair(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 3)
                {
                    this.CheckBotsThreeOfAKind(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 4)
                {
                    this.CheckBotsStraight(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    this.CheckBotsFlush(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus);
                }
                if (botCurrent == 6)
                {
                    this.CheckBotsFullHouse(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 7)
                {
                    this.CheckBotsFourOfAKind(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 8 || botCurrent == 9)
                {
                    this.CheckBotsStraightFlush(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
            }

            if (hasBotFolded)
            {
                cardsImages[botFirstCard].Visible = false;
                cardsImages[botSecondCard].Visible = false;
            }
        }

        private void CheckBotsHighCard(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            BotsMoveFirst(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 20, 25);
        }

        private void PairTable(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            BotsMoveFirst(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 16, 25);
        }

        private void CheckBotsPairHand(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);

            if (botPower <= 199 && botPower >= 140)
            {
                BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 9, rRaise);
            }
        }

        private void CheckBotsTwoPair(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);

            if (botPower <= 290 && botPower >= 246)
            {
                BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 4, rRaise);
            }
        }

        private void CheckBotsThreeOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);

            if (botPower <= 390 && botPower >= 330)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }

            if (botPower <= 327 && botPower >= 321)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }

            if (botPower < 321 && botPower >= 303)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }
        }

        private void CheckBotsStraight(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);

            if (botPower <= 480 && botPower >= 410)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }

            if (botPower <= 409 && botPower >= 407)//10  8
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }

            if (botPower < 407 && botPower >= 404)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }
        }

        private void CheckBotsFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fCall);
        }

        private void CheckBotsFullHouse(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);

            if (botPower <= 626 && botPower >= 620)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall);
            }

            if (botPower < 620 && botPower >= 602)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall);
            }
        }

        private void CheckBotsFourOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);

            if (botPower <= 752 && botPower >= 704)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fkCall);
            }
        }

        private void CheckBotsStraightFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sfCall);
            }
        }
        #endregion

        #region check bots possibility

        private void ChangeStatusToFold(ref bool isBotTurn, ref bool sFTurn, Label sStatus)
        {
            isRaising = false;
            sStatus.Text = "Fold";
            isBotTurn = false;
            sFTurn = true;
        }

        //Changes the status label of the bot when it is checking the community cards
        private void ChangeStatusToChecking(ref bool isBotsTurn, Label statusLabel)
        {
            statusLabel.Text = "Check";
            isBotsTurn = false;
            isRaising = false;
        }

        // The bots call. Change status to call
        private void Call(ref int botsChips, ref bool isBotsTurn, Label statusLabel)
        {
            isRaising = false;
            isBotsTurn = false;
            botsChips -= callChipsValue;
            statusLabel.Text = "Call " + callChipsValue;
            potTextBox.Text = (int.Parse(potTextBox.Text) + callChipsValue).ToString();
        }

        private void RaiseBet(ref int botChips, ref bool isBotsTurn, Label statusLabel)
        {
            botChips -= Convert.ToInt32(Raise);
            statusLabel.Text = "Raise " + Raise;
            potTextBox.Text = (int.Parse(potTextBox.Text) + Convert.ToInt32(Raise)).ToString();
            callChipsValue = Convert.ToInt32(Raise);
            isRaising = true;
            isBotsTurn = false;
        }

        //Calculate the maximum amount of money that the bot can play with on this particular turn
        private static double BotMaximumBidAbility(int botChips, int behaviour)
        {
            double maximumBidChips = Math.Round((botChips / behaviour) / 100d, 0) * 100;
            return maximumBidChips;
        }

        private void BotsMoveFirst(ref int botChips, ref bool isBotsTurn, ref bool hasBotFold, Label statusLabel, double safePlay, int n, int riskPlay)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (callChipsValue <= 0)
            {
                ChangeStatusToChecking(ref isBotsTurn, statusLabel);
            }
            if (callChipsValue > 0)
            {
                if (rnd == 1)
                {
                    if (callChipsValue <= BotMaximumBidAbility(botChips, n))
                    {
                        Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
                if (rnd == 2)
                {
                    if (callChipsValue <= BotMaximumBidAbility(botChips, riskPlay))
                    {
                        Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
            }

            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = callChipsValue * 2;
                    RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                }
                else
                {
                    if (Raise <= BotMaximumBidAbility(botChips, n))
                    {
                        Raise = callChipsValue * 2;
                        RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
            }

            if (botChips <= 0)
            {
                hasBotFold = true;
            }
        }

        // checks if it has pair or two pair
        private void BotsMoveSecond(ref int botChips, ref bool isBotsTurn, ref bool botFolds, Label labelStatus, int raiseFactor, int botsPower, int callPower)
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 3);

            if (totalRounds < 2)
            {
                if (callChipsValue <= 0)
                {
                    ChangeStatusToChecking(ref isBotsTurn, labelStatus);
                }

                if (callChipsValue > 0)
                {
                    if (callChipsValue >= BotMaximumBidAbility(botChips, botsPower))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (Raise > BotMaximumBidAbility(botChips, raiseFactor))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor) && callChipsValue <= BotMaximumBidAbility(botChips, botsPower))
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (Raise <= BotMaximumBidAbility(botChips, raiseFactor) && Raise >= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (Raise <= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = BotMaximumBidAbility(botChips, raiseFactor);
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                Raise = callChipsValue * 2;
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }

                    }
                }
            }
            if (totalRounds >= 2)
            {
                if (callChipsValue > 0)
                {
                    if (callChipsValue >= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (Raise > BotMaximumBidAbility(botChips, raiseFactor - randomNumber))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && callChipsValue <= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (Raise <= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && Raise >= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (Raise <= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = BotMaximumBidAbility(botChips, raiseFactor - randomNumber);
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                Raise = callChipsValue * 2;
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }
                    }
                }

                if (callChipsValue <= 0)
                {
                    Raise = BotMaximumBidAbility(botChips, callPower - randomNumber);
                    RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                }
            }

            if (botChips <= 0)
            {
                botFolds = true;
            }
        }

        void BotsMoveThirdPossibility(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, int behaviour)
        {
            Random rand = new Random();

            if (callChipsValue <= 0)
            {
                ChangeStatusToChecking(ref isBotTurn, botStatus);
            }
            else
            {
                if (callChipsValue >= BotMaximumBidAbility(botChips, behaviour))
                {
                    if (botChips > callChipsValue)
                    {
                        Call(ref botChips, ref isBotTurn, botStatus);
                    }
                    else if (botChips <= callChipsValue)
                    {
                        isRaising = false;
                        isBotTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        potTextBox.Text = (int.Parse(potTextBox.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (botChips >= Raise * 2)
                        {
                            Raise *= 2;
                            RaiseBet(ref botChips, ref isBotTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref isBotTurn, botStatus);
                        }
                    }
                    else
                    {
                        Raise = callChipsValue * 2;
                        RaiseBet(ref botChips, ref isBotTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                hasBotFolded = true;
            }
        }
        #endregion

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (timerBar.Value <= 0)
            {
                hasPlayerBankrupted = true;
                await Turns();
            }

            if (t > 0)
            {
                t--;
                timerBar.Value = (t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (playerChips <= 0)
            {
                playerChipsTextBox.Text = "Chips : 0";
            }

            if (botOnehips <= 0)
            {
                botOneChipsTextBox.Text = "Chips : 0";
            }

            if (botTwoChips <= 0)
            {
                botTwoChipsTextBox.Text = "Chips : 0";
            }

            if (botThreeChips <= 0)
            {
                botThreeChipsTextBox.Text = "Chips : 0";
            }

            if (botFourChips <= 0)
            {
                botFourChipsTextBox.Text = "Chips : 0";
            }

            if (botFiveChips <= 0)
            {
                botFiveChipsTextBox.Text = "Chips : 0";
            }

            playerChipsTextBox.Text = "Chips : " + playerChips.ToString();
            botOneChipsTextBox.Text = "Chips : " + botOnehips;
            botTwoChipsTextBox.Text = "Chips : " + botTwoChips;
            botThreeChipsTextBox.Text = "Chips : " + botThreeChips;
            botFourChipsTextBox.Text = "Chips : " + botFourChips;
            botFiveChipsTextBox.Text = "Chips : " + botFiveChips;

            if (playerChips <= 0)
            {
                playerTurn = false;
                hasPlayerBankrupted = true;
                playerCallButton.Enabled = false;
                playerRaiseButton.Enabled = false;
                playerFoldButton.Enabled = false;
                playerCheckButton.Enabled = false;
            }

            if (DefaultMaximumMoney > 0)
            {
                DefaultMaximumMoney--;
            }

            if (playerChips >= callChipsValue)
            {
                playerCallButton.Text = "Call " + callChipsValue.ToString();
            }
            else
            {
                playerCallButton.Text = "All in";
                playerRaiseButton.Enabled = false;
            }

            if (callChipsValue > 0)
            {
                playerCheckButton.Enabled = false;
            }

            if (callChipsValue <= 0)
            {
                playerCheckButton.Enabled = true;
                playerCallButton.Text = "Call";
                playerCallButton.Enabled = false;
            }

            if (playerChips <= 0)
            {
                playerRaiseButton.Enabled = false;
            }

            int parsedValue;

            if (playerRaiseTextBox.Text != "" && int.TryParse(playerRaiseTextBox.Text, out parsedValue))
            {
                if (playerChips <= int.Parse(playerRaiseTextBox.Text))
                {
                    playerRaiseButton.Text = "All in";
                }
                else
                {
                    playerRaiseButton.Text = "Raise";
                }
            }

            if (playerChips < callChipsValue)
            {
                playerRaiseButton.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            playerTurn = false;
            hasPlayerBankrupted = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (callChipsValue <= 0)
            {
                playerTurn = false;
                playerStatus.Text = "Check";
            }
            else
            {
                playerCheckButton.Enabled = false;
            }
            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            CurrentRules.GameRules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
            if (playerChips >= callChipsValue)
            {
                playerChips -= callChipsValue;
                playerChipsTextBox.Text = "Chips : " + playerChips;

                if (potTextBox.Text != "")
                {
                    potTextBox.Text = (int.Parse(potTextBox.Text) + callChipsValue).ToString();
                }
                else
                {
                    potTextBox.Text = callChipsValue.ToString();
                }
                playerTurn = false;
                playerStatus.Text = "Call " + callChipsValue;
                playerCall = callChipsValue;
            }

            else if (playerChips <= callChipsValue && callChipsValue > 0)
            {
                potTextBox.Text = (int.Parse(potTextBox.Text) + playerChips).ToString();
                playerStatus.Text = "All in " + playerChips;
                playerChips = 0;
                playerChipsTextBox.Text = "Chips : " + playerChips;
                playerTurn = false;
                playerFoldButton.Enabled = false;
                playerCall = playerChips;
            }
            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            CurrentRules.GameRules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
            int parsedValue;
            if (playerRaiseTextBox.Text != "" && int.TryParse(playerRaiseTextBox.Text, out parsedValue))
            {
                if (playerChips > callChipsValue)
                {
                    if (Raise * 2 > int.Parse(playerRaiseTextBox.Text))
                    {
                        playerRaiseTextBox.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must Raise atleast twice as the current Raise !");
                        return;
                    }

                    if (playerChips >= int.Parse(playerRaiseTextBox.Text))
                    {
                        callChipsValue = int.Parse(playerRaiseTextBox.Text);
                        Raise = int.Parse(playerRaiseTextBox.Text);
                        playerStatus.Text = "Raise " + callChipsValue;
                        potTextBox.Text = (int.Parse(potTextBox.Text) + callChipsValue).ToString();
                        playerCallButton.Text = "Call";
                        playerChips -= int.Parse(playerRaiseTextBox.Text);
                        isRaising = true;
                        last = 0;
                        playerRaise = Convert.ToInt32(Raise);
                    }
                    else
                    {
                        callChipsValue = playerChips;
                        Raise = playerChips;
                        potTextBox.Text = (int.Parse(potTextBox.Text) + playerChips).ToString();
                        playerStatus.Text = "Raise " + callChipsValue;
                        playerChips = 0;
                        isRaising = true;
                        last = 0;
                        playerRaise = Convert.ToInt32(Raise);
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            playerTurn = false;
            await Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (addChipsTextBox.Text == "") { }
            else
            {
                playerChips += int.Parse(addChipsTextBox.Text);
                botOnehips += int.Parse(addChipsTextBox.Text);
                botTwoChips += int.Parse(addChipsTextBox.Text);
                botThreeChips += int.Parse(addChipsTextBox.Text);
                botFourChips += int.Parse(addChipsTextBox.Text);
                botFiveChips += int.Parse(addChipsTextBox.Text);
            }

            playerChipsTextBox.Text = "Chips : " + playerChips;
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            bigBlindTextBox.Text = bigBlind.ToString();
            smallBlindTextBox.Text = smallBlind.ToString();

            if (bigBlindTextBox.Visible == false)
            {
                bigBlindTextBox.Visible = true;
                smallBlindTextBox.Visible = true;
                bigBlindButton.Visible = true;
                smallBlindButton.Visible = true;
            }
            else
            {
                bigBlindTextBox.Visible = false;
                smallBlindTextBox.Visible = false;
                bigBlindButton.Visible = false;
                smallBlindButton.Visible = false;
            }
        }

        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (smallBlindTextBox.Text.Contains(",") || smallBlindTextBox.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                smallBlindTextBox.Text = smallBlind.ToString();
                return;
            }

            if (!int.TryParse(smallBlindTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                smallBlindTextBox.Text = smallBlind.ToString();
                return;
            }

            if (int.Parse(smallBlindTextBox.Text) > 100000100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                smallBlindTextBox.Text = smallBlind.ToString();
            }

            if (int.Parse(smallBlindTextBox.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(smallBlindTextBox.Text) >= 250
                && int.Parse(smallBlindTextBox.Text) <= 100000)
            {
                smallBlind = int.Parse(smallBlindTextBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (bigBlindTextBox.Text.Contains(",") || bigBlindTextBox.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                bigBlindTextBox.Text = bigBlind.ToString();
                return;
            }
            if (!int.TryParse(smallBlindTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                smallBlindTextBox.Text = bigBlind.ToString();
                return;
            }
            if (int.Parse(bigBlindTextBox.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                bigBlindTextBox.Text = bigBlind.ToString();
            }
            if (int.Parse(bigBlindTextBox.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(bigBlindTextBox.Text) >= 500 && int.Parse(bigBlindTextBox.Text) <= 200000)
            {
                bigBlind = int.Parse(bigBlindTextBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion
    }
}