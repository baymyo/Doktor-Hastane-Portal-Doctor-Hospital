<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mesajlar.ascx.cs" Inherits="modul_editor_mesajlar" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Gelen Sorular
    </h1>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <div class="left">
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></div>
    </asp:Panel>
    <div class="clear">
    </div>
    <ul class="single-list">
        <li class="first"></li>
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li>
                    <img title="<%# Eval("Konu") %>" class="toolTip left" src="<%# Settings.ImagesPath+"soru.png" %>"
                        alt="<%# Eval("Konu") %>" />
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("mesaj",Eval("ID"),Eval("Konu")) %>">
                                <%# Eval("Konu")%></a>
                            <div class="job right">
                                <%# ((BAYMYO.UI.Converts.NullToByte(Eval("Durum")) <= 1) ? "<a class=\"right\" href=\"" + Settings.CreateLink("mesajyanitla", Eval("ID"), Eval("Konu")) + "\"><img class=\"toolTip\" title='Bu soruya yanıt vermek için tıklayın!' src=\"" + Settings.IconsPath + "16/editor-lock.png\"/></a>" : "<a class=\"right\" href=\"" + Settings.CreateLink("mesajyanitla", Eval("ID"), Eval("Konu")) + "\"><img class=\"toolTip\" title='Verdiğiniz yanıtı düzeltmek için tıklayınız!' src=\"" + Settings.IconsPath + "16/editor.png\"/></a>")%><%# (BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) ? "<img class=\"toolTip\" title='Herkese Açık!' src=\"" + Settings.IconsPath + "16/global.png\"/>" : "<img class=\"toolTip\" title='Sadece ben ve hasta!' src=\"" + Settings.IconsPath + "16/users.png\"/>"%>
                            </div>
                        </h1>
                        <span class="description">
                            <%# BAYMYO.UI.Commons.SubStringText(Eval("Icerik").ToString(),87) %>.. .</span>
                        <span class="info">
                            <%# "Sayın, <b>" + Eval("Adi") +"</b> "+ Settings.TarihFormat(Eval("KayitTarihi")) + " sordu."%></span>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
