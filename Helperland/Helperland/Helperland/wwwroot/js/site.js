// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#accordion").on("hide.bs.collapse show.bs.collapse", e => {
    $(e.target)
        .prev()
        .find("img:last-child")
        .toggleClass("right rotated");
});


$("#accordion2").on("hide.bs.collapse show.bs.collapse", e => {
    $(e.target)
        .prev()
        .find("img:last-child")
        .toggleClass("right rotated");
});

$(document).ready(function () {

    $('.dismiss, .overlay').on('click', function () {
        // hide sidebar
        $('.sidebar').removeClass('active');
        // hide overlay
        $('.overlay').removeClass('active');
    });

    $('.sidebarCollapse').on('click', function () {
        // open sidebar
        $('.sidebar').addClass('active');
        // fade in the overlay
        $('.overlay').addClass('active');
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
});


var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl)
})

$(function () {
    $('[data-toggle="popover"]').popover({
        html: true,
        placement: "bottom",
        title: function () {
            return $("#popover-title").html();
        },
        content: function () {
            return $('#popover-content').html();
        },
    });
});


jQuery(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.location = $(this).data("href");
    });
});

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

$(document).ready(function () {

    if (window.location.href.indexOf('#forgot-password-Modal') != -1) {
        $('#forgot-password-Modal').modal('show');
    }

    if (window.location.href.indexOf('#confirm-password-Modal') != -1) {
        $('#confirm-password-Modal').modal('show');
    }

});



