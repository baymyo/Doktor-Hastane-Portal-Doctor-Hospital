<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sorduklarim.ascx.cs" Inherits="modul_hesap_sorduklarim" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Sorduğum Sorular
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
                    <img title="<%# Eval("Konu") %>" class="toolTip images left" src="<%# Settings.ImagesPath+"soru.png" %>"
                        alt="<%# Eval("Konu") %>" />
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("mesaj",Eval("ID"),Eval("Konu")) %>">
                                <%# Eval("Konu")%></a><div class="job right">
                                    <%# Settings.YoneticiDurum(Eval("YoneticiOnay"), 16) + ((BAYMYO.UI.Converts.NullToByte(Eval("Durum")) <= 1) ? "<img class=\"toolTip\" title='Bu soruya henüz cevap verilmemiş!' src=\"" + Settings.IconsPath + "16/answer-lock.png\"/>" : "<img class=\"toolTip\" title='Sorunuza cevap verildi!' src=\"" + Settings.IconsPath + "16/answer.png\"/>")%><%# ((BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) ? "<img class=\"toolTip\" title='Herkese Açık!' src=\"" + Settings.IconsPath + "16/global.png\"/>" : "<img class=\"toolTip\" title='Sadece ben ve doktor!' src=\"" + Settings.IconsPath + "16/users.png\"/>")%>
                                </div>
                        </h1>
                        <span class="description">
                            <%# BAYMYO.UI.Commons.SubStringText(Eval("Icerik").ToString(),87) %>.. .</span>
                        <span class="info">
                            <%# "Bu soruyu <b>" + Settings.TarihFormat(Eval("KayitTarihi")) + "</b>&nbsp;" + Eval("Uzman") + " isimli uzmana sordunuz."%></span>
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
