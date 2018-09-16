<%@ Control Language="C#" AutoEventWireup="true" CodeFile="yorumliste.ascx.cs" Inherits="panel_yorum_yorumliste" %>
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
        AutoGenerateColumns="False" DataKeyNames="ID,ModulID,IcerikID">
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
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP">
                <HeaderStyle Width="14%" />
                <ItemStyle Width="14%" />
            </asp:BoundField>
            <asp:BoundField DataField="Mail" HeaderText="Mail" SortExpression="Mail">
                <HeaderStyle Width="22%" />
                <ItemStyle Width="22%" />
            </asp:BoundField>
            <asp:BoundField DataField="Icerik" HeaderText="Yorum" SortExpression="Icerik">
                <HeaderStyle Width="56%" />
                <ItemStyle Width="56%" />
            </asp:BoundField>
            <asp:BoundField DataField="KayitTarihi" HeaderText="Tarih" SortExpression="KayitTarihi">
                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# Settings.CreateLink(Eval("ModulID").ToString(),Eval("IcerikID"),Eval("Adi")) %>'
                        target="_blank">
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "Adi") + " isimli kişinin yorum yaptığı içeriği görüntülemek için tıkla!' src=\"" + Settings.IconsPath + "admin/link.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="Yayımlama Durumu">D</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.IcerikDurum(Eval("Aktif")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="Yönetici Onayı">A</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.YoneticiDurum(Eval("YoneticiOnay"))%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
