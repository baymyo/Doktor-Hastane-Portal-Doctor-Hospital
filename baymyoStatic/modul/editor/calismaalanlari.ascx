<%@ Control Language="C#" AutoEventWireup="true" CodeFile="calismaalanlari.ascx.cs"
    Inherits="modul_editor_calismaalanlari" %>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Tüm Muayenehanlerim
    </h1>
    <div class="clear">
    </div>
    <div id="card-view">
        <ul>
            <asp:Repeater ID="rptMuayene" runat="server">
                <ItemTemplate>
                    <li class="title">
                        <img class="icons" src="/images/icons/16/home.png" /><%# Eval("Kurum") %>
                            <%# "<a class=\"right\" href=\"" + Settings.CreateLink("calismaalani", Eval("ID"), Eval("Kurum")) + "\"><img class=\"toolTip\" title='Muayenehane bilgilerini düzeltmek için tıklayın!' src=\"" + Settings.IconsPath + "16/editor.png\"/></a>"%>
                            <%# (BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) ? "<img class=\"toolTip right\" title='Herkese Açık!' src=\"" + Settings.IconsPath + "16/global.png\"/>" : "<img class=\"toolTip right\" title='Sadece ben!' src=\"" + Settings.IconsPath + "16/users.png\"/>"%>
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
