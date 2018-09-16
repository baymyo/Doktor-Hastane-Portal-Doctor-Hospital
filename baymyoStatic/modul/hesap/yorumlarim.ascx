<%@ Control Language="C#" AutoEventWireup="true" CodeFile="yorumlarim.ascx.cs" Inherits="modul_hesap_yorumlarim" %>
<div class="modul-main-box">
    <asp:Literal ID="ltrOpenComment" runat="server" />
    <div class="clear">
        &nbsp;</div>
    <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand">
        <ItemTemplate>
            <div class="comments">
                <div class="<%# tipi %>">
                    <span class="name">
                        <%# Eval("Adi") %>
                    </span><span class="job"><a href='<%# Settings.CreateLink(Eval("ModulID").ToString(),Eval("IcerikID"),Eval("Adi")) %>'
                        target="_blank">
                        <%# "<img class=\"toolTip\" title='Yorum yaptığınız içeriği görüntülemek için tıkla!' src=\"" + Settings.IconsPath + "12/link.png\"/>&nbsp;&nbsp;" + Settings.YorumGosterim(Eval("Aktif")) + "&nbsp;&nbsp;" + Settings.YoneticiDurum(Eval("YoneticiOnay"),12)%></a><asp:ImageButton
                            ID="imgRemove" CssClass="toolTip" ToolTip="Yorumu kaldırmak için tıkla!" ImageUrl="~/images/icons/12/remove.png"
                            runat="server" CommandName="remove" />
                    </span><span class="toolTip date" title="<%# Eval("KayitTarihi") %>">
                        <%# Settings.TarihFormat(Eval("KayitTarihi"))%>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="content">
                    <%# (!string.IsNullOrEmpty(resimUrl) ? "<a href=\"" + Settings.VirtualPath + url + "\" class=\"toolTip\" alt=\"" + Eval("Adi") + "\" title=\"Sayfasını görüntülemek için tıkla.\"><img src=\"" + Settings.ImagesPath + "profil/" + resimUrl + "\"/></a>" : "") + Eval("Icerik")%>
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
