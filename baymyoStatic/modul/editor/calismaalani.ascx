<%@ Control Language="C#" AutoEventWireup="true" CodeFile="calismaalani.ascx.cs"
    Inherits="modul_editor_calismaalani" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<script language="javascript" type="text/javascript" src="/common/js/jquery.autocomplete.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        function format(item) {
            return item.Name;
        }
        $('.kurum').autocomplete('/data/json/kurumlar.js', {
            multiple: false,
            matchContains: true,
            dataType: "json",
            parse: function (data) {
                return $.map(data, function (row) {
                    return {
                        data: row,
                        value: row.Name,
                        result: row.Name
                    }
                });
            },
            formatItem: function (item) {
                return format(item);
            }
        });
    });
</script>
<div class="modul-main-box">
    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" FormScriptEnable="true"
        runat="server" />
</div>
