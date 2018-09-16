<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MakaleHaftaninKonusu.ascx.cs"
    Inherits="common_ascx_MakaleHaftaninKonusu" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.getJSON('data/json/makaleweekof.js', { json: true }, function (data) {
            $('#subject-week').empty();
            $('#subject-week-link').empty();
            $.each(data, function (i, item) {
                $('#subject-week').append('<div class="image left"><img src="images/makale/' + item.ResimUrl + '" alt="' + item.Baslik + '" /></div><div class="text left"><h1><a href="' + item.Link + '">' + item.Baslik + '</a></h1><p>' + item.Ozet + '&nbsp;<a href="' + item.Link + '">devamı</a></p></div>');
                $('#subject-week-link').append('<a href="/?go=makaleliste&view=single&type=butunhafta&kid=' + item.KategoriID + '">Bütün Hafta</a>');
            });
        });
    });
</script>
<div class="green-block">
    <div class="cbHeader">
        <div class="tL left">
            <span class="title"><b>Doktorlar.net'de</b> Haftanın Konusu </span>
        </div>
        <div class="tR left">
            <div id="subject-week-link" class="link"></div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="cbBody">
        <div class="cbContainer">
            <div id="subject-week">
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="cbFooter">
    </div>
</div>
