using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

public class Global : System.Web.HttpApplication
{
    #region --- Web Form Designer generated code ---
    private void InitializeComponent()
    {
    }
    #endregion

    public Global()
    {
        InitializeComponent();
    }

    protected void Application_Start()
    {
    }

    protected void Session_Start(Object sender, EventArgs e)
    {
        try
        {
            if (Session["UserInfo"] == null)
                FormsAuthentication.SignOut();
        }
        catch (Exception)
        {
        }
    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        try
        {
            switch (System.IO.Path.GetExtension(Request.Path))
            {
                case ".aspx":
                    string[] path = Request.Path.Replace(".aspx", "").Split('-');
                    if (path.Length > 2)
                    {
                        switch (path[(path.Length - 2)])
                        {
                            case "a":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makalegoster&mklid=" + path[(path.Length - 1)], true);
                                break;
                            case "aw":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makale&mklid=" + path[(path.Length - 1)], true);
                                break;
                            case "ac":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makaleliste&kid=" + path[(path.Length - 1)], true);
                                break;
                            case "ad":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makaleliste&hspid=" + path[(path.Length - 1)].Replace(',', '-'), true);
                                break;
                            case "n":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=habergoster&hid=" + path[(path.Length - 1)], true);
                                break;
                            case "nc":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=haberliste&kid=" + path[(path.Length - 1)], true);
                                break;
                            case "v":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=videogoster&vid=" + path[(path.Length - 1)], true);
                                break;
                            case "vu":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=video&vid=" + path[(path.Length - 1)], true);
                                break;
                            case "vc":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=videoliste&kid=" + path[(path.Length - 1)], true);
                                break;
                            case "msg":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=mesajgoster&mid=" + path[(path.Length - 1)], true);
                                break;
                            case "qs":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=mesajliste&hspid=" + path[(path.Length - 1)].Replace(',', '-'), true);
                                break;
                            case "answer":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=mesaj&mid=" + path[(path.Length - 1)], true);
                                break;
                            case "page":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=sayfagoster&sid=" + path[(path.Length - 1)], true);
                                break;
                            case "profil":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=profil&type=profil&url=" + path[(path.Length - 1)], true);
                                break;
                            case "about":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=profil&type=about&url=" + path[(path.Length - 1)], true);
                                break;
                            case "contact":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=profil&type=contact&url=" + path[(path.Length - 1)], true);
                                break;
                            case "randevu":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=profil&type=randevu&url=" + path[(path.Length - 1)].Split(';')[0] + "&caid=" + path[(path.Length - 1)].Split(';')[1].Replace(',', '-'), true);
                                break;
                            case "rt":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=randevutalebi&rndid=" + path[(path.Length - 1)].Replace(',', '-'), true);
                                break;
                            case "work":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=calismaalani&caid=" + path[(path.Length - 1)].Replace(',', '-'), true);
                                break;
                        }
                    }
                    path = null;
                    break;
                case "":
                    string[] url = Request.Path.Replace(".aspx", "").Split('/');
                    if (url.Length >= 2)
                    {
                        switch (url[1])
                        {
                            case "":
                            case "3":
                            case "remember":
                            case "4":
                            case "account":
                                break;
                            case "0":
                            case "logout":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "l=0", true);
                                break;
                            case "1":
                            case "login":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "l=1", true);
                                break;
                            case "2":
                            case "register":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "l=2", true);
                                break;
                            case "password":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "l=3&r=sifre", true);
                                break;
                            case "activation":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "l=3&r=aktivasyon", true);
                                break;
                            case "5":
                            case "myaccount":
                            case "hesabim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "l=5", true);
                                break;
                            case "home":
                            case "index":
                            case "anasayfa":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "ref=" + url[1], true);
                                break;
                            case "contact":
                            case "iletisim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=contact", true);
                                break;
                            case "maps":
                            case "harita":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=harita", true);
                                break;
                            case "doctors":
                            case "doktorlar":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=hesapliste&type=doktor", true);
                                break;
                            case "medical":
                            case "hastaneler":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=hesapliste&type=hastane", true);
                                break;
                            case "yenimakale":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makale", true);
                                break;
                            case "makalelerim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makaleler", true);
                                break;
                            case "makaleler":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=makaleliste", true);
                                break;
                            case "yenivideo":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=video", true);
                                break;
                            case "videolarim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=videolar", true);
                                break;
                            case "videolar":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=videoliste", true);
                                break;
                            case "haberler":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=haberliste", true);
                                break;
                            case "mesajlar":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=mesajlar", true);
                                break;
                            case "sorular":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=mesajliste", true);
                                break;
                            case "sorduklarim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=sorduklarim", true);
                                break;
                            case "yorumlar":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=yorumlar", true);
                                break;
                            case "yorumlarim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=yorumlarim", true);
                                break;
                            case "randevu":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=randevu", true);
                                break;
                            case "yenicalismaalani":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=calismaalani", true);
                                break;
                            case "calismaalanlari":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=calismaalanlari", true);
                                break;
                            case "yenirandevu":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=randevu", true);
                                break;
                            case "randevutalebleri":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=randevutalebleri", true);
                                break;
                            case "randevularim":
                                Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=randevularim", true);
                                break;
                            default:
                                if (url[1].Length > 5 & !Settings.InValidUrl.Contains(";" + url[1].ToLower() + ";"))
                                    Context.RewritePath(Settings.VirtualPath + "Default.aspx", "", "go=profil&type=profil&url=" + url[1], true);
                                break;
                        }
                    }
                    url = null;
                    break;
            }
        }
        catch (Exception)
        {
        }
    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.User != null)
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        HttpContext.Current.User = new GenericPrincipal(id, ticket.UserData.Split(','));
                    }
        }
        catch (Exception)
        {
        }
    }
}