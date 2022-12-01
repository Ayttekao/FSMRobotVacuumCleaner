namespace Visualization
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
            this.demo21 = new Visualization.Demo2();
            this.topPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.hideForm = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // demo21
            // 
            this.demo21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.demo21.Location = new System.Drawing.Point(15, 73);
            this.demo21.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.demo21.Name = "demo21";
            this.demo21.Size = new System.Drawing.Size(571, 427);
            this.demo21.TabIndex = 3;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.MidnightBlue;
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.button2);
            this.topPanel.Controls.Add(this.hideForm);
            this.topPanel.Location = new System.Drawing.Point(0, -1);
            this.topPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(754, 49);
            this.topPanel.TabIndex = 4;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeDragable);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragForm);
            this.topPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DisableDrag);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Robot Vacuum Cleaner";
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::Visualization.Properties.Resources.icons8_delete_64px;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button2.Location = new System.Drawing.Point(713, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 44);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // hideForm
            // 
            this.hideForm.BackgroundImage = global::Visualization.Properties.Resources.icons8_subtract_64px;
            this.hideForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hideForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideForm.ForeColor = System.Drawing.Color.MidnightBlue;
            this.hideForm.Location = new System.Drawing.Point(668, 4);
            this.hideForm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hideForm.Name = "hideForm";
            this.hideForm.Size = new System.Drawing.Size(38, 44);
            this.hideForm.TabIndex = 0;
            this.hideForm.UseVisualStyleBackColor = true;
            this.hideForm.Click += new System.EventHandler(this.HideForm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(754, 573);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.demo21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Demo2 demo21;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button hideForm;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}