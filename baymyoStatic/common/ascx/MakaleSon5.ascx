<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MakaleSon5.ascx.cs" Inherits="common_ascx_MakaleSon5" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.getJSON('data/json/makaledate.js', { json: true }, function (data) {
            $('#last-article > .text-view').empty();
            $('#last-article > .small-view').empty();
            $.each(data, function (i, item) {
                $('#last-article > .text-view').append('<div class="big-view"><div class="text"><h1><a href="' + item.Link + '">' + item.Baslik + '</a></h1><p>' + item.Ozet + '&nbsp;<a class="toolTip" href="' + item.Link + '" title="' + item.KayitTarihi + ' eklendi"><b>devamı</b></a></p></div></div>');
                $('#last-article > .small-view').append('<li><a href="' + item.Link + '">' + (i + 1).toString() + '</a></li>');
            });
            createFlashBox('#last-article');
            toolTipLoad();
        });
    });
</script>
<div class="green-box">
    <div class="cbHeader">
        <div class="tL left">
            <span class="title"><b>Doktorlar.net'de</b> Makaleler </span>
        </div>
        <div class="tR left">
            <span class="link">&nbsp;</span>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="cbBody">
        <div class="cbContainer">
            <div id="last-article">
                <div class="text-view">
                </div>
                <div class="clear">
                </div>
                <ul class="small-view">
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="cbFooter">
    </div>
</div>
