<%@ Control Language="C#" AutoEventWireup="true" CodeFile="reklamliste.ascx.cs" Inherits="panel_reklam_reklamliste" %>
<div class="modul-main-box">
    <script type="text/javascript">
        var allCheckBoxSelector = '#<%=dataGrid1.ClientID%> input[id*="chkAll"]:checkbox';
        var checkBoxSelector = '#<%=dataGrid1.ClientID%> input[id*="chkSelected"]:checkbox';

        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
            checkedCheckboxes = totalCheckboxes.filter(":checked"),
            noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
            allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        }

        $(document).ready(function () {
            $(allCheckBoxSelector).live('click', function () {
                $(checkBoxSelector).attr('checked', $(this).is(':checked'));
                ToggleCheckUncheckAllOptionAsNeeded();
            });
            $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);
            ToggleCheckUncheckAllOptionAsNeeded();
        });
    </script>
    <div class="job-set">
        <div class="left">
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></div>
        <div class="right">
            <asp:Button ID="btnSaveChanges" runat="server" Text="Seçilenlere Uygula" CssClass="right"
                OnClick="btnSaveChanges_Click" />
            <asp:DropDownList ID="ddlIslemler" runat="server" CssClass="right" Width="200px">
            </asp:DropDownList>
        </div>
    </div>
    <div class="clear">
    </div>
    <asp:GridView ID="dataGrid1" runat="server" GridLines="None" CssClass="dataGridView"
        AutoGenerateColumns="False" DataKeyNames="ID">
        <RowStyle CssClass="rowStyle" />
        <AlternatingRowStyle CssClass="alternatingRowStyle" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox runat="server" ID="chkAll" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkSelected" />
                </ItemTemplate>
                <HeaderStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField DataField="BannerName" HeaderText="Başlık" ReadOnly="True">
                <HeaderStyle Width="28%" />
                <ItemStyle Width="28%" />
            </asp:BoundField>
            <asp:BoundField DataField="AlternateText" HeaderText="Alternatif Yazı" ReadOnly="True">
                <HeaderStyle Width="50%" />
                <ItemStyle Width="50%" />
            </asp:BoundField>
            <asp:BoundField DataField="Impressions" HeaderText="I" ReadOnly="True">
                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Width" HeaderText="W" ReadOnly="True">
                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Height" HeaderText="H" ReadOnly="True">
                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                <ItemStyle Width="5%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="Yayımlama Durumu">D</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.IcerikDurum(Eval("IsActive")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# Settings.PanelPath +"?go=reklam&rklid="+ Eval("ID")  %>'>
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "BannerName") + " reklamı düzeltmek için tıkla!' src=\"" + Settings.IconsPath + "admin/edit.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
