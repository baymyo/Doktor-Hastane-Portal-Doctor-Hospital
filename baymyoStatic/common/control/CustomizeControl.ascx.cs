using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomizeControl : System.Web.UI.UserControl
{
    #region --- Properties ---
    public delegate void ButtonEvent(SortedDictionary<string, Control> controls);
    private const string FormControlPath = "\\common\\control\\";

    public string FormTitle { get; set; }
    public bool FormTitleVisible { get; set; }
    private bool formScriptEnable = true;
    public bool FormScriptEnable
    {
        get { return formScriptEnable; }
        set { formScriptEnable = value; }
    } 
    #endregion

    #region --- Panel Set ---
    public bool PanelVisible
    {
        get { return CustomControls.Visible; }
        set { CustomControls.Visible = value; }
    }
    public bool PanelEnabled
    {
        get { return CustomControls.Enabled; }
        set { CustomControls.Enabled = value; }
    }
    #endregion

    #region --- Message Button Set ---
    public string MessageText
    {
        get { return CustomMessage.Text; }
        set { CustomMessage.Text = value; }
    }
    public bool MessageVisible
    {
        get { return CustomMessage.Visible; }
        set { CustomMessage.Visible = value; }
    }
    #endregion

    #region --- Status Button Set ---
    public string StatusText
    {
        get { return CustomStatus.Text; }
        set { CustomStatus.Text = value; }
    }
    public string StatusToolTip
    {
        get { return CustomStatus.ToolTip; }
        set { CustomStatus.ToolTip = value; }
    }
    public bool StatusEnabled
    {
        get { return CustomStatus.Enabled; }
        set { CustomStatus.Enabled = value; }
    }
    public bool StatusVisible
    {
        get { return CustomStatus.Visible; }
        set { CustomStatus.Visible = value; }
    }
    #endregion

    #region --- Submit Button Set ---
    public string SubmitToolTip
    {
        get { return CustomSubmit.ToolTip; }
        set { CustomSubmit.ToolTip = value; }
    }
    public string SubmitOnClientClick
    {
        get { return CustomSubmit.OnClientClick; }
        set { CustomSubmit.OnClientClick = value; }
    }
    public string SubmitCssClass
    {
        get { return CustomSubmit.CssClass; }
        set { CustomSubmit.CssClass = value; }
    }
    public string SubmitText
    {
        get { return CustomSubmit.Text; }
        set { CustomSubmit.Text = value; }
    }
    public bool SubmitEnabled
    {
        get { return CustomSubmit.Enabled; }
        set { CustomSubmit.Enabled = value; }
    }
    public bool SubmitVisible
    {
        get { return CustomSubmit.Visible; }
        set { CustomSubmit.Visible = value; }
    }
    public Unit SubmitWidth
    {
        get { return CustomSubmit.Width; }
        set { CustomSubmit.Width = value; }
    }
    public Unit SubmitHeight
    {
        get { return CustomSubmit.Height; }
        set { CustomSubmit.Height = value; }
    }

    public event ButtonEvent SubmitClick;
    #endregion

    #region --- Update Button Set ---
    public string UpdateToolTip
    {
        get { return CustomUpdate.ToolTip; }
        set { CustomUpdate.ToolTip = value; }
    }
    public string UpdateOnClientClick
    {
        get { return CustomUpdate.OnClientClick; }
        set { CustomUpdate.OnClientClick = value; }
    }
    public string UpdateCssClass
    {
        get { return CustomUpdate.CssClass; }
        set { CustomUpdate.CssClass = value; }
    }
    public string UpdateText
    {
        get { return CustomUpdate.Text; }
        set { CustomUpdate.Text = value; }
    }
    public bool UpdateEnabled
    {
        get { return CustomUpdate.Enabled; }
        set { CustomUpdate.Enabled = value; }
    }
    public bool UpdateVisible
    {
        get { return CustomUpdate.Visible; }
        set { CustomUpdate.Visible = value; }
    }
    public Unit UpdateWidth
    {
        get { return CustomUpdate.Width; }
        set { CustomUpdate.Width = value; }
    }
    public Unit UpdateHeight
    {
        get { return CustomUpdate.Height; }
        set { CustomUpdate.Height = value; }
    }

    public event ButtonEvent UpdateClick;
    #endregion

    #region --- Remove Button Set ---
    public string RemoveToolTip
    {
        get { return CustomRemove.ToolTip; }
        set { CustomRemove.ToolTip = value; }
    }
    public string RemoveOnClientClick
    {
        get { return CustomRemove.OnClientClick; }
        set { CustomRemove.OnClientClick = value; }
    }
    public string RemoveCssClass
    {
        get { return CustomRemove.CssClass; }
        set { CustomRemove.CssClass = value; }
    }
    public string RemoveText
    {
        get { return CustomRemove.Text; }
        set { CustomRemove.Text = value; }
    }
    public bool RemoveEnabled
    {
        get { return CustomRemove.Enabled; }
        set { CustomRemove.Enabled = value; }
    }
    public bool RemoveVisible
    {
        get { return CustomRemove.Visible; }
        set { CustomRemove.Visible = value; }
    }
    public Unit RemoveWidth
    {
        get { return CustomRemove.Width; }
        set { CustomRemove.Width = value; }
    }
    public Unit RemoveHeight
    {
        get { return CustomRemove.Height; }
        set { CustomRemove.Height = value; }
    }

    public event ButtonEvent RemoveClick;
    #endregion

    #region --- Custom Control Add ---
    SortedDictionary<string, Control> m_CustomControls = new SortedDictionary<string, Control>();
    public SortedDictionary<string, Control> ControlList
    {
        get { return m_CustomControls; }
    }
    public void AddTitle(string title)
    {
        Literal ltr = new Literal();
        ltr.Text = "<div class=\"clear\"></div><h1 class=\"mmb-Title\">" + title + "</h1><div class=\"clear\"></div>";
        this.CustomPanel.Controls.Add(ltr);
    }
    public void AddTitle(Literal control)
    {
        this.CustomPanel.Controls.Add(control);
    }
    public void AddControl(string title, Control control)
    {
        m_CustomControls.Add(control.ID, control);
        switch (control.Visible)
        {
            case true:
                UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                ((Panel)pnlControl.FindControl("pnlControl")).Controls.Add(control);
                this.CustomPanel.Controls.Add(pnlControl);
                break;
        }
    }
    public void AddControl(string title, Control control, Control extender)
    {
        m_CustomControls.Add(control.ID, control);
        switch (control.Visible)
        {
            case true:
                UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                pnl.Controls.Add(control);
                pnl.Controls.Add(extender);
                this.CustomPanel.Controls.Add(pnlControl);
                break;
        }
    }
    public void AddControl(string title, Control control, Control[] extenders)
    {
        m_CustomControls.Add(control.ID, control);
        switch (control.Visible)
        {
            case true:
                UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                pnl.Controls.Add(control);
                foreach (Control item in extenders)
                    pnl.Controls.Add(item);
                this.CustomPanel.Controls.Add(pnlControl);
                break;
        }
    }
    public void AddControl(string title, Control control, string description)
    {
        m_CustomControls.Add(control.ID, control);
        switch (control.Visible)
        {
            case true:
                UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                ((Panel)pnlControl.FindControl("pnlControl")).Controls.Add(control);
                if (!string.IsNullOrEmpty(description))
                    ((Literal)pnlControl.FindControl("ltrExample")).Text = "<div class=\"description\">" + description + "</div>";
                this.CustomPanel.Controls.Add(pnlControl);
                break;
        }
    }
    public void AddControl(string title, Control control, Control extender, string description)
    {
        m_CustomControls.Add(control.ID, control);
        switch (control.Visible)
        {
            case true:
                UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                pnl.Controls.Add(control);
                pnl.Controls.Add(extender);
                if (!string.IsNullOrEmpty(description))
                    ((Literal)pnlControl.FindControl("ltrExample")).Text = "<div class=\"description\">" + description + "</div>";
                this.CustomPanel.Controls.Add(pnlControl);
                break;
        }
    }
    public void AddControl(string title, Control control, Control[] extenders, string description)
    {
        m_CustomControls.Add(control.ID, control);
        switch (control.Visible)
        {
            case true:
                UserControl pnlControl = (UserControl)this.Page.LoadControl(FormControlPath + "CustomizeControlShema.ascx");
                ((Literal)pnlControl.FindControl("ltrTitle")).Text = title;
                Panel pnl = ((Panel)pnlControl.FindControl("pnlControl"));
                pnl.Controls.Add(control);
                foreach (Control item in extenders)
                    pnl.Controls.Add(item);
                if (!string.IsNullOrEmpty(description))
                    ((Literal)pnlControl.FindControl("ltrExample")).Text = "<div class=\"description\">" + description + "</div>";
                this.CustomPanel.Controls.Add(pnlControl);
                break;
        }
    } 
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (FormScriptEnable)
        {
            CustomSubmit.CssClass = "submitButton right";
            CustomUpdate.CssClass = "submitButton right";
        }
    }

    protected void CustomSubmit_Click(object sender, EventArgs e)
    {
        if (SubmitClick != null)
            SubmitClick(m_CustomControls);
    }
    protected void CustomUpdate_Click(object sender, EventArgs e)
    {
        if (UpdateClick != null)
            UpdateClick(m_CustomControls);
    }
    protected void CustomRemove_Click(object sender, EventArgs e)
    {
        if (RemoveClick != null)
            RemoveClick(m_CustomControls);
    }
}