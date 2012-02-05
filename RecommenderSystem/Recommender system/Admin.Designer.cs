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
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLogResample = new System.Windows.Forms.TextBox();
            this.btnTemp = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLogML = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnTest = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStatisticCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMutual_avg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCV = new System.Windows.Forms.Button();
            this.txtCV = new System.Windows.Forms.TextBox();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.btnRecommend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.cbBudget = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbCompanion = new System.Windows.Forms.ComboBox();
            this.cbWeather = new System.Windows.Forms.ComboBox();
            this.txtRecommend = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestML
            // 
            this.btnTestML.Location = new System.Drawing.Point(6, 18);
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
            this.label1.Location = new System.Drawing.Point(22, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "MAE";
            // 
            // txtPerformance
            // 
            this.txtPerformance.Location = new System.Drawing.Point(111, 71);
            this.txtPerformance.Name = "txtPerformance";
            this.txtPerformance.ReadOnly = true;
            this.txtPerformance.Size = new System.Drawing.Size(100, 20);
            this.txtPerformance.TabIndex = 2;
            // 
            // txtCorrelation
            // 
            this.txtCorrelation.Location = new System.Drawing.Point(111, 113);
            this.txtCorrelation.Name = "txtCorrelation";
            this.txtCorrelation.ReadOnly = true;
            this.txtCorrelation.Size = new System.Drawing.Size(100, 20);
            this.txtCorrelation.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Correlation_avg";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabPage1);
            this.tab.Controls.Add(this.tabPage2);
            this.tab.Controls.Add(this.tabPage3);
            this.tab.Controls.Add(this.tabPage4);
            this.tab.Location = new System.Drawing.Point(3, 2);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(425, 363);
            this.tab.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLogResample);
            this.tabPage1.Controls.Add(this.btnTemp);
            this.tabPage1.Controls.Add(this.btnTestML);
            this.tabPage1.Controls.Add(this.txtCorrelation);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtPerformance);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(417, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Movielens 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtLogResample
            // 
            this.txtLogResample.Location = new System.Drawing.Point(37, 165);
            this.txtLogResample.Multiline = true;
            this.txtLogResample.Name = "txtLogResample";
            this.txtLogResample.Size = new System.Drawing.Size(326, 152);
            this.txtLogResample.TabIndex = 6;
            // 
            // btnTemp
            // 
            this.btnTemp.Location = new System.Drawing.Point(277, 18);
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.Size = new System.Drawing.Size(75, 23);
            this.btnTemp.TabIndex = 5;
            this.btnTemp.Text = "Temp";
            this.btnTemp.UseVisualStyleBackColor = true;
            this.btnTemp.Click += new System.EventHandler(this.btnTemp_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLogML);
            this.tabPage2.Controls.Add(this.btnRun);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.txtInput);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnInput);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(417, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Movielens 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLogML
            // 
            this.txtLogML.Location = new System.Drawing.Point(43, 164);
            this.txtLogML.Multiline = true;
            this.txtLogML.Name = "txtLogML";
            this.txtLogML.Size = new System.Drawing.Size(330, 145);
            this.txtLogML.TabIndex = 12;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(242, 91);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(70, 41);
            this.btnRun.TabIndex = 11;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 124);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "MAE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "Correlation_avg";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(108, 82);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 6;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(108, 42);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(233, 20);
            this.txtInput.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Input";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(360, 38);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(40, 24);
            this.btnInput.TabIndex = 0;
            this.btnInput.Text = "...";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.btnTest);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(417, 336);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "RS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(36, 17);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Phase 1";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.txtStatisticCount);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.txtMutual_avg);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.btnCV);
            this.tabPage4.Controls.Add(this.txtCV);
            this.tabPage4.Controls.Add(this.txtSQL);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(417, 336);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Statistic";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(50, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 14);
            this.label9.TabIndex = 22;
            this.label9.Text = "Tổng số đánh giá";
            // 
            // txtStatisticCount
            // 
            this.txtStatisticCount.Location = new System.Drawing.Point(164, 233);
            this.txtStatisticCount.Name = "txtStatisticCount";
            this.txtStatisticCount.Size = new System.Drawing.Size(100, 20);
            this.txtStatisticCount.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(189, 14);
            this.label8.TabIndex = 20;
            this.label8.Text = "Trung bình số đánh giá cho 1 địa điểm";
            // 
            // txtMutual_avg
            // 
            this.txtMutual_avg.Location = new System.Drawing.Point(244, 198);
            this.txtMutual_avg.Name = "txtMutual_avg";
            this.txtMutual_avg.Size = new System.Drawing.Size(100, 20);
            this.txtMutual_avg.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "SQL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "CV";
            // 
            // btnCV
            // 
            this.btnCV.Location = new System.Drawing.Point(164, 274);
            this.btnCV.Name = "btnCV";
            this.btnCV.Size = new System.Drawing.Size(75, 23);
            this.btnCV.TabIndex = 16;
            this.btnCV.Text = "Process";
            this.btnCV.UseVisualStyleBackColor = true;
            this.btnCV.Click += new System.EventHandler(this.btnCV_Click);
            // 
            // txtCV
            // 
            this.txtCV.Location = new System.Drawing.Point(164, 169);
            this.txtCV.Name = "txtCV";
            this.txtCV.Size = new System.Drawing.Size(100, 20);
            this.txtCV.TabIndex = 15;
            // 
            // txtSQL
            // 
            this.txtSQL.Location = new System.Drawing.Point(53, 9);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(330, 145);
            this.txtSQL.TabIndex = 14;
            // 
            // btnRecommend
            // 
            this.btnRecommend.Location = new System.Drawing.Point(254, 111);
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.Size = new System.Drawing.Size(75, 43);
            this.btnRecommend.TabIndex = 1;
            this.btnRecommend.Text = "Recommend";
            this.btnRecommend.UseVisualStyleBackColor = true;
            this.btnRecommend.Click += new System.EventHandler(this.btnRecommend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtRecommend);
            this.groupBox1.Controls.Add(this.cbWeather);
            this.groupBox1.Controls.Add(this.cbCompanion);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbBudget);
            this.groupBox1.Controls.Add(this.dtpTime);
            this.groupBox1.Controls.Add(this.btnRecommend);
            this.groupBox1.Location = new System.Drawing.Point(36, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 284);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Phase 2";
            // 
            // dtpTime
            // 
            this.dtpTime.Location = new System.Drawing.Point(105, 57);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(200, 20);
            this.dtpTime.TabIndex = 2;
            // 
            // cbBudget
            // 
            this.cbBudget.FormattingEnabled = true;
            this.cbBudget.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbBudget.Location = new System.Drawing.Point(105, 92);
            this.cbBudget.Name = "cbBudget";
            this.cbBudget.Size = new System.Drawing.Size(121, 22);
            this.cbBudget.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(59, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 14);
            this.label10.TabIndex = 4;
            this.label10.Text = "Time";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(47, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 14);
            this.label11.TabIndex = 5;
            this.label11.Text = "Budget";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 14);
            this.label12.TabIndex = 6;
            this.label12.Text = "Companion";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(40, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 14);
            this.label13.TabIndex = 7;
            this.label13.Text = "Weather";
            // 
            // cbCompanion
            // 
            this.cbCompanion.FormattingEnabled = true;
            this.cbCompanion.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbCompanion.Location = new System.Drawing.Point(105, 122);
            this.cbCompanion.Name = "cbCompanion";
            this.cbCompanion.Size = new System.Drawing.Size(121, 22);
            this.cbCompanion.TabIndex = 8;
            // 
            // cbWeather
            // 
            this.cbWeather.FormattingEnabled = true;
            this.cbWeather.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbWeather.Location = new System.Drawing.Point(105, 154);
            this.cbWeather.Name = "cbWeather";
            this.cbWeather.Size = new System.Drawing.Size(121, 22);
            this.cbWeather.TabIndex = 9;
            // 
            // txtRecommend
            // 
            this.txtRecommend.Location = new System.Drawing.Point(42, 213);
            this.txtRecommend.Multiline = true;
            this.txtRecommend.Name = "txtRecommend";
            this.txtRecommend.Size = new System.Drawing.Size(262, 56);
            this.txtRecommend.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(57, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 14);
            this.label14.TabIndex = 11;
            this.label14.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(105, 25);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(199, 20);
            this.txtEmail.TabIndex = 12;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 373);
            this.Controls.Add(this.tab);
            this.Name = "Admin";
            this.Text = "Admin";
            this.tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPerformance;
        private System.Windows.Forms.TextBox txtCorrelation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.TextBox txtLogML;
        private System.Windows.Forms.Button btnTemp;
        private System.Windows.Forms.TextBox txtLogResample;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCV;
        private System.Windows.Forms.TextBox txtCV;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMutual_avg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStatisticCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRecommend;
        private System.Windows.Forms.ComboBox cbWeather;
        private System.Windows.Forms.ComboBox cbCompanion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbBudget;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Button btnRecommend;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label14;

    }
}