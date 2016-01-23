using System;
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
        public int callChipsValue = 500;
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
        public double Raise;

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

        public const int Flop = 1;
        public const int Turn = 2;
        public const int River = 3;
        private const int End = 4;
        public int maxLeft = 6;

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
        private readonly CheckRaiseDealer checkRaiseDealer;
        private readonly BotHandsChecker _botHandsChecker;
        private readonly Bot _bot;
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
            checkRaiseDealer = new CheckRaiseDealer(this);
            _botHandsChecker = new BotHandsChecker(this);
            _bot = new Bot(this);
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

        public CheckRaiseDealer CheckRaiseDealer
        {
            get { return checkRaiseDealer; }
        }

        public BotHandsChecker BotHandsChecker
        {
            get { return _botHandsChecker; }
        }

        public Bot Bot
        {
            get { return _bot; }
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

        /// <summary>
        /// Dealer's turns
        /// </summary>
        /// <returns></returns>
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
                        BotHandsChecker.CheckBotsHand(2, 3, ref botOnehips, ref botOneTurn, ref hasBotOneBankrupted, botOneStatus, botOnePower, botOneType);
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
                        BotHandsChecker.CheckBotsHand(4, 5, ref botTwoChips, ref botTwoTurn, ref hasBotTwoBankrupted, botTwoStatus, botTwoPower, botTwoType);

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
                        BotHandsChecker.CheckBotsHand(6, 7, ref botThreeChips, ref botThreeTurn, ref hasBotThreeBankrupted, botThreeStatus, botThreePower, botThreeType);

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
                        BotHandsChecker.CheckBotsHand(8, 9, ref botFourChips, ref botFourTurn, ref hasBotFourBankrupted, botFourStatus, botFourPower, botFourType);
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
                        BotHandsChecker.CheckBotsHand(10, 11, ref botFiveChips, ref botFiveTurn, ref hasBotFiveBankrupted, botFiveStatus, botFivePower, botFiveType);
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
            RaiseButtonInitializer();
            turnCount++;
            CheckCurrentBid(playerStatus, ref playerCall, ref playerRaise, 2);
        }

        private void RaiseButtonInitializer()
        {
            playerRaiseButton.Enabled = true;
            playerCallButton.Enabled = true;
            playerRaiseButton.Enabled = true;
            playerRaiseButton.Enabled = true;
            playerFoldButton.Enabled = true;
        }

        #region possible hands - Most likely for the player
        //HandRules class
        #endregion

        #region CheckRaise Methods
        /// <summary>
        /// Dealer's checkraise
        /// </summary>
        /// <param name="currentTurn"></param>
        /// <returns></returns>
        async Task CheckRaise(int currentTurn)
        {
            CheckRaiseDealer.CheckIfSomeoneRaised(currentTurn);

            CheckRaiseDealer.CheckFlopTurnOrRiver();

            if (totalRounds == End && maxLeft == 6)
            {
                string fixedLast = string.Empty;

                fixedLast = CheckRaiseDealer.CheckPlayerBotsStatus(fixedLast);

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

                CheckRaiseDealer.AddChipsIfLost();

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

        #endregion

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
            await CheckAllIn();
            #endregion

            int currentWinnerNumber = bankruptPlayers.Count(x => x == false);

            #region LastManStanding
            if (currentWinnerNumber == 1)
            {
                CheckWhoWins();

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

        #region WhoIsAllInMethods

        private void CheckWhoWins()
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
        }

        private async Task CheckAllIn()
        {
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
        }
        #endregion

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