<%@ Control Language="C#" AutoEventWireup="true" CodeFile="hesap.ascx.cs" Inherits="modul_hesap_hesap" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl" TagPrefix="baymyoCnt" %>
<%@ Register Src="~/common/control/DateTimeControl.ascx" TagName="DateTimeControl" TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="false"
        runat="server" />
</div>