namespace WinFormLogin {
    partial class LoginByFacebook {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnBack = new System.Windows.Forms.Button();
            this.webBrowserLogin = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(24, 501);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "回前頁";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // webBrowserLogin
            // 
            this.webBrowserLogin.Location = new System.Drawing.Point(1, -2);
            this.webBrowserLogin.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserLogin.Name = "webBrowserLogin";
            this.webBrowserLogin.Size = new System.Drawing.Size(250, 250);
            this.webBrowserLogin.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(269, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 80);
            this.label1.TabIndex = 9;
            this.label1.Text = "1.由工具箱拉進一個「WebBrowser」工具\r\n2.在NuGet套件中增加「Facebook」套件\r\n3.using Facebook;\r\n4.using S" +
    "ystem.Dynamic;\r\n5.";
            // 
            // LoginByFacebook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 568);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webBrowserLogin);
            this.Controls.Add(this.btnBack);
            this.Name = "LoginByFacebook";
            this.Text = "LoginByFacebook";
            this.Load += new System.EventHandler(this.LoginByFacebook_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.WebBrowser webBrowserLogin;
        private System.Windows.Forms.Label label1;
    }
}