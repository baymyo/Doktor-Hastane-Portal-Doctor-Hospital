<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DoktorYeniler.ascx.cs"
    Inherits="common_ascx_DoktorYeniler" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.getJSON('data/json/uyedate.js', { json: true }, function (data) {
            $('.block-box .new-users-block > .user-list').empty();
            $.each(data, function (i, item) {
                $('.block-box .new-users-block > .user-list').append('<li><div class="images left" style="background: transparent url(images/profil/' + item.ResimUrl + ') center center no-repeat; background-size: 36px;"></div><div class="info"><h1 class="name"><a href="' + item.Url + '">' + item.AdiSoyadi + '</a></h1><span class="date">#' + item.KayitTarihi + '</span></div><div class="clear"></div></li>');
            });
        });
    });
</script>
<div class="block-box">
    <div class="blHeader">
        <div class="tL left">
            <span class="title"><b>yeni</b> doktorlar</span>
        </div>
        <div class="tR left">
            <span class="link">
                <div class="right">
                    <a href="/doktorlar">Tüm Doktorlar</a></div>
                <div class="double-arrow right">
                </div>
            </span>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="blBody">
        <div class="blContainer">
            <div class="new-users-block">
                <ul class="user-list">
                </ul>
            </div>
        </div>
    </div>
    <div class="blFooter">
    </div>
</div>
