<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentControl.ascx.cs"
    Inherits="CommentControl" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <div id="comment">
        <asp:Literal ID="ltrOpenComment" runat="server" />
        <div id="writeBox" class="writeBox" runat="server">
            <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="false" SubmitText="Gönder"
                RemoveVisible="false" runat="server" />
        </div>
    </div>
    <div class="clear">
        &nbsp;</div>
    <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand">
        <ItemTemplate>
            <div class="comments">
                <div class="<%# Eval("Tipi") %>">
                    <span class="name">
                        <%# Eval("Adi") %></span><span class="job" <%#  (!IsCommandActive) ? "style=\"display:none;visibility:hidden;\"" : "" %>>
                            <asp:ImageButton CssClass="toolTip" ToolTip="Yorumu pasif etmek için tıkla" ImageUrl="~/images/icons/12/content-1.png"
                                runat="server" ID="imgActive" CommandName="pasif" Visible='<%#  (IsCommandActive & BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) %>' /><asp:ImageButton
                                    CssClass="toolTip" ToolTip="Yorumu aktif etmek için tıkla" ImageUrl="~/images/icons/12/content-0.png"
                                    runat="server" ID="imgPassive" CommandName="aktif" Visible='<%#  (IsCommandActive & !BAYMYO.UI.Converts.NullToBool(Eval("Aktif"))) %>' /><asp:ImageButton
                                        CssClass="toolTip" ToolTip="Yorumu kaldırmak için tıkla!" ImageUrl="~/images/icons/12/remove.png"
                                        runat="server" ID="imgRemove" CommandName="remove" Visible="<%#  IsCommandActive %>" />
                        </span><span class="toolTip date" title="<%# Eval("KayitTarihi") %>">
                            <%# Settings.TarihFormat(Eval("KayitTarihi"))%></span>
                    <div class="clear">
                    </div>
                </div>
                <div class="content">
                    <%# (!string.IsNullOrEmpty(Eval("ResimUrl").ToString()) ? "<a href=\"" + Settings.VirtualPath + Eval("Url") + "\" class=\"toolTip\" alt=\"" + Eval("Adi") + "\" title=\"Sayfasını görüntülemek için tıkla.\"><img src=\"" + Settings.ImagesPath + "profil/" + Eval("ResimUrl") + "\"/></a>" : "") + Eval("Icerik")%>
                </div>
            </div>
            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
        </ItemTemplate>
    </asp:Repeater>
    <div class="clear">
        &nbsp;</div>
    <div>
        <asp:Literal ID="pageNumberLiteral" runat="server" Text="" />
    </div>
    <script type="text/javascript">
        $("#comment .job-set span.writeOpen").click(function () {
            $("#comment .writeBox").show("slow");
        });
    </script>
</div>
