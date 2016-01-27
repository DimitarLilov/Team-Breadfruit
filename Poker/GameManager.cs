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
    using Poker.Models.Players.Bot;
    using Poker.Models.Rules;

    public partial class GameManager : Form
    {
        //ProgressBar asd = new ProgressBar();
        //public int Nm;
        public const int DefaultStartingChips = 10000;
        public const int LastRound = 4;
        
        public const int Flop = 1;
        public const int Turn = 2;
        public const int River = 3;
        private const int End = 4;

        private const string ChipsString = "Chips : ";
        private const string CallString = "Call";
        private const string AllInString = "All in";
        private const string RaiseString = "Raise";
        private const string CheckString = "Check";

        Panel playerPanel = new Panel();
        Panel botOnePanel = new Panel();
        Panel botTwoPanel = new Panel();
        Panel botThreePanel = new Panel();
        Panel botFourPanel = new Panel();
        Panel botFivePanel = new Panel();

        public int callChipsValue = 500;
        public int foldedPlayers = 5;
        private int TotalTableCards = 17;

        public int playerChips = DefaultStartingChips;
        public int botOneChips = DefaultStartingChips;
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
        
        public bool hasPlayerBankrupted;
        public bool playerTurn = true;
        public bool shouldRestart;
        public bool isRaising;

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

        public int winners;

        public int maxLeft = 6;

        public int last;
        public int raisedTurn = 1;
       
        public List<bool?> bankruptPlayers = new List<bool?>();

        public List<Type> winningingHands = new List<Type>();
        public List<string> CheckWinners = new List<string>();
        public List<int> totalAllInChips = new List<int>();
        
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

        private int height;
        private int width;

        private readonly Rules currentRules;
        public readonly Winner winner;
        private readonly Dealer checkRaiseDealer;
        private readonly Bot bot;

        // public readonly Dealer dealer;

        public GameManager()
        {
            this.callChipsValue = bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Updates.Start();

            this.InitializeComponent();

            this.width = this.Width;
            this.height = this.Height;
            this.Shuffle();

            this.EnableAllTextBoxes();

            this.InitializeChipsTextBoxes();

            this.InitializeTimer();

            this.InitializeBlindsBoxes();

            this.playerRaiseTextBox.Text = (this.bigBlind * 2).ToString();
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
            get { return this.winner; }
        }

        public Dealer CheckRaiseDealer
        {
            get { return this.checkRaiseDealer; }
        }

        public Bot Bot
        {
            get { return this.bot; }
        }

        public Winner Winner { get; set; }
        
        private void InitializeBlindsBoxes()
        {
            this.bigBlindTextBox.Visible = false;
            this.smallBlindTextBox.Visible = false;
            this.bigBlindButton.Visible = false;
            this.smallBlindButton.Visible = false;
        }

        private void InitializeTimer()
        {
            this.timer.Interval = 1000;
            this.timer.Tick += this.TimerTick;
            this.Updates.Interval = 100;
            this.Updates.Tick += this.UpdateTick;
        }

        private void InitializeChipsTextBoxes()
        {
            this.playerChipsTextBox.Text = ChipsString + this.playerChips;
            this.botOneChipsTextBox.Text = ChipsString + this.botOneChips;
            this.botTwoChipsTextBox.Text = ChipsString + this.botTwoChips;
            this.botThreeChipsTextBox.Text = ChipsString + this.botThreeChips;
            this.botFourChipsTextBox.Text = ChipsString + this.botFourChips;
            this.botFiveChipsTextBox.Text = ChipsString + this.botFiveChips;
        }

        private void EnableAllTextBoxes()
        {
            this.potTextBox.Enabled = false;
            this.playerChipsTextBox.Enabled = false;
            this.botOneChipsTextBox.Enabled = false;
            this.botTwoChipsTextBox.Enabled = false;
            this.botThreeChipsTextBox.Enabled = false;
            this.botFourChipsTextBox.Enabled = false;
            this.botFiveChipsTextBox.Enabled = false;
        }
        
        async Task Shuffle()
        {
            Random randomNumber = new Random();

            this.bankruptPlayers.Add(this.hasPlayerBankrupted);
            this.bankruptPlayers.Add(this.hasBotOneBankrupted);
            this.bankruptPlayers.Add(this.hasBotTwoBankrupted);
            this.bankruptPlayers.Add(this.hasBotThreeBankrupted);
            this.bankruptPlayers.Add(this.hasBotFourBankrupted);
            this.bankruptPlayers.Add(this.hasBotFiveBankrupted);

            this.playerCallButton.Enabled = false;
            this.playerRaiseButton.Enabled = false;
            this.playerFoldButton.Enabled = false;
            this.playerCheckButton.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            bool isCardOnTheTable = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");

            int horizontal = 580;
            int vertical = 480;

            //shuffles cards
            this.ShuffleCards(randomNumber);

            await this.DealOutCards(horizontal, vertical, isCardOnTheTable, backImage);

            this.CheckIfGameShouldBeRestarted();

            this.CheckIfBotsActionsShouldBeEnabled();
        }

        /// <summary>
        /// This method rearranges the cards' positions in random orders.
        /// </summary>
        /// <param name="random">Add a random generator variable.</param>
        public void ShuffleCards(Random random)
        {
            int cardsCount = this.ImgLocation.Length;

            for (int rearrangesCount = cardsCount; rearrangesCount > 0; rearrangesCount--)
            {
                int nextRandomNumber = random.Next(rearrangesCount);
                var saveRandomCardNumber = this.ImgLocation[nextRandomNumber];

                //Both cards switch places.
                this.ImgLocation[nextRandomNumber] = this.ImgLocation[rearrangesCount - 1];
                this.ImgLocation[rearrangesCount - 1] = saveRandomCardNumber;
            }
        }

        public void CheckIfBotsActionsShouldBeEnabled()
        {
            if (this.dealtCards == 17)
            {
               this.playerRaiseButton.Enabled = true;
               this.playerCallButton.Enabled = true;
               this.playerRaiseButton.Enabled = true;
               this.playerRaiseButton.Enabled = true;
               this.playerFoldButton.Enabled = true;
            }
        }

        public void CheckIfGameShouldBeRestarted()
        {
            if (this.foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show(@"Would You Like To Play Again ?", @"You Won , Congratulations ! ", MessageBoxButtons.YesNo);
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
                this.foldedPlayers = 5;
            }
        }

        private async Task DealOutCards(int horizontal, int vertical, bool isCardOnTheTable, Bitmap backImage)
        {

            for (this.dealtCards = 0; this.dealtCards < this.TotalTableCards; this.dealtCards++)
            {
                this.GetCardNumber();

                this.cardsAsNumbers[this.dealtCards] = int.Parse(this.ImgLocation[this.dealtCards]) - 1;
                this.cardsImages[this.dealtCards] = new PictureBox();
                this.cardsImages[this.dealtCards].SizeMode = PictureBoxSizeMode.StretchImage;
                this.cardsImages[this.dealtCards].Height = 130;
                this.cardsImages[this.dealtCards].Width = 80;
                this.Controls.Add(this.cardsImages[this.dealtCards]);
                this.cardsImages[this.dealtCards].Name = "pb" + this.dealtCards;
                await Task.Delay(200);

                if (this.dealtCards < 2)
                {
                    if (this.cardsImages[0].Tag != null)
                    {
                        this.cardsImages[1].Tag = this.cardsAsNumbers[1];
                    }

                    this.cardsImages[0].Tag = this.cardsAsNumbers[0];
                    this.cardsImages[this.dealtCards].Image = this.Deck[this.dealtCards];
                    this.cardsImages[this.dealtCards].Anchor = (AnchorStyles.Bottom);
                    this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                    horizontal += this.cardsImages[this.dealtCards].Width;

                    this.Controls.Add(this.playerPanel);
                    this.playerPanel.Location = new Point(this.cardsImages[0].Left - 10, this.cardsImages[0].Top - 10);
                    this.playerPanel.BackColor = Color.DarkBlue;
                    this.playerPanel.Height = 150;
                    this.playerPanel.Width = 180;
                    this.playerPanel.Visible = false;
                }

                if (this.botOneChips > 0)
                {
                    this.foldedPlayers--;

                    //checks which player to give cards to
                    if (this.dealtCards >= 2 && this.dealtCards < 4)
                    {
                        if (this.cardsImages[2].Tag != null)
                        {
                            this.cardsImages[3].Tag = this.cardsAsNumbers[3];
                        }

                        this.cardsImages[2].Tag = this.cardsAsNumbers[2];

                        if (!isCardOnTheTable)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        isCardOnTheTable = true;

                        this.cardsImages[this.dealtCards].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.cardsImages[this.dealtCards].Image = backImage;
                        this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsImages[this.dealtCards].Width;
                        this.cardsImages[this.dealtCards].Visible = true;

                        this.Controls.Add(this.botOnePanel);
                        this.botOnePanel.Location = new Point(this.cardsImages[2].Left - 10, this.cardsImages[2].Top - 10);
                        this.botOnePanel.BackColor = Color.DarkBlue;
                        this.botOnePanel.Height = 150;
                        this.botOnePanel.Width = 180;
                        this.botOnePanel.Visible = false;

                        if (this.dealtCards == 3)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                if (this.botTwoChips > 0)
                {
                    this.foldedPlayers--;

                    //checks which player to give cards to
                    if (this.dealtCards >= 4 && this.dealtCards < 6)
                    {
                        if (this.cardsImages[4].Tag != null)
                        {
                            this.cardsImages[5].Tag = this.cardsAsNumbers[5];
                        }

                        this.cardsImages[4].Tag = this.cardsAsNumbers[4];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        isCardOnTheTable = true;
                        this.cardsImages[this.dealtCards].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.cardsImages[this.dealtCards].Image = backImage;
                        
                        this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsImages[this.dealtCards].Width;
                        this.cardsImages[this.dealtCards].Visible = true;

                        this.Controls.Add(this.botTwoPanel);
                        this.botTwoPanel.Location = new Point(this.cardsImages[4].Left - 10, this.cardsImages[4].Top - 10);
                        this.botTwoPanel.BackColor = Color.DarkBlue;
                        this.botTwoPanel.Height = 150;
                        this.botTwoPanel.Width = 180;
                        this.botTwoPanel.Visible = false;

                        if (this.dealtCards == 5)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }
               
                if (this.botThreeChips > 0)
                {
                    this.foldedPlayers--;

                    if (this.dealtCards >= 6 && this.dealtCards < 8)
                    {
                        if (this.cardsImages[6].Tag != null)
                        {
                            this.cardsImages[7].Tag = this.cardsAsNumbers[7];
                        }

                        this.cardsImages[6].Tag = this.cardsAsNumbers[6];

                        if (!isCardOnTheTable)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        isCardOnTheTable = true;

                        this.cardsImages[this.dealtCards].Anchor = (AnchorStyles.Top);
                        this.cardsImages[this.dealtCards].Image = backImage;
                        this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsImages[this.dealtCards].Width;
                        this.cardsImages[this.dealtCards].Visible = true;
                        this.Controls.Add(this.botThreePanel);
                        this.botThreePanel.Location = new Point(this.cardsImages[6].Left - 10, this.cardsImages[6].Top - 10);
                        this.botThreePanel.BackColor = Color.DarkBlue;
                        this.botThreePanel.Height = 150;
                        this.botThreePanel.Width = 180;
                        this.botThreePanel.Visible = false;

                        if (this.dealtCards == 7)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                if (this.botFourChips > 0)
                {
                    this.foldedPlayers--;

                    if (this.dealtCards >= 8 && this.dealtCards < 10)
                    {
                        if (this.cardsImages[8].Tag != null)
                        {
                            this.cardsImages[9].Tag = this.cardsAsNumbers[9];
                        }

                        this.cardsImages[8].Tag = this.cardsAsNumbers[8];

                        if (!isCardOnTheTable)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        isCardOnTheTable = true;

                        this.cardsImages[this.dealtCards].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.cardsImages[this.dealtCards].Image = backImage;
                        this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsImages[this.dealtCards].Width;
                        this.cardsImages[this.dealtCards].Visible = true;

                        this.Controls.Add(this.botFourPanel);
                        this.botFourPanel.Location = new Point(this.cardsImages[8].Left - 10, this.cardsImages[8].Top - 10);
                        this.botFourPanel.BackColor = Color.DarkBlue;
                        this.botFourPanel.Height = 150;
                        this.botFourPanel.Width = 180;
                        this.botFourPanel.Visible = false;

                        if (this.dealtCards == 9)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                if (this.botFiveChips > 0)
                {
                    this.foldedPlayers--;

                    //checks which player to give cards to
                    if (this.dealtCards >= 10 && this.dealtCards < 12)
                    {
                        if (this.cardsImages[10].Tag != null)
                        {
                            this.cardsImages[11].Tag = this.cardsAsNumbers[11];
                        }

                        this.cardsImages[10].Tag = this.cardsAsNumbers[10];
                        if (!isCardOnTheTable)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        isCardOnTheTable = true;

                        this.cardsImages[this.dealtCards].Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                        this.cardsImages[this.dealtCards].Image = backImage;
                        
                        this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += this.cardsImages[this.dealtCards].Width;
                        this.cardsImages[this.dealtCards].Visible = true;
                        this.Controls.Add(this.botFivePanel);
                        this.botFivePanel.Location = new Point(this.cardsImages[10].Left - 10, this.cardsImages[10].Top - 10);
                        this.botFivePanel.BackColor = Color.DarkBlue;
                        this.botFivePanel.Height = 150;
                        this.botFivePanel.Width = 180;
                        this.botFivePanel.Visible = false;

                        if (this.dealtCards == 11)
                        {
                            isCardOnTheTable = false;
                        }
                    }
                }

                //sets the cards on the table face down
                if (this.dealtCards >= 12)
                {
                    this.cardsImages[12].Tag = this.cardsAsNumbers[12];

                    if (this.dealtCards > 12)
                    {
                        this.cardsImages[13].Tag = this.cardsAsNumbers[13];
                    }

                    if (this.dealtCards > 13)
                    {
                        this.cardsImages[14].Tag = this.cardsAsNumbers[14];
                    }

                    if (this.dealtCards > 14)
                    {
                        this.cardsImages[15].Tag = this.cardsAsNumbers[15];
                    }

                    if (this.dealtCards > 15)
                    {
                        this.cardsImages[16].Tag = this.cardsAsNumbers[16];
                    }

                    if (!isCardOnTheTable)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    isCardOnTheTable = true;

                    if (this.cardsImages[this.dealtCards] != null)
                    {
                        this.cardsImages[this.dealtCards].Anchor = AnchorStyles.None;
                        this.cardsImages[this.dealtCards].Image = backImage;
                        this.cardsImages[this.dealtCards].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }


                if (this.botOneChips <= 0)
                {
                    this.hasBotOneBankrupted = true;
                    this.cardsImages[2].Visible = false;
                    this.cardsImages[3].Visible = false;
                }
                else
                {
                    this.hasBotOneBankrupted = false;

                    if (this.dealtCards == 3)
                    {
                        if (this.cardsImages[3] != null)
                        {
                            this.cardsImages[2].Visible = true;
                            this.cardsImages[3].Visible = true;
                        }
                    }
                }

                if (this.botTwoChips <= 0)
                {
                    this.hasBotTwoBankrupted = true;
                    this.cardsImages[4].Visible = false;
                    this.cardsImages[5].Visible = false;
                }
                else
                {
                    this.hasBotTwoBankrupted = false;

                    if (this.dealtCards == 5)
                    {
                        if (this.cardsImages[5] != null)
                        {
                            this.cardsImages[4].Visible = true;
                            this.cardsImages[5].Visible = true;
                        }
                    }
                }

                if (this.botThreeChips <= 0)
                {
                    this.hasBotThreeBankrupted = true;
                    this.cardsImages[6].Visible = false;
                    this.cardsImages[7].Visible = false;
                }
                else
                {
                    this.hasBotThreeBankrupted = false;

                    if (this.dealtCards == 7)
                    {
                        if (this.cardsImages[7] != null)
                        {
                            this.cardsImages[6].Visible = true;
                            this.cardsImages[7].Visible = true;
                        }
                    }
                }

                if (this.botFourChips <= 0)
                {
                    this.hasBotFourBankrupted = true;
                    this.cardsImages[8].Visible = false;
                    this.cardsImages[9].Visible = false;
                }
                else
                {
                    this.hasBotFourBankrupted = false;

                    if (this.dealtCards == 9)
                    {
                        if (this.cardsImages[9] != null)
                        {
                            this.cardsImages[8].Visible = true;
                            this.cardsImages[9].Visible = true;
                        }
                    }
                }

                if (this.botFiveChips <= 0)
                {
                    this.hasBotFiveBankrupted = true;
                    this.cardsImages[10].Visible = false;
                    this.cardsImages[11].Visible = false;
                }
                else
                {
                    this.hasBotFiveBankrupted = false;

                    if (this.dealtCards == 11)
                    {
                        if (this.cardsImages[11] != null)
                        {
                            this.cardsImages[10].Visible = true;
                            this.cardsImages[11].Visible = true;
                        }
                    }
                }

                if (this.dealtCards == 16)
                {
                    if (!this.shouldRestart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }

                    this.timer.Start();
                }
            }
        }

        private void GetCardNumber()
        {
            this.Deck[this.dealtCards] = Image.FromFile(this.ImgLocation[this.dealtCards]);

            var charsToRemove = new string[] {"Assets\\Cards\\", ".png"};

            foreach (var c in charsToRemove)
            {
                this.ImgLocation[this.dealtCards] = this.ImgLocation[this.dealtCards].Replace(c, string.Empty);
            }
        }



        #region Turns
        async Task Turns()
        {
            if (!this.hasPlayerBankrupted && this.playerTurn)
            {
                this.CurrentTurnPlayerAction();
            }

            if (this.hasPlayerBankrupted || !this.playerTurn)
            {
                await this.WhoIsAllIn();

                if (this.hasPlayerBankrupted && !this.hasPlayerFolded)
                {
                    if (this.playerCallButton.Text.Contains(AllInString) == false || this.playerRaiseButton.Text.Contains(AllInString) == false)
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
                        this.Bot.CheckBotsHand(2, 3, ref this.botOneChips, ref this.botOneTurn, ref this.hasBotOneBankrupted, this.botOneStatus, this.botOnePower, this.botOneType);
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

                this.potTextBox.Text = @"0";
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

        async Task WhoIsAllIn()
        {
            await this.CheckAllIn();

            int currentWinnerNumber = this.bankruptPlayers.Count(x => x == false);
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

            if (currentWinnerNumber < 6 &&
                currentWinnerNumber > 1 && this.totalRounds >= End)
            {
                await this.Finish(2);
            }
        }

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
                this.botOneChips += int.Parse(this.potTextBox.Text);
                this.botOneChipsTextBox.Text = this.botOneChips.ToString();
                this.botOnePanel.Visible = true;
                MessageBox.Show(@"Bot 1 Wins");
            }

            if (index == 2)
            {
                this.botTwoChips += int.Parse(this.potTextBox.Text);
                this.botTwoChipsTextBox.Text = this.botTwoChips.ToString();
                this.botTwoPanel.Visible = true;
                MessageBox.Show(@"Bot 2 Wins");
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

            if (this.botOneChips <= 0 && !this.hasBotOneBankrupted)
            {
                if (!this.hasAddedChips)
                {
                    this.totalAllInChips.Add(this.botOneChips);
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

            if (this.totalAllInChips.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.totalAllInChips.Clear();
            }
        }
        
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
            this.potTextBox.Text = @"0";
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
                    this.botOneChips += addChipsWhenLost.AddChipsValue;
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

        private async void TimerTick(object sender, object e)
        {
            if (this.timerBar.Value <= 0)
            {
                this.hasPlayerBankrupted = true;

                await this.Turns();
            }

            if (this.t > 0)
            {
                this.t--;
                this.timerBar.Value = (this.t / 6) * 100;
                this.timerBar.Invalidate();
            }
        }

        private void UpdateTick(object sender, object e)
        {
            if (this.playerChips <= 0)
            {
                this.playerChipsTextBox.Text = ChipsString + @"0";
            }

            if (this.botOneChips <= 0)
            {
                this.botOneChipsTextBox.Text = ChipsString + @"0";
            }

            if (this.botTwoChips <= 0)
            {
                this.botTwoChipsTextBox.Text = ChipsString + @"0";
            }

            if (this.botThreeChips <= 0)
            {
                this.botThreeChipsTextBox.Text = ChipsString + @"0";
            }

            if (this.botFourChips <= 0)
            {
                this.botFourChipsTextBox.Text = ChipsString + @"0";
            }

            if (this.botFiveChips <= 0)
            {
                this.botFiveChipsTextBox.Text = ChipsString + @"0";
            }

            this.playerChipsTextBox.Text = ChipsString + this.playerChips;
            this.botOneChipsTextBox.Text = ChipsString + this.botOneChips;
            this.botTwoChipsTextBox.Text = ChipsString + this.botTwoChips;
            this.botThreeChipsTextBox.Text = ChipsString + this.botThreeChips;
            this.botFourChipsTextBox.Text = ChipsString + this.botFourChips;
            this.botFiveChipsTextBox.Text = ChipsString + this.botFiveChips;

            if (this.playerChips <= 0)
            {
                this.playerTurn = false;
                this.hasPlayerBankrupted = true;
                this.playerCallButton.Enabled = false;
                this.playerRaiseButton.Enabled = false;
                this.playerFoldButton.Enabled = false;
                this.playerCheckButton.Enabled = false;
            }

            if (this.DefaultMaximumMoney > 0)
            {
                this.DefaultMaximumMoney--;
            }

            if (this.playerChips >= this.callChipsValue)
            {
                this.playerCallButton.Text = CallString + this.callChipsValue;
            }
            else
            {
                this.playerCallButton.Text = AllInString;
                this.playerRaiseButton.Enabled = false;
            }

            if (this.callChipsValue > 0)
            {
                this.playerCheckButton.Enabled = false;
            }

            if (this.callChipsValue <= 0)
            {
                this.playerCheckButton.Enabled = true;
                this.playerCallButton.Text = CallString;
                this.playerCallButton.Enabled = false;
            }   

            if (this.playerChips <= 0)
            {
                this.playerRaiseButton.Enabled = false;
            }

            int parsedValue;

            if (this.playerRaiseTextBox.Text != "" && int.TryParse(this.playerRaiseTextBox.Text, out parsedValue))
            {
                if (this.playerChips <= int.Parse(this.playerRaiseTextBox.Text))
                {
                    this.playerRaiseButton.Text = AllInString;
                }
                else
                {
                    this.playerRaiseButton.Text = RaiseString;
                }
            }

            if (this.playerChips < this.callChipsValue)
            {
                this.playerRaiseButton.Enabled = false;
            }
        }

        private async void ButtonFoldClick(object sender, EventArgs e)
        {
            this.playerStatus.Text = Winner.FoldString;
            this.playerTurn = false;
            this.hasPlayerBankrupted = true;
            await this.Turns();
        }

        private async void ButtonCheckClick(object sender, EventArgs e)
        {
            if (this.callChipsValue <= 0)
            {
                this.playerTurn = false;
                this.playerStatus.Text = CheckString;
            }
            else
            {
                this.playerCheckButton.Enabled = false;
            }

            await this.Turns();
        }

        private async void ButtonCallClick(object sender, EventArgs e)
        {
            this.CurrentRules.GameRules(0, 1, ref this.playerType, ref this.playerPower, this.hasPlayerBankrupted);

            if (this.playerChips >= this.callChipsValue)
            {
                this.playerChips -= this.callChipsValue;
                this.playerChipsTextBox.Text = ChipsString + this.playerChips;

                if (this.potTextBox.Text != "")
                {
                    this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.callChipsValue).ToString();
                }
                else
                {
                    this.potTextBox.Text = this.callChipsValue.ToString();
                }

                this.playerTurn = false;
                this.playerStatus.Text = CallString + this.callChipsValue;
                this.playerCall = this.callChipsValue;
            }
            else if (this.playerChips <= this.callChipsValue && this.callChipsValue > 0)
            {
                this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.playerChips).ToString();
                this.playerStatus.Text = AllInString + this.playerChips;
                this.playerChips = 0;
                this.playerChipsTextBox.Text = ChipsString + this.playerChips;
                this.playerTurn = false;
                this.playerFoldButton.Enabled = false;
                this.playerCall = this.playerChips;
            }

            await this.Turns();
        }

        private async void ButtonRaiseClick(object sender, EventArgs e)
        {
            this.CurrentRules.GameRules(0, 1, ref this.playerType, ref this.playerPower, this.hasPlayerBankrupted);

            int parsedValue;
            if (this.playerRaiseTextBox.Text != "" && int.TryParse(this.playerRaiseTextBox.Text, out parsedValue))
            {
                if (this.playerChips > this.callChipsValue)
                {
                    if (this.Raise * 2 > int.Parse(this.playerRaiseTextBox.Text))
                    {
                        this.playerRaiseTextBox.Text = (this.Raise * 2).ToString();
                        MessageBox.Show(@"You must Raise atleast twice as the current Raise !");

                        return;
                    }

                    if (this.playerChips >= int.Parse(this.playerRaiseTextBox.Text))
                    {
                        this.callChipsValue = int.Parse(this.playerRaiseTextBox.Text);
                        this.Raise = int.Parse(this.playerRaiseTextBox.Text);
                        this.playerStatus.Text = RaiseString + this.callChipsValue;
                        this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.callChipsValue).ToString();
                        this.playerCallButton.Text = CallString;
                        this.playerChips -= int.Parse(this.playerRaiseTextBox.Text);
                        this.isRaising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.Raise);
                    }
                    else
                    {
                        this.callChipsValue = this.playerChips;
                        this.Raise = this.playerChips;
                        this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.playerChips).ToString();
                        this.playerStatus.Text = RaiseString + this.callChipsValue;
                        this.playerChips = 0;
                        this.isRaising = true;
                        this.last = 0;
                        this.playerRaise = Convert.ToInt32(this.Raise);
                    }
                }
            }
            else
            {
                MessageBox.Show(@"This is a number only field");
                return;
            }

            this.playerTurn = false;
            await this.Turns();
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            if (this.addChipsTextBox.Text == "")
            {
                
            }
            else
            {
                this.playerChips += int.Parse(this.addChipsTextBox.Text);
                this.botOneChips += int.Parse(this.addChipsTextBox.Text);
                this.botTwoChips += int.Parse(this.addChipsTextBox.Text);
                this.botThreeChips += int.Parse(this.addChipsTextBox.Text);
                this.botFourChips += int.Parse(this.addChipsTextBox.Text);
                this.botFiveChips += int.Parse(this.addChipsTextBox.Text);
            }

            this.playerChipsTextBox.Text = ChipsString + this.playerChips;
        }

        private void BlindOptionsClick(object sender, EventArgs e)
        {
            this.bigBlindTextBox.Text = this.bigBlind.ToString();
            this.smallBlindTextBox.Text = this.smallBlind.ToString();

            if (this.bigBlindTextBox.Visible == false)
            {
                this.bigBlindTextBox.Visible = true;
                this.smallBlindTextBox.Visible = true;
                this.bigBlindButton.Visible = true;
                this.smallBlindButton.Visible = true;
            }
            else
            {
                this.bigBlindTextBox.Visible = false;
                this.smallBlindTextBox.Visible = false;
                this.bigBlindButton.Visible = false;
                this.smallBlindButton.Visible = false;
            }
        }

        private void ButtonSmallBlindClick(object sender, EventArgs e)
        {
            int parsedValue;

            if (this.smallBlindTextBox.Text.Contains(",") || this.smallBlindTextBox.Text.Contains("."))
            {
                MessageBox.Show(@"The Small Blind can be only round number !");
                this.smallBlindTextBox.Text = this.smallBlind.ToString();

                return;
            }

            if (!int.TryParse(this.smallBlindTextBox.Text, out parsedValue))
            {
                MessageBox.Show(@"This is a number only field");
                this.smallBlindTextBox.Text = this.smallBlind.ToString();

                return;
            }

            if (int.Parse(this.smallBlindTextBox.Text) > 100000)
            {
                MessageBox.Show(@"The maximum of the Small Blind is 100 000 $");
                this.smallBlindTextBox.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.smallBlindTextBox.Text) < 250)
            {
                MessageBox.Show(@"The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.smallBlindTextBox.Text) >= 250 &&
                int.Parse(this.smallBlindTextBox.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.smallBlindTextBox.Text);
                MessageBox.Show(@"The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void ButtonBigBlindClick(object sender, EventArgs e)
        {
            int parsedValue;

            if (this.bigBlindTextBox.Text.Contains(",") ||
                this.bigBlindTextBox.Text.Contains("."))
            {
                MessageBox.Show(@"The Big Blind can be only round number !");
                this.bigBlindTextBox.Text = this.bigBlind.ToString();

                return;
            }

            if (!int.TryParse(this.smallBlindTextBox.Text, out parsedValue))
            {
                MessageBox.Show(@"This is a number only field");
                this.smallBlindTextBox.Text = this.bigBlind.ToString();

                return;
            }

            if (int.Parse(this.bigBlindTextBox.Text) > 200000)
            {
                MessageBox.Show(@"The maximum of the Big Blind is 200 000");
                this.bigBlindTextBox.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.bigBlindTextBox.Text) < 500)
            {
                MessageBox.Show(@"The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.bigBlindTextBox.Text) >= 500 && int.Parse(this.bigBlindTextBox.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.bigBlindTextBox.Text);
                MessageBox.Show(@"The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void LayoutChange(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }
    }
}