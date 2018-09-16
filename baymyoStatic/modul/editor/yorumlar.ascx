<%@ Control Language="C#" AutoEventWireup="true" CodeFile="yorumlar.ascx.cs" Inherits="modul_editor_yorumlar" %>
<div class="modul-main-box">
    <asp:Literal ID="ltrOpenComment" runat="server" />
    <div class="clear">
        &nbsp;</div>
    <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand">
        <ItemTemplate>
            <div class="comments">
                <div class="<%# Eval("Tipi") %>">
                    <span class="name">
                        <%# Eval("Adi") %>
                    </span><span class="job"><a href='<%# Settings.CreateLink(Eval("ModulID").ToString(),Eval("IcerikID"),Eval("Adi")) %>'
                        target="_blank">
                        <%# "<img class=\"toolTip\" title='Yorum yapılan içeriğinizi görüntülemek için tıkla!' src=\"" + Settings.IconsPath + "12/link.png\"/>"%></a><asp:ImageButton CssClass="toolTip" ToolTip="Yorumu pasif etmek için tıkla" ImageUrl="~/images/icons/12/content-1.png"
                                runat="server" ID="imgActive" CommandName="pasif" Visible='<%#  BAYMYO.UI.Converts.NullToBool(Eval("Aktif")) %>' /><asp:ImageButton
                                    CssClass="toolTip" ToolTip="Yorumu aktif etmek için tıkla" ImageUrl="~/images/icons/12/content-0.png"
                                    runat="server" ID="imgPassive" CommandName="aktif" Visible='<%#  !BAYMYO.UI.Converts.NullToBool(Eval("Aktif")) %>' /><asp:ImageButton
                            ID="imgRemove" CssClass="toolTip" ToolTip="Yorumu kaldırmak için tıkla!" ImageUrl="~/images/icons/12/remove.png"
                            runat="server" CommandName="remove" />
                    </span><span class="toolTip date" title="<%# Eval("KayitTarihi") %>">
                        <%# Settings.TarihFormat(Eval("KayitTarihi"))%>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="content">
                    <%# (!string.IsNullOrEmpty(Eval("ResimUrl").ToString()) ? "<a href=\"" + Settings.VirtualPath + Eval("Url") + "\" class=\"toolTip\" alt=\"" + Eval("Adi") + "\" title=\"Sayfasını görüntülemek için tıkla.\"><img src=\"" + Settings.ImagesPath + "profil/" + Eval("ResimUrl") + "\"/></a>" : "") + Eval("Icerik")%>
                </div>
            </div>
            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
        </ItemTemplate>
    </asp:Repeater>
    <div class="clear">
        &nbsp;</div>
    <div>
        <asp:Literal ID="pageNumberLiteral" runat="server" Text="" />
    </div>
</div>