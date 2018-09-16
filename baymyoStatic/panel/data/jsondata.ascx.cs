using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_data_jsondata : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "JSon Data", "Yönetimi");
        CustomizeControl1.RemoveVisible = false;
        CustomizeControl1.SubmitVisible = false;

        Button btn = new Button();
        btn.ID = "manset";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Manşet", btn);

        btn = new Button();
        btn.ID = "makaleweekof";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Haftanın Makalesi", btn);

        btn = new Button();
        btn.ID = "makaledate";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Son 5 Makale", btn);

        btn = new Button();
        btn.ID = "makalepopuler";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Populer Makaleler", btn);

        btn = new Button();
        btn.ID = "videodate";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Son Videolar", btn);

        btn = new Button();
        btn.ID = "videopopuler";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Populer Videolar", btn);

        btn = new Button();
        btn.ID = "uyedate";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Son 5 Doktor", btn);

        btn = new Button();
        btn.ID = "uyepopuler";
        btn.Text = "Tıklayın";
        btn.Click += new EventHandler(btn_Click);
        CustomizeControl1.AddControl("Populer Doktorlar", btn);

        base.OnInit(e);
    }

    void btn_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text.Equals("Oluşturuldu!"))
            return;
        string title = ((Button)sender).ID;
        switch (title)
        {
            case "manset":
                Settings.CreateJSonData(title, Lib.MansetMethods.GetJSON(9));
                break;
            case "makaleweekof":
                Settings.CreateJSonData(title, Lib.MakaleMethods.GetJSON(QueryType.WeekOfThe, 1));
                break;
            case "makaledate":
                Settings.CreateJSonData(title, Lib.MakaleMethods.GetJSON(QueryType.Date, 5));
                break;
            case "makalepopuler":
                Settings.CreateJSonData(title, Lib.MakaleMethods.GetJSON(QueryType.Populer, 5));
                break;
            case "videodate":
                Settings.CreateJSonData(title, Lib.VideoMethods.GetJSON(QueryType.Date, 12));
                break;
            case "videopopuler":
                Settings.CreateJSonData(title, Lib.VideoMethods.GetJSON(QueryType.Populer, 12));
                break;
            case "uyedate":
                Settings.CreateJSonData(title, Lib.HesapMethods.GetJSON(QueryType.Date, 5));
                break;
            case "uyepopuler":
                Settings.CreateJSonData(title, Lib.HesapMethods.GetJSON(QueryType.Populer, 18));
                break;
        }
        title = null;
        ((Button)sender).Text = "Oluşturuldu!";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}