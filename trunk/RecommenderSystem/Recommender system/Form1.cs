using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecommenderSystem.Core;
using RecommenderSystem.Core.Helper;
using RecommenderSystem.Core.RS_Core;
using RecommenderSystem.Core.Model;


namespace Recommender_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (DbHelper.Test())
                MessageBox.Show("OK");
            else
                MessageBox.Show("Oops!");
        }

        private void btnImportExcelData_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xlsx|*.xlsx";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataSync dsync = new DataSync();
                bool result = dsync.ImportExcelData(dlg.FileName);

                if (result == true)
                {
                    MessageBox.Show("Excel Data import OK");
                }
                else
                {
                    MessageBox.Show("Problem import Excel data, please check log file for details");
                }
            }
        }

        private void btnEstimate_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(txtUser.Text);
            int item_id = Convert.ToInt32(txtItem.Text);
            DataTable FullSegment = Rating.GetFullSegment();
            double estimate = CollaborativeFiltering.EstimateRating(new Item(item_id), new User(user_id), FullSegment);
            MessageBox.Show(estimate.ToString());
        }

        private void btnTestAdomd_Click(object sender, EventArgs e)
        {
            //if (DbHelper.TestADOMD())
            //    MessageBox.Show("OK");
            //else
            //    MessageBox.Show("Oops!");
            string mdx = "select ([Dim Budget].[Budget].Members, [Dim Companion].[Companion].Members, [Dim User].[Email].Members) ON COLUMNS, [Dim Place].[Name].Members ON ROWS from [Travel H2V DW]";

            DbHelper.RunMDX(mdx);
        }
    }
}
