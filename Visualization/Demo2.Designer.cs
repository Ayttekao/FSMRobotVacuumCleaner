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
            ((System.ComponentModel.ISupportInitialize)(this.batteryImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(352, 3);
            this.btn_play.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(66, 28);
            this.btn_play.TabIndex = 1;
            this.btn_play.Text = "Start";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.StartStopButtonClick);
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.stateLabel.Location = new System.Drawing.Point(352, 115);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(134, 15);
            this.stateLabel.TabIndex = 7;
            this.stateLabel.Text = "Current state:  ----------";
            // 
            // batteryImage
            // 
            this.batteryImage.Image = global::Visualization.Properties.Resources.BATTERY_INIT;
            this.batteryImage.Location = new System.Drawing.Point(424, 3);
            this.batteryImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.batteryImage.Name = "batteryImage";
            this.batteryImage.Size = new System.Drawing.Size(66, 28);
            this.batteryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.batteryImage.TabIndex = 8;
            this.batteryImage.TabStop = false;
            // 
            // Demo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.batteryImage);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.btn_play);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Demo2";
            this.Size = new System.Drawing.Size(500, 320);
            ((System.ComponentModel.ISupportInitialize)(this.batteryImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.PictureBox pb_image;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.PictureBox batteryImage;
    }
}
