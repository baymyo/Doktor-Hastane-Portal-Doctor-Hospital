<%@ Control Language="C#" AutoEventWireup="true" CodeFile="makaleler.ascx.cs" Inherits="modul_editor_makaleler" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Yazdığım Makaleler
    </h1>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <div class="left">
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></div>
    </asp:Panel>
    <div class="clear">
    </div>
    <ul class="double-list">
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li class="left"><a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                    <img title="<%# Eval("Baslik") %>" class="toolTip images left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "makale/"+Eval("ResimUrl") : "yok.png") %>" /></a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                                <%# Eval("Baslik") %></a></h1>
                        <span class="description">
                            <%# BAYMYO.UI.Commons.SubStringText(Eval("Ozet").ToString(), 135)%></span>
                        <div class="info">
                            <span class="name left"><%# "<a class=\"right\" href=\""+ Settings.CreateLink("makaleguncelle",Eval("ID"),Eval("Baslik")) +"\">[Düzelt]</a>"%><%# (BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) ? "<span class=\"active-t right\">[Herkese Açık]</span>" : "<span class=\"passive-t right\">[Sadece Ben]</span>"%></span><span class="toolTip date right" title="<%# Eval("KayitTarihi") %>"><%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <li class="right"><a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                    <img title="<%# Eval("Baslik") %>" class="toolTip images left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "makale/"+Eval("ResimUrl") : "yok.png") %>" /></a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                                <%# Eval("Baslik") %></a></h1>
                        <span class="description">
                            <%# BAYMYO.UI.Commons.SubStringText(Eval("Ozet").ToString(),135) %></span>
                        <div class="info">
                            <span class="name left"><%# "<a class=\"right\" href=\""+ Settings.CreateLink("makaleguncelle",Eval("ID"),Eval("Baslik")) +"\">[Düzelt]</a>"%><%# (BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) ? "<span class=\"active-t right\">[Herkese Açık]</span>" : "<span class=\"passive-t right\">[Sadece Ben]</span>"%></span><span class="toolTip date right" title="<%# Eval("KayitTarihi") %>"><%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ul>
</div>
