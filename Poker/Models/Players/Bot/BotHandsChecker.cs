namespace Poker.Models.Players.Bot
{
    using System;
    using System.Windows.Forms;

    public class BotHandsChecker
    {
        private GameManager currentForm;

        public BotHandsChecker(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }

        public void CheckBotsHand(int botFirstCard, int botSecondCard, ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower, double botCurrent)
        {
            if (!hasBotFolded)
            {
                if (botCurrent == -1)
                {
                    this.CheckBotsHighCard(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 0)
                {
                    this.PairTable(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
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
                this.currentForm.cardsImages[botFirstCard].Visible = false;
                this.currentForm.cardsImages[botSecondCard].Visible = false;
            }
        }

        private void CheckBotsHighCard(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            this.currentForm.Bot.BotsMoveFirst(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 20, 25);
        }

        private void PairTable(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            this.currentForm.Bot.BotsMoveFirst(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 16, 25);
        }

        private void CheckBotsPairHand(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);

            if (botPower <= 199 && botPower >= 140)
            {
                this.currentForm.Bot.BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                this.currentForm.Bot.BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                this.currentForm.Bot.BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 9, rRaise);
            }
        }

        private void CheckBotsTwoPair(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);

            if (botPower <= 290 && botPower >= 246)
            {
                this.currentForm.Bot.BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                this.currentForm.Bot.BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                this.currentForm.Bot.BotsMoveSecond(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 4, rRaise);
            }
        }

        private void CheckBotsThreeOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);

            if (botPower <= 390 && botPower >= 330)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }

            if (botPower <= 327 && botPower >= 321)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }

            if (botPower < 321 && botPower >= 303)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }
        }

        private void CheckBotsStraight(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);

            if (botPower <= 480 && botPower >= 410)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }

            if (botPower <= 409 && botPower >= 407)//10  8
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }

            if (botPower < 407 && botPower >= 404)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }
        }

        private void CheckBotsFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fCall);
        }

        private void CheckBotsFullHouse(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);

            if (botPower <= 626 && botPower >= 620)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall);
            }

            if (botPower < 620 && botPower >= 602)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall);
            }
        }

        private void CheckBotsFourOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);

            if (botPower <= 752 && botPower >= 704)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fkCall);
            }
        }

        private void CheckBotsStraightFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                this.currentForm.Bot.BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sfCall);
            }
        }
    }
}