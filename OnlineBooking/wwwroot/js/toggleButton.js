(function ($) {
    $.fn.toggleButton = function (options ) {
        return this.each(function () {
            var $this = $(this);
            $this.click(function () {
                var data = JSON.stringify({
                    otelId: 3,
                    odatipi: "tip",
                    ozellik: "ozellik",
                    acKapa: true
                });
                var dis = $(this);
                var acKapa = dis.hasClass("fa-toggle-on");
                $.ajax({
                    type: "POST",
                    url: "/Tesis/OdaTipiAcKapa",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //data: JSON.stringify({
                    //    otelId: 3,
                    //    odatipi: "tip",
                    //    ozellik: "ozellik",
                    //    acKapa: true
                    //}),
                    data: data,
                    success: function (result) {
                        if (dis.hasClass("fa-toggle-on")) {
                            dis.removeClass("fa-toggle-on");
                            dis.addClass("fa-toggle-off");
                        } else {
                            dis.removeClass("fa-toggle-off");
                            dis.addClass("fa-toggle-on");
                        }
                    },
                    error: function (r) {
                        alert(r.status);
                    }
                });
            });
        });
    };
}(jQuery));