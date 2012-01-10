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
            this.SuspendLayout();
            // 
            // btnTestML
            // 
            this.btnTestML.Location = new System.Drawing.Point(12, 12);
            this.btnTestML.Name = "btnTestML";
            this.btnTestML.Size = new System.Drawing.Size(97, 34);
            this.btnTestML.TabIndex = 0;
            this.btnTestML.Text = "Test MovieLens";
            this.btnTestML.UseVisualStyleBackColor = true;
            this.btnTestML.Click += new System.EventHandler(this.btnTestML_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 243);
            this.Controls.Add(this.btnTestML);
            this.Name = "Admin";
            this.Text = "Admin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestML;

    }
}