<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mesajgoster.ascx.cs" Inherits="modul_mesaj_mesajgoster" %>
<%@ Register Src="../../common/control/CommentControl.ascx" TagName="CommentControl" TagPrefix="baymyoCnt" %>
<%@ Register src="../../common/control/AdvertisingControl.ascx" tagname="AdvertisingControl" tagprefix="baymyoCnt" %>
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<div class="clear">
</div>
<baymyoCnt:AdvertisingControl ID="AdvertisingControl1" runat="server" />
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Hızlı Cevap" />
