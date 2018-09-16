<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateTimeControl.ascx.cs"
    Inherits="DateTimeControl" %>
<div style="display: block">
    <asp:Panel ID="pnlDate" runat="server" CssClass="left">
        <asp:DropDownList ID="ddlGun" runat="server" CssClass="left" Width="60">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlAy" runat="server" CssClass="left" Width="60">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlYil" runat="server" CssClass="left" Width="75">
        </asp:DropDownList>
        <div class="clear">
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTime" runat="server" CssClass="left">
        <span class="left" style="margin-left: 10px; margin-right: 5px;">
            <img src="images/icons/32/time.png" alt="Saati" /></span>
        <asp:DropDownList ID="ddlSaat" runat="server" CssClass="left" Width="60">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlDakika" runat="server" CssClass="left" Width="60">
        </asp:DropDownList>
        <div class="clear">
        </div>
    </asp:Panel>
    <div class="clear">
    </div>
</div>
