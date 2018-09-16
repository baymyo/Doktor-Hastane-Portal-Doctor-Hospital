<%@ Control Language="C#" AutoEventWireup="true" CodeFile="profil.ascx.cs" Inherits="modul_default_profil" %>
<%@ Register Src="../../common/control/CommentControl.ascx" TagName="CommentControl" TagPrefix="baymyoCnt" %>
<%@ Register Src="contact.ascx" TagName="contact" TagPrefix="baymyoCnt" %>
<%@ Register Src="randevu.ascx" TagName="randevu" TagPrefix="baymyoCnt" %>
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<div class="clear">
</div>
<asp:Panel ID="workArea" runat="server" CssClass="details-block" Visible="false">
    <div class="cbHeader">
        <div class="tL left">
            <span class="title"><b>Muayene</b>hanelerim</span>
        </div>
        <div class="tR right">
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="cbBody">
        <div class="cbContainer">
            <div id="card-view">
                <ul>
                    <asp:Repeater ID="rptMuayene" runat="server">
                        <ItemTemplate>
                            <li class="title">
                                <img class="icons" src="/images/icons/16/home.png" /><%# Eval("Kurum") + Settings.RandevuAl(Eval("Kurum"), Settings.CreateLink("randevu", Request.QueryString["url"] + ";" + Eval("ID"), "go"), Eval("Randevu"))%>
                            </li>
                            <li>
                                <h1>
                                    <img src="/images/icons/10/phone.png" />Telefon
                                </h1>
                                <span>:</span>
                                <div>
                                    <%# Eval("Telefon")%>
                                </div>
                                <div class="clear">
                                </div>
                            </li>
                            <li>
                                <h1>
                                    <img src="/images/icons/10/mail.png" />Adres
                                </h1>
                                <span>:</span>
                                <div>
                                    <%# Eval("Adres")%>
                                </div>
                                <div class="clear">
                                </div>
                            </li>
                            <li>
                                <h1>
                                    <img src="/images/icons/10/work.png" />İlçe/Semt
                                </h1>
                                <span>:</span>
                                <div>
                                    <%# Eval("Semt") %>/<%# Eval("Sehir")%>
                                </div>
                                <div class="clear">
                                </div>
                            </li>
                            <li>
                                <h1>
                                    <img src="/images/icons/10/arrow_ne.png" />Web
                                </h1>
                                <span>:</span>
                                <div>
                                    <a href="http://<%# Eval("WebSitesi") %>" target="_blank">
                                        <%# Eval("WebSitesi")%></a>
                                </div>
                                <div class="clear">
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
    <div class="cbFooter">
    </div>
</asp:Panel>
<asp:Panel ID="lastArticle" runat="server" CssClass="details-block" Visible="false">
    <div class="cbHeader">
        <div class="tL left">
            <span class="title"><b>En&nbsp;Son</b>&nbsp;Makaleleri</span></div>
        <div class="tR right">
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="cbBody">
        <div class="cbContainer">
            <asp:Literal ID="ltrMessage" runat="server" Visible="false" />
            <ul class="single-list">
                <li class="first"></li>
                <asp:Repeater ID="rptMakaleler" runat="server">
                    <ItemTemplate>
                        <li><a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                            <img title="<%# Eval("Baslik") %>" class="toolTip images left" src="<%# Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("ResimUrl").ToString()))? "makale/"+Eval("ResimUrl") : "yok.png") %>" /></a>
                            <div class="text left">
                                <h1 class="title">
                                    <a href="<%# Settings.CreateLink("makale",Eval("ID"),Eval("Baslik")) %>">
                                        <%# Eval("Baslik") %></a></h1>
                                <span class="description">
                                    <%# Eval("Ozet")%></span>
                                <div class="info">
                                    <span class="toolTip date left" title="<%# Eval("KayitTarihi") %>">
                                        <%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    <div class="cbFooter">
    </div>
</asp:Panel>
<asp:Panel ID="mapsArea" runat="server" CssClass="details-block" Visible="false">
    <script src="http://maps.google.com/maps?file=api&amp;v=3&amp;key=ABQIAAAAr0XqoonXlKxCejGJvDNgExQOT-wykrdxjDw066ab7YrAYT9GcRTOo9H8uym7zrN6trq29VTclhjC5w&sensor=true"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.getJSON('/data/json/maps/<%= UniqueID %>.js', { json: true }, function (data) {
                if (data != null) {
                    $('#map-Area').append('<div class="cbHeader"><div class="tL left"><span class="title"><b>Haritadaki</b>&nbsp;Konum</span></div><div class="tR right"></div><div class="clear"></div></div><div class="cbBody"><div class="cbContainer"><div id="map" style="width: 100%; height: 300px"></div></div></div><div class="cbFooter"></div>');
                    if (GBrowserIsCompatible()) {
                        var map = new GMap2(document.getElementById("map"));
                        map.addControl(new GMapTypeControl());
                        map.setUIToDefault();

                        var center = new GLatLng(parseFloat(data.Lat), parseFloat(data.Lng));
                        map.setCenter(center, parseFloat(data.Zoom));

                        var marker = new GMarker(center, { draggable: true });
                        map.addOverlay(marker);
                        marker.openInfoWindowHtml("<b>" + data.Title + "</b><br/>" + data.Description);
                    }
                }
            });
        });
    </script>
    <div id="map-Area">
    </div>
</asp:Panel>
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Yorum Yaz" Visible="false" />
<baymyoCnt:contact ID="contact1" runat="server" Visible="false" />
<baymyoCnt:randevu ID="randevu1" runat="server" Visible="false" />
