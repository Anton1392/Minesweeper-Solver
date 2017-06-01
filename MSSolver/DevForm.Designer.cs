namespace MSSolver
{
    partial class DevForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.btnWindowMoveTest = new System.Windows.Forms.Button();
            this.picImageTest = new System.Windows.Forms.PictureBox();
            this.btnScreenshotTest = new System.Windows.Forms.Button();
            this.btnMouseMoveClickTest = new System.Windows.Forms.Button();
            this.checkRightClick = new System.Windows.Forms.CheckBox();
            this.btnDetectionTest = new System.Windows.Forms.Button();
            this.lstBoard = new System.Windows.Forms.ListBox();
            this.btnFlagTest = new System.Windows.Forms.Button();
            this.btnTestSolve = new System.Windows.Forms.Button();
            this.rdBeginner = new System.Windows.Forms.RadioButton();
            this.rdIntermediate = new System.Windows.Forms.RadioButton();
            this.rdExpert = new System.Windows.Forms.RadioButton();
            this.chckAllowRand = new System.Windows.Forms.CheckBox();
            this.btnTestLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImageTest)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWindowMoveTest
            // 
            this.btnWindowMoveTest.Location = new System.Drawing.Point(13, 13);
            this.btnWindowMoveTest.Name = "btnWindowMoveTest";
            this.btnWindowMoveTest.Size = new System.Drawing.Size(183, 23);
            this.btnWindowMoveTest.TabIndex = 0;
            this.btnWindowMoveTest.Text = "Test Minesweeper window moving.";
            this.btnWindowMoveTest.UseVisualStyleBackColor = true;
            this.btnWindowMoveTest.Click += new System.EventHandler(this.btnWindowMoveTest_Click);
            // 
            // picImageTest
            // 
            this.picImageTest.Location = new System.Drawing.Point(263, 12);
            this.picImageTest.Name = "picImageTest";
            this.picImageTest.Size = new System.Drawing.Size(143, 122);
            this.picImageTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImageTest.TabIndex = 1;
            this.picImageTest.TabStop = false;
            // 
            // btnScreenshotTest
            // 
            this.btnScreenshotTest.Location = new System.Drawing.Point(13, 43);
            this.btnScreenshotTest.Name = "btnScreenshotTest";
            this.btnScreenshotTest.Size = new System.Drawing.Size(183, 23);
            this.btnScreenshotTest.TabIndex = 2;
            this.btnScreenshotTest.Text = "Test screenshot function.";
            this.btnScreenshotTest.UseVisualStyleBackColor = true;
            this.btnScreenshotTest.Click += new System.EventHandler(this.btnScreenshotTest_Click);
            // 
            // btnMouseMoveClickTest
            // 
            this.btnMouseMoveClickTest.Location = new System.Drawing.Point(13, 73);
            this.btnMouseMoveClickTest.Name = "btnMouseMoveClickTest";
            this.btnMouseMoveClickTest.Size = new System.Drawing.Size(162, 23);
            this.btnMouseMoveClickTest.TabIndex = 3;
            this.btnMouseMoveClickTest.Text = "Test mouse movement + click.";
            this.btnMouseMoveClickTest.UseVisualStyleBackColor = true;
            this.btnMouseMoveClickTest.Click += new System.EventHandler(this.btnMouseMoveClickTest_Click);
            // 
            // checkRightClick
            // 
            this.checkRightClick.AutoSize = true;
            this.checkRightClick.Location = new System.Drawing.Point(181, 77);
            this.checkRightClick.Name = "checkRightClick";
            this.checkRightClick.Size = new System.Drawing.Size(76, 17);
            this.checkRightClick.TabIndex = 4;
            this.checkRightClick.Text = "Right-click";
            this.checkRightClick.UseVisualStyleBackColor = true;
            // 
            // btnDetectionTest
            // 
            this.btnDetectionTest.Location = new System.Drawing.Point(13, 103);
            this.btnDetectionTest.Name = "btnDetectionTest";
            this.btnDetectionTest.Size = new System.Drawing.Size(183, 23);
            this.btnDetectionTest.TabIndex = 5;
            this.btnDetectionTest.Text = "Test detection for beginner board.";
            this.btnDetectionTest.UseVisualStyleBackColor = true;
            this.btnDetectionTest.Click += new System.EventHandler(this.btnDetectionTest_Click);
            // 
            // lstBoard
            // 
            this.lstBoard.FormattingEnabled = true;
            this.lstBoard.Location = new System.Drawing.Point(13, 270);
            this.lstBoard.Name = "lstBoard";
            this.lstBoard.Size = new System.Drawing.Size(209, 199);
            this.lstBoard.TabIndex = 6;
            // 
            // btnFlagTest
            // 
            this.btnFlagTest.Location = new System.Drawing.Point(13, 133);
            this.btnFlagTest.Name = "btnFlagTest";
            this.btnFlagTest.Size = new System.Drawing.Size(183, 23);
            this.btnFlagTest.TabIndex = 7;
            this.btnFlagTest.Text = "Flag the square at 2,3";
            this.btnFlagTest.UseVisualStyleBackColor = true;
            this.btnFlagTest.Click += new System.EventHandler(this.btnFlagTest_Click);
            // 
            // btnTestSolve
            // 
            this.btnTestSolve.Location = new System.Drawing.Point(13, 163);
            this.btnTestSolve.Name = "btnTestSolve";
            this.btnTestSolve.Size = new System.Drawing.Size(183, 23);
            this.btnTestSolve.TabIndex = 8;
            this.btnTestSolve.Text = "Test solving.";
            this.btnTestSolve.UseVisualStyleBackColor = true;
            this.btnTestSolve.Click += new System.EventHandler(this.btnTestSolve_Click);
            // 
            // rdBeginner
            // 
            this.rdBeginner.AutoSize = true;
            this.rdBeginner.Location = new System.Drawing.Point(202, 166);
            this.rdBeginner.Name = "rdBeginner";
            this.rdBeginner.Size = new System.Drawing.Size(31, 17);
            this.rdBeginner.TabIndex = 9;
            this.rdBeginner.Text = "b";
            this.rdBeginner.UseVisualStyleBackColor = true;
            // 
            // rdIntermediate
            // 
            this.rdIntermediate.AutoSize = true;
            this.rdIntermediate.Location = new System.Drawing.Point(230, 166);
            this.rdIntermediate.Name = "rdIntermediate";
            this.rdIntermediate.Size = new System.Drawing.Size(27, 17);
            this.rdIntermediate.TabIndex = 10;
            this.rdIntermediate.Text = "i";
            this.rdIntermediate.UseVisualStyleBackColor = true;
            // 
            // rdExpert
            // 
            this.rdExpert.AutoSize = true;
            this.rdExpert.Checked = true;
            this.rdExpert.Location = new System.Drawing.Point(253, 166);
            this.rdExpert.Name = "rdExpert";
            this.rdExpert.Size = new System.Drawing.Size(31, 17);
            this.rdExpert.TabIndex = 11;
            this.rdExpert.TabStop = true;
            this.rdExpert.Text = "e";
            this.rdExpert.UseVisualStyleBackColor = true;
            // 
            // chckAllowRand
            // 
            this.chckAllowRand.AutoSize = true;
            this.chckAllowRand.Checked = true;
            this.chckAllowRand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckAllowRand.Location = new System.Drawing.Point(291, 166);
            this.chckAllowRand.Name = "chckAllowRand";
            this.chckAllowRand.Size = new System.Drawing.Size(119, 17);
            this.chckAllowRand.TabIndex = 12;
            this.chckAllowRand.Text = "Allow random clicks";
            this.chckAllowRand.UseVisualStyleBackColor = true;
            // 
            // btnTestLog
            // 
            this.btnTestLog.Location = new System.Drawing.Point(13, 193);
            this.btnTestLog.Name = "btnTestLog";
            this.btnTestLog.Size = new System.Drawing.Size(183, 23);
            this.btnTestLog.TabIndex = 13;
            this.btnTestLog.Text = "Test Log writing + saving";
            this.btnTestLog.UseVisualStyleBackColor = true;
            this.btnTestLog.Click += new System.EventHandler(this.btnTestLog_Click);
            // 
            // DevForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 487);
            this.Controls.Add(this.btnTestLog);
            this.Controls.Add(this.chckAllowRand);
            this.Controls.Add(this.rdExpert);
            this.Controls.Add(this.rdIntermediate);
            this.Controls.Add(this.rdBeginner);
            this.Controls.Add(this.btnTestSolve);
            this.Controls.Add(this.btnFlagTest);
            this.Controls.Add(this.lstBoard);
            this.Controls.Add(this.btnDetectionTest);
            this.Controls.Add(this.checkRightClick);
            this.Controls.Add(this.btnMouseMoveClickTest);
            this.Controls.Add(this.btnScreenshotTest);
            this.Controls.Add(this.picImageTest);
            this.Controls.Add(this.btnWindowMoveTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DevForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DevForm";
            ((System.ComponentModel.ISupportInitialize)(this.picImageTest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWindowMoveTest;
        private System.Windows.Forms.PictureBox picImageTest;
        private System.Windows.Forms.Button btnScreenshotTest;
        private System.Windows.Forms.Button btnMouseMoveClickTest;
        private System.Windows.Forms.CheckBox checkRightClick;
        private System.Windows.Forms.Button btnDetectionTest;
        private System.Windows.Forms.ListBox lstBoard;
        private System.Windows.Forms.Button btnFlagTest;
        private System.Windows.Forms.Button btnTestSolve;
        private System.Windows.Forms.RadioButton rdBeginner;
        private System.Windows.Forms.RadioButton rdIntermediate;
        private System.Windows.Forms.RadioButton rdExpert;
        private System.Windows.Forms.CheckBox chckAllowRand;
        private System.Windows.Forms.Button btnTestLog;
    }
}