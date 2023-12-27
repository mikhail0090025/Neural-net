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
            this.btn_increase_sensivity = new System.Windows.Forms.Button();
            this.btn_reduce_sensivity = new System.Windows.Forms.Button();
            this.lbl_data = new System.Windows.Forms.Label();
            this.lbl_sensivity_learning = new System.Windows.Forms.Label();
            this.btn_learn_untill_stop = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_time_for_1_gen = new System.Windows.Forms.Button();
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
            // btn_increase_sensivity
            // 
            this.btn_increase_sensivity.Location = new System.Drawing.Point(12, 42);
            this.btn_increase_sensivity.Name = "btn_increase_sensivity";
            this.btn_increase_sensivity.Size = new System.Drawing.Size(117, 45);
            this.btn_increase_sensivity.TabIndex = 1;
            this.btn_increase_sensivity.Text = "Increase sensivity learning in 10 times";
            this.btn_increase_sensivity.UseVisualStyleBackColor = true;
            this.btn_increase_sensivity.Click += new System.EventHandler(this.btn_increase_sensivity_Click);
            // 
            // btn_reduce_sensivity
            // 
            this.btn_reduce_sensivity.Location = new System.Drawing.Point(13, 93);
            this.btn_reduce_sensivity.Name = "btn_reduce_sensivity";
            this.btn_reduce_sensivity.Size = new System.Drawing.Size(117, 45);
            this.btn_reduce_sensivity.TabIndex = 2;
            this.btn_reduce_sensivity.Text = "Reduce sensivity learning in 10 times";
            this.btn_reduce_sensivity.UseVisualStyleBackColor = true;
            this.btn_reduce_sensivity.Click += new System.EventHandler(this.btn_reduce_sensivity_Click);
            // 
            // lbl_data
            // 
            this.lbl_data.AutoSize = true;
            this.lbl_data.Location = new System.Drawing.Point(136, 18);
            this.lbl_data.Name = "lbl_data";
            this.lbl_data.Size = new System.Drawing.Size(41, 13);
            this.lbl_data.TabIndex = 3;
            this.lbl_data.Text = "Error: 0";
            // 
            // lbl_sensivity_learning
            // 
            this.lbl_sensivity_learning.AutoSize = true;
            this.lbl_sensivity_learning.Location = new System.Drawing.Point(136, 58);
            this.lbl_sensivity_learning.Name = "lbl_sensivity_learning";
            this.lbl_sensivity_learning.Size = new System.Drawing.Size(52, 13);
            this.lbl_sensivity_learning.TabIndex = 4;
            this.lbl_sensivity_learning.Text = "Sensivity:";
            // 
            // btn_learn_untill_stop
            // 
            this.btn_learn_untill_stop.Location = new System.Drawing.Point(13, 145);
            this.btn_learn_untill_stop.Name = "btn_learn_untill_stop";
            this.btn_learn_untill_stop.Size = new System.Drawing.Size(116, 23);
            this.btn_learn_untill_stop.TabIndex = 5;
            this.btn_learn_untill_stop.Text = "Learn untill stop";
            this.btn_learn_untill_stop.UseVisualStyleBackColor = true;
            this.btn_learn_untill_stop.Click += new System.EventHandler(this.btn_learn_untill_stop_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(13, 175);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(116, 23);
            this.btn_stop.TabIndex = 6;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_time_for_1_gen
            // 
            this.btn_time_for_1_gen.Location = new System.Drawing.Point(13, 205);
            this.btn_time_for_1_gen.Name = "btn_time_for_1_gen";
            this.btn_time_for_1_gen.Size = new System.Drawing.Size(116, 23);
            this.btn_time_for_1_gen.TabIndex = 7;
            this.btn_time_for_1_gen.Text = "Time for 1 generation";
            this.btn_time_for_1_gen.UseVisualStyleBackColor = true;
            this.btn_time_for_1_gen.Click += new System.EventHandler(this.btn_time_for_1_gen_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.btn_time_for_1_gen);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_learn_untill_stop);
            this.Controls.Add(this.lbl_sensivity_learning);
            this.Controls.Add(this.lbl_data);
            this.Controls.Add(this.btn_reduce_sensivity);
            this.Controls.Add(this.btn_increase_sensivity);
            this.Controls.Add(this.btn_pass_one_gn);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_pass_one_gn;
        private System.Windows.Forms.Button btn_increase_sensivity;
        private System.Windows.Forms.Button btn_reduce_sensivity;
        private System.Windows.Forms.Label lbl_data;
        private System.Windows.Forms.Label lbl_sensivity_learning;
        private System.Windows.Forms.Button btn_learn_untill_stop;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_time_for_1_gen;
    }
}