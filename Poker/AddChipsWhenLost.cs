using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
/// <summary>
/// Form that shows up only when we've lost our chips.
/// </summary>
    public partial class AddChipsWhenLost : Form
    {
        #region fields
        private int addChipsValue;
        private const int MaximumChipsCanBeAdded = 100000000;
        #endregion

        #region constructor
        public AddChipsWhenLost()
        {
            FontFamily fontFamily = new FontFamily("Arial");
            InitializeComponent();
            ControlBox = false;
            label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }
        #endregion

        #region properties
        public int AddChipsValue
        {
            get { return this.addChipsValue; }
            set { this.addChipsValue = value; }
        }
        #endregion

        #region event button methods
        /// <summary>
        /// Event that listens if the add button is clicked on the window when we lose all our chips.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddButton(object sender, EventArgs e)
        {
            int parsedValue;

            // If the value we try to add is more than the maximum
            if (int.Parse(addChipsTextBox.Text) > MaximumChipsCanBeAdded)
            {
                MessageBox.Show("The maximium chips you can add is 100000000");
                return;
            }
            //If the value we try to add is not a number
            if (!int.TryParse(addChipsTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;

            }
            // Add the value
            else if (int.TryParse(addChipsTextBox.Text, out parsedValue) && int.Parse(addChipsTextBox.Text) <= MaximumChipsCanBeAdded)
            {
                this.AddChipsValue = int.Parse(addChipsTextBox.Text);
                this.Close();
            }
        }

        /// <summary>
        /// Event that listens if the quit button is clicked on the window when we lose all our chips.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitButton(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(
            message, title,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }
        #endregion
    }
}
