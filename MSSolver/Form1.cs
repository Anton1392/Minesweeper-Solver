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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Sets the difficulty drop-down list to beginner by default. This seems to be the only way to do it.
            ddlDifficulty.SelectedIndex = 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Keyboard shortcut to developer mode, Ctrl+D.
            if (e.Control && e.KeyCode == Keys.D)
            {
                // Creates a new instance of the DevForm form.
                DevForm devFrm = new DevForm();

                // Shows the DevForm as a dialog box.
                devFrm.ShowDialog();
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            // Creates the difficulty preset for the Solver.
            Difficulty diff = Difficulty.Beginner;
            if (ddlDifficulty.SelectedItem.ToString() == "Beginner") { diff = Difficulty.Beginner; }
            else if (ddlDifficulty.SelectedItem.ToString() == "Intermediate") { diff = Difficulty.Intermediate; }
            else if (ddlDifficulty.SelectedItem.ToString() == "Expert"){ diff = Difficulty.Expert; }

            // Creates a new Solver with the above difficulty.
            MSSolver solv = new MSSolver(diff);

            // Enables random clicks within the solver.
            solv.allowRandomClicks = true;

            // Starts solving.
            solv.Solve();
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            // Creates an instance of the help form, and then displays it as a dialog box.
            HelpForm helpFrm = new HelpForm();
            helpFrm.ShowDialog();
        }
    }
}
