using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormLogin {
    public partial class LoginBySSO : Form {
        public LoginBySSO() {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            Form1 f = new Form1();
            f.Show(this);
            f.TopMost=true;
            this.Close();
        }
    }
}
