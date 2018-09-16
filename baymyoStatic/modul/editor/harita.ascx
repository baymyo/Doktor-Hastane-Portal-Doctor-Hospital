<%@ Control Language="C#" AutoEventWireup="true" CodeFile="harita.ascx.cs" Inherits="modul_editor_harita" %>
<script src="http://maps.google.com/maps?file=api&amp;v=3&amp;key=ABQIAAAAr0XqoonXlKxCejGJvDNgExQOT-wykrdxjDw066ab7YrAYT9GcRTOo9H8uym7zrN6trq29VTclhjC5w&sensor=true"
    type="text/javascript"></script>
<script type="text/javascript">
    function mapsInitialize() {
        if (GBrowserIsCompatible()) {
            var map = new GMap2(document.getElementById("map"));
            map.addControl(new GSmallMapControl());
            map.addControl(new GMapTypeControl());

            var center = new GLatLng(parseFloat('<%= maps.Lat %>'), parseFloat('<%= maps.Lng %>'));
            map.setCenter(center, parseFloat('<%= maps.Zoom %>'));

            var marker = new GMarker(center, { draggable: true });
            map.addOverlay(marker);
            marker.openInfoWindowHtml("<b>" + '<%= maps.Title %>' + "</b><br/>" + '<%= maps.Description %>');

            GEvent.addListener(map, 'click', function (marker, point) {
                if (marker != null) {
                    map.removeOverlay(marker);
                }
                else {
                    var marker = new GMarker(point);
                    map.addOverlay(marker);
                    $('#koordinat').val(point);
                    $('#zoom').val(map.getZoom());
                    marker.openInfoWindowHtml("<b>" + $('#<%= baslik.ClientID %>').val() + "</b><br/>" + $('#<%= adres.ClientID %>').val());
                }
            });
        }
    }
    $(document).ready(function () {
        mapsInitialize();
        $('#kaydet').click(function () {
            $("#loading").fadeIn(1000);
            $.ajax({
                type: "POST",
                url: "/common/service/Default.aspx/MapsSaved",
                data: '{ "point":"' + $('#koordinat').val() + '","zoom":"' + $('#zoom').val() + '" ,"title":"' + $('#<%= baslik.ClientID %>').val() + '","description":"' + $('#<%= adres.ClientID %>').val() + '"  }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#loading").removeClass();
                    $("#loading").empty();
                    $("#loading").append('Haritadaki konumunuz başarılı bir şekilde kaydedildi.');
                    $("#loading").addClass('success-box');
                }
            });
        });
        $('#sil').click(function () {
            $("#loading").fadeIn(1000);
            $.ajax({
                type: "POST",
                url: "/common/service/Default.aspx/MapsRemove",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#loading").removeClass();
                    $("#loading").empty();
                    $("#loading").append('Haritadaki konumunuz yayından kaldırıldı.');
                    $("#loading").addClass('error-box');
                }
            });
        });
    });
</script>
<div class="modul-main-box">
    <h1 class="mmb-Title">
        Haritadaki Konumunuzu Belirleyin
    </h1>
    <div class="default-block">
        <div id="map" style="width: 651px; height: 300px">
        </div>
        <br />
        <input id="koordinat" type="hidden" value="" />
        <input id="zoom" type="hidden" value="" />
        <label for="baslik">
            Başlık</label>&nbsp;:&nbsp;<asp:TextBox ID="baslik" runat="server" Text="" Width="155" />
        <label for="adres">
            Adres</label>&nbsp;:&nbsp;<asp:TextBox ID="adres" runat="server" Text="" Width="250" />
        <input id="kaydet" type="button" class="right" value="Kaydet" />
        <input id="sil" type="button" class="removeButton right" value="Sil" />
    </div>
    <div id="loading" style="display: none;" class="message-box">
        Lütfen bekleyin işleminiz gerçekleştiriliyor.. .</div>
</div>
