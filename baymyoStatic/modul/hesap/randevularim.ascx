<%@ Control Language="C#" AutoEventWireup="true" CodeFile="randevularim.ascx.cs"
    Inherits="modul_hesap_randevularim" %>
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
        <asp:Repeater ID="rptListe" runat="server" OnItemCommand="rptListe_ItemCommand">
            <ItemTemplate>
                <li>
                    <%# Settings.RandevuKabul(Eval("Durum")) %>
                    <div class="text left">
                        <h1 class="title">
                            <asp:LinkButton ID="detailButton" runat="server" CssClass="toolTip" ToolTip='<%# Eval("Icerik") %>'
                                CommandName="details" Text='<%# "Sayın, <b>" + Eval("Adi") + "</b> " + Eval("TarihSaat") + "</b> için randevu talep etti."%>'>
                            </asp:LinkButton>
                        </h1>
                        <span class="description">
                            <%# BAYMYO.UI.Commons.SubStringText(Eval("Icerik").ToString(),87) %>.. .</span>
                        <span class="info">
                            <%# "İletişim: " + Eval("Mail") + " - " + Eval("Telefon") + " - " + Eval("GSM") %></span>
                    </div>
                    <div class="clear">
                    </div>
                </li>
                <asp:Label ID="infoLabel" runat="server" ToolTip='<%# Eval("ModulID") %>' Text='<%# Eval("IcerikID") %>'
                    Visible="false" />
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
