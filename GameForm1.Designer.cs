﻿
namespace chế_độ_cổ_điển
{
    partial class GameForm1
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
            this.SuspendLayout();
            // 
            // GameForm1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "GameForm1";
            this.Text = "Caro Game";
            this.Load += new System.EventHandler(this.GameForm1_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}