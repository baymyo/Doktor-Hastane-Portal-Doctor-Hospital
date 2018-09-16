<%@ Control Language="C#" AutoEventWireup="true" CodeFile="blokliste.ascx.cs" Inherits="panel_blok_blokliste" %>
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
            <asp:Literal ID="infoLiteral" runat="server" Text="..." /></div>
        <div class="right">
            <asp:Button ID="btnSaveChanges" runat="server" Text="Değişiklikleri Uygula" CssClass="right"
                OnClick="btnSaveChanges_Click" />
        </div>
    </div>
    <div class="clear">
    </div>
    <asp:GridView ID="dataGrid1" runat="server" GridLines="None" CssClass="dataGridView"
        AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="true" OnPageIndexChanging="dataGrid1_PageIndexChanging">
        <RowStyle CssClass="rowStyle"></RowStyle>
        <AlternatingRowStyle CssClass="alternatingRowStyle"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderTemplate>
                    <asp:CheckBox runat="server" ID="chkAll" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkSelected" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Başlık">
                <HeaderStyle Width="28%" HorizontalAlign="Left" />
                <ItemStyle Width="28%" HorizontalAlign="Left" />
                <ItemTemplate>
                    <%# "<a href=\"" + Settings.PanelPath +"?go=blok&bid="+ Eval("ID") + "&type="+ Eval("Tipi") + "\">" + Eval("Baslik") + "</a>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipi">
                <HeaderStyle Width="6%" HorizontalAlign="Center" />
                <ItemStyle Width="6%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <b>
                        <%# Eval("Tipi") %></b>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Yer">
                <HeaderStyle Width="18%" HorizontalAlign="Center" />
                <ItemStyle Width="18%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:DropDownList ID="ddlYer" runat="server" DataSource='<%# Lib.Blok.GetYerlesimler() %>'
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Eval("Yer") %>'
                        Width="200px">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sıra">
                <HeaderStyle Width="9%" HorizontalAlign="Center" />
                <ItemStyle Width="9%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:DropDownList ID="ddlSira" runat="server" DataSource='<%# Settings.SiraNumaralari() %>'
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Eval("Sira") %>'
                        Width="100px">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dil">
                <HeaderStyle Width="9%" HorizontalAlign="Center" />
                <ItemStyle Width="9%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:DropDownList ID="ddlDil" runat="server" DataSource='<%# Settings.DilSecenekleri() %>'
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Eval("Dil") %>'
                        Width="100px">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="2%" />
                <HeaderTemplate>
                    <span class="toolTip" title="Tüm diller de gösterim">D</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkTumDil" Checked='<%# BAYMYO.UI.Converts.NullToBool(Eval("TumDil")) %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="2%" />
                <HeaderTemplate>
                    <span class="toolTip" title="Tüm sayfalar da gösterim">S</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkTumSayfa" Checked='<%# BAYMYO.UI.Converts.NullToBool(Eval("TumSayfa")) %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="2%" />
                <HeaderTemplate>
                    <span class="toolTip" title="Blok gösterim durumu">A</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkAktif" Checked='<%# BAYMYO.UI.Converts.NullToBool(Eval("Aktif")) %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Width="2%" />
                <ItemTemplate>
                    <a href='<%# Settings.PanelPath +"?go=blok&bid="+ Eval("ID") + "&type="+ Eval("Tipi")  %>'>
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "Baslik") + " isimli bloğu düzeltmek için tıkla!' src=\"" + Settings.IconsPath + "admin/edit.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
