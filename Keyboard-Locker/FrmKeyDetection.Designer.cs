namespace Keyboard_Locker
{
    partial class FrmKeyDetection
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
            this.txtKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtKey.Location = new System.Drawing.Point(0, 0);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(634, 361);
            this.txtKey.TabIndex = 0;
            // 
            // FrmKeyDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 361);
            this.Controls.Add(this.txtKey);
            this.Name = "FrmKeyDetection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Key Detection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKeyDetection_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtKey;
    }
}