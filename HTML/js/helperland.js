
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