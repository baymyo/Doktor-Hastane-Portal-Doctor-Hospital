<%@ Control Language="C#" AutoEventWireup="true" CodeFile="randevutalebleri.ascx.cs"
    Inherits="modul_editor_randevutalebleri" %>
<div class="modul-main-box">
    <asp:Literal ID="ltrOpenComment" runat="server" />
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
                    <%# Settings.RandevuKabul(Eval("Durum")) %>
                    <div class="text left">
                        <h1 class="title">
                            <a class="toolTip" href="<%# Settings.CreateLink("randevutalebi",Eval("ID"),Eval("Adi")) %>" title="<%# Eval("Icerik") %>">
                                <%# "Sayın, <b>" + Eval("Adi") + "</b> " + Eval("TarihSaat") + "</b> için randevu talep etti."%>
                            </a>
                        </h1>
                        <span class="description">
                            <%# BAYMYO.UI.Commons.SubStringText(Eval("Icerik").ToString(),87) %>.. .</span>
                        <span class="info">
                            <%# "İletişim: " + Eval("Mail") + " - " + Eval("Telefon") + " - " + Eval("GSM") %></span>
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
