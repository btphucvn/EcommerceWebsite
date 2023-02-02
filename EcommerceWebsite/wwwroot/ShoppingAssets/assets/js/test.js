$(document).ready(function () {
    var wrapper = $('.toolbar'),
        btnDropdown = wrapper.find('[data-toggle]');

    btnDropdown.on('click', (event) => {
        console.log("clicked");

        event.preventDefault();
        event.stopPropagation();
        var self = $(event.target);

        if ($(".dropdown-menu").hasClass("is-open")) {
            $(".dropdown-menu").removeClass("is-open");
        } else {
            self.siblings(".dropdown-menu").addClass("is-open");
        }

    });
});