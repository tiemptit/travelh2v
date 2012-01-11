namespace Recommender_system
{
    partial class Admin
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
            this.btnTestML = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPerformance = new System.Windows.Forms.TextBox();
            this.txtCorrelation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTestML
            // 
            this.btnTestML.Location = new System.Drawing.Point(12, 13);
            this.btnTestML.Name = "btnTestML";
            this.btnTestML.Size = new System.Drawing.Size(97, 37);
            this.btnTestML.TabIndex = 0;
            this.btnTestML.Text = "Test MovieLens";
            this.btnTestML.UseVisualStyleBackColor = true;
            this.btnTestML.Click += new System.EventHandler(this.btnTestML_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "MAE";
            // 
            // txtPerformance
            // 
            this.txtPerformance.Location = new System.Drawing.Point(117, 66);
            this.txtPerformance.Name = "txtPerformance";
            this.txtPerformance.ReadOnly = true;
            this.txtPerformance.Size = new System.Drawing.Size(100, 20);
            this.txtPerformance.TabIndex = 2;
            // 
            // txtCorrelation
            // 
            this.txtCorrelation.Location = new System.Drawing.Point(117, 108);
            this.txtCorrelation.Name = "txtCorrelation";
            this.txtCorrelation.ReadOnly = true;
            this.txtCorrelation.Size = new System.Drawing.Size(100, 20);
            this.txtCorrelation.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Correlation_avg";
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtCorrelation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPerformance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTestML);
            this.Name = "Admin";
            this.Text = "Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPerformance;
        private System.Windows.Forms.TextBox txtCorrelation;
        private System.Windows.Forms.Label label2;

    }
}