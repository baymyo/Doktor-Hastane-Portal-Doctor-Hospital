<%@ Control Language="C#" AutoEventWireup="true" CodeFile="haberliste.ascx.cs" Inherits="modul_haber_haberliste" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Haberler ve Duyurular<div class="right">
            <a href="<%= Settings.VirtualPath + "?go=haberliste&view=double&kid=" + Request.QueryString["kid"] +"&q=" + Request.QueryString["q"]  %>">
                <img src="/images/icons/16/double.png" /></a>&nbsp;&nbsp;<a href="<%= Settings.VirtualPath + "?go=haberliste&view=single&kid=" + Request.QueryString["kid"] +"&q=" + Request.QueryString["q"] %>"><img
                    src="/images/icons/16/single.png" /></a>
        </div>
    </h1>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <div class="left">
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></div>
    </asp:Panel>
    <div class="clear">
    </div>
    <ul class="<%= GetClassName() %>">
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li class="left"><a href="<%# Settings.CreateLink("haber",Eval("ID"),Eval("Baslik")) %>">
                    <img title="<%# Eval("Baslik") %>" class="toolTip left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "haber/"+Eval("ResimUrl") : "yok.png") %>" /></a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("haber",Eval("ID"),Eval("Baslik")) %>">
                                <%# Eval("Baslik") %></a></h1>
                        <span class="description">
                            <%# Eval("Ozet")%></span>
                        <div class="info">
                            <span class="name left"><%# Eval("KategoriAdi") %></span><span class="toolTip date right" title="<%# Eval("KayitTarihi") %>"><%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <li class="right"><a href="<%# Settings.CreateLink("haber",Eval("ID"),Eval("Baslik")) %>">
                    <img title="<%# Eval("Baslik") %>" class="toolTip left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "haber/"+Eval("ResimUrl") : "yok.png") %>" /></a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("haber",Eval("ID"),Eval("Baslik")) %>">
                                <%# Eval("Baslik") %></a></h1>
                        <span class="description">
                            <%# Eval("Ozet") %></span>
                        <div class="info">
                            <span class="name left"><%# Eval("KategoriAdi") %></span><span class="toolTip date right" title="<%# Eval("KayitTarihi") %>"><%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ul>
</div>
