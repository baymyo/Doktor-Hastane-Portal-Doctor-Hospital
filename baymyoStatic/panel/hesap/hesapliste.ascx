<%@ Control Language="C#" AutoEventWireup="true" CodeFile="hesapliste.ascx.cs" Inherits="panel_hesap_hesapliste" %>
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
            <asp:BoundField DataField="Adi" HeaderText="Adı" ReadOnly="True">
                <HeaderStyle Width="15%" />
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="Soyadi" HeaderText="Soyadı" ReadOnly="True">
                <HeaderStyle Width="14%" />
                <ItemStyle Width="14%" />
            </asp:BoundField>
            <asp:BoundField DataField="Mail" HeaderText="Mail">
                <HeaderStyle Width="30%" />
                <ItemStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="Roller" HeaderText="Roller" ReadOnly="True">
                <HeaderStyle Width="21%" />
                <ItemStyle Width="21%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Türü">
                <HeaderStyle Width="8%" HorizontalAlign="Center" />
                <ItemStyle Width="8%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# Settings.HesapTipi(BAYMYO.MultiSQLClient.MConvert.NullToByte(Eval("Tipi")))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="Yorum yazma durumu">Y</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.YorumDurum(Eval("Yorum")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="e-Mail abonelik durumu">E</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.AbonelikDurum(Eval("Abonelik")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="Mail aktivasyon durumu">M</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.AktivasyonDurum(Eval("Aktivasyon")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <HeaderTemplate>
                    <span class="toolTip" title="Hesap durumu">D</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Settings.IcerikDurum(Eval("Aktif")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# Settings.PanelPath +"?go=hesap&uid="+ Eval("ID")  %>'>
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "Adi") + " isimli üyenin bilgilerini düzeltmek için tıkla!' src=\"" + Settings.IconsPath + "admin/edit.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
