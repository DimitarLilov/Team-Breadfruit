using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
        private int foldedPlayers = 5;
        private int TotalTableCards = 17;

        public const int DefaultStartingChips = 10000;
        public const int LastRound = 4;

        private int playerChips = DefaultStartingChips;
        public int botOnehips = DefaultStartingChips;
        public int botTwoChips = DefaultStartingChips;
        public int botThreeChips = DefaultStartingChips;
        public int botFourChips = DefaultStartingChips;
        public int botFiveChips = DefaultStartingChips;


        private double type = 0;
        private double totalRounds = 0;
        private double botOnePower = 0;
        private double botTwoPower = 0;
        private double botThreePower = 0;
        private double botFourPower;
        private double botFivePower;
        private double playerPower = 0;
        private double playerType = -1;
        double Raise = 0;

        private double botOneType = -1;
        private double botTwoType = -1;
        private double botThreeType = -1;
        private double botFourType = -1;
        private double botFiveType = -1; // bots

        private bool botOneTurn = false;
        private bool botTwoTurn = false;
        private bool botThreeTurn = false;
        private bool botFourTurn = false;
        private bool botFiveTurn = false;

        //If the bots folded
        private bool hasBotOneBankrupted = false;
        private bool hasBotTwoBankrupted = false;
        private bool hasBotThreeBankrupted = false;
        private bool hasBotFourBankrupted = false;
        private bool hasBotFiveBankrupted = false;

        private bool hasPlayerFolded;
        private bool botOneFolded;
        private bool botTwoFolded;
        private bool botThreeFolded;
        private bool botFourFolded;
        private bool botFiveFolded;

        private bool hasAddedChips;
        private bool changed;

        private int playerCall = 0;
        private int botOneCall = 0;
        private int botTwoCall = 0;
        private int botThreeCall = 0;
        private int botFourCall = 0;
        private int botFiveCall = 0;

        private int playerRaise = 0;
        private int botOneRaise = 0;
        private int botTwoRaise = 0;
        private int botThreeRaise = 0;
        private int botFourRaise = 0;
        private int botFiveRaise = 0;

        private int height = 0;
        private int width = 0;
        private int winners = 0;

        private const int Flop = 1;
        private const int Turn = 2;
        private const int River = 3;
        private const int End = 4;
        private int maxLeft = 6;

        private int last = 123;
        private int raisedTurn = 1;

        //Lists
        public List<bool?> bankruptPlayers { get; } = new List<bool?>();

        List<Type> winningingHands = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> totalAllInChips = new List<int>();

        private bool hasPlayerBankrupted = false;
        private bool playerTurn = true;
        private bool shouldRestart = false;
        private bool isRaising = false;

        Poker.Type sorted;

        string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/

        // Nothing has changed down here
        int[] cardsAsNumbers = new int[17];
        Image[] Deck = new Image[52];
        PictureBox[] cardsImages = new PictureBox[52];
        Timer timer = new Timer();
        Timer Updates = new Timer();

        private int t = 60;
        //TODO previous name - dealt cards
        private int dealtCards = 0;
        private int i = 0;
        private int bigBlind = 500;
        private int smallBlind = 250;
        private int DefaultMaximumMoney = 10000000;
        private int turnCount = 0;

        #endregion

        #region Form1 main
        public Form1()
        {
            //listOfBooleans.Add(hasPlayerBankrupted); listOfBooleans.Add(hasBotOneBankrupted); listOfBooleans.Add(hasBotTwoBankrupted); listOfBooleans.Add(hasBotThreeBankrupted); listOfBooleans.Add(hasBotFourBankrupted); listOfBooleans.Add(hasBotFiveBankrupted);

            callChipsValue = bigBlind;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            InitializeComponent();
            width = this.Width;
            height = this.Height;

            //Shuffle the cards
            Shuffle();

            // deal out the cards
            potTextBox.Enabled = false;
            playerChipsTextBox.Enabled = false;
            botOneChipsTextBox.Enabled = false;
            botTwoChipsTextBox.Enabled = false;
            botThreeChipsTextBox.Enabled = false;
            botFourChipsTextBox.Enabled = false;
            botFiveChipsTextBox.Enabled = false;
            playerChipsTextBox.Text = "Chips : " + playerChips.ToString();
            botOneChipsTextBox.Text = "Chips : " + botOnehips.ToString();
            botTwoChipsTextBox.Text = "Chips : " + botTwoChips.ToString();
            botThreeChipsTextBox.Text = "Chips : " + botThreeChips.ToString();
            botFourChipsTextBox.Text = "Chips : " + botFourChips.ToString();
            botFiveChipsTextBox.Text = "Chips : " + botFiveChips.ToString();
            timer.Interval = (1 * 1 * 1000);//?
            timer.Tick += timer_Tick;
            Updates.Interval = (1 * 1 * 100);
            Updates.Tick += Update_Tick;
            bigBlindTextBox.Visible = true;
            smallBlindTextBox.Visible = true;
            bigBlindButton.Visible = true;
            smallBlindButton.Visible = true;
            bigBlindTextBox.Visible = true;
            smallBlindTextBox.Visible = true;
            bigBlindButton.Visible = true;
            smallBlindButton.Visible = true;
            bigBlindTextBox.Visible = false;
            smallBlindTextBox.Visible = false;
            bigBlindButton.Visible = false;
            smallBlindButton.Visible = false;
            playerRaiseTextBox.Text = (bigBlind * 2).ToString();
        }
        #endregion

        #region Shuffle
        //TODO straighten up the cohesion and loose the coupling
        async Task Shuffle()
        {
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

            Random randomNumber = new Random();

            //shuffles cards
            ShuffleCards(randomNumber);

            //deals cards
            // dealtCards
            await DealOutCards(horizontal, vertical, isCardOnTheTable, backImage);

            CheckIfGameShouldBeRestarted();

            CheckIfBotsActionsShouldBeEnabled();
        }

        #region Shuffle methods
        private void ShuffleCards(Random random)
        {
            for (int i = ImgLocation.Length; i > 0; i--)
            {
                int nextRandomNumber = random.Next(i);
                var k = ImgLocation[nextRandomNumber];
                ImgLocation[nextRandomNumber] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }
        }

        private void CheckIfBotsActionsShouldBeEnabled()
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

        private void CheckIfGameShouldBeRestarted()
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
                //remove file location and extension
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

                //deal cards
                if (dealtCards < 2)
                {
                    if (cardsImages[0].Tag != null)
                    {
                        cardsImages[1].Tag = cardsAsNumbers[1];
                    }
                    cardsImages[0].Tag = cardsAsNumbers[0];
                    cardsImages[dealtCards].Image = Deck[dealtCards];
                    cardsImages[dealtCards].Anchor = (AnchorStyles.Bottom);
                    //cardsImages[i].Dock = DockStyle.Top;
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
                        //cardsImages[i].Image = Deck[i];
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
                    //checks which player to give cards to
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
                        //cardsImages[i].Image = Deck[i];
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
                    //checks which player to give cards to
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
                        //cardsImages[i].Image = Deck[i];
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
                    if (dealtCards > 12) cardsImages[13].Tag = cardsAsNumbers[13];
                    if (dealtCards > 13) cardsImages[14].Tag = cardsAsNumbers[14];
                    if (dealtCards > 14) cardsImages[15].Tag = cardsAsNumbers[15];
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
                await CheckRaise(0, 0);
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
                        Rules(2, 3, ref botOneType, ref botOnePower, hasBotOneBankrupted);
                        MessageBox.Show("Bot 1's Turn");
                        AI(2, 3, ref botOnehips, ref botOneTurn, ref hasBotOneBankrupted, botOneStatus, botOnePower, botOneType);
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
                    await CheckRaise(1, 1);
                    botTwoTurn = true;
                }
                if (!hasBotTwoBankrupted)
                {
                    if (botTwoTurn)
                    {
                        CheckCurrentBid(botTwoStatus, ref botTwoCall, ref botTwoRaise, 1);
                        CheckCurrentBid(botTwoStatus, ref botTwoCall, ref botTwoRaise, 2);
                        Rules(4, 5, ref botTwoType, ref botTwoPower, hasBotTwoBankrupted);
                        MessageBox.Show("Bot 2's Turn");
                        AI(4, 5, ref botTwoChips, ref botTwoTurn, ref hasBotTwoBankrupted, botTwoStatus, botTwoPower, botTwoType);
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
                    await CheckRaise(2, 2);
                    botThreeTurn = true;
                }
                if (!hasBotThreeBankrupted)
                {
                    if (botThreeTurn)
                    {
                        CheckCurrentBid(botThreeStatus, ref botThreeCall, ref botThreeRaise, 1);
                        CheckCurrentBid(botThreeStatus, ref botThreeCall, ref botThreeRaise, 2);
                        Rules(6, 7, ref botThreeType, ref botThreePower, hasBotThreeBankrupted);
                        MessageBox.Show("Bot 3's Turn");
                        AI(6, 7, ref botThreeChips, ref botThreeTurn, ref hasBotThreeBankrupted, botThreeStatus, botThreePower, botThreeType);
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
                    await CheckRaise(3, 3);
                    botFourTurn = true;
                }
                if (!hasBotFourBankrupted)
                {
                    if (botFourTurn)
                    {
                        CheckCurrentBid(botFourStatus, ref botFourCall, ref botFourRaise, 1);
                        CheckCurrentBid(botFourStatus, ref botFourCall, ref botFourRaise, 2);
                        Rules(8, 9, ref botFourType, ref botFourPower, hasBotFourBankrupted);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, ref botFourChips, ref botFourTurn, ref hasBotFourBankrupted, botFourStatus, botFourPower, botFourType);
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
                    await CheckRaise(4, 4);
                    botFiveTurn = true;
                }
                if (!hasBotFiveBankrupted)
                {
                    if (botFiveTurn)
                    {
                        CheckCurrentBid(botFiveStatus, ref botFiveCall, ref botFiveRaise, 1);
                        CheckCurrentBid(botFiveStatus, ref botFiveCall, ref botFiveRaise, 2);
                        Rules(10, 11, ref botFiveType, ref botFivePower, hasBotFiveBankrupted);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, ref botFiveChips, ref botFiveTurn, ref hasBotFiveBankrupted, botFiveStatus, botFivePower, botFiveType);
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
                    await CheckRaise(5, 5);
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
            //MessageBox.Show("Player's Turn");
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

        void Rules(int cardOne, int cardTwo, ref double current, ref double Power, bool foldedTurn)
        {
            //WTF IS THIS SHIT
            if (cardOne == 0 && cardTwo == 1)
            {
            }
            if (!foldedTurn || cardOne == 0 && cardTwo == 1 && playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] cardsOnTheTable = new int[5];
                int[] currentPlayerAndTableCards = new int[7];

                currentPlayerAndTableCards[0] = cardsAsNumbers[cardOne];
                currentPlayerAndTableCards[1] = cardsAsNumbers[cardTwo];
                currentPlayerAndTableCards[2] = cardsAsNumbers[12];
                currentPlayerAndTableCards[3] = cardsAsNumbers[13];
                currentPlayerAndTableCards[4] = cardsAsNumbers[14];
                currentPlayerAndTableCards[5] = cardsAsNumbers[15];
                currentPlayerAndTableCards[6] = cardsAsNumbers[16];

                cardsOnTheTable[0] = cardsAsNumbers[12];
                cardsOnTheTable[1] = cardsAsNumbers[13];
                cardsOnTheTable[2] = cardsAsNumbers[14];
                cardsOnTheTable[3] = cardsAsNumbers[15];
                cardsOnTheTable[4] = cardsAsNumbers[16];

                var clubs = currentPlayerAndTableCards.Where(o => o % 4 == 0).ToArray();
                var diamonds = currentPlayerAndTableCards.Where(o => o % 4 == 1).ToArray();
                var hearts = currentPlayerAndTableCards.Where(o => o % 4 == 2).ToArray();
                var spades = currentPlayerAndTableCards.Where(o => o % 4 == 3).ToArray();

                var clubsStrenghtValues = clubs.Select(o => o / 4).Distinct().ToArray();
                var diamondsStrenghtValues = diamonds.Select(o => o / 4).Distinct().ToArray();
                var heartsStrenghtValues = hearts.Select(o => o / 4).Distinct().ToArray();
                var spadesStrenghtValues = spades.Select(o => o / 4).Distinct().ToArray();

                Array.Sort(currentPlayerAndTableCards);
                Array.Sort(clubsStrenghtValues);
                Array.Sort(diamondsStrenghtValues);
                Array.Sort(heartsStrenghtValues);
                Array.Sort(spadesStrenghtValues);
                #endregion
                for (int i = 0; i < 16; i++)
                {
                    if (cardsAsNumbers[i] == int.Parse(cardsImages[cardOne].Tag.ToString()) && cardsAsNumbers[i + 1] == int.Parse(cardsImages[cardTwo].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(ref current, ref Power, currentPlayerAndTableCards);
                        #endregion

                        #region Straight current = 4
                        rStraight(ref current, ref Power, currentPlayerAndTableCards);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(ref current, ref Power, ref vf,cardsOnTheTable);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(ref current, ref Power, ref done, currentPlayerAndTableCards);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(ref current, ref Power, currentPlayerAndTableCards);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(ref current, ref Power, clubsStrenghtValues, diamondsStrenghtValues, heartsStrenghtValues, spadesStrenghtValues);
                        #endregion

                        #region High Card current = -1
                        rHighCard(ref current, ref Power);
                        #endregion
                    }
                }
            }
        }

        #region possible hands - Most likely for the player

        private void rStraightFlush(ref double current, ref double Power, int[] clubsStrenghtValues, int[] diamondsStrenghtValues, int[] heartsStrenghtValues, int[] spadesStrenghtValues)
        {
            if (current >= -1)
            {
                if (clubsStrenghtValues.Length >= 5)
                {
                    if (clubsStrenghtValues[0] + 4 == clubsStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, clubsStrenghtValues);
                    }
                    if (clubsStrenghtValues[0] == 0 && clubsStrenghtValues[1] == 9 && clubsStrenghtValues[2] == 10 && clubsStrenghtValues[3] == 11 && clubsStrenghtValues[0] + 12 == clubsStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, clubsStrenghtValues);
                    }
                }
                if (diamondsStrenghtValues.Length >= 5)
                {
                    if (diamondsStrenghtValues[0] + 4 == diamondsStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, diamondsStrenghtValues);
                    }
                    if (diamondsStrenghtValues[0] == 0 && diamondsStrenghtValues[1] == 9 && diamondsStrenghtValues[2] == 10 && diamondsStrenghtValues[3] == 11 && diamondsStrenghtValues[0] + 12 == diamondsStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, diamondsStrenghtValues);
                    }
                }
                if (heartsStrenghtValues.Length >= 5)
                {
                    if (heartsStrenghtValues[0] + 4 == heartsStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, heartsStrenghtValues);
                    }
                    if (heartsStrenghtValues[0] == 0 && heartsStrenghtValues[1] == 9 && heartsStrenghtValues[2] == 10 && heartsStrenghtValues[3] == 11 && heartsStrenghtValues[0] + 12 == heartsStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, heartsStrenghtValues);
                    }
                }
                if (spadesStrenghtValues.Length >= 5)
                {
                    if (spadesStrenghtValues[0] + 4 == spadesStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, spadesStrenghtValues);
                    }
                    if (spadesStrenghtValues[0] == 0 && spadesStrenghtValues[1] == 9 && spadesStrenghtValues[2] == 10 && spadesStrenghtValues[3] == 11 && spadesStrenghtValues[0] + 12 == spadesStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, spadesStrenghtValues);
                    }
                }
            }
        }
        
        private void sortWinningHands(double current, out double Power, int[] strenght)
        {
            Power = (strenght.Max())/4 + current*100;
            winningingHands.Add(new Type() {Power = Power, Current = 8});
            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        }

        private void rFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 7 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 7 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                winningingHands.Add(new Type() { Power = Power, Current = 6 });
                                sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                winningingHands.Add(new Type() { Power = Power, Current = 6 });
                                sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }
                if (current != 6)
                {
                    Power = type;
                }
            }
        }

        private void rFlush(ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var clubs = Straight1.Where(o => o % 4 == 0).ToArray();
                var diamonds = Straight1.Where(o => o % 4 == 1).ToArray();
                var hearts = Straight1.Where(o => o % 4 == 2).ToArray();
                var spades = Straight1.Where(o => o % 4 == 3).ToArray();

                if (clubs.Length == 3 || clubs.Length == 4)
                {
                    if (cardsAsNumbers[i] % 4 == cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == clubs[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true; //vf = very fucked
                        }
                        if (cardsAsNumbers[i + 1] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (cardsAsNumbers[i] / 4 < clubs.Max() / 4 && cardsAsNumbers[i + 1] / 4 < clubs.Max() / 4)
                        {
                            current = 5;
                            Power = clubs.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (clubs.Length == 4)//different cards in hand
                {
                    if (cardsAsNumbers[i] % 4 != cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == clubs[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = clubs.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (cardsAsNumbers[i + 1] % 4 != cardsAsNumbers[i] % 4 && cardsAsNumbers[i + 1] % 4 == clubs[0] % 4)
                    {
                        if (cardsAsNumbers[i + 1] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = clubs.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (clubs.Length == 5)
                {
                    if (cardsAsNumbers[i] % 4 == clubs[0] % 4 && cardsAsNumbers[i] / 4 > clubs.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (cardsAsNumbers[i + 1] % 4 == clubs[0] % 4 && cardsAsNumbers[i + 1] / 4 > clubs.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i + 1] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (cardsAsNumbers[i] / 4 < clubs.Min() / 4 && cardsAsNumbers[i + 1] / 4 < clubs.Min())
                    {
                        current = 5;
                        Power = clubs.Max() + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (diamonds.Length == 3 || diamonds.Length == 4)
                {
                    if (cardsAsNumbers[i] % 4 == cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == diamonds[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (cardsAsNumbers[i + 1] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (cardsAsNumbers[i] / 4 < diamonds.Max() / 4 && cardsAsNumbers[i + 1] / 4 < diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = diamonds.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (diamonds.Length == 4)//different cards in hand
                {
                    if (cardsAsNumbers[i] % 4 != cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == diamonds[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = diamonds.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (cardsAsNumbers[i + 1] % 4 != cardsAsNumbers[i] % 4 && cardsAsNumbers[i + 1] % 4 == diamonds[0] % 4)
                    {
                        if (cardsAsNumbers[i + 1] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = diamonds.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (diamonds.Length == 5)
                {
                    if (cardsAsNumbers[i] % 4 == diamonds[0] % 4 && cardsAsNumbers[i] / 4 > diamonds.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (cardsAsNumbers[i + 1] % 4 == diamonds[0] % 4 && cardsAsNumbers[i + 1] / 4 > diamonds.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i + 1] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (cardsAsNumbers[i] / 4 < diamonds.Min() / 4 && cardsAsNumbers[i + 1] / 4 < diamonds.Min())
                    {
                        current = 5;
                        Power = diamonds.Max() + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (hearts.Length == 3 || hearts.Length == 4)
                {
                    if (cardsAsNumbers[i] % 4 == cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == hearts[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (cardsAsNumbers[i + 1] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (cardsAsNumbers[i] / 4 < hearts.Max() / 4 && cardsAsNumbers[i + 1] / 4 < hearts.Max() / 4)
                        {
                            current = 5;
                            Power = hearts.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (hearts.Length == 4)//different cards in hand
                {
                    if (cardsAsNumbers[i] % 4 != cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == hearts[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = hearts.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (cardsAsNumbers[i + 1] % 4 != cardsAsNumbers[i] % 4 && cardsAsNumbers[i + 1] % 4 == hearts[0] % 4)
                    {
                        if (cardsAsNumbers[i + 1] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = hearts.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (hearts.Length == 5)
                {
                    if (cardsAsNumbers[i] % 4 == hearts[0] % 4 && cardsAsNumbers[i] / 4 > hearts.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (cardsAsNumbers[i + 1] % 4 == hearts[0] % 4 && cardsAsNumbers[i + 1] / 4 > hearts.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i + 1] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (cardsAsNumbers[i] / 4 < hearts.Min() / 4 && cardsAsNumbers[i + 1] / 4 < hearts.Min())
                    {
                        current = 5;
                        Power = hearts.Max() + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (spades.Length == 3 || spades.Length == 4)
                {
                    if (cardsAsNumbers[i] % 4 == cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == spades[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (cardsAsNumbers[i + 1] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (cardsAsNumbers[i] / 4 < spades.Max() / 4 && cardsAsNumbers[i + 1] / 4 < spades.Max() / 4)
                        {
                            current = 5;
                            Power = spades.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (spades.Length == 4)//different cards in hand
                {
                    if (cardsAsNumbers[i] % 4 != cardsAsNumbers[i + 1] % 4 && cardsAsNumbers[i] % 4 == spades[0] % 4)
                    {
                        if (cardsAsNumbers[i] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = spades.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (cardsAsNumbers[i + 1] % 4 != cardsAsNumbers[i] % 4 && cardsAsNumbers[i + 1] % 4 == spades[0] % 4)
                    {
                        if (cardsAsNumbers[i + 1] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = cardsAsNumbers[i + 1] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = spades.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 5 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (spades.Length == 5)
                {
                    if (cardsAsNumbers[i] % 4 == spades[0] % 4 && cardsAsNumbers[i] / 4 > spades.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (cardsAsNumbers[i + 1] % 4 == spades[0] % 4 && cardsAsNumbers[i + 1] / 4 > spades.Min() / 4)
                    {
                        current = 5;
                        Power = cardsAsNumbers[i + 1] + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (cardsAsNumbers[i] / 4 < spades.Min() / 4 && cardsAsNumbers[i + 1] / 4 < spades.Min())
                    {
                        current = 5;
                        Power = spades.Max() + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (clubs.Length > 0)
                {
                    if (cardsAsNumbers[i] / 4 == 0 && cardsAsNumbers[i] % 4 == clubs[0] % 4 && vf && clubs.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsAsNumbers[i + 1] / 4 == 0 && cardsAsNumbers[i + 1] % 4 == clubs[0] % 4 && vf && clubs.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (diamonds.Length > 0)
                {
                    if (cardsAsNumbers[i] / 4 == 0 && cardsAsNumbers[i] % 4 == diamonds[0] % 4 && vf && diamonds.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsAsNumbers[i + 1] / 4 == 0 && cardsAsNumbers[i + 1] % 4 == diamonds[0] % 4 && vf && diamonds.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (hearts.Length > 0)
                {
                    if (cardsAsNumbers[i] / 4 == 0 && cardsAsNumbers[i] % 4 == hearts[0] % 4 && vf && hearts.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsAsNumbers[i + 1] / 4 == 0 && cardsAsNumbers[i + 1] % 4 == hearts[0] % 4 && vf && hearts.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (spades.Length > 0)
                {
                    if (cardsAsNumbers[i] / 4 == 0 && cardsAsNumbers[i] % 4 == spades[0] % 4 && vf && spades.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsAsNumbers[i + 1] / 4 == 0 && cardsAsNumbers[i + 1] % 4 == spades[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 4 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 4 });
                            sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        winningingHands.Add(new Type() { Power = Power, Current = 4 });
                        sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 3 });
                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 3 });
                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (cardsAsNumbers[i] / 4 != cardsAsNumbers[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (cardsAsNumbers[i] / 4 == cardsAsNumbers[tc] / 4 && cardsAsNumbers[i + 1] / 4 == cardsAsNumbers[tc - k] / 4 ||
                                    cardsAsNumbers[i + 1] / 4 == cardsAsNumbers[tc] / 4 && cardsAsNumbers[i] / 4 == cardsAsNumbers[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (cardsAsNumbers[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (cardsAsNumbers[i + 1] / 4) * 2 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (cardsAsNumbers[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (cardsAsNumbers[i] / 4) * 2 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (cardsAsNumbers[i + 1] / 4 != 0 && cardsAsNumbers[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (cardsAsNumbers[i] / 4) * 2 + (cardsAsNumbers[i + 1] / 4) * 2 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (cardsAsNumbers[tc] / 4 == cardsAsNumbers[tc - k] / 4)
                            {
                                if (cardsAsNumbers[tc] / 4 != cardsAsNumbers[i] / 4 && cardsAsNumbers[tc] / 4 != cardsAsNumbers[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (cardsAsNumbers[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (cardsAsNumbers[i] / 4) * 2 + 13 * 4 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (cardsAsNumbers[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (cardsAsNumbers[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (cardsAsNumbers[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (cardsAsNumbers[tc] / 4) * 2 + (cardsAsNumbers[i + 1] / 4) * 2 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (cardsAsNumbers[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (cardsAsNumbers[tc] / 4) * 2 + (cardsAsNumbers[i] / 4) * 2 + current * 100;
                                            winningingHands.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (cardsAsNumbers[i] / 4 > cardsAsNumbers[i + 1] / 4)
                                        {
                                            if (cardsAsNumbers[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + cardsAsNumbers[i] / 4 + current * 100;
                                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = cardsAsNumbers[tc] / 4 + cardsAsNumbers[i] / 4 + current * 100;
                                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (cardsAsNumbers[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + cardsAsNumbers[i + 1] + current * 100;
                                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = cardsAsNumbers[tc] / 4 + cardsAsNumbers[i + 1] / 4 + current * 100;
                                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (cardsAsNumbers[i] / 4 == cardsAsNumbers[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (cardsAsNumbers[i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 1 });
                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (cardsAsNumbers[i + 1] / 4) * 4 + current * 100;
                            winningingHands.Add(new Type() { Power = Power, Current = 1 });
                            sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (cardsAsNumbers[i + 1] / 4 == cardsAsNumbers[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (cardsAsNumbers[i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + cardsAsNumbers[i] / 4 + current * 100;
                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (cardsAsNumbers[i + 1] / 4) * 4 + cardsAsNumbers[i] / 4 + current * 100;
                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (cardsAsNumbers[i] / 4 == cardsAsNumbers[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (cardsAsNumbers[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + cardsAsNumbers[i + 1] / 4 + current * 100;
                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (cardsAsNumbers[tc] / 4) * 4 + cardsAsNumbers[i + 1] / 4 + current * 100;
                                winningingHands.Add(new Type() { Power = Power, Current = 1 });
                                sorted = winningingHands.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (cardsAsNumbers[i] / 4 > cardsAsNumbers[i + 1] / 4)
                {
                    current = -1;
                    Power = cardsAsNumbers[i] / 4;
                    winningingHands.Add(new Type() { Power = Power, Current = -1 });
                    sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = cardsAsNumbers[i + 1] / 4;
                    winningingHands.Add(new Type() { Power = Power, Current = -1 });
                    sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (cardsAsNumbers[i] / 4 == 0 || cardsAsNumbers[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    winningingHands.Add(new Type() { Power = Power, Current = -1 });
                    sorted = winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }
        #endregion

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (cardsImages[j].Visible)
                    cardsImages[j].Image = Deck[j];
            }
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(potTextBox.Text) / winners;
                        playerChipsTextBox.Text = this.playerChips.ToString();
                        //playerPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOnehips += int.Parse(potTextBox.Text) / winners;
                        botOneChipsTextBox.Text = botOnehips.ToString();
                        //botOnePanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwoChips += int.Parse(potTextBox.Text) / winners;
                        botTwoChipsTextBox.Text = botTwoChips.ToString();
                        //botTwoPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThreeChips += int.Parse(potTextBox.Text) / winners;
                        botThreeChipsTextBox.Text = botThreeChips.ToString();
                        //botThreePanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFourChips += int.Parse(potTextBox.Text) / winners;
                        botFourChipsTextBox.Text = botFourChips.ToString();
                        //botFourPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFiveChips += int.Parse(potTextBox.Text) / winners;
                        botFiveChipsTextBox.Text = botFiveChips.ToString();
                        //botFivePanel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //playerPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        botOnehips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //botOnePanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        botTwoChips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //botTwoPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        botThreeChips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //botThreePanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        botFourChips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //botFourPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        botFiveChips += int.Parse(potTextBox.Text);
                        //await Finish(1);
                        //botFivePanel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
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
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        callChipsValue = 0;
                        raisedTurn = 123;
                        totalRounds++;
                        if (!hasPlayerBankrupted)
                            playerStatus.Text = "";
                        if (!hasBotOneBankrupted)
                            botOneStatus.Text = "";
                        if (!hasBotTwoBankrupted)
                            botTwoStatus.Text = "";
                        if (!hasBotThreeBankrupted)
                            botThreeStatus.Text = "";
                        if (!hasBotFourBankrupted)
                            botFourStatus.Text = "";
                        if (!hasBotFiveBankrupted)
                            botFiveStatus.Text = "";
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
                        playerCall = 0; playerRaise = 0;
                        botOneCall = 0; botOneRaise = 0;
                        botTwoCall = 0; botTwoRaise = 0;
                        botThreeCall = 0; botThreeRaise = 0;
                        botFourCall = 0; botFourRaise = 0;
                        botFiveCall = 0; botFiveRaise = 0;
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
                    Rules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
                }
                if (!botOneStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, ref botOneType, ref botOnePower, hasBotOneBankrupted);
                }
                if (!botTwoStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, ref botTwoType, ref botTwoPower, hasBotTwoBankrupted);
                }
                if (!botThreeStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, ref botThreeType, ref botThreePower, hasBotThreeBankrupted);
                }
                if (!botFourStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, ref botFourType, ref botFourPower, hasBotFourBankrupted);
                }
                if (!botFiveStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, ref botFiveType, ref botFivePower, hasBotFiveBankrupted);
                }
                Winner(playerType, playerPower, "Player", playerChips, fixedLast);
                Winner(botOneType, botOnePower, "Bot 1", botOnehips, fixedLast);
                Winner(botTwoType, botTwoPower, "Bot 2", botTwoChips, fixedLast);
                Winner(botThreeType, botThreePower, "Bot 3", botThreeChips, fixedLast);
                Winner(botFourType, botFourPower, "Bot 4", botFourChips, fixedLast);
                Winner(botFiveType, botFivePower, "Bot 5", botFiveChips, fixedLast);
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
                playerPanel.Visible = false; botOnePanel.Visible = false; botTwoPanel.Visible = false; botThreePanel.Visible = false; botFourPanel.Visible = false; botFivePanel.Visible = false;
                playerCall = 0; playerRaise = 0;
                botOneCall = 0; botOneRaise = 0;
                botTwoCall = 0; botTwoRaise = 0;
                botThreeCall = 0; botThreeRaise = 0;
                botFourCall = 0; botFourRaise = 0;
                botFiveCall = 0; botFiveRaise = 0;
                last = 0;
                callChipsValue = bigBlind;
                Raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bankruptPlayers.Clear();
                totalRounds = 0;
                playerPower = 0; playerType = -1;
                type = 0; botOnePower = 0; botTwoPower = 0; botThreePower = 0; botFourPower = 0; botFivePower = 0;
                botOneType = -1; botTwoType = -1; botThreeType = -1; botFourType = -1; botFiveType = -1;
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


                    //changeRaise is the number as a string in the Raise text box
                    //chipRaise is the same number parsed as a int
                    //same goes for call
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
                //plashta se razlikata

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

        /// <summary>
        /// TODO:Split in methods
        /// </summary>
        /// <returns></returns>
        async Task WhoIsAllIn()
        {
            #region All in
            if (playerChips <= 0 && !hasAddedChips)
            {
                //if (playerStatus.Text.Contains("Raise"))
                //{
                totalAllInChips.Add(playerChips);
                hasAddedChips = true;
                //}
                //if (playerStatus.Text.Contains("Call"))
                //{
                //    totalAllInChips.Add(playerChips);
                //    hasAddedChips = true;
                //}
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
            if (currentWinnerNumber < 6 && currentWinnerNumber > 1 && totalRounds >= End)
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
            botOnePower = 0; botTwoPower = 0;
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

            height = 0; width = 0; winners = 0;
            //Flop = 1;
            //Turn = 2;
            //River = 3;
            //End = 4;
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
            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                cardsImages[os].Image = null;
                cardsImages[os].Invalidate();
                cardsImages[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
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
                Rules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
            }

            if (!botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, ref botOneType, ref botOnePower, hasBotOneBankrupted);
            }

            if (!botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, ref botTwoType, ref botTwoPower, hasBotTwoBankrupted);
            }

            if (!botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, ref botThreeType, ref botThreePower, hasBotThreeBankrupted);
            }

            if (!botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, ref botFourType, ref botFourPower, hasBotFourBankrupted);
            }

            if (!botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, ref botFiveType, ref botFivePower, hasBotFiveBankrupted);
            }

            Winner(playerType, playerPower, "Player", playerChips, fixedLast);
            Winner(botOneType, botOnePower, "Bot 1", botOnehips, fixedLast);
            Winner(botTwoType, botTwoPower, "Bot 2", botTwoChips, fixedLast);
            Winner(botThreeType, botThreePower, "Bot 3", botThreeChips, fixedLast);
            Winner(botFourType, botFourPower, "Bot 4", botFourChips, fixedLast);
            Winner(botFiveType, botFivePower, "Bot 5", botFiveChips, fixedLast);
        }

        #region second possible hands - most likely for the bots (randoms)
        void AI(int c1, int c2, ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower, double botCurrent)
        {
            if (!hasBotFolded)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    PairTable(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    PairHand(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    TwoPair(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 4)
                {
                    Straight(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 6)
                {
                    FullHouse(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 7)
                {
                    FourOfAKind(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
            }
            if (hasBotFolded)
            {
                cardsImages[c1].Visible = false;
                cardsImages[c2].Visible = false;
            }
        }

        private void HighCard(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            BotsMoveFirst(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 20, 25);
        }

        private void PairTable(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            BotsMoveFirst(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 16, 25);
        }

        private void PairHand(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
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

        private void TwoPair(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
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

        private void ThreeOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall, tRaise);
            }
            if (botPower <= 327 && botPower >= 321)//10  8
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303)//7 2
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall, tRaise);
            }
        }

        private void Straight(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall, sRaise);
            }
            if (botPower <= 409 && botPower >= 407)//10  8
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall, sRaise);
            }
            if (botPower < 407 && botPower >= 404)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall, sRaise);
            }
        }

        private void Flush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fCall, fRaise);
        }

        private void FullHouse(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall, fhRaise);
            }
            if (botPower < 620 && botPower >= 602)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall, fhRaise);
            }
        }

        private void FourOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fkCall, fkRaise);
            }
        }

        private void StraightFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sfCall, sfRaise);
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

        // The bots call // Change status to call
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

        void BotsMoveThirdPossibility(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, int behaviour, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);

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
                //playerStatus.Text = "All in " + Chips;

                playerCheckButton.Enabled = false;
            }
            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
            if (playerChips >= callChipsValue)
            {
                playerChips -= callChipsValue;
                playerChipsTextBox.Text = "Chips : " + playerChips.ToString();
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
                playerChipsTextBox.Text = "Chips : " + playerChips.ToString();
                playerTurn = false;
                playerFoldButton.Enabled = false;
                playerCall = playerChips;
            }
            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, ref playerType, ref playerPower, hasPlayerBankrupted);
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
                    else
                    {
                        if (playerChips >= int.Parse(playerRaiseTextBox.Text))
                        {
                            callChipsValue = int.Parse(playerRaiseTextBox.Text);
                            Raise = int.Parse(playerRaiseTextBox.Text);
                            playerStatus.Text = "Raise " + callChipsValue.ToString();
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
                            playerStatus.Text = "Raise " + callChipsValue.ToString();
                            playerChips = 0;
                            isRaising = true;
                            last = 0;
                            playerRaise = Convert.ToInt32(Raise);
                        }
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
            playerChipsTextBox.Text = "Chips : " + playerChips.ToString();
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
            if (int.Parse(smallBlindTextBox.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                smallBlindTextBox.Text = smallBlind.ToString();
            }
            if (int.Parse(smallBlindTextBox.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(smallBlindTextBox.Text) >= 250 && int.Parse(smallBlindTextBox.Text) <= 100000)
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