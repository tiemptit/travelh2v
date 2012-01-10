using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecommenderSystem.Core.RS_Core;

namespace Recommender_system
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {

        }

        private void btnTestML_Click(object sender, EventArgs e)
        {
            Reduction.Test();
        }
    }
}
