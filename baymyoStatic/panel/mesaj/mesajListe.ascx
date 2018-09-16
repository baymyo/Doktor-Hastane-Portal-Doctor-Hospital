<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mesajListe.ascx.cs" Inherits="panel_mesaj_mesajListe" %>
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
            <asp:BoundField DataField="Adi" HeaderText="Adı Soyadı" ReadOnly="True">
                <HeaderStyle Width="16%" />
                <ItemStyle Width="16%" />
            </asp:BoundField>
            <asp:BoundField DataField="Mail" HeaderText="Mail" ReadOnly="True">
                <HeaderStyle Width="15%" />
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="Telefon" HeaderText="Telefon" ReadOnly="True">
                <HeaderStyle Width="10%" />
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Mesaj">
                <HeaderStyle Width="49%" />
                <ItemStyle Width="49%" />
                <ItemTemplate>
                    <h1 class="titleFormat1">
                        Konu:&nbsp;<%# Eval("Konu")%></h1>
                    <var class="textFormat1">
                        <%# DataBinder.Eval(Container, "DataItem.Icerik") %></var>
                    <div class="clear">
                    </div>
                    <h1 class="titleFormat2">
                        --- Yanıt ---</h1>
                    <div class="textFormat2">
                        <%# DataBinder.Eval(Container, "DataItem.Yanit") %>.. .
                    </div>
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
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# Settings.CreateLink("mesaj",Eval("ID"),Eval("konu")) %>' target="_blank">
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "Adi") + " isimli kullanıcının sorusunu görüntülemek için tıkla!' src=\"" + Settings.IconsPath + "admin/link.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# Settings.PanelPath +"?go=mesaj&mid="+ Eval("ID")  %>'>
                        <%# "<img class=\"toolTip\" title='" + DataBinder.Eval(Container.DataItem, "Adi") + " isimli kullanıcının sorusunu yanıtlamak için tıkla!' src=\"" + Settings.IconsPath + "admin/edit.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
