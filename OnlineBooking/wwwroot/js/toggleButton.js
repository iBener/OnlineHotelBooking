// toggle button plugin. İbrahim Bener
(function ($) {
    $.fn.toggleButton = function (options) {
        return this.each(function () {
            var $this = $(this);
            $this.click(function () {
                var acKapa = !$this.hasClass("fa-toggle-on");
                $.post("/Tesis/" + options.metod, { otelId: options.otelId, id: $this.data("id"), acKapa: acKapa })
                    .done(function (data) {
                        if ($this.hasClass("fa-toggle-on")) {
                            $this.removeClass("fa-toggle-on");
                            $this.addClass("fa-toggle-off");
                        } else {
                            $this.removeClass("fa-toggle-off");
                            $this.addClass("fa-toggle-on");
                        }
                    });
            });
        });
    };
}(jQuery));