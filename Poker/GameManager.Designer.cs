namespace Poker
{
    partial class GameManager
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
            this.playerFoldButton.Location = new System.Drawing.Point(447, 812);
            this.playerFoldButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerFoldButton.Name = "playerFoldButton";
            this.playerFoldButton.Size = new System.Drawing.Size(173, 76);
            this.playerFoldButton.TabIndex = 0;
            this.playerFoldButton.Text = "Fold";
            this.playerFoldButton.UseVisualStyleBackColor = true;
            this.playerFoldButton.Click += new System.EventHandler(this.bFold_Click);
            // 
            // playerCheckButton
            // 
            this.playerCheckButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerCheckButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerCheckButton.Location = new System.Drawing.Point(659, 812);
            this.playerCheckButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerCheckButton.Name = "playerCheckButton";
            this.playerCheckButton.Size = new System.Drawing.Size(179, 76);
            this.playerCheckButton.TabIndex = 2;
            this.playerCheckButton.Text = "Check";
            this.playerCheckButton.UseVisualStyleBackColor = true;
            this.playerCheckButton.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // playerCallButton
            // 
            this.playerCallButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerCallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerCallButton.Location = new System.Drawing.Point(889, 814);
            this.playerCallButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerCallButton.Name = "playerCallButton";
            this.playerCallButton.Size = new System.Drawing.Size(168, 76);
            this.playerCallButton.TabIndex = 3;
            this.playerCallButton.Text = "Call";
            this.playerCallButton.UseVisualStyleBackColor = true;
            this.playerCallButton.Click += new System.EventHandler(this.bCall_Click);
            // 
            // playerRaiseButton
            // 
            this.playerRaiseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerRaiseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerRaiseButton.Location = new System.Drawing.Point(1113, 814);
            this.playerRaiseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerRaiseButton.Name = "playerRaiseButton";
            this.playerRaiseButton.Size = new System.Drawing.Size(165, 76);
            this.playerRaiseButton.TabIndex = 4;
            this.playerRaiseButton.Text = "Raise";
            this.playerRaiseButton.UseVisualStyleBackColor = true;
            this.playerRaiseButton.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // timerBar
            // 
            this.timerBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.timerBar.BackColor = System.Drawing.SystemColors.Control;
            this.timerBar.Location = new System.Drawing.Point(447, 777);
            this.timerBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timerBar.Maximum = 1000;
            this.timerBar.Name = "timerBar";
            this.timerBar.Size = new System.Drawing.Size(889, 28);
            this.timerBar.TabIndex = 5;
            this.timerBar.Value = 1000;
            // 
            // playerRaiseTextBox
            // 
            this.playerRaiseTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerRaiseTextBox.Location = new System.Drawing.Point(1287, 865);
            this.playerRaiseTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerRaiseTextBox.Name = "playerRaiseTextBox";
            this.playerRaiseTextBox.Size = new System.Drawing.Size(143, 22);
            this.playerRaiseTextBox.TabIndex = 0;
            // 
            // playerChipsTextBox
            // 
            this.playerChipsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerChipsTextBox.Location = new System.Drawing.Point(1007, 681);
            this.playerChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerChipsTextBox.Name = "playerChipsTextBox";
            this.playerChipsTextBox.Size = new System.Drawing.Size(216, 26);
            this.playerChipsTextBox.TabIndex = 6;
            this.playerChipsTextBox.Text = "playerChips : 0";
            // 
            // playerStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(1007, 713);
            this.playerStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playerStatus.Name = "playerStatus";
            this.playerStatus.Size = new System.Drawing.Size(217, 39);
            this.playerStatus.TabIndex = 30;
            // 
            // botOneChipsTextBox
            // 
            this.botOneChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botOneChipsTextBox.Location = new System.Drawing.Point(241, 681);
            this.botOneChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.botOneChipsTextBox.Name = "botOneChipsTextBox";
            this.botOneChipsTextBox.Size = new System.Drawing.Size(188, 26);
            this.botOneChipsTextBox.TabIndex = 13;
            this.botOneChipsTextBox.Text = "playerChips : 0";
            // 
            // botOneStatus
            // 
            this.botOneStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botOneStatus.Location = new System.Drawing.Point(241, 713);
            this.botOneStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.botOneStatus.Name = "botOneStatus";
            this.botOneStatus.Size = new System.Drawing.Size(189, 39);
            this.botOneStatus.TabIndex = 29;
            // 
            // botTwoChipsTextBox
            // 
            this.botTwoChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botTwoChipsTextBox.Location = new System.Drawing.Point(368, 100);
            this.botTwoChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.botTwoChipsTextBox.Name = "botTwoChipsTextBox";
            this.botTwoChipsTextBox.Size = new System.Drawing.Size(176, 26);
            this.botTwoChipsTextBox.TabIndex = 12;
            this.botTwoChipsTextBox.Text = "playerChips : 0";
            // 
            // botTwoStatus
            // 
            this.botTwoStatus.Location = new System.Drawing.Point(368, 132);
            this.botTwoStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.botTwoStatus.Name = "botTwoStatus";
            this.botTwoStatus.Size = new System.Drawing.Size(177, 39);
            this.botTwoStatus.TabIndex = 31;
            // 
            // botThreeChipsTextBox
            // 
            this.botThreeChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botThreeChipsTextBox.Location = new System.Drawing.Point(1007, 100);
            this.botThreeChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.botThreeChipsTextBox.Name = "botThreeChipsTextBox";
            this.botThreeChipsTextBox.Size = new System.Drawing.Size(165, 26);
            this.botThreeChipsTextBox.TabIndex = 11;
            this.botThreeChipsTextBox.Text = "playerChips : 0";
            // 
            // botThreeStatus
            // 
            this.botThreeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botThreeStatus.Location = new System.Drawing.Point(1007, 132);
            this.botThreeStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.botThreeStatus.Name = "botThreeStatus";
            this.botThreeStatus.Size = new System.Drawing.Size(167, 39);
            this.botThreeStatus.TabIndex = 28;
            // 
            // botFourChipsTextBox
            // 
            this.botFourChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botFourChipsTextBox.Location = new System.Drawing.Point(1293, 100);
            this.botFourChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.botFourChipsTextBox.Name = "botFourChipsTextBox";
            this.botFourChipsTextBox.Size = new System.Drawing.Size(163, 26);
            this.botFourChipsTextBox.TabIndex = 10;
            this.botFourChipsTextBox.Text = "playerChips : 0";
            // 
            // botFourStatus
            // 
            this.botFourStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.botFourStatus.Location = new System.Drawing.Point(1293, 132);
            this.botFourStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.botFourStatus.Name = "botFourStatus";
            this.botFourStatus.Size = new System.Drawing.Size(164, 39);
            this.botFourStatus.TabIndex = 27;
            // 
            // botFiveChipsTextBox
            // 
            this.botFiveChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.botFiveChipsTextBox.Location = new System.Drawing.Point(1349, 681);
            this.botFiveChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.botFiveChipsTextBox.Name = "botFiveChipsTextBox";
            this.botFiveChipsTextBox.Size = new System.Drawing.Size(201, 26);
            this.botFiveChipsTextBox.TabIndex = 9;
            this.botFiveChipsTextBox.Text = "playerChips : 0";
            // 
            // botFiveStatus
            // 
            this.botFiveStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botFiveStatus.Location = new System.Drawing.Point(1349, 713);
            this.botFiveStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.botFiveStatus.Name = "botFiveStatus";
            this.botFiveStatus.Size = new System.Drawing.Size(203, 39);
            this.botFiveStatus.TabIndex = 26;
            // 
            // addChipsButton
            // 
            this.addChipsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsButton.Location = new System.Drawing.Point(16, 858);
            this.addChipsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addChipsButton.Name = "addChipsButton";
            this.addChipsButton.Size = new System.Drawing.Size(100, 31);
            this.addChipsButton.TabIndex = 7;
            this.addChipsButton.Text = "Add playerChips";
            this.addChipsButton.UseVisualStyleBackColor = true;
            this.addChipsButton.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // addChipsTextBox
            // 
            this.addChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsTextBox.Location = new System.Drawing.Point(124, 862);
            this.addChipsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addChipsTextBox.Name = "addChipsTextBox";
            this.addChipsTextBox.Size = new System.Drawing.Size(165, 22);
            this.addChipsTextBox.TabIndex = 8;
            // 
            // potLabel
            // 
            this.potLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potLabel.Location = new System.Drawing.Point(872, 231);
            this.potLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(41, 26);
            this.potLabel.TabIndex = 0;
            this.potLabel.Text = "Pot";
            // 
            // potTextBox
            // 
            this.potTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potTextBox.Location = new System.Drawing.Point(808, 261);
            this.potTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.potTextBox.Name = "potTextBox";
            this.potTextBox.Size = new System.Drawing.Size(165, 26);
            this.potTextBox.TabIndex = 14;
            this.potTextBox.Text = "0";
            // 
            // blindOptions
            // 
            this.blindOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.blindOptions.Location = new System.Drawing.Point(16, 15);
            this.blindOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blindOptions.Name = "blindOptions";
            this.blindOptions.Size = new System.Drawing.Size(100, 44);
            this.blindOptions.TabIndex = 15;
            this.blindOptions.Text = "BB/SB";
            this.blindOptions.UseVisualStyleBackColor = true;
            this.blindOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // smallBlindButton
            // 
            this.smallBlindButton.Location = new System.Drawing.Point(16, 245);
            this.smallBlindButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smallBlindButton.Name = "smallBlindButton";
            this.smallBlindButton.Size = new System.Drawing.Size(100, 28);
            this.smallBlindButton.TabIndex = 18;
            this.smallBlindButton.Text = "Small Blind";
            this.smallBlindButton.UseVisualStyleBackColor = true;
            this.smallBlindButton.Click += new System.EventHandler(this.bSB_Click);
            // 
            // smallBlindTextBox
            // 
            this.smallBlindTextBox.Location = new System.Drawing.Point(16, 281);
            this.smallBlindTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smallBlindTextBox.Name = "smallBlindTextBox";
            this.smallBlindTextBox.Size = new System.Drawing.Size(99, 22);
            this.smallBlindTextBox.TabIndex = 17;
            this.smallBlindTextBox.Text = "250";
            // 
            // bigBlindButton
            // 
            this.bigBlindButton.Location = new System.Drawing.Point(16, 313);
            this.bigBlindButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bigBlindButton.Name = "bigBlindButton";
            this.bigBlindButton.Size = new System.Drawing.Size(100, 28);
            this.bigBlindButton.TabIndex = 16;
            this.bigBlindButton.Text = "Big Blind";
            this.bigBlindButton.UseVisualStyleBackColor = true;
            this.bigBlindButton.Click += new System.EventHandler(this.bBB_Click);
            // 
            // bigBlindTextBox
            // 
            this.bigBlindTextBox.Location = new System.Drawing.Point(16, 348);
            this.bigBlindTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bigBlindTextBox.Name = "bigBlindTextBox";
            this.bigBlindTextBox.Size = new System.Drawing.Size(99, 22);
            this.bigBlindTextBox.TabIndex = 19;
            this.bigBlindTextBox.Text = "500";
            // 
            // GameManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1674, 897);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GameManager";
            this.Text = "GLS Texas Poker";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button playerFoldButton;
        public System.Windows.Forms.Button playerCheckButton;
        public System.Windows.Forms.Button playerCallButton;
        public System.Windows.Forms.Button playerRaiseButton;
        private System.Windows.Forms.TextBox playerRaiseTextBox;
        public System.Windows.Forms.TextBox playerChipsTextBox;
        public System.Windows.Forms.Label playerStatus;

        private System.Windows.Forms.ProgressBar timerBar;

        private System.Windows.Forms.Label potLabel;
        public System.Windows.Forms.TextBox potTextBox;

        private System.Windows.Forms.TextBox addChipsTextBox;
        private System.Windows.Forms.Button addChipsButton;

        public System.Windows.Forms.TextBox botOneChipsTextBox;
        public System.Windows.Forms.Label botOneStatus;

        public System.Windows.Forms.TextBox botTwoChipsTextBox;
        public System.Windows.Forms.Label botTwoStatus;

        public System.Windows.Forms.TextBox botThreeChipsTextBox;
        public System.Windows.Forms.Label botThreeStatus;

        public System.Windows.Forms.TextBox botFourChipsTextBox;
        public System.Windows.Forms.Label botFourStatus;

        public System.Windows.Forms.TextBox botFiveChipsTextBox;
        public System.Windows.Forms.Label botFiveStatus;

        private System.Windows.Forms.Button blindOptions;
        private System.Windows.Forms.TextBox smallBlindTextBox;
        private System.Windows.Forms.Button smallBlindButton;
        private System.Windows.Forms.TextBox bigBlindTextBox;
        private System.Windows.Forms.Button bigBlindButton;




    }
}

