<%@ Control Language="C#" AutoEventWireup="true" CodeFile="makale.ascx.cs" Inherits="modul_editor_makale" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<div class="modul-main-box">
    <script type="text/javascript">
        $().ready(function () {
            $('textarea.mceSimple').tinymce({
                // Location of TinyMCE script
                script_url: '/common/js/tiny_mce/tiny_mce.js',

                // General options
                theme: "simple",
                plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,advlist",

                // Example content CSS (should be your site CSS)
                content_css: "css/content.css",

                // Replace values for the template plugin
                template_replace_values: {
                    username: "Some User",
                    staffid: "991234"
                }
            });
        });
    </script>
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" FormScriptEnable="true"
        runat="server" />
</div>
