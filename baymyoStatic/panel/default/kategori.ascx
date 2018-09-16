<%@ Control Language="C#" AutoEventWireup="true" CodeFile="kategori.ascx.cs" Inherits="panel_kategori_kategori" %>
<%@ Register Assembly="BAYMYO.UI" Namespace="BAYMYO.UI.Data" TagPrefix="cc1" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true"
        runat="server" />
</div>
<cc1:HierarchicalObjectDataSource ID="hierarDataSource" DataParentField="ParentID"
    DataValueField="Value" DataTextField="Text" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetHierarchical" TypeName="Lib.KategoriMethods">
    <SelectParameters>
        <asp:QueryStringParameter Name="modulID" QueryStringField="mdl"
            Type="String" />
        <asp:Parameter DefaultValue="true" Name="rootNode" Type="Boolean" />
    </SelectParameters>
</cc1:HierarchicalObjectDataSource>
