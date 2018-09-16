<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MakalePopuler.ascx.cs"
    Inherits="common_ascx_MakalePopuler" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.getJSON('data/json/makalepopuler.js', { json: true }, function (data) {
            $('.article-block #populer').empty();
            $.each(data, function (i, item) {
                $('.article-block #populer').append('<li><div class="top"><h1 class="title left"><a href="' + item.Link + '">' + item.Baslik + '</a></h1><div class="clear"></div></div><div class="text">' + item.Ozet + '</div><div class="more"><div class="double-arrow left"></div><div class="left"><a href="' + item.Link + '">devamı</a></div></div><div class="clear"></div></li>');
            });
        });
    });
</script>
<div class="article-block">
    <div class="header">
    </div>
    <div class="middle">
        <div class="container">
            <ul id="populer">
            </ul>
        </div>
    </div>
    <div class="footer">
    </div>
</div>
