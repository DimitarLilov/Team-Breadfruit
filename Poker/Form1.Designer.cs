namespace Poker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean DefaultMaximumMoney any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playerFoldButton = new System.Windows.Forms.Button();
            this.playerCheckButton = new System.Windows.Forms.Button();
            this.playerCallButton = new System.Windows.Forms.Button();
            this.playerRaiseButton = new System.Windows.Forms.Button();

            this.timerBar = new System.Windows.Forms.ProgressBar();

            this.playerRaiseTextBox = new System.Windows.Forms.TextBox();

            this.playerChipsTextBox = new System.Windows.Forms.TextBox();
            this.playerStatus = new System.Windows.Forms.Label();

            this.botOneChipsTextBox = new System.Windows.Forms.TextBox();
            this.botOneStatus = new System.Windows.Forms.Label();

            this.botTwoChipsTextBox = new System.Windows.Forms.TextBox();
            this.botTwoStatus = new System.Windows.Forms.Label();

            this.botThreeChipsTextBox = new System.Windows.Forms.TextBox();
            this.botThreeStatus = new System.Windows.Forms.Label();

            this.botFourChipsTextBox = new System.Windows.Forms.TextBox();
            this.botFourStatus = new System.Windows.Forms.Label();

            this.botFiveChipsTextBox = new System.Windows.Forms.TextBox();
            this.botFiveStatus = new System.Windows.Forms.Label();

            this.addChipsButton = new System.Windows.Forms.Button();
            this.addChipsTextBox = new System.Windows.Forms.TextBox();

            this.potLabel = new System.Windows.Forms.Label();
            this.potTextBox = new System.Windows.Forms.TextBox();

            this.blindOptions = new System.Windows.Forms.Button();
            this.smallBlindButton = new System.Windows.Forms.Button();
            this.smallBlindTextBox = new System.Windows.Forms.TextBox();
            this.bigBlindButton = new System.Windows.Forms.Button();
            this.bigBlindTextBox = new System.Windows.Forms.TextBox();


            this.SuspendLayout();
            // 
            // playerFoldButton
            // 
            this.playerFoldButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerFoldButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerFoldButton.Location = new System.Drawing.Point(335, 660);
            this.playerFoldButton.Name = "playerFoldButton";
            this.playerFoldButton.Size = new System.Drawing.Size(130, 62);
            this.playerFoldButton.TabIndex = 0;
            this.playerFoldButton.Text = "Fold";
            this.playerFoldButton.UseVisualStyleBackColor = true;
            this.playerFoldButton.Click += new System.EventHandler(this.bFold_Click);
            // 
            // playerCheckButton
            // 
            this.playerCheckButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerCheckButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerCheckButton.Location = new System.Drawing.Point(494, 660);
            this.playerCheckButton.Name = "playerCheckButton";
            this.playerCheckButton.Size = new System.Drawing.Size(134, 62);
            this.playerCheckButton.TabIndex = 2;
            this.playerCheckButton.Text = "Check";
            this.playerCheckButton.UseVisualStyleBackColor = true;
            this.playerCheckButton.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // playerCallButton
            // 
            this.playerCallButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerCallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerCallButton.Location = new System.Drawing.Point(667, 661);
            this.playerCallButton.Name = "botCall";
            this.playerCallButton.Size = new System.Drawing.Size(126, 62);
            this.playerCallButton.TabIndex = 3;
            this.playerCallButton.Text = "Call";
            this.playerCallButton.UseVisualStyleBackColor = true;
            this.playerCallButton.Click += new System.EventHandler(this.bCall_Click);
            // 
            // playerRaiseButton
            // 
            this.playerRaiseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerRaiseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerRaiseButton.Location = new System.Drawing.Point(835, 661);
            this.playerRaiseButton.Name = "playerRaiseButton";
            this.playerRaiseButton.Size = new System.Drawing.Size(124, 62);
            this.playerRaiseButton.TabIndex = 4;
            this.playerRaiseButton.Text = "Raise";
            this.playerRaiseButton.UseVisualStyleBackColor = true;
            this.playerRaiseButton.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // timerBar
            // 
            this.timerBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.timerBar.BackColor = System.Drawing.SystemColors.Control;
            this.timerBar.Location = new System.Drawing.Point(335, 631);
            this.timerBar.Maximum = 1000;
            this.timerBar.Name = "timerBar";
            this.timerBar.Size = new System.Drawing.Size(667, 23);
            this.timerBar.TabIndex = 5;
            this.timerBar.Value = 1000;
            // 
            // playerChipsTextBox
            // 
            this.playerChipsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerChipsTextBox.Location = new System.Drawing.Point(755, 553);
            this.playerChipsTextBox.Name = "playerChipsTextBox";
            this.playerChipsTextBox.Size = new System.Drawing.Size(163, 23);
            this.playerChipsTextBox.TabIndex = 6;
            this.playerChipsTextBox.Text = "playerChips : 0";
            // 
            // addChipsButton
            // 
            this.addChipsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsButton.Location = new System.Drawing.Point(12, 697);
            this.addChipsButton.Name = "addChipsButton";
            this.addChipsButton.Size = new System.Drawing.Size(75, 25);
            this.addChipsButton.TabIndex = 7;
            this.addChipsButton.Text = "Add playerChips";
            this.addChipsButton.UseVisualStyleBackColor = true;
            this.addChipsButton.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // addChipsTextBox
            // 
            this.addChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsTextBox.Location = new System.Drawing.Point(93, 700);
            this.addChipsTextBox.Name = "addChipsTextBox";
            this.addChipsTextBox.Size = new System.Drawing.Size(125, 20);
            this.addChipsTextBox.TabIndex = 8;
            // 
            // botFiveChipsTextBox
            // 
            this.botFiveChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botFiveChipsTextBox.Location = new System.Drawing.Point(1012, 553);
            this.botFiveChipsTextBox.Name = "botFiveChipsTextBox";
            this.botFiveChipsTextBox.Size = new System.Drawing.Size(152, 23);
            this.botFiveChipsTextBox.TabIndex = 9;
            this.botFiveChipsTextBox.Text = "playerChips : 0";
            // 
            // botFourChipsTextBox
            // 
            this.botFourChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botFourChipsTextBox.Location = new System.Drawing.Point(970, 81);
            this.botFourChipsTextBox.Name = "botFourChipsTextBox";
            this.botFourChipsTextBox.Size = new System.Drawing.Size(123, 23);
            this.botFourChipsTextBox.TabIndex = 10;
            this.botFourChipsTextBox.Text = "playerChips : 0";
            // 
            // botThreeChipsTextBox
            // 
            this.botThreeChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botThreeChipsTextBox.Location = new System.Drawing.Point(755, 81);
            this.botThreeChipsTextBox.Name = "botThreeChipsTextBox";
            this.botThreeChipsTextBox.Size = new System.Drawing.Size(125, 23);
            this.botThreeChipsTextBox.TabIndex = 11;
            this.botThreeChipsTextBox.Text = "playerChips : 0";
            // 
            // botTwoChipsTextBox
            // 
            this.botTwoChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botTwoChipsTextBox.Location = new System.Drawing.Point(276, 81);
            this.botTwoChipsTextBox.Name = "botTwoChipsTextBox";
            this.botTwoChipsTextBox.Size = new System.Drawing.Size(133, 23);
            this.botTwoChipsTextBox.TabIndex = 12;
            this.botTwoChipsTextBox.Text = "playerChips : 0";
            // 
            // botOneChipsTextBox
            // 
            this.botOneChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botOneChipsTextBox.Location = new System.Drawing.Point(181, 553);
            this.botOneChipsTextBox.Name = "botOneChipsTextBox";
            this.botOneChipsTextBox.Size = new System.Drawing.Size(142, 23);
            this.botOneChipsTextBox.TabIndex = 13;
            this.botOneChipsTextBox.Text = "playerChips : 0";
            // 
            // potTextBox
            // 
            this.potTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potTextBox.Location = new System.Drawing.Point(606, 212);
            this.potTextBox.Name = "potTextBox";
            this.potTextBox.Size = new System.Drawing.Size(125, 23);
            this.potTextBox.TabIndex = 14;
            this.potTextBox.Text = "0";
            // 
            // blindOptions
            // 
            this.blindOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.blindOptions.Location = new System.Drawing.Point(12, 12);
            this.blindOptions.Name = "blindOptions";
            this.blindOptions.Size = new System.Drawing.Size(75, 36);
            this.blindOptions.TabIndex = 15;
            this.blindOptions.Text = "BB/SB";
            this.blindOptions.UseVisualStyleBackColor = true;
            this.blindOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // bigBlindButton
            // 
            this.bigBlindButton.Location = new System.Drawing.Point(12, 254);
            this.bigBlindButton.Name = "bigBlindButton";
            this.bigBlindButton.Size = new System.Drawing.Size(75, 23);
            this.bigBlindButton.TabIndex = 16;
            this.bigBlindButton.Text = "Big Blind";
            this.bigBlindButton.UseVisualStyleBackColor = true;
            this.bigBlindButton.Click += new System.EventHandler(this.bBB_Click);
            // 
            // smallBlindTextBox
            // 
            this.smallBlindTextBox.Location = new System.Drawing.Point(12, 228);
            this.smallBlindTextBox.Name = "smallBlindTextBox";
            this.smallBlindTextBox.Size = new System.Drawing.Size(75, 20);
            this.smallBlindTextBox.TabIndex = 17;
            this.smallBlindTextBox.Text = "250";
            // 
            // smallBlindButton
            // 
            this.smallBlindButton.Location = new System.Drawing.Point(12, 199);
            this.smallBlindButton.Name = "smallBlindButton";
            this.smallBlindButton.Size = new System.Drawing.Size(75, 23);
            this.smallBlindButton.TabIndex = 18;
            this.smallBlindButton.Text = "Small Blind";
            this.smallBlindButton.UseVisualStyleBackColor = true;
            this.smallBlindButton.Click += new System.EventHandler(this.bSB_Click);
            // 
            // bigBlindTextBox
            // 
            this.bigBlindTextBox.Location = new System.Drawing.Point(12, 283);
            this.bigBlindTextBox.Name = "bigBlindTextBox";
            this.bigBlindTextBox.Size = new System.Drawing.Size(75, 20);
            this.bigBlindTextBox.TabIndex = 19;
            this.bigBlindTextBox.Text = "500";
            // 
            // botFiveStatus
            // 
            this.botFiveStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveStatus.Location = new System.Drawing.Point(1012, 579);
            this.botFiveStatus.Name = "botFiveStatus";
            this.botFiveStatus.Size = new System.Drawing.Size(152, 32);
            this.botFiveStatus.TabIndex = 26;
            // 
            // botFourStatus
            // 
            this.botFourStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourStatus.Location = new System.Drawing.Point(970, 107);
            this.botFourStatus.Name = "botFourStatus";
            this.botFourStatus.Size = new System.Drawing.Size(123, 32);
            this.botFourStatus.TabIndex = 27;
            // 
            // botThreeStatus
            // 
            this.botThreeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeStatus.Location = new System.Drawing.Point(755, 107);
            this.botThreeStatus.Name = "botThreeStatus";
            this.botThreeStatus.Size = new System.Drawing.Size(125, 32);
            this.botThreeStatus.TabIndex = 28;
            // 
            // botOneStatus
            // 
            this.botOneStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneStatus.Location = new System.Drawing.Point(181, 579);
            this.botOneStatus.Name = "botOneStatus";
            this.botOneStatus.Size = new System.Drawing.Size(142, 32);
            this.botOneStatus.TabIndex = 29;
            // 
            // playerStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(755, 579);
            this.playerStatus.Name = "playerStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // botTwoStatus
            // 
            this.botTwoStatus.Location = new System.Drawing.Point(276, 107);
            this.botTwoStatus.Name = "botTwoStatus";
            this.botTwoStatus.Size = new System.Drawing.Size(133, 32);
            this.botTwoStatus.TabIndex = 31;
            // 
            // potLabel
            // 
            this.potLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potLabel.Location = new System.Drawing.Point(654, 188);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(31, 21);
            this.potLabel.TabIndex = 0;
            this.potLabel.Text = "Pot";
            // 
            // playerRaiseTextBox
            // 
            this.playerRaiseTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerRaiseTextBox.Location = new System.Drawing.Point(965, 703);
            this.playerRaiseTextBox.Name = "playerRaiseTextBox";
            this.playerRaiseTextBox.Size = new System.Drawing.Size(108, 20);
            this.playerRaiseTextBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.playerRaiseTextBox);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.botTwoStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.botOneStatus);
            this.Controls.Add(this.botThreeStatus);
            this.Controls.Add(this.botFourStatus);
            this.Controls.Add(this.botFiveStatus);
            this.Controls.Add(this.bigBlindTextBox);
            this.Controls.Add(this.smallBlindButton);
            this.Controls.Add(this.smallBlindTextBox);
            this.Controls.Add(this.bigBlindButton);
            this.Controls.Add(this.blindOptions);
            this.Controls.Add(this.potTextBox);
            this.Controls.Add(this.botOneChipsTextBox);
            this.Controls.Add(this.botTwoChipsTextBox);
            this.Controls.Add(this.botThreeChipsTextBox);
            this.Controls.Add(this.botFourChipsTextBox);
            this.Controls.Add(this.botFiveChipsTextBox);
            this.Controls.Add(this.addChipsTextBox);
            this.Controls.Add(this.addChipsButton);
            this.Controls.Add(this.playerChipsTextBox);
            this.Controls.Add(this.timerBar);
            this.Controls.Add(this.playerRaiseButton);
            this.Controls.Add(this.playerCallButton);
            this.Controls.Add(this.playerCheckButton);
            this.Controls.Add(this.playerFoldButton);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playerFoldButton;
        private System.Windows.Forms.Button playerCheckButton;
        private System.Windows.Forms.Button playerCallButton;
        private System.Windows.Forms.Button playerRaiseButton;
        private System.Windows.Forms.TextBox playerRaiseTextBox;
        private System.Windows.Forms.TextBox playerChipsTextBox;
        private System.Windows.Forms.Label playerStatus;

        private System.Windows.Forms.ProgressBar timerBar;

        private System.Windows.Forms.Label potLabel;
        private System.Windows.Forms.TextBox potTextBox;

        private System.Windows.Forms.TextBox addChipsTextBox;
        private System.Windows.Forms.Button addChipsButton;

        private System.Windows.Forms.TextBox botOneChipsTextBox;
        private System.Windows.Forms.Label botOneStatus;
        
        private System.Windows.Forms.TextBox botTwoChipsTextBox;
        private System.Windows.Forms.Label botTwoStatus;   

        private System.Windows.Forms.TextBox botThreeChipsTextBox;
        private System.Windows.Forms.Label botThreeStatus;

        private System.Windows.Forms.TextBox botFourChipsTextBox;
        private System.Windows.Forms.Label botFourStatus;

        private System.Windows.Forms.TextBox botFiveChipsTextBox;
        private System.Windows.Forms.Label botFiveStatus;

        private System.Windows.Forms.Button blindOptions;
        private System.Windows.Forms.TextBox smallBlindTextBox;
        private System.Windows.Forms.Button smallBlindButton;
        private System.Windows.Forms.TextBox bigBlindTextBox;
        private System.Windows.Forms.Button bigBlindButton;




    }
}

