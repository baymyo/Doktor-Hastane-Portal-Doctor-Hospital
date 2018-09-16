<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoSlider.ascx.cs" Inherits="common_ascx_VideoSlider" %>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $.getJSON('data/json/videodate.js', { json: true }, function (data) {
            var slides = "", divs = "";
            var index = 0;
            $('#video-block .container-box > .slides-box').empty();
            $.each(data, function (i, item) {
                if (index < 3) {
                    divs = divs + '<div class="slide-Item left"><a href="' + item.Link + '"><img class=\"toolTip\" src="/images/video/' + item.ResimUrl + '" title="' + item.Baslik + '" /><h1>' + item.Baslik + '</h1></a></div>';
                    index = index + 1;
                }
                else {
                    divs = divs + '<div class="slide-Item left"><a href="' + item.Link + '"><img class=\"toolTip\" src="/images/video/' + item.ResimUrl + '" title="' + item.Baslik + '" /><h1>' + item.Baslik + '</h1></a></div>';
                    slides = slides + '<div>' + divs + '</div>';
                    divs = "";
                    index = 0;
                }
            });
            if (slides != '')
                $('#video-block .container-box > .slides-box').append(slides);
            else
                $('#video-block .container-box > .slides-box').append('<div>' + divs + '</div>');
            createSliderBox('#video-block');
            toolTipLoad();
        });
    });
</script>
<div class="blue-block">
    <div class="cbHeader">
        <div class="tL left">
            <span class="title"><b>Video</b> Galeri </span>
        </div>
        <div class="tR left">
            <span class="link"><a href="/videolar">Tümü</a></span>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="cbBody">
        <div class="cbContainer">
            <div id="video-block">
                <div class="container-box">
                    <div class="slides-box">
                    </div>
                </div>
                <div class="pagination-box">
                    <a class="previous" href="#">previous</a> <a class="next" href="#">next</a>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="cbFooter">
    </div>
</div>
