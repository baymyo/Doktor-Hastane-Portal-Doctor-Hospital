<%@ Control Language="C#" AutoEventWireup="true" CodeFile="register.ascx.cs" Inherits="modul_default_register" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<%@ Register Src="~/common/control/DateTimeControl.ascx" TagName="DateTimeControl"
    TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <asp:Panel ID="pnlUyelikTipi" runat="server" CssClass="user-type">
        <div class="left">
            <a href="<%= Settings.VirtualPath +"?l=2&type=standart" %>">Standart Üyelik Formu</a>
        </div>
        <div class="right">
            <a href="<%= Settings.VirtualPath +"?l=2&type=editor" %>">Editör Üyelik Formu</a>
        </div>
    </asp:Panel>
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" runat="server" />
</div>
