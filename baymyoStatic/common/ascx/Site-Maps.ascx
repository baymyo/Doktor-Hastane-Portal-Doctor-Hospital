<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Site-Maps.ascx.cs" Inherits="common_ascx_Site_Maps" %>
<div class="site-map">
    <asp:SiteMapPath ID="categorySiteMaps" runat="server">
        <RootNodeStyle CssClass="toolTip root left" />
        <NodeStyle CssClass="toolTip node left" />
        <CurrentNodeStyle CssClass="toolTip current left" />
        <PathSeparatorStyle CssClass="double-arrow left" />
        <PathSeparatorTemplate>
            &nbsp;
        </PathSeparatorTemplate>
    </asp:SiteMapPath>
</div>
