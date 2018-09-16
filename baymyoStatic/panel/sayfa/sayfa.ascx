<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sayfa.ascx.cs" Inherits="panel_sayfa_sayfa" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl" TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true"
        runat="server" />
</div>