using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Facebook;
using System.Dynamic;

namespace WinFormLogin {
    public partial class LoginByFacebook : Form 
    {
        private const string AppId="866140893547638";   //this is my app id
        private Uri _loginUrl;
        private const string _ExtendedPermissions = "user_about_me,publish_stream,offline_access";
        FacebookClient fbClient=new FacebookClient();
        
        
        public LoginByFacebook() {
            InitializeComponent();
        }

        //private void LoginByFacebook_Load(object sender, EventArgs e)
        //{
        //    Login();
        //}
        private void LoginByFacebook_Load(object sender, EventArgs e) {
            Login();
        }


        public void Login()
        {
            dynamic parameters = new ExpandoObject();
            parameters.client_id=AppId;
            parameters.redirect_uri="https://www.facebook.com/connect/login_success.html";

            //  The requested responce: an access token (token), as authorization code (code), or both (code token).
            parameters.response_type = "token";

            //  list of additional display modes can be fount as https://develops.facebook.com/docs/reference/dialogs/............
            parameters.display = "popup";

            //  add the ;scope' parameter only if we have extendedPermissions.
            if (!string.IsNullOrWhiteSpace(_ExtendedPermissions))
                parameters.scope = _ExtendedPermissions;

            //  generate the login url
            var fb = new FacebookClient();
            _loginUrl = fb.GetLoginUrl(parameters);
            //_logoutUrl = fb.GetLogoutUrl(parameters);

            webBrowserLogin.Navigate(_loginUrl.AbsoluteUri);
            //webBrowser1.Navigate(_logoutUrl.AbsoluteUri); //if you want to logout

        }

        private void btnBack_Click(object sender, EventArgs e) {
            Form1 f = new Form1();
            f.Show(this);
            f.TopMost=true;
            this.Close();
        }

    }
}
