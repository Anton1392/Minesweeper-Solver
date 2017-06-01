using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSSolver
{
    public partial class DevForm : Form
    {
        public DevForm()
        {
            InitializeComponent();
        }

        private void btnWindowMoveTest_Click(object sender, EventArgs e)
        {
            // Calls the MSIO function that resets the Minesweeper windowpos to 0,0.
            MSIO.ResetMSWindowPos();
        }

        private void btnScreenshotTest_Click(object sender, EventArgs e)
        {
            // Calls the MSIO function that takes a screenshot, takes a 300x200 screenshot, and sets it as the image of a PictureBox.
            picImageTest.Image = MSIO.Screenshot(0, 0, 300, 200);
        }

        private void btnMouseMoveClickTest_Click(object sender, EventArgs e)
        {
            // Moves the mouse to position 0,0. Then performs a click, or a rightclick.
            MSIO.MoveMouse(0, 0);
            if (checkRightClick.Checked)
            {
                MSIO.RightClick();
            }
            else
            {
                MSIO.Click();
            }
        }

        private void btnDetectionTest_Click(object sender, EventArgs e)
        {
            // Creates a new MSData
            MSData data = new MSData(Difficulty.Beginner);

            // Refresh the board
            data.RefreshBoard();

            // Displays the board visually for debugging purposes.
            lstBoard.Items.Clear();
            for (int y = 0; y < data.Board.GetLength(1); y++)
            {
                string row = "";
                for (int x = 0; x < data.Board.GetLength(0); x++)
                {
                    row += data.Board[x, y].ToString();
                }
                lstBoard.Items.Add(row);
            }
        }

        private void btnFlagTest_Click(object sender, EventArgs e)
        {
            // Creates an MSSolver, then flags a square in it.
            MSSolver solv = new MSSolver(Difficulty.Beginner);
            solv.Flag(2, 3, true);
        }

        private void btnTestSolve_Click(object sender, EventArgs e)
        {
            // Finds the difficulty of the MSSolver.
            Difficulty diff;
            if (rdBeginner.Checked) { diff = Difficulty.Beginner; }
            else if (rdIntermediate.Checked) { diff = Difficulty.Intermediate; }
            else { diff = Difficulty.Expert; }

            // Creates an MSSolver and calls the Solve function.
            MSSolver solv = new MSSolver(diff);
            solv.allowRandomClicks = chckAllowRand.Checked;
            solv.Solve();
        }

        private void btnTestLog_Click(object sender, EventArgs e)
        {
            // Adds messages to two different log files and saves them.
            MSLog.AddMessage("Test number 1");
            MSLog.SaveLog();
            MSLog.ClearLog();
            System.Threading.Thread.Sleep(1000);
            MSLog.AddMessage("Test number 2");
            MSLog.SaveLog();
            MSLog.ClearLog();
        }
    }
}
