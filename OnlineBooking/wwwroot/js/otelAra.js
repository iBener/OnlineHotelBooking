var yetiskin = 2;
var cocuk = 0;

var startDate;
var endDate;

$(function () {
    $('#giris-cikis-tarih').daterangepicker({
        "autoApply": true,
        "locale": {
            "format": "DD.MM.YYYY",
            "separator": " - ",
            "applyLabel": "Tamam",
            "cancelLabel": "Vazgeç",
            "fromLabel": "From",
            "toLabel": "To",
            "customRangeLabel": "Custom",
            "weekLabel": "W",
            "daysOfWeek": [
                "Pz",
                "Pt",
                "Sl",
                "Çr",
                "Pr",
                "Cu",
                "Ct"
            ],
            "monthNames": [
                "Ocak",
                "Şubat",
                "Mart",
                "Nisan",
                "Mayıs",
                "Haziran",
                "Temmuz",
                "Auğustos",
                "Eylül",
                "Ekim",
                "Kasım",
                "Aralık"
            ],
            "firstDay": 1
        },
        "startDate": startDate,
        "endDate": endDate
    }, function (giris, cikis, label) {
        //console.log("New date range selected: " + start.format('YYYY-MM-DD') + " to " + end.format('YYYY-MM-DD') + " (predefined range: " + label + ")");
        $("#giris").val(giris.format("YYYY-MM-DD"));
        $("#cikis").val(cikis.format("YYYY-MM-DD"));
    });
});

$("#yetiskin").change(function (a) {
    yetiskin = $("#yetiskin").val();
    $("#misafir-sayisi").html(yetiskin + " yetişkin, " + cocuk + " çocuk");
});

$("#cocuk").change(function (a) {
    cocuk = $("#cocuk").val();
    $("#misafir-sayisi").html(yetiskin + " yetişkin, " + cocuk + " çocuk");
});

$("#misafir-secim-dropdown").click(function (e) {
    e.stopPropagation();
});