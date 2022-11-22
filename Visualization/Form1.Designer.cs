﻿namespace Visualization
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.createRobot = new System.Windows.Forms.Button();
            this.drawingSurface = new Visualization.DrawingSurface();
            this.moveButton = new System.Windows.Forms.Button();
            this.demo21 = new Visualization.Demo2();
            this.SuspendLayout();
            // 
            // createRobot
            // 
            this.createRobot.Location = new System.Drawing.Point(566, 16);
            this.createRobot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.createRobot.Name = "createRobot";
            this.createRobot.Size = new System.Drawing.Size(94, 31);
            this.createRobot.TabIndex = 0;
            this.createRobot.Text = "Create";
            this.createRobot.UseVisualStyleBackColor = true;
            this.createRobot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CreateRobotButtonChangeState);
            // 
            // drawingSurface
            // 
            this.drawingSurface.Location = new System.Drawing.Point(14, 16);
            this.drawingSurface.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.drawingSurface.Name = "drawingSurface";
            this.drawingSurface.Size = new System.Drawing.Size(539, 303);
            this.drawingSurface.TabIndex = 1;
            this.drawingSurface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddObject);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(566, 64);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(94, 29);
            this.moveButton.TabIndex = 2;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoveRobotButton);
            // 
            // demo21
            // 
            this.demo21.Location = new System.Drawing.Point(14, 327);
            this.demo21.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.demo21.Name = "demo21";
            this.demo21.Size = new System.Drawing.Size(602, 305);
            this.demo21.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.demo21);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.drawingSurface);
            this.Controls.Add(this.createRobot);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button createRobot;
        private DrawingSurface drawingSurface;
        private System.Windows.Forms.Button moveButton;
        private Demo2 demo21;
    }
}