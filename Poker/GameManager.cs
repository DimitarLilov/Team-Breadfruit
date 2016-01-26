namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Models;
    using Poker.Models.Players;
    using Poker.Models.Players.Bot;
    using Poker.Models.Rules;

    public partial class GameManager : Form
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
        public readonly Winner winner;
        private readonly Dealer checkRaiseDealer;
        private readonly Bot bot;

        // public readonly Dealer dealer;

        #endregion

        #region GameManager main
        public GameManager()
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
            this.currentRules = new Rules(this);
            this.winner = new Winner(this);
            this.checkRaiseDealer = new Dealer(this);
            bot = new Bot(this);
        }

        public Rules CurrentRules
        {
            get { return this.currentRules; }
        }

        

        public Winner Winner1
        {
            get { return winner; }
        }

        public Dealer CheckRaiseDealer
        {
            get { return checkRaiseDealer; }
        }

       

        public Bot Bot
        {
            get { return bot; }
        }

        public Winner Winner { get; set; }

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

        /// <summary>
        /// This method rearranges the cards' positions in random orders.
        /// </summary>
        /// <param name="random">Add a random generator variable.</param>
        public void ShuffleCards(Random random)
        {
            int cardsCount = ImgLocation.Length;
            for (int rearrangesCount = cardsCount; rearrangesCount > 0; rearrangesCount--)
            {
                int nextRandomNumber = random.Next(rearrangesCount);
                var saveRandomCardNumber = ImgLocation[nextRandomNumber];

                //Both cards switch places.
                ImgLocation[nextRandomNumber] = ImgLocation[rearrangesCount - 1];
                ImgLocation[rearrangesCount - 1] = saveRandomCardNumber;
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
               
                GetCardNumber();

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

        private void GetCardNumber()
        {
            Deck[dealtCards] = Image.FromFile(ImgLocation[dealtCards]);
            var charsToRemove = new string[] {"Assets\\Cards\\", ".png"};
            foreach (var c in charsToRemove)
            {
                ImgLocation[dealtCards] = ImgLocation[dealtCards].Replace(c, string.Empty);
            }
        }

        #endregion

        #endregion

        #region Turns
        async Task Turns()
        {
            #region Rotating
            if (!this.hasPlayerBankrupted && this.playerTurn)
            {
                this.CurrentTurnPlayerAction();
            }

            if (this.hasPlayerBankrupted || !this.playerTurn)
            {
                await WhoIsAllIn();

                if (this.hasPlayerBankrupted && !this.hasPlayerFolded)
                {
                    if (this.playerCallButton.Text.Contains("All in") == false || this.playerRaiseButton.Text.Contains("All in") == false)
                    {
                        this.bankruptPlayers.RemoveAt(0);
                        this.bankruptPlayers.Insert(0, null);
                        this.maxLeft--;
                        this.hasPlayerFolded = true;
                    }
                }

                await this.CheckRaise(0);

                this.timerBar.Visible = false;
                this.playerRaiseButton.Enabled = false;
                this.playerCallButton.Enabled = false;
                this.playerRaiseButton.Enabled = false;
                this.playerRaiseButton.Enabled = false;
                this.playerFoldButton.Enabled = false;

                this.timer.Stop();

                this.botOneTurn = true;

                if (!this.hasBotOneBankrupted)
                {
                    if (this.botOneTurn)
                    {
                        this.CheckCurrentBid(this.botOneStatus, ref this.botOneCall, ref this.botOneRaise, 1);
                        this.CheckCurrentBid(this.botOneStatus, ref this.botOneCall, ref this.botOneRaise, 2);
                        this.CurrentRules.GameRules(2, 3, ref this.botOneType, ref this.botOnePower, this.hasBotOneBankrupted);
                        MessageBox.Show(@"Bot 1's Turn");
                        this.Bot.CheckBotsHand(2, 3, ref this.botOnehips, ref this.botOneTurn, ref this.hasBotOneBankrupted, this.botOneStatus, this.botOnePower, this.botOneType);
                        this.turnCount++;
                        this.last = 1;
                        this.botOneTurn = false;
                        this.botTwoTurn = true;
                    }
                }

                if (this.hasBotOneBankrupted && !this.botOneFolded)
                {
                    this.bankruptPlayers.RemoveAt(1);
                    this.bankruptPlayers.Insert(1, null);
                    this.maxLeft--;
                    this.botOneFolded = true;
                }

                if (this.hasBotOneBankrupted || !this.botOneTurn)
                {
                    await this.CheckRaise(1);
                    this.botTwoTurn = true;
                }

                if (!this.hasBotTwoBankrupted)
                {
                    if (this.botTwoTurn)
                    {
                        this.CheckCurrentBid(this.botTwoStatus, ref this.botTwoCall, ref this.botTwoRaise, 1);
                        this.CheckCurrentBid(this.botTwoStatus, ref this.botTwoCall, ref this.botTwoRaise, 2);
                        this.CurrentRules.GameRules(4, 5, ref this.botTwoType, ref this.botTwoPower, this.hasBotTwoBankrupted);

                        MessageBox.Show(@"Bot 2's Turn");
                        this.Bot.CheckBotsHand(4, 5, ref this.botTwoChips, ref this.botTwoTurn, ref this.hasBotTwoBankrupted, this.botTwoStatus, this.botTwoPower, this.botTwoType);

                        this.turnCount++;
                        this.last = 2;
                        this.botTwoTurn = false;
                        this.botThreeTurn = true;
                    }
                }

                if (this.hasBotTwoBankrupted && !this.botTwoFolded)
                {
                    this.bankruptPlayers.RemoveAt(2);
                    this.bankruptPlayers.Insert(2, null);

                    this.maxLeft--;
                    this.botTwoFolded = true;
                }

                if (this.hasBotTwoBankrupted || !this.botTwoTurn)
                {
                    await this.CheckRaise(2);
                    this.botThreeTurn = true;
                }

                if (!this.hasBotThreeBankrupted)
                {
                    if (this.botThreeTurn)
                    {
                        this.CheckCurrentBid(this.botThreeStatus, ref this.botThreeCall, ref this.botThreeRaise, 1);
                        this.CheckCurrentBid(this.botThreeStatus, ref this.botThreeCall, ref this.botThreeRaise, 2);
                        this.CurrentRules.GameRules(6, 7, ref this.botThreeType, ref this.botThreePower, this.hasBotThreeBankrupted);
                        MessageBox.Show(@"Bot 3's Turn");
                        this.Bot.CheckBotsHand(6, 7, ref this.botThreeChips, ref this.botThreeTurn, ref this.hasBotThreeBankrupted, this.botThreeStatus, this.botThreePower, this.botThreeType);

                        this.turnCount++;
                        this.last = 3;
                        this.botThreeTurn = false;
                        this.botFourTurn = true;
                    }
                }

                if (this.hasBotThreeBankrupted && !this.botThreeFolded)
                {
                    this.bankruptPlayers.RemoveAt(3);
                    this.bankruptPlayers.Insert(3, null);
                    this.maxLeft--;
                    this.botThreeFolded = true;
                }

                if (this.hasBotThreeBankrupted || !this.botThreeTurn)
                {
                    await this.CheckRaise(3);
                    this.botFourTurn = true;
                }

                if (!this.hasBotFourBankrupted)
                {
                    if (this.botFourTurn)
                    {
                        this.CheckCurrentBid(this.botFourStatus, ref this.botFourCall, ref this.botFourRaise, 1);
                        this.CheckCurrentBid(this.botFourStatus, ref this.botFourCall, ref this.botFourRaise, 2);
                        this.CurrentRules.GameRules(8, 9, ref this.botFourType, ref this.botFourPower, this.hasBotFourBankrupted);
                        MessageBox.Show(@"Bot 4's Turn");
                        this.Bot.CheckBotsHand(8, 9, ref this.botFourChips, ref this.botFourTurn, ref this.hasBotFourBankrupted, this.botFourStatus, this.botFourPower, this.botFourType);
                        this.turnCount++;
                        this.last = 4;
                        this.botFourTurn = false;
                        this.botFiveTurn = true;
                    }
                }

                if (this.hasBotFourBankrupted && !this.botFourFolded)
                {
                    this.bankruptPlayers.RemoveAt(4);
                    this.bankruptPlayers.Insert(4, null);
                    this.maxLeft--;
                    this.botFourFolded = true;
                }


                if (this.hasBotFourBankrupted || !this.botFourTurn)
                {
                    await this.CheckRaise(4);
                    this.botFiveTurn = true;
                }

                if (!this.hasBotFiveBankrupted)
                {
                    if (this.botFiveTurn)
                    {
                        this.CheckCurrentBid(this.botFiveStatus, ref this.botFiveCall, ref this.botFiveRaise, 1);
                        this.CheckCurrentBid(this.botFiveStatus, ref this.botFiveCall, ref this.botFiveRaise, 2);
                        this.CurrentRules.GameRules(10, 11, ref this.botFiveType, ref this.botFivePower, this.hasBotFiveBankrupted);
                        MessageBox.Show(@"Bot 5's Turn");
                        this.Bot.CheckBotsHand(10, 11, ref this.botFiveChips, ref this.botFiveTurn, ref this.hasBotFiveBankrupted, this.botFiveStatus, this.botFivePower, this.botFiveType);
                        this.turnCount++;
                        this.last = 5;
                        this.botFiveTurn = false;
                    }
                }

                if (this.hasBotFiveBankrupted && !this.botFiveFolded)
                {
                    this.bankruptPlayers.RemoveAt(5);
                    this.bankruptPlayers.Insert(5, null);
                    this.maxLeft--;
                    this.botFiveFolded = true;
                }

                if (this.hasBotFiveBankrupted || !this.botFiveTurn)
                {
                    await this.CheckRaise(5);
                    this.playerTurn = true;
                }

                if (this.hasPlayerBankrupted && !this.hasPlayerFolded)
                {
                    if (this.playerCallButton.Text.Contains("All in") == false || this.playerRaiseButton.Text.Contains("All in") == false)
                    {
                        this.bankruptPlayers.RemoveAt(0);
                        this.bankruptPlayers.Insert(0, null);
                        this.maxLeft--;
                        this.hasPlayerFolded = true;
                    }
                }
                #endregion
                // Check who is all in
                await this.WhoIsAllIn();

                if (!this.shouldRestart)
                {
                    await this.Turns();
                }

                this.shouldRestart = false;
            }
        }

        private void CurrentTurnPlayerAction()
        {
            this.CheckCurrentBid(this.playerStatus, ref this.playerCall, ref this.playerRaise, 1);

            this.timerBar.Visible = true;
            this.timerBar.Value = 1000;
            this.t = 60;

            this.timer.Start();
            this.RaiseButtonInitializer();
            this.turnCount++;
            this.CheckCurrentBid(this.playerStatus, ref this.playerCall, ref this.playerRaise, 2);
        }

        private void RaiseButtonInitializer()
        {
            this.playerRaiseButton.Enabled = true;
            this.playerCallButton.Enabled = true;
            this.playerRaiseButton.Enabled = true;
            this.playerRaiseButton.Enabled = true;
            this.playerFoldButton.Enabled = true;
        }

        #region CheckRaise Methods

        async Task CheckRaise(int currentTurn)
        {
            this.CheckRaiseDealer.CheckIfSomeoneRaised(currentTurn);

            this.CheckRaiseDealer.CheckFlopTurnOrRiver();

            if (this.totalRounds == End && this.maxLeft == 6)
            {
                string fixedLast = string.Empty;

                fixedLast = this.CheckRaiseDealer.CheckPlayerBotsStatus(fixedLast);

                this.Winner1.WinnerRules(this.playerType, this.playerPower, "Player", fixedLast);
                this.Winner1.WinnerRules(this.botOneType, this.botOnePower, "Bot 1", fixedLast);
                this.Winner1.WinnerRules(this.botTwoType, this.botTwoPower, "Bot 2", fixedLast);
                this.Winner1.WinnerRules(this.botThreeType, this.botThreePower, "Bot 3", fixedLast);
                this.Winner1.WinnerRules(this.botFourType, this.botFourPower, "Bot 4", fixedLast);
                this.Winner1.WinnerRules(this.botFiveType, this.botFivePower, "Bot 5", fixedLast);

                this.shouldRestart = true;
                this.playerTurn = true;

                this.hasPlayerBankrupted = false;
                this.hasBotOneBankrupted = false;
                this.hasBotTwoBankrupted = false;
                this.hasBotThreeBankrupted = false;
                this.hasBotFourBankrupted = false;
                this.hasBotFiveBankrupted = false;

                this.CheckRaiseDealer.AddChipsIfLost();

                this.playerPanel.Visible = false;
                this.botOnePanel.Visible = false;
                this.botTwoPanel.Visible = false;
                this.botThreePanel.Visible = false;
                this.botFourPanel.Visible = false;
                this.botFivePanel.Visible = false;

                this.playerCall = 0;
                this.playerRaise = 0;
                this.botOneCall = 0;
                this.botOneRaise = 0;
                this.botTwoCall = 0;
                this.botTwoRaise = 0;
                this.botThreeCall = 0;
                this.botThreeRaise = 0;
                this.botFourCall = 0;
                this.botFourRaise = 0;
                this.botFiveCall = 0;
                this.botFiveRaise = 0;

                this.last = 0;
                this.callChipsValue = this.bigBlind;
                this.Raise = 0;
                this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.bankruptPlayers.Clear();
                this.totalRounds = 0;
                this.playerPower = 0;
                this.playerType = -1;
                this.type = 0;

                this.botOnePower = 0;
                this.botTwoPower = 0;
                this.botThreePower = 0;
                this.botFourPower = 0;
                this.botFivePower = 0;

                this.botOneType = -1;
                this.botTwoType = -1;
                this.botThreeType = -1;
                this.botFourType = -1;
                this.botFiveType = -1;

                this.totalAllInChips.Clear();

                this.CheckWinners.Clear();

                this.winners = 0;
                this.winningingHands.Clear();
                this.sorted.Current = 0;
                this.sorted.Power = 0;

                for (int os = 0; os < 17; os++)
                {
                    this.cardsImages[os].Image = null;
                    this.cardsImages[os].Invalidate();
                    this.cardsImages[os].Visible = false;
                }

                this.potTextBox.Text = "0";
                this.playerStatus.Text = "";

                await this.Shuffle();
                await this.Turns();
            }
        }

        #endregion

        public void CheckCurrentBid(Label status, ref int chipCall, ref int chipRaise, int options)
        {
            if (this.totalRounds != LastRound)
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
                    if (chipRaise != this.Raise && chipRaise <= this.Raise)
                    {
                        this.callChipsValue = Convert.ToInt32(this.Raise) - chipRaise;
                    }

                    if (chipCall != this.callChipsValue || chipCall <= this.callChipsValue)
                    {
                        this.callChipsValue = this.callChipsValue - chipCall;
                    }

                    if (chipRaise == this.Raise && this.Raise > 0)
                    {
                        this.callChipsValue = 0;
                        this.playerCallButton.Enabled = false;
                        this.playerCallButton.Text = @"Call";
                    }
                }
            }
        }

        #endregion

        #region CheckWhoIsAllIn
        async Task WhoIsAllIn()
        {
            #region All in
            await CheckAllIn();
            #endregion

            int currentWinnerNumber = bankruptPlayers.Count(x => x == false);

            #region LastManStanding
            if (currentWinnerNumber == 1)
            {
                this.CheckWhoWins();

                for (int cardNumber = 0; cardNumber < this.TotalTableCards; cardNumber++)
                {
                    this.cardsImages[cardNumber].Visible = false;
                }

                await this.Finish(1);
            }

            this.hasAddedChips = false;
            #endregion

            #region FiveOrLessLeft
            if (currentWinnerNumber < 6 &&
                currentWinnerNumber > 1 && this.totalRounds >= End)
            {
                await this.Finish(2);
            }
            #endregion
        }

        #region WhoIsAllInMethods

        private void CheckWhoWins()
        {
            int index = this.bankruptPlayers.IndexOf(false);
            if (index == 0)
            {
                this.playerChips += int.Parse(this.potTextBox.Text);
                this.playerChipsTextBox.Text = this.playerChips.ToString();
                this.playerPanel.Visible = true;
                MessageBox.Show(@"Player Wins");
            }

            if (index == 1)
            {
                this.botOnehips += int.Parse(this.potTextBox.Text);
                this.botOneChipsTextBox.Text = this.botOnehips.ToString();
                this.botOnePanel.Visible = true;
                MessageBox.Show(@"Bot 1 Wins");
            }

            if (index == 2)
            {
                this.botTwoChips += int.Parse(this.potTextBox.Text);
                this.botTwoChipsTextBox.Text = this.botTwoChips.ToString();
                this.botTwoPanel.Visible = true;
                MessageBox.Show("Bot 2 Wins");
            }

            if (index == 3)
            {
                this.botThreeChips += int.Parse(this.potTextBox.Text);
                this.botThreeChipsTextBox.Text = this.botThreeChips.ToString();
                this.botThreePanel.Visible = true;
                MessageBox.Show(@"Bot 3 Wins");
            }

            if (index == 4)
            {
                this.botFourChips += int.Parse(this.potTextBox.Text);
                this.botFourChipsTextBox.Text = this.botFourChips.ToString();
                this.botFourPanel.Visible = true;
                MessageBox.Show(@"Bot 4 Wins");
            }

            if (index == 5)
            {
                this.botFiveChips += int.Parse(this.potTextBox.Text);
                this.botFiveChipsTextBox.Text = this.botFiveChips.ToString();
                this.botFivePanel.Visible = true;
                MessageBox.Show(@"Bot 5 Wins");
            }
        }

        private async Task CheckAllIn()
        {
            if (this.playerChips <= 0 && !this.hasAddedChips)
            {
                this.totalAllInChips.Add(this.playerChips);
                this.hasAddedChips = true;
            }

            this.hasAddedChips = false;

            if (this.botOnehips <= 0 && !this.hasBotOneBankrupted)
            {
                if (!this.hasAddedChips)
                {
                    this.totalAllInChips.Add(this.botOnehips);
                    this.hasAddedChips = true;
                }

                this.hasAddedChips = false;
            }

            if (this.botTwoChips <= 0 && !this.hasBotTwoBankrupted)
            {
                if (!this.hasAddedChips)
                {
                    this.totalAllInChips.Add(this.botTwoChips);
                    this.hasAddedChips = true;
                }

                this.hasAddedChips = false;
            }

            if (this.botThreeChips <= 0 && !this.hasBotThreeBankrupted)
            {
                if (!this.hasAddedChips)
                {
                    this.totalAllInChips.Add(this.botThreeChips);
                    this.hasAddedChips = true;
                }

                this.hasAddedChips = false;
            }

            if (this.botFourChips <= 0 && !this.hasBotFourBankrupted)
            {
                if (!this.hasAddedChips)
                {
                    this.totalAllInChips.Add(this.botFourChips);
                    this.hasAddedChips = true;
                }

                this.hasAddedChips = false;
            }

            if (this.botFiveChips <= 0 && !this.hasBotFiveBankrupted)
            {
                if (!this.hasAddedChips)
                {
                    this.totalAllInChips.Add(this.botFiveChips);
                    this.hasAddedChips = true;
                }
            }

            if (this.totalAllInChips.ToArray().Length == maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.totalAllInChips.Clear();
            }
        }
        #endregion
        #endregion

        #region Finish
        async Task Finish(int n)
        {
            if (n == 2)
            {
                this.winner.FixWinners();
            }

            this.playerPanel.Visible = false;
            this.botOnePanel.Visible = false;
            this.botTwoPanel.Visible = false;
            this.botThreePanel.Visible = false;
            this.botFourPanel.Visible = false;
            this.botFivePanel.Visible = false;

            this.callChipsValue = this.bigBlind;
            this.Raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.totalRounds = 0;
            this.botOnePower = 0;
            this.botTwoPower = 0;
            this.botThreePower = 0;
            this.botFourPower = 0;
            this.botFivePower = 0;
            this.playerPower = 0;
            this.playerType = -1;
            this.Raise = 0;

            this.botOneType = -1;
            this.botTwoType = -1;
            this.botThreeType = -1;
            this.botFourType = -1;
            this.botFiveType = -1;

            this.botOneTurn = false;
            this.botTwoTurn = false;
            this.botThreeTurn = false;
            this.botFourTurn = false;
            this.botFiveTurn = false;

            this.hasBotOneBankrupted = false;
            this.hasBotTwoBankrupted = false;
            this.hasBotThreeBankrupted = false;
            this.hasBotFourBankrupted = false;
            this.hasBotFiveBankrupted = false;

            this.hasPlayerFolded = false;
            this.botOneFolded = false;
            this.botTwoFolded = false;
            this.botThreeFolded = false;
            this.botFourFolded = false;
            this.botFiveFolded = false;

            this.hasPlayerBankrupted = false;
            this.playerTurn = true;
            this.shouldRestart = false;
            this.isRaising = false;

            this.playerCall = 0;
            this.botOneCall = 0;
            this.botTwoCall = 0;
            this.botThreeCall = 0;
            this.botFourCall = 0;
            this.botFiveCall = 0;

            this.playerRaise = 0;
            this.botOneRaise = 0;
            this.botTwoRaise = 0;
            this.botThreeRaise = 0;
            this.botFourRaise = 0;
            this.botFiveRaise = 0;

            this.height = 0;
            this.width = 0;
            this.winners = 0;

            this.maxLeft = 6;
            this.last = 0;
            this.raisedTurn = 1;

            this.bankruptPlayers.Clear();
            this.CheckWinners.Clear();
            this.totalAllInChips.Clear();
            this.winningingHands.Clear();

            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.potTextBox.Text = "0";
            this.t = 60;
            this.turnCount = 0;

            this.playerStatus.Text = "";
            this.botOneStatus.Text = "";
            this.botTwoStatus.Text = "";
            this.botThreeStatus.Text = "";
            this.botFourStatus.Text = "";
            this.botFiveStatus.Text = "";

            if (this.playerChips <= 0)
            {
                AddChipsWhenLost addChipsWhenLost = new AddChipsWhenLost();
                addChipsWhenLost.ShowDialog();

                if (addChipsWhenLost.AddChipsValue != 0)
                {
                    this.playerChips = addChipsWhenLost.AddChipsValue;
                    this.botOnehips += addChipsWhenLost.AddChipsValue;
                    this.botTwoChips += addChipsWhenLost.AddChipsValue;
                    this.botThreeChips += addChipsWhenLost.AddChipsValue;
                    this.botFourChips += addChipsWhenLost.AddChipsValue;
                    this.botFiveChips += addChipsWhenLost.AddChipsValue;

                    this.hasPlayerBankrupted = false;
                    this.playerTurn = true;
                    this.playerRaiseButton.Enabled = true;
                    this.playerFoldButton.Enabled = true;
                    this.playerCheckButton.Enabled = true;
                    this.playerRaiseButton.Text = @"Raise";
                }
            }

            this.ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);

            for (int cardNumber = 0; cardNumber < 17; cardNumber++)
            {
                this.cardsImages[cardNumber].Image = null;
                this.cardsImages[cardNumber].Invalidate();
                this.cardsImages[cardNumber].Visible = false;
            }

            await this.Shuffle();
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
                timerBar.Invalidate();
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