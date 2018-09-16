<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Manset.ascx.cs" Inherits="common_ascx_Manset" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.getJSON('data/json/manset.js', { json: true }, function (data) {
            $('#flash-box > .flash-images').empty();
            $('#flash-box > .small-view').empty();
            $.each(data, function (i, item) {
                $('#flash-box > .flash-images').append('<div class="big-view"><div class="text"><h1>' + item.Baslik + '</h1><p>' + item.Aciklama + '</p></div><img src="/images/manset/' + item.ModulID + '/' + item.ResimUrl + '" alt="' + item.Baslik + '" /></div>');
                if (i < 8) {
                    $('#flash-box > .small-view').append('<li><a href="' + item.Link + '"><img src="/images/manset/' + item.ModulID + '/' + item.ResimKucuk + '" alt="' + item.Baslik + '" /></a></li>');
                }
                else {
                    $('#flash-box > .small-view').append('<li class="last"><a href="' + item.Link + '"><img src="/images/manset/' + item.ModulID + '/' + item.ResimKucuk + '" alt="' + item.Baslik + '" /></a></li>');
                }
            });
            createFlashBox('#flash-box');
        });
    });
</script>
<div id="flash-box">
    <div class="flash-images">
    </div>
    <div class="clear">
    </div>
    <ul class="small-view">
    </ul>
    <div class="clear">
    </div>
</div>
