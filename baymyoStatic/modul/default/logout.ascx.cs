using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_default_logout : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["l"].Equals("logout") 
            || Request.QueryString["l"].Equals("0"))
        {
            Session.Remove("UserInfo");
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect(Settings.VirtualPath, false);
        }
    }
}