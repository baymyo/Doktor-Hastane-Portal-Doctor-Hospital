﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mesajliste.ascx.cs" Inherits="modul_mesaj_mesajliste" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Soru Bankası
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
                                <%# Eval("Konu")%></a></h1>
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
