<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reklam-250x250.ascx.cs"
    Inherits="common_ascx_Reklam_250x250" %>
<div class="default-block">
    <center>
        <span class="sponsor">--- reklam ---</span>
        <div class="clear"></div>
        <asp:AdRotator ID="adv250" runat="server" DataSourceID="ObjectDataSource2" />
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetSelect"
            TypeName="Lib.ReklamMethods">
            <SelectParameters>
                <asp:Parameter DefaultValue="250" Name="width" Type="Int32" />
                <asp:Parameter DefaultValue="250" Name="height" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div class="clear"></div>
        <span class="sponsor">--- reklam ---</span>
    </center>
</div>
<div class="clear">
</div>
