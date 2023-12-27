namespace Neural_net
{
    partial class MainWindow
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
            this.btn_pass_one_gn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_pass_one_gn
            // 
            this.btn_pass_one_gn.Location = new System.Drawing.Point(13, 13);
            this.btn_pass_one_gn.Name = "btn_pass_one_gn";
            this.btn_pass_one_gn.Size = new System.Drawing.Size(117, 23);
            this.btn_pass_one_gn.TabIndex = 0;
            this.btn_pass_one_gn.Text = "Pass one generation";
            this.btn_pass_one_gn.UseVisualStyleBackColor = true;
            this.btn_pass_one_gn.Click += new System.EventHandler(this.btn_pass_one_gn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.btn_pass_one_gn);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main window";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_pass_one_gn;
    }
}