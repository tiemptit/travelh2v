namespace Recommender_system
{
    partial class Form1
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
            this.btnTest = new System.Windows.Forms.Button();
            this.btnImportExcelData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEstimate = new System.Windows.Forms.Button();
            this.btnTestAdomd = new System.Windows.Forms.Button();
            this.btnTemp = new System.Windows.Forms.Button();
            this.cboCompanion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFamiliarity = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboMood = new System.Windows.Forms.ComboBox();
            this.btnEstimate_Context = new System.Windows.Forms.Button();
            this.btnTestMatrix = new System.Windows.Forms.Button();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(24, 25);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnImportExcelData
            // 
            this.btnImportExcelData.Location = new System.Drawing.Point(139, 25);
            this.btnImportExcelData.Name = "btnImportExcelData";
            this.btnImportExcelData.Size = new System.Drawing.Size(115, 23);
            this.btnImportExcelData.TabIndex = 1;
            this.btnImportExcelData.Text = "Import Excel Data";
            this.btnImportExcelData.UseVisualStyleBackColor = true;
            this.btnImportExcelData.Click += new System.EventHandler(this.btnImportExcelData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(116, 84);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 20);
            this.txtUser.TabIndex = 3;
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(116, 125);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(100, 20);
            this.txtItem.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Item";
            // 
            // btnEstimate
            // 
            this.btnEstimate.Location = new System.Drawing.Point(105, 178);
            this.btnEstimate.Name = "btnEstimate";
            this.btnEstimate.Size = new System.Drawing.Size(75, 23);
            this.btnEstimate.TabIndex = 6;
            this.btnEstimate.Text = "Estimate";
            this.btnEstimate.UseVisualStyleBackColor = true;
            this.btnEstimate.Click += new System.EventHandler(this.btnEstimate_Click);
            // 
            // btnTestAdomd
            // 
            this.btnTestAdomd.Location = new System.Drawing.Point(24, 227);
            this.btnTestAdomd.Name = "btnTestAdomd";
            this.btnTestAdomd.Size = new System.Drawing.Size(75, 23);
            this.btnTestAdomd.TabIndex = 7;
            this.btnTestAdomd.Text = "Test Cube";
            this.btnTestAdomd.UseVisualStyleBackColor = true;
            this.btnTestAdomd.Click += new System.EventHandler(this.btnTestAdomd_Click);
            // 
            // btnTemp
            // 
            this.btnTemp.Location = new System.Drawing.Point(179, 227);
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.Size = new System.Drawing.Size(75, 23);
            this.btnTemp.TabIndex = 8;
            this.btnTemp.Text = "Temp";
            this.btnTemp.UseVisualStyleBackColor = true;
            this.btnTemp.Click += new System.EventHandler(this.btnTemp_Click);
            // 
            // cboCompanion
            // 
            this.cboCompanion.FormattingEnabled = true;
            this.cboCompanion.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboCompanion.Location = new System.Drawing.Point(457, 22);
            this.cboCompanion.Name = "cboCompanion";
            this.cboCompanion.Size = new System.Drawing.Size(121, 22);
            this.cboCompanion.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "Companion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "Familiarity";
            // 
            // cboFamiliarity
            // 
            this.cboFamiliarity.FormattingEnabled = true;
            this.cboFamiliarity.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cboFamiliarity.Location = new System.Drawing.Point(457, 59);
            this.cboFamiliarity.Name = "cboFamiliarity";
            this.cboFamiliarity.Size = new System.Drawing.Size(121, 22);
            this.cboFamiliarity.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(395, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "Mood";
            // 
            // cboMood
            // 
            this.cboMood.FormattingEnabled = true;
            this.cboMood.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.cboMood.Location = new System.Drawing.Point(457, 102);
            this.cboMood.Name = "cboMood";
            this.cboMood.Size = new System.Drawing.Size(121, 22);
            this.cboMood.TabIndex = 13;
            // 
            // btnEstimate_Context
            // 
            this.btnEstimate_Context.Location = new System.Drawing.Point(457, 156);
            this.btnEstimate_Context.Name = "btnEstimate_Context";
            this.btnEstimate_Context.Size = new System.Drawing.Size(75, 23);
            this.btnEstimate_Context.TabIndex = 15;
            this.btnEstimate_Context.Text = "Estimate";
            this.btnEstimate_Context.UseVisualStyleBackColor = true;
            this.btnEstimate_Context.Click += new System.EventHandler(this.btnEstimate_Context_Click);
            // 
            // btnTestMatrix
            // 
            this.btnTestMatrix.Location = new System.Drawing.Point(312, 227);
            this.btnTestMatrix.Name = "btnTestMatrix";
            this.btnTestMatrix.Size = new System.Drawing.Size(75, 23);
            this.btnTestMatrix.TabIndex = 16;
            this.btnTestMatrix.Text = "Test Matrix";
            this.btnTestMatrix.UseVisualStyleBackColor = true;
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.Location = new System.Drawing.Point(457, 203);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(94, 47);
            this.btnEvaluate.TabIndex = 17;
            this.btnEvaluate.Text = "Test Evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 262);
            this.Controls.Add(this.btnEvaluate);
            this.Controls.Add(this.btnTestMatrix);
            this.Controls.Add(this.btnEstimate_Context);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboMood);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboFamiliarity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCompanion);
            this.Controls.Add(this.btnTemp);
            this.Controls.Add(this.btnTestAdomd);
            this.Controls.Add(this.btnEstimate);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImportExcelData);
            this.Controls.Add(this.btnTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnImportExcelData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEstimate;
        private System.Windows.Forms.Button btnTestAdomd;
        private System.Windows.Forms.Button btnTemp;
        private System.Windows.Forms.ComboBox cboCompanion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboFamiliarity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboMood;
        private System.Windows.Forms.Button btnEstimate_Context;
        private System.Windows.Forms.Button btnTestMatrix;
        private System.Windows.Forms.Button btnEvaluate;
    }
}

