<%@ Control Language="C#" AutoEventWireup="true" CodeFile="hesapliste.ascx.cs" Inherits="modul_default_hesapliste" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        <asp:Literal ID="ltrInfo" runat="server" />
    </h1>
    <div class="clear">
    </div>
    <div class="default-block">
        <div id="flashContent">
            <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" width="650" height="364"
                id="TÜRKİYE haritası" align="middle">
                <param name="movie" value="<%= mapsUrl %>" />
                <param name="quality" value="high" />
                <param name="bgcolor" value="#0099cc" />
                <param name="play" value="true" />
                <param name="loop" value="true" />
                <param name="wmode" value="window" />
                <param name="scale" value="showall" />
                <param name="menu" value="true" />
                <param name="devicefont" value="false" />
                <param name="salign" value="" />
                <param name="allowScriptAccess" value="sameDomain" />
                <!--[if !IE]>-->
                <object type="application/x-shockwave-flash" data="<%= mapsUrl %>" width="650" height="364">
                    <param name="movie" value="<%= mapsUrl %>" />
                    <param name="quality" value="high" />
                    <param name="bgcolor" value="#0099cc" />
                    <param name="play" value="true" />
                    <param name="loop" value="true" />
                    <param name="wmode" value="window" />
                    <param name="scale" value="showall" />
                    <param name="menu" value="true" />
                    <param name="devicefont" value="false" />
                    <param name="salign" value="" />
                    <param name="allowScriptAccess" value="sameDomain" />
                    <!--<![endif]-->
                    <a href="http://www.adobe.com/go/getflash">
                        <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                            alt="Get Adobe Flash player" />
                    </a>
                    <!--[if !IE]>-->
                </object>
                <!--<![endif]-->
            </object>
        </div>
    </div>
    <div class="clear">
    </div>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <div class="left">
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></div>
    </asp:Panel>
    <div class="clear">
    </div>
    <ul class="users-list">
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li class="left"><a href="<%# Settings.VirtualPath + Eval("Url") %>">
                    <%# "<div class=\"images left\" style=\"background: transparent url(" + Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("ResimUrl").ToString())) ? "profil/" + Eval("ResimUrl") : "yok.png") + ") center center no-repeat; background-size: 90px;\"></div>"%>
                </a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.VirtualPath + Eval("Url") %>">
                                <%# Eval("AdiSoyadi")%></a>
                        </h1>
                        <span class="info">
                            <%# Eval("UzmanlikAlani") %></span> <span class="job">
                                <%# "<a href=\"" + Settings.CreateLink("iletisim", Eval("Url"), "go") + "\"><img class=\"toolTip left\" title=\"Soru sormak için tıklayın!\" src=\"" + Settings.ImagesPath + "sorusor-liste-big.png\"/></a>&nbsp;" + Randevu(Eval("Url"), Eval("AdiSoyadi"), Eval("Randevu"))%>
                                <div class="clear">
                                </div>
                            </span>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <li class="right"><a href="<%# Settings.VirtualPath + Eval("Url") %>">
                    <%# "<div class=\"images left\" style=\"background: transparent url(" + Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("ResimUrl").ToString())) ? "profil/" + Eval("ResimUrl") : "yok.png") + ") center center no-repeat; background-size: 90px;\"></div>"%>
                </a>
                    <div class="text left">
                        <h1 class="title">
                            <a href="<%# Settings.VirtualPath + Eval("Url") %>">
                                <%# Eval("AdiSoyadi")%></a>
                        </h1>
                        <span class="info">
                            <%# Eval("UzmanlikAlani") %></span> <span class="job">
                                <%# "<a href=\"" + Settings.CreateLink("iletisim", Eval("Url"), "go") + "\"><img class=\"toolTip left\" title=\"Soru sormak için tıklayın!\" src=\"" + Settings.ImagesPath + "sorusor-liste-big.png\"/></a>&nbsp;" + Randevu(Eval("Url"), Eval("AdiSoyadi"), Eval("Randevu"))%>
                                <div class="clear">
                                </div>
                            </span>
                    </div>
                    <div class="clear">
                    </div>
                </li>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
