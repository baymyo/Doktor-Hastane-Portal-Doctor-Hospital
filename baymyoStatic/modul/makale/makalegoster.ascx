<%@ Control Language="C#" AutoEventWireup="true" CodeFile="makalegoster.ascx.cs" Inherits="modul_makale_makalegoster" %>
<%@ Register src="../../common/control/CommentControl.ascx" tagname="CommentControl" tagprefix="baymyoCnt" %>
<%@ Register src="../../common/control/AdvertisingControl.ascx" tagname="AdvertisingControl" tagprefix="baymyoCnt" %>
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<div class="clear">
</div>
<baymyoCnt:AdvertisingControl ID="AdvertisingControl1" runat="server" />
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Yorum Yaz" />


