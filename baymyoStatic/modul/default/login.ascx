<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login.ascx.cs" Inherits="modul_default_login" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" FormTitle="Kullanıcı Girişi"
        SubmitText="Giriş Yap" RemoveVisible="false" runat="server" />
</div>
