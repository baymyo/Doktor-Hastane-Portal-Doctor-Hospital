<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvertisingControl.ascx.cs" Inherits="AdvertisingControl" %>
<div class="default-block">
    <center>
        <div class="clear"></div>
        <asp:AdRotator ID="adv660" runat="server" DataSourceID="ObjectDataSource2" />
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetSelect"
            TypeName="Lib.ReklamMethods">
            <SelectParameters>
                <asp:Parameter DefaultValue="650" Name="width" Type="Int32" />
                <asp:Parameter DefaultValue="80" Name="height" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div class="clear"></div>
    </center>
</div>
<div class="clear">
</div>