<%@ Control Language="C#" AutoEventWireup="true" CodeFile="randevutalebi.ascx.cs"
    Inherits="modul_editor_randevutalebi" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<%@ Register Src="~/common/control/DateTimeControl.ascx" TagName="DateTimeControl"
    TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Randevu Talebi Onayla
    </h1>
    <div class="clear"></div>
    <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
    <div class="clear">
    </div>
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" runat="server" />
</div>
