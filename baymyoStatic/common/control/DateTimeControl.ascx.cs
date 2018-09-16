using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DateTimeControl : System.Web.UI.UserControl
{
    public enum CreateType
    {
        None,
        Randevu,
        DogumTarihi
    }
    public CreateType OlusturmaTipi { get { return (CreateType)ViewState["OlusturmaTipi"]; } set { ViewState["OlusturmaTipi"] = value; } }

    DateTime m_TarihSaat;
    public DateTime TarihSaat
    {
        get
        {
            switch (OlusturmaTipi)
            {
                case CreateType.Randevu:
                    return Convert.ToDateTime(string.Format("{0}/{1}/{2} {3}:{4}:00", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue, ddlSaat.SelectedValue, ddlDakika.SelectedValue));
                case CreateType.DogumTarihi:
                    return Convert.ToDateTime(string.Format("{0}/{1}/{2} 00:00:00", ddlGun.SelectedValue, ddlAy.SelectedValue, ddlYil.SelectedValue));
                default:
                    return DateTime.Now;
            }
        }
        set
        {
            m_TarihSaat = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (OlusturmaTipi)
            {
                case CreateType.Randevu:
                    DateTime tempDate = DateTime.Now;
                    int startIndex = 0;
                    if (tempDate.Hour > 13)
                        startIndex = 1;
                    for (int i = startIndex; i < 8; i++)
                    {
                        tempDate = DateTime.Today.AddDays(i);
                        //if (!tempDate.DayOfWeek.Equals(DayOfWeek.Sunday) & !tempDate.DayOfWeek.Equals(DayOfWeek.Saturday))
                        //{
                        ddlGun.Items.Add(new ListItem(tempDate.Day.ToString("0#"), tempDate.Day.ToString("0#")));

                        if (ddlAy.Items.FindByValue(tempDate.Month.ToString("0#")) == null)
                            ddlAy.Items.Add(new ListItem(tempDate.Month.ToString("0#"), tempDate.Month.ToString("0#")));

                        if (ddlYil.Items.FindByValue(tempDate.Year.ToString("0000")) == null)
                            ddlYil.Items.Add(new ListItem(tempDate.Year.ToString("0000"), tempDate.Year.ToString("0000")));
                        //}
                    }
                    for (int i = 8; i <= 18; i++)
                        ddlSaat.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                    ddlDakika.Items.Add(new ListItem("00", "00"));
                    ddlDakika.Items.Add(new ListItem("30", "30"));
                    pnlTime.Visible = true;
                    CurrentDate(2011);
                    break;
                case CreateType.DogumTarihi:
                    for (int i = 1; i <= 31; i++)
                        ddlGun.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                    for (int i = 1; i <= 12; i++)
                        ddlAy.Items.Add(new ListItem(i.ToString("0#"), i.ToString("0#")));
                    Int16 year = Convert.ToInt16(DateTime.Now.Year - 7);
                    for (int i = 1923; i < year; i++)
                        ddlYil.Items.Add(new ListItem(i.ToString("0000"), i.ToString("0000")));
                    pnlTime.Visible = false;
                    if (m_TarihSaat.Year > 1923)
                    {
                        ddlGun.SelectedValue = m_TarihSaat.Day.ToString("00");
                        ddlAy.SelectedValue = m_TarihSaat.Month.ToString("00");
                        ddlYil.SelectedValue = m_TarihSaat.Year.ToString("0000");
                    }
                    else
                    {
                        ddlGun.Items.Insert(0, new ListItem("Gün", "00"));
                        ddlAy.Items.Insert(0, new ListItem("Ay", "00"));
                        ddlYil.Items.Insert(0, new ListItem("Yıl", "0000"));
                    }
                    break;
            }
        }
    }

    private void CurrentDate(Int16 year)
    {
        if (m_TarihSaat.Year > year)
        {
            if (ddlGun.Items.FindByValue(m_TarihSaat.Day.ToString("00")) == null)
                ddlGun.Items.Insert(0, new ListItem(m_TarihSaat.Day.ToString("00"), m_TarihSaat.Day.ToString("00")));
            else
                ddlGun.SelectedValue = m_TarihSaat.Day.ToString("00");

            if (ddlAy.Items.FindByValue(m_TarihSaat.Month.ToString("00")) == null)
                ddlAy.Items.Insert(0, new ListItem(m_TarihSaat.Month.ToString("00"), m_TarihSaat.Month.ToString("00")));
            else
                ddlAy.SelectedValue = m_TarihSaat.Month.ToString("00");

            if (ddlYil.Items.FindByValue(m_TarihSaat.Year.ToString("0000")) == null)
                ddlYil.Items.Insert(0, new ListItem(m_TarihSaat.Year.ToString("0000"), m_TarihSaat.Year.ToString("0000")));
            else
                ddlYil.SelectedValue = m_TarihSaat.Year.ToString("0000");

            ddlSaat.SelectedValue = m_TarihSaat.Hour.ToString("00");
            ddlDakika.SelectedValue = m_TarihSaat.Minute.ToString("00");
        }
    }
}