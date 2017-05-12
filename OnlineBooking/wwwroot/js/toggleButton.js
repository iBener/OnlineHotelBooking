(function ($) {
    $.fn.toggleButton = function () {
        return this.each(function () {
            var $this = $(this);
            $this.click(function () {
                $.get("OdaTipiAcKapa?otelId=3", function () {
                    if ($(this).hasClass("fa-toggle-on")) {
                        $(this).removeClass("fa-toggle-on");
                        $(this).addClass("fa-toggle-off");
                    } else {
                        $(this).removeClass("fa-toggle-off");
                        $(this).addClass("fa-toggle-on");
                    }
                });
            });
        });
    };
}(jQuery));