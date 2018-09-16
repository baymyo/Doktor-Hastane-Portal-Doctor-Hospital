<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomizeControlShema.ascx.cs" Inherits="CustomizeControlShema" %>
<div class="mmb-Row">
    <div class="mmb-T">
        <asp:Literal ID="ltrTitle" runat="server" Text="Title"></asp:Literal>
    </div>
    <div class="mmb-S">
        :
    </div>
    <div class="mmb-C">
        <asp:Panel ID="pnlControl" runat="server">
        </asp:Panel>
        <asp:Literal ID="ltrExample" runat="server" Text=""></asp:Literal>
    </div>
</div>
