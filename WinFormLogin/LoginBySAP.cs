using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormLogin {
    public partial class LoginBySAP : Form {
        public LoginBySAP() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (txtAccount.Text == "" || txtPassword.Text == "") {
                label4.Text = "部分資料未輸入";
            }
            else {
                Portal.SIM_ServiceService obj = new Portal.SIM_ServiceService();
                Portal.tkUperson p = default(Portal.tkUperson);

                try {
                    p = obj.authentication(txtAccount.Text, txtPassword.Text, "","");
                    if (p.uid == txtAccount.Text) {
                        //return true;
                        label4.Text="帳號密碼正確";
                    }
                }
                catch (Exception ex) {
                    //return false;
                    label4.Text="帳號密碼錯誤";
                } 
                finally {
                    obj.Dispose();
                }        
            }
        }

        private void btnBack_Click(object sender, EventArgs e) {
            Form1 f = new Form1();
            f.Show(this);
            f.TopMost=true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            txtAccount.Text="";
            txtPassword.Text="";
            label4.Text="";
        }

        private void txtAccount_TextChanged(object sender, EventArgs e) {
            label4.Text="";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {
            label4.Text="";
        }
    }
}
