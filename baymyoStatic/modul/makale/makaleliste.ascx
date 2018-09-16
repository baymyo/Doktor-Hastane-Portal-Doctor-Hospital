<%@ Control Language="C#" AutoEventWireup="true" CodeFile="makaleliste.ascx.cs" Inherits="modul_makale_makaleliste" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Makaleler<div class="right">
            <a href="<%= Settings.VirtualPath + "?go=makaleliste&view=double&kid=" + Request.QueryString["kid"] +"&hspid=" + Request.QueryString["hspid"] +"&q=" + Request.QueryString["q"]  %>">
                <img src="/images/icons/16/double.png" /></a>&nbsp;&nbsp;<a href="<%= Settings.VirtualPath + "?go=makaleliste&view=single&kid=" + Request.QueryString["kid"] +"&hspid=" + Request.QueryString["hspid"] +"&q=" + Request.QueryString["q"] %>"><img
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
                <li class="left"><a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                    <img title="<%# Eval("Baslik") %>" class="toolTip images left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "makale/"+Eval("ResimUrl") : "yok.png") %>" /></a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                                <%# Eval("Baslik") %></a></h1>
                        <span class="description">
                            <%# Eval("Ozet")%></span>
                        <div class="info">
                            <span class="name left"><a href="<%# Settings.CreateLink("makaledoktor",Eval("HesapID"),Eval("Adi")+ " " + Eval("Soyadi"))  %>"><%# Eval("Adi") + " " + Eval("Soyadi")%></a></span><span class="toolTip date right" title="<%# Eval("KayitTarihi") %>"><%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
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
                            <%# Eval("Ozet") %></span>
                        <div class="info">
                            <span class="name left"><a href="<%# Settings.CreateLink("makaledoktor",Eval("HesapID"),Eval("Adi")+ " " + Eval("Soyadi")) %>"><%# Eval("Adi") + " " + Eval("Soyadi")%></a></span><span class="toolTip date right"
                                title="<%# Eval("KayitTarihi") %>"><%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ul>
</div>
