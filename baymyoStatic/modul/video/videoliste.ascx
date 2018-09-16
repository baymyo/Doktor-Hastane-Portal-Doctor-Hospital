﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="videoliste.ascx.cs" Inherits="modul_video_videoliste" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Doktorların Videoları
    </h1>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <div class="left">
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></div>
    </asp:Panel>
    <div class="clear">
    </div>
    <ul class="album-list">
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li><a href="<%# Settings.CreateLink("video",Eval("ID"),Eval("Baslik")) %>">
                    <img title="<%# Eval("Baslik") %>" class="toolTip left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "video/" + Eval("ResimUrl") : "yok.png") %>" />
                    <div class="clear">
                    </div>
                    <h1>
                        <%# Eval("Baslik") %></h1>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>