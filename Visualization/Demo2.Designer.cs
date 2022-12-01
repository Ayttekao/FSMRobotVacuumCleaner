namespace Visualization
{
    partial class Demo2
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_play = new System.Windows.Forms.Button();
            this.stateLabel = new System.Windows.Forms.Label();
            this.batteryImage = new System.Windows.Forms.PictureBox();
            this.imageFull = new System.Windows.Forms.PictureBox();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.batteryImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFull)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(402, 4);
            this.btn_play.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(75, 37);
            this.btn_play.TabIndex = 1;
            this.btn_play.Text = "Start";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.StartStopButtonClick);
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.stateLabel.Location = new System.Drawing.Point(402, 168);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(96, 20);
            this.stateLabel.TabIndex = 7;
            this.stateLabel.Text = "Current state:";
            // 
            // batteryImage
            // 
            this.batteryImage.Image = global::Visualization.Properties.Resources.BATTERY_INIT;
            this.batteryImage.Location = new System.Drawing.Point(402, 48);
            this.batteryImage.Name = "batteryImage";
            this.batteryImage.Size = new System.Drawing.Size(75, 37);
            this.batteryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.batteryImage.TabIndex = 8;
            this.batteryImage.TabStop = false;
            // 
            // imageFull
            // 
            this.imageFull.Image = global::Visualization.Properties.Resources.FULL_ICO;
            this.imageFull.Location = new System.Drawing.Point(402, 91);
            this.imageFull.Name = "imageFull";
            this.imageFull.Size = new System.Drawing.Size(75, 65);
            this.imageFull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageFull.TabIndex = 9;
            this.imageFull.TabStop = false;
            // 
            // stateTextBox
            // 
            this.stateTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.stateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stateTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.stateTextBox.Location = new System.Drawing.Point(407, 191);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(160, 20);
            this.stateTextBox.TabIndex = 10;
            this.stateTextBox.Text = "Start";
            // 
            // Demo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.stateTextBox);
            this.Controls.Add(this.imageFull);
            this.Controls.Add(this.batteryImage);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.btn_play);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Demo2";
            this.Size = new System.Drawing.Size(666, 427);
            ((System.ComponentModel.ISupportInitialize)(this.batteryImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFull)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.PictureBox batteryImage;
        private System.Windows.Forms.PictureBox imageFull;
        private System.Windows.Forms.TextBox stateTextBox;
    }
}
