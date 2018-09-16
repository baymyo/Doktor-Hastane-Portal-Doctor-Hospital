<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DoktorPopuler.ascx.cs"
    Inherits="common_ascx_DoktorPopuler" %>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $.getJSON('data/json/uyepopuler.js', { json: true }, function (data) {
            var slides = "", divs = "";
            var index = 0;
            $('#populer-doctor .container-box > .slides-box').empty();
            $.each(data, function (i, item) {
                if (index < 5) {
                    divs = divs + '<div class="slide-Item left"><div class="images left" style="background: transparent url(images/profil/' + item.ResimUrl + ') center center no-repeat; background-size: 46px;"></div><div class="text left"><a href="' + item.Url + '"><h1 class="title">' + item.UzmanlikAlani + '</h1></a><a href="' + item.Url + '"><span class="name">' + item.AdiSoyadi + '</span></a></div></div>';
                    index = index + 1;
                }
                else {
                    divs = divs + '<div class="slide-Item left"><div class="images left" style="background: transparent url(images/profil/' + item.ResimUrl + ') center center no-repeat; background-size: 46px;"></div><div class="text left"><a href="' + item.Url + '"><h1 class="title">' + item.UzmanlikAlani + '</h1></a><a href="' + item.Url + '"><span class="name">' + item.AdiSoyadi + '</span></a></div></div>';
                    slides = slides + '<div>' + divs + '</div>';
                    divs = "";
                    index = 0;
                }
            });
            if (slides != '') {
                $('#populer-doctor .container-box > .slides-box').append(slides);
                if (divs != '')
                    $('#populer-doctor .container-box > .slides-box').append('<div>' + divs + '</div>');
            }
            else
                $('#populer-doctor .container-box > .slides-box').append('<div>' + divs + '</div>');
            createSliderBox('#populer-doctor');
        });
    });
</script>
<div id="populer-doctor" class="blue-box">
    <div class="cbHeader">
        <div class="tL left">
            <span class="title"><b>Popüler</b> Doktorlar </span>
        </div>
        <div class="tR left">
            <div class="pagination-box right">
                <a class="previous" href="#">previous</a> <a class="next" href="#">next</a>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="cbBody">
        <div class="cbContainer">
            <div class="container-box">
                <div class="slides-box">
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="cbFooter">
    </div>
</div>
