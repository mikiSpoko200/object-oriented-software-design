﻿namespace zadanie_2
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.kółkoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kwadratToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ątToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kółkoToolStripMenuItem,
            this.kwadratToolStripMenuItem,
            this.ątToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // kółkoToolStripMenuItem
            // 
            this.kółkoToolStripMenuItem.Name = "kółkoToolStripMenuItem";
            this.kółkoToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.kółkoToolStripMenuItem.Text = "Kółko";
            // 
            // kwadratToolStripMenuItem
            // 
            this.kwadratToolStripMenuItem.Name = "kwadratToolStripMenuItem";
            this.kwadratToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.kwadratToolStripMenuItem.Text = "Kwadrat";
            // 
            // ątToolStripMenuItem
            // 
            this.ątToolStripMenuItem.Name = "ątToolStripMenuItem";
            this.ątToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.ątToolStripMenuItem.Text = "Prostokat";
            this.ątToolStripMenuItem.Click += new System.EventHandler(this.ątToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem kółkoToolStripMenuItem;
        private ToolStripMenuItem kwadratToolStripMenuItem;
        private ToolStripMenuItem ątToolStripMenuItem;
    }
}